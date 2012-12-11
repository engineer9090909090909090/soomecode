using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;
using System.Net;

namespace AliRank
{
    class Passporter
    {
        private string loginUrl = "https://login.alibaba.com/";
        string homeUrl = "http://sh.vip.alibaba.com/";

        private WebBrowser browser;
        ManualResetEvent eventX = new ManualResetEvent(false);
        private string UserName;
        private string Password;
        public Passporter(WebBrowser b) 
        {
            browser = b;
        }

        public void DoLogin(string u, string p)
        {
            this.UserName = u;
            this.Password = p;
            browser.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(browser_LogoinCompleted);
            browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(browser_LogoinCompleted);
            browser.Navigate(loginUrl);
            eventX.WaitOne(Timeout.Infinite, true);
            Console.WriteLine("线程池结束！");
        }

        void browser_LogoinCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        { 
            WebBrowser browser = (WebBrowser)sender;
            System.Diagnostics.Trace.WriteLine("= "+browser.Url.ToString());
            if (browser.ReadyState != System.Windows.Forms.WebBrowserReadyState.Complete)
                return;
            if (e.Url.ToString() != browser.Url.ToString())
                return;
            if (e.Url.ToString() == homeUrl)
            {
                browser.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(browser_LogoinCompleted);
                eventX.Set();
            }
            
            if (browser.Url.ToString() == loginUrl)
            {
                string html = browser.Document.Body.InnerHtml;
                string dmtrackPageid = GetDmtrackPageid(html);
                CookieContainer cookieContainer = new CookieContainer();
                string token = GetToken(this.UserName, this.Password, dmtrackPageid, ref cookieContainer);
                string st = GetST(token, ref cookieContainer);
                GetLoginUrl(this.UserName, this.Password, dmtrackPageid, st, ref cookieContainer);

                CookieCollection cookies = cookieContainer.GetCookies(new Uri("http://www.alibaba.com"));
                string cookie_string = string.Empty;
                foreach (Cookie cook in cookies)
                {
                    string cookstring = cook.Name + "=" + cook.Value + ";";
                    Console.WriteLine(cookstring);
                    cookie_string = cookstring + cookie_string;
                    IEHandleUtils.InternetSetCookie(homeUrl, cook.Name, cook.Value);
                }
                browser.Navigate(homeUrl, "", null, "Cookie: " + cookie_string + Environment.NewLine);

               //browser.Navigate(homeUrl);
            }
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

        public string GetToken(string userId, string password, string dmtrackPageid, ref CookieContainer cookieContainer)
        {
            string preUrl = "https://login.alibaba.com/xman/xlogin.js?pd=alibaba&pageFrom=standardlogin&u_token=&xloginPassport={0}&xloginPassword={1}&xloginCheckToken=&rememberme=rememberme&runatm=runatm&dmtrack_pageid={2}";
            string url = string.Format(preUrl, userId, password, dmtrackPageid);
            string html = HttpHelper.GetHtml(url);
            System.Diagnostics.Trace.WriteLine("GetToken = " + html);
            Regex r = new Regex("var xman_login_token={\"token\":\"(.*?)\"}");
            GroupCollection gc = r.Match(html).Groups;
            if (gc != null && gc.Count > 1)
            {
                return gc[1].Value.Trim();
            }
            return "";
        }

        public string GetST(string token, ref CookieContainer cookieContainer)
        {
            string preUrl = "https://passport.alibaba.com/mini_apply_st.js?site=4&callback=window.xmanDealTokenCallback&token={0}";
            string url = string.Format(preUrl, token);
            string html = HttpHelper.GetHtml(url);
            System.Diagnostics.Trace.WriteLine("GetST = " + html);
            Regex r = new Regex("{\"data\":{\"st\":\"(.*?)\"}");
            GroupCollection gc = r.Match(html).Groups;
            if (gc != null && gc.Count > 1)
            {
                return gc[1].Value.Trim();
            }
            return "";
        }

        public void GetLoginUrl(string userId, string password, string dmtrackPageid, string st, ref CookieContainer cookieContainer)
        {
            string preUrl = "https://login.alibaba.com/validateST.htm?pd=alibaba&pageFrom=standardlogin&u_token=&xloginPassport={0}&xloginPassword={1}&xloginCheckToken=&rememberme=rememberme&runatm=runatm&dmtrack_pageid={2}&st={3}";
            string url = string.Format(preUrl, userId, password, dmtrackPageid, st);
            string html = HttpHelper.GetHtml(url);
            System.Diagnostics.Trace.WriteLine("GetLoginUrl = " + html);

            string xloginCallBackForRisUrl = "https://login.alibaba.com/xloginCallBackForRisk.do";
            string postString = "dmtrack_pageid_info=" + dmtrackPageid + ";xloginPassport=" + userId + ";xloginPassword=" + password + ";ua=;pd=alibaba";
            HttpHelper.GetHtml(xloginCallBackForRisUrl, postString, cookieContainer);

            if (!string.IsNullOrEmpty(html))
            {
                string context = html.Replace("var xman_success=", "").Trim();
                AliLoginUser aliLoginUser = JsonConvert.FromJson<AliLoginUser>(context);
                List<string> urls = aliLoginUser.xlogin_urls;
                foreach (string urlstring in urls)
                {
                    HttpHelper.GetHtml(urlstring, cookieContainer);
                }
            }
            //string vipHtml = HttpHelper.GetHtml("http://sh.vip.alibaba.com/", cookieContainer);
            //System.Diagnostics.Trace.WriteLine("vipHtml = " + vipHtml);
        }

    }
}
