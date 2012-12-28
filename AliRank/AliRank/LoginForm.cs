using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AliRank.Bussness;

namespace AliRank
{
    public partial class LoginForm : Form
    {
        private string IniFile;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            this.ErrorMsg.Text = string.Empty;
            string account = this.accountBox.Text;
            string password = this.passwordBox.Text;
            if (string.IsNullOrEmpty(account))
            {
                this.ErrorMsg.Text = "帐号不能为空！";
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                this.ErrorMsg.Text = "密码不能为空！";
                return;
            }
            if (this.remind.Checked)
            {
                FileUtils.IniWriteValue(Constants.CLICK_SECTIONS, Constants.LOGIN_USER, account, IniFile);
                FileUtils.IniWriteValue(Constants.CLICK_SECTIONS, Constants.LOGIN_PASS, password, IniFile);
                FileUtils.IniWriteValue(Constants.CLICK_SECTIONS, Constants.LOGIN_REMINDE, "1", IniFile);
            }
            else {
                FileUtils.IniWriteValue(Constants.CLICK_SECTIONS, Constants.LOGIN_USER, "", IniFile);
                FileUtils.IniWriteValue(Constants.CLICK_SECTIONS, Constants.LOGIN_PASS, "", IniFile);
                FileUtils.IniWriteValue(Constants.CLICK_SECTIONS, Constants.LOGIN_REMINDE, "", IniFile);
            }
            this.ErrorMsg.Text = "正在进行登录，请稍候...";
            string msg = RemoteDataManager.Instance.UserLoginSystem(account, password);
            if (string.IsNullOrEmpty(msg))
            {
                RemoteDataManager.Instance.PostAccounts();
                RemoteDataManager.Instance.PostVpns();
                this.DialogResult = DialogResult.OK;
            }
            else {
                this.ErrorMsg.Text = msg;
            }
        }

        private void cannelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            IniFile = FileUtils.CreateAppDataFolderEmptyTextFile(Constants.INI_FILE);
            string remind = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.LOGIN_REMINDE, IniFile);
            string remindUser = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.LOGIN_USER, IniFile);
            string remindPass = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.LOGIN_PASS, IniFile);
            if (!string.IsNullOrEmpty(remind))
            {
                this.remind.Checked = true;
            }
            if (!string.IsNullOrEmpty(remindUser))
            {
                this.accountBox.Text = remindUser;
            }
            if (!string.IsNullOrEmpty(remindPass))
            {
                this.passwordBox.Text = remindPass;
            }
            
        }
    }
}
