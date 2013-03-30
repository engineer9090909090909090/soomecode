using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Database;
using Soomes;

namespace AliHelper
{
    public partial class DbsetForm : Form
    {
        private BaseManager manager;
        public DbsetForm()
        {
            manager = new BaseManager();
            InitializeComponent();
        }

        private void ConnTestButton_Click(object sender, EventArgs e)
        {
            string server = this.Server.Text.Trim();
            string database = this.Database.Text.Trim();
            string username = this.Username.Text.Trim();
            string password = this.Password.Text.Trim();
            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(database) || string.IsNullOrEmpty(username))
            {
                this.ErrorMsg.Text = "MySql服务器地址、MySql数据库名称、数据库连接用户不能为空。";
                return;
            }
            bool TestResult = ValidateConnection();
            string text = TestResult ? "数据库连接测试成功!" : "不能正常连接到指定数据库，请检查!";
            MessageBox.Show(text);
        }

        private bool ValidateConnection()
        {
            string server = this.Server.Text.Trim();
            string database = this.Database.Text.Trim();
            string username = this.Username.Text.Trim();
            string password = this.Password.Text.Trim();
            string connection_str = "server=" + server + ";uid=" + username + ";pwd=" + password
                + ";database=" + database + ";Charset=utf8;Allow Zero Datetime=true";
            MysqlDBHelper dbHelper = new MysqlDBHelper(connection_str);
            bool TestResult = dbHelper.ConnectionTest();
            return TestResult;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (this.LocalDbCheck.Checked)
            {
                SaveToDb();
                return;
            }
            string server = this.Server.Text.Trim();
            string database = this.Database.Text.Trim();
            string username = this.Username.Text.Trim();
            string password = this.Password.Text.Trim();
            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(database) || string.IsNullOrEmpty(username))
            {
                this.ErrorMsg.Text = "MySql服务器地址、MySql数据库名称、数据库连接用户不能为空。";
                return;
            }

            bool TestResult = ValidateConnection();
            if (!TestResult)
            {
                MessageBox.Show("不能正常连接到指定数据库，请检查!");
                return;
            }
            SaveToDb();
        }

        private void SaveToDb()
        {
            string dbType = this.LocalDbCheck.Checked ? "Sqlite" : "MySql";
            string server = this.Server.Text.Trim();
            string database = this.Database.Text.Trim();
            string username = this.Username.Text.Trim();
            string password = this.Password.Text.Trim();
            manager.SetDbConfig(dbType, server, database, username, password);
            Application.Exit();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DbCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (this.LocalDbCheck.Checked)
            {
                MySqlSetGroup.Visible = false;
            }
            else 
            {
                MySqlSetGroup.Visible = true;
            }
        }

        private void DbsetForm_Load(object sender, EventArgs e)
        {
            string dbType = manager.GetConfigValue(Constants.Db_Config, Constants.Db_Type);
            string dbUrl = manager.GetConfigValue(Constants.Db_Config, Constants.Db_Url);
            string dbName = manager.GetConfigValue(Constants.Db_Config, Constants.Db_Name);
            string dbUser = manager.GetConfigValue(Constants.Db_Config, Constants.Db_User);
            string dbPass = manager.GetConfigValue(Constants.Db_Config, Constants.Db_Pass);
            this.Server.Text = dbUrl;
            this.Database.Text = dbName;
            this.Username.Text = dbUser;
            this.Password.Text = dbPass;
            if (dbType == Constants.DbType_MySql)
            {
                this.MySqlSetGroup.Visible = false;
                this.LocalDbCheck.Checked = false;
                this.RemoteDbCheck.Checked = true;
            }
            else 
            {
                this.MySqlSetGroup.Visible = false;
                this.LocalDbCheck.Checked = true;
                this.RemoteDbCheck.Checked = false;
            }
        }
    }
}
