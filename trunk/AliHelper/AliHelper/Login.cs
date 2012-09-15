using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace AliHelper
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            IEHandleUtils.ClearIECookie();
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        string loginUrl = "https://login.alibaba.com/";
        string indexUrl = "https://login.alibaba.com/xloginCallBackForRisk.do";
        string successUrl = "https://login.alibaba.com/xman/success_proxy.htm";

        void loginPageLoadCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
                WebBrowser browser = (WebBrowser)sender;
            if (browser.ReadyState != System.Windows.Forms.WebBrowserReadyState.Complete)
                return;
            if (e.Url.ToString() == indexUrl)
            {
                browser.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(loginPageLoadCompleted);
                string cookie = browser.Document.Cookie;
                ShareCookie.Instance.LoginCookie = FullWebBrowserCookie.GetCookieInternal(browser.Url, false);
               // ShareCookie.Instance.LoginCookieContainer = FullWebBrowserCookie.getUriCookie(browser.Url);
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

        
    }
}
