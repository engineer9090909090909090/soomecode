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
        string indexUrl = "http://www.alibaba.com/";

        void loginPageLoadCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = (WebBrowser)sender;
            if (browser.ReadyState != System.Windows.Forms.WebBrowserReadyState.Complete)
                return;
            if (e.Url.ToString() != browser.Url.ToString())
                return;
            if (browser.Url.ToString() == loginUrl)
            {
                HtmlElement header = browser.Document.GetElementById("header");
                header.Style = "display:none";
                HtmlElement footer = browser.Document.GetElementById("footer");
                footer.Style = "display:none";
                HtmlElement page760Div = browser.Document.GetElementById("page760");
                page760Div.Style = "width:310px";
                HtmlElement benefits = browser.Document.GetElementById("benefits");
                benefits.Style = "display:none";
                HtmlElement remember = browser.Document.GetElementById("remember");
                remember.Parent.Parent.Style = "display:none";
                HtmlElement checkBox = browser.Document.GetElementById("runatm");
                checkBox.SetAttribute("checked", "");
            }
            if (browser.Url.ToString() == indexUrl)
            {
                browser.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(loginPageLoadCompleted);

            }
        }

        
    }
}
