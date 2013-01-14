using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;
using System.Net;
using Soomes;

namespace AliHelper
{
    class Passporter
    {
        private string loginUrl = "https://login.alibaba.com/";
        string homeUrl = "http://www.alibaba.com/";

        private WebBrowser browser;
        ManualResetEvent eventX = new ManualResetEvent(false);
        private string UserName;
        private string Password;
        private bool LoginSuccess = true;
        public Passporter(WebBrowser b) 
        {
            browser = b;
        }

        public bool DoLogin(string Account, string Password)
        {
            this.UserName = Account;
            this.Password = Password;

            string html = HttpHelper.GetHtml(loginUrl);
            string dmtrackPageid = GetDmtrackPageid(html);
            if (string.IsNullOrEmpty(dmtrackPageid))
            {
                return false;
            }
            string token = GetToken(this.UserName, this.Password, dmtrackPageid);
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            string st = GetST(token);
            if (string.IsNullOrEmpty(st))
            {
                return false;
            }
            CookieContainer cookieContainer = new CookieContainer();
            return GetLoginUrl(this.UserName, this.Password, dmtrackPageid, st, ref cookieContainer);
        }


        public string GetDmtrackPageid(string html)
        {
            Regex r = new Regex("var dmtrack_pageid='(.*?)';");
            GroupCollection gc = r.Match(html).Groups;
            if (gc != null && gc.Count > 1)
            {
                return gc[1].Value.Trim();
            }
            return "";
        }

        public string GetToken(string userId, string password, string dmtrackPageid)
        {
            string preUrl = "https://login.alibaba.com/xman/xlogin.js?pd=alibaba&pageFrom=standardlogin&u_token=&xloginPassport={0}&xloginPassword={1}&xloginCheckToken=&rememberme=rememberme&runatm=runatm&dmtrack_pageid={2}";
            string url = string.Format(preUrl, userId, password, dmtrackPageid);
            string html = HttpHelper.GetHtml(url);
            //System.Diagnostics.Trace.WriteLine("GetToken = " + html);
            Regex r = new Regex("var xman_login_token={\"token\":\"(.*?)\"}");
            GroupCollection gc = r.Match(html).Groups;
            if (gc != null && gc.Count > 1)
            {
                return gc[1].Value.Trim();
            }
            return "";
        }

        public string GetST(string token)
        {
            string preUrl = "https://passport.alibaba.com/mini_apply_st.js?site=4&callback=window.xmanDealTokenCallback&token={0}";
            string url = string.Format(preUrl, token);
            string html = HttpHelper.GetHtml(url);
            //System.Diagnostics.Trace.WriteLine("GetST = " + html);
            Regex r = new Regex("{\"data\":{\"st\":\"(.*?)\"}");
            if (string.IsNullOrEmpty(html))
            {
                return "";
            }
            GroupCollection gc = r.Match(html).Groups;
            if (gc != null && gc.Count > 1)
            {
                return gc[1].Value.Trim();
            }
            return "";
        }

        public bool GetLoginUrl(string userId, string password, string dmtrackPageid, string st, ref CookieContainer cookieContainer)
        {
            string preUrl = "https://login.alibaba.com/validateST.htm?pd=alibaba&pageFrom=standardlogin&u_token=&xloginPassport={0}&xloginPassword={1}&xloginCheckToken=&rememberme=rememberme&runatm=runatm&dmtrack_pageid={2}&st={3}";
            string url = string.Format(preUrl, userId, password, dmtrackPageid, st);
            string html = HttpHelper.GetHtml(url);
            string xloginCallBackForRisUrl = "https://login.alibaba.com/xloginCallBackForRisk.do";
            string postString = "dmtrack_pageid_info=" + dmtrackPageid + "&xloginPassport=" + userId + "&xloginPassword=" + password + "&ua=&pd=alibaba";
            HttpHelper.GetHtml(xloginCallBackForRisUrl, postString, cookieContainer);

            if (string.IsNullOrEmpty(html) || html.IndexOf("var xman_success=") == -1)
            {
                return false;
            }
            string context = html.Replace("var xman_success=", "").Trim();
            AliLoginUser aliLoginUser = JsonConvert.FromJson<AliLoginUser>(context);
            List<string> urls = aliLoginUser.xlogin_urls;
            foreach (string urlstring in urls)
            {
                HttpHelper.GetHtml(urlstring, cookieContainer);
            }
            string manageHtml = HttpHelper.GetHtml(MainForm.ManageHtml, cookieContainer);
            ShareCookie.Instance.CsrfToken = HttpClient.GetCsrfToken(manageHtml);
            ShareCookie.Instance.CheckCodeUrl = HttpClient.GetCheckCodeUrl(manageHtml);
            ShareCookie.Instance.LoginCookieContainer = cookieContainer;
            return true;
        }

    }
}
