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
    public partial class AccountForm : Form
    {
        private InquiryDAO inquiryDAO;

        #region 初始化
        public AccountForm()
        {
            InitializeComponent();
            LoadDataview();
        }

        void LoadDataview()
        {
            inquiryDAO = DAOFactory.Instance.GetInquiryDAO();
            this.dataGridView.DataBindings.Clear();
            this.dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            DataTable dt = new DataTable();
            dt.Columns.Add("Check", typeof(Boolean));
            dt.Columns.Add("Account", typeof(string));
            dt.Columns.Add("Password", typeof(string));
            dt.Columns.Add("Country", typeof(string));
            dt.Columns.Add("LoginIp", typeof(string));
            dt.Columns.Add("InquiryNum", typeof(string));
            dt.Columns.Add("ID", typeof(Int32));
            this.dataGridView.DataSource = dt;
            DataGridViewColumn column0 = this.dataGridView.Columns[0];
            column0.HeaderText = "Check";
            column0.Width = 80;
            DataGridViewColumn column = this.dataGridView.Columns[1];
            column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column.HeaderText = "Account";
            column.Width = 230;
            DataGridViewColumn column2 = this.dataGridView.Columns[2];
            column2.HeaderText = "Password";
            column2.Width = 100;
            DataGridViewColumn column3 = this.dataGridView.Columns[3];
            column3.HeaderText = "Country";
            column3.Width = 150;
            DataGridViewColumn column4 = this.dataGridView.Columns[4];
            column4.HeaderText = "Last LoginIp";
            column4.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column4.Width = 150;
            DataGridViewColumn column5 = this.dataGridView.Columns[5];
            column5.HeaderText = "InquiryNum";
            column5.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column5.Width = 120;
            DataGridViewColumn column6 = this.dataGridView.Columns[6];
            column6.HeaderText = "Id";
            column6.Width = 10;
            column6.Visible = false;
            List<AliAccounts> accountList = inquiryDAO.GetAccounts();
            if (accountList.Count > 0)
            {
                foreach (AliAccounts item in accountList)
                {
                    DataRow row = dt.NewRow();
                    row["Check"] = false;
                    row["Account"] = item.Email;
                    row["Country"] = item.Country;
                    row["Password"] = "******";
                    row["LoginIp"] = item.LoginIp;
                    row["InquiryNum"] = item.InquiryNum;
                    row["Id"] = item.AccountId;
                    dt.Rows.Add(row);
                }
            }
        }
        
        #endregion

        #region 新增按钮事件
        private void InsertBtn_Click(object sender, EventArgs e)
        {
            AliAccounts model = new AliAccounts();
            model.Email = this.AccountBox.Text.Trim();
            if (string.IsNullOrEmpty(model.Email))
            {
                ErrorMsg.Text = "帐号不能为空.";
                return;
            }
            model.Password = PasswordBox.Text.Trim();
            if (string.IsNullOrEmpty(model.Password))
            {
                ErrorMsg.Text = "密码不能为空.";
                return;
            }
            model.Country = CountryBox.Text.Trim();
            if (string.IsNullOrEmpty(model.Country))
            {
                ErrorMsg.Text = "国家不能为空.";
                return;
            }
            bool existAddress = inquiryDAO.ExistAccount(model.Email);
            if (existAddress)
            {
                ErrorMsg.Text = "帐号已经存在列表中.";
                return;
            }
            inquiryDAO.InsertAccount(model);
            AccountBox.Text = "";
            PasswordBox.Text = "";
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
                    removeList.Add(dr[6].ToString());
                    dt.Rows.Remove(dr);
                }
            }
            if (removeList.Count > 0)
            {
                inquiryDAO.DeleteAccount(removeList);
                MessageBox.Show(removeList.Count + " 行记录被删除。");
            }
        }
        #endregion

        private void ImportBtn_Click(object sender, EventArgs e)
        {

        }

    }
}
