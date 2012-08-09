using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AliRank
{
    public partial class VpnForm : Form
    {
        private VpnDAO vpnDao;
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
            this.dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            DataTable dt = new DataTable();
            dt.Columns.Add("Check", typeof(Boolean));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("Username", typeof(string));
            dt.Columns.Add("Password", typeof(string));
            dt.Columns.Add("VpnType", typeof(string));
            dt.Columns.Add("L2tpSec", typeof(string));
            dt.Columns.Add("UpdateTime", typeof(DateTime));
            this.dataGridView.DataSource = dt;
            DataGridViewColumn column0 = this.dataGridView.Columns[0];
            column0.HeaderText = "Check";
            column0.Width = 50;
            DataGridViewColumn column = this.dataGridView.Columns[1];
            column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column.HeaderText = "Address";
            column.Width = 200;
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
            List<VpnModel> vpnModelList = vpnDao.GetVpnModelList();
            if (vpnModelList.Count > 0)
            {
                foreach (VpnModel item in vpnModelList)
                {
                    DataRow row = dt.NewRow();
                    row["Check"] = false;
                    row["Address"] = item.Address;
                    row["Username"] = item.Username;
                    row["Password"] = item.Password;
                    row["VpnType"] = item.VpnType;
                    row["L2tpSec"] = item.L2tpSec;
                    row["updateTime"] = item.UpdateTime;
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
    }
}
