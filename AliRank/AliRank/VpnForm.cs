using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace AliRank
{
    public partial class VpnForm : Form
    {
        private VpnDAO vpnDao;

        #region 初始化
        public VpnForm()
        {
            InitializeComponent();
            L2tpKeyLabel.Hide();
            L2tpKeyTxtBox.Hide();
            LoadDataview();
        }

        void LoadDataview()
        {
            vpnDao = DAOFactory.Instance.GetVpnDAO();
            this.dataGridView.DataBindings.Clear();
            this.dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            DataTable dt = new DataTable();
            dt.Columns.Add("Check", typeof(Boolean));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("Username", typeof(string));
            dt.Columns.Add("Password", typeof(string));
            dt.Columns.Add("VpnType", typeof(string));
            dt.Columns.Add("L2tpSec", typeof(string));
            dt.Columns.Add("UpdateTime", typeof(DateTime));
            dt.Columns.Add("ID", typeof(Int32));
            this.dataGridView.DataSource = dt;
            DataGridViewColumn column0 = this.dataGridView.Columns[0];
            column0.HeaderText = "Check";
            column0.Width = 50;
            DataGridViewColumn column = this.dataGridView.Columns[1];
            column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column.HeaderText = "Address";
            column.Width = 230;
            DataGridViewColumn column2 = this.dataGridView.Columns[2];
            column2.HeaderText = "User Name";
            column2.Width = 100;
            DataGridViewColumn column3 = this.dataGridView.Columns[3];
            column3.HeaderText = "Password";
            column3.Width = 100;
            DataGridViewColumn column4 = this.dataGridView.Columns[4];
            column4.HeaderText = "VPN Type";
            column4.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column4.Width = 100;
            DataGridViewColumn column5 = this.dataGridView.Columns[5];
            column5.HeaderText = "L2TP Sec";
            column5.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column5.Width = 100;
            DataGridViewColumn column6 = this.dataGridView.Columns[6];
            column6.HeaderText = "Update Time";
            column6.Width = 120;
            DataGridViewColumn column7 = this.dataGridView.Columns[7];
            column7.HeaderText = "Id";
            column7.Width = 10;
            column7.Visible = false;
            List<VpnModel> vpnModelList = vpnDao.GetVpnModelList();
            if (vpnModelList.Count > 0)
            {
                foreach (VpnModel item in vpnModelList)
                {
                    DataRow row = dt.NewRow();
                    row["Check"] = false;
                    row["Address"] = item.Address;
                    row["Username"] = item.Username;
                    row["Password"] = "******";
                    row["VpnType"] = item.VpnType;
                    row["L2tpSec"] = item.L2tpSec;
                    row["updateTime"] = item.UpdateTime;
                    row["Id"] = item.Id;
                    dt.Rows.Add(row);
                }
            }
        }
        
        private void L2tpBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (L2tpBtn.Checked)
            {
                L2tpKeyLabel.Show();
                L2tpKeyTxtBox.Show();
            }
            else
            {
                L2tpKeyLabel.Hide();
                L2tpKeyTxtBox.Hide();
            }
        }
        
        #endregion

        #region 新增按钮事件
        private void InsertBtn_Click(object sender, EventArgs e)
        {
            VpnModel model = new VpnModel();
            model.Address = AddressBox.Text.Trim().Replace(" ", "");
            try
            {
                IPAddress.Parse(model.Address);
            }catch{
                ErrorMsg.Text = "输入的VPN IP地址不合法.";
                return;
            }
            model.Username = UsernameBox.Text.Trim();
            if (string.IsNullOrEmpty(model.Username))
            {
                ErrorMsg.Text = "VPN 用户名不能为空.";
                return;
            }
            model.Password = PasswordBox.Text.Trim();
            if (string.IsNullOrEmpty(model.Password))
            {
                ErrorMsg.Text = "VPN 用户名密码不能为空.";
                return;
            }
            if (PptpBtn.Checked)
            {
                model.VpnType = Constants.PPTP;
                model.L2tpSec = string.Empty;
            }
            else
            {
                model.VpnType = Constants.L2TP;
                model.L2tpSec = L2tpKeyTxtBox.Text.Trim();
            }
            bool existAddress = vpnDao.ExistAddress(model.Address, model.VpnType);
            if (existAddress)
            {
                ErrorMsg.Text = "VPN 地址已经存在列表中.";
                return;
            }
            vpnDao.Insert(model);
            AddressBox.Text = "";
            LoadDataview();
        }
        #endregion

        #region 删除按钮事件
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView == null || dataGridView.Rows.Count == 0)
            {
                return;
            }
            List<string> removeList = new List<string>();
            DataTable dt = (DataTable)dataGridView.DataSource;
            for (int j = dt.Rows.Count - 1; j >= 0; j--)
            {
                DataRow dr = dt.Rows[j];
                Boolean chk = System.Boolean.Parse(dr[0].ToString());
                if (chk)
                {
                    removeList.Add(dr[7].ToString());
                    dt.Rows.Remove(dr);
                }
            }
            if (removeList.Count > 0)
            {
                vpnDao.DeleteVpn(removeList);
                MessageBox.Show(removeList.Count + " 行记录被删除。");
            }
        }
        #endregion

    }
}
