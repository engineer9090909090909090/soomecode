﻿using System;
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
                DAOFactory.Instance.GetProfileDAO().SetValue(Constants.LOGIN_USER, account);
                DAOFactory.Instance.GetProfileDAO().SetValue(Constants.LOGIN_PASS, password);
                DAOFactory.Instance.GetProfileDAO().SetValue(Constants.LOGIN_REMINDE, "1");
            }
            else {
                DAOFactory.Instance.GetProfileDAO().SetValue(Constants.LOGIN_USER, "");
                DAOFactory.Instance.GetProfileDAO().SetValue(Constants.LOGIN_PASS, "");
                DAOFactory.Instance.GetProfileDAO().SetValue(Constants.LOGIN_REMINDE, "");
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
            string remind = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.LOGIN_REMINDE);
            string remindUser = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.LOGIN_USER);
            string remindPass = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.LOGIN_PASS);
            if (!string.IsNullOrEmpty(remindUser))
            {
                this.accountBox.Text = remindUser;
            }
            if (!string.IsNullOrEmpty(remindPass))
            {
                this.passwordBox.Text = remindPass;
            }
            if (!string.IsNullOrEmpty(remind))
            {
                this.remind.Checked = true;
                this.loginBtn.Focus();
            }
        }
    }
}
