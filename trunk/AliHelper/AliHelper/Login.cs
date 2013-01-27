using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;

namespace AliHelper
{
    public partial class LoginForm : Form
    {

        private bool IsIELoginModel = false;
        private string loginUrl = "https://login.alibaba.com/";
        private string homeUrl = "http://www.alibaba.com/";
        BackgroundWorker bgWorker = new BackgroundWorker();
        private Passporter passporter;
        public LoginForm()
        {
            IEHandleUtils.ClearIECookie();
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            passporter = new Passporter(this.webBrowser1);
        }

        void loginPageLoadCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = (WebBrowser)sender;
            if (browser.ReadyState != System.Windows.Forms.WebBrowserReadyState.Complete)
                return;
            if (e.Url.ToString() == homeUrl)
            {
                browser.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(loginPageLoadCompleted);
                ShareCookie.Instance.LoginCookie = FullWebBrowserCookie.GetCookieInternal(browser.Url, false);
                string manageHtml = HttpClient.RemoteRequest(MainForm.ManageHtml, null);
                DataCache.Instance.CsrfToken = HttpClient.GetCsrfToken(manageHtml);
                DataCache.Instance.CheckCodeUrl = HttpClient.GetCheckCodeUrl(manageHtml);
                this.DialogResult = DialogResult.OK;
            }
            if (e.Url.ToString() != browser.Url.ToString())
                return;
            if (browser.Url.ToString() == loginUrl)
            {
                HtmlElement header = browser.Document.GetElementById("header");
                if (header != null) header.Style = "display:none";
                HtmlElement footer = browser.Document.GetElementById("footer");
                if (footer != null) footer.Style = "display:none";
                HtmlElement page760Div = browser.Document.GetElementById("page760");
                if (page760Div != null) page760Div.Style = "width:310px";
                HtmlElement benefits = browser.Document.GetElementById("benefits");
                if (benefits != null) benefits.Style = "display:none";
            }
            
        }

        private void ChangeModel_Click(object sender, EventArgs e)
        {
            if (!IsIELoginModel)
            {
                this.webBrowser1.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(loginPageLoadCompleted);
                this.webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(loginPageLoadCompleted);
                this.webBrowser1.Navigate(loginUrl);

                this.changeModel.Text = "切换到普通登录模式";
                this.Size = new Size(330, 450);
                this.loginPanel.Size = new Size(325, 382);
                this.IsIELoginModel = true;
                this.BtnLogin.Enabled = false;
                this.BtnCancel.Enabled = false;
                this.BtnLogin.Visible = false;
                this.BtnCancel.Visible = false;
                this.webBrowser1.Visible = true;
                this.LogPanel.Visible = false;
                this.changeModel.Location = new Point(9, 388);
                this.Location = new Point(this.Location.X, this.Location.Y - 100);
            }
            else {
                this.changeModel.Text = "切换到网页登录模式";
                this.Size = new Size(330, 230);
                this.IsIELoginModel = false;
                this.BtnLogin.Enabled = true;
                this.BtnCancel.Enabled = true;
                this.BtnLogin.Visible = true;
                this.BtnCancel.Visible = true;
                this.webBrowser1.Visible = false;
                this.LogPanel.Visible = true;
                this.loginPanel.Size = new Size(325, 230);
                this.changeModel.Location = new Point(9,169);
                this.Location = new Point(this.Location.X, this.Location.Y + 100);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnLogin_Click(sender, e);
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string account = this.cmbAccount.Text;
            if (string.IsNullOrEmpty(account))
            {
                this.labErrorMsg.Text = "帐号不能为空!";
            }
            string password = this.txtPassword.Text;
            if (string.IsNullOrEmpty(password))
            {
                this.labErrorMsg.Text = "密码不能为空!";
            }
            this.BtnLogin.Enabled = false;
            this.labErrorMsg.Text = "";
            this.loadingImage.Visible = true;
            this.changeModel.Enabled = false;
            bgWorker.DoWork -= new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerAsync();

        }

        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            bgWorker.DoWork -= new DoWorkEventHandler(bgWorker_DoWork);
            string account = this.cmbAccount.Text;
            string password = this.txtPassword.Text;
            bool loginSuccess = passporter.DoLogin(account, password);
            this.loadingImage.Visible = false;
            this.changeModel.Enabled = true;
            if (loginSuccess)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else 
            {
                this.labErrorMsg.Text = "登录失败,请检查帐号密码!";
                this.BtnLogin.Enabled = true;
            }
            
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
