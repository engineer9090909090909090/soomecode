using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;
using System.Net;
using Soomes;
using System.Web;
using HtmlAgilityPack;

namespace AliHelper
{
    class Passporter2
    {
        private string loginUrl = "https://login.alibaba.com/";
        //string homeUrl = "http://www.alibaba.com/";

        private WebBrowser browser;
        ManualResetEvent eventX = new ManualResetEvent(false);
        private string UserName;
        private string Password;
        //private bool LoginSuccess = true;
        public Passporter2(WebBrowser b) 
        {
            browser = b;
        }

        public bool DoLogin(string Account, string Password)
        {
            this.UserName = Account;
            this.Password = Password;
            CookieContainer cookieContainer = new CookieContainer();
            string html = HttpHelper.GetHtml(loginUrl, cookieContainer);
            string dmtrackPageid = GetDmtrackPageid(html);
            if (string.IsNullOrEmpty(dmtrackPageid))
            {
                return false;
            }
            
            string littleLoginUrl = "https://passport.alipay.com/littleLogin/littleLogin.htm?params="
            + HttpUtility.UrlEncode("{\"lang\":\"en_US\",\"fromSite\":\"4\",\"styleType\":\"vertical\",\"pageId\":\"" + dmtrackPageid + "\"}")
            + "&rnd=" + new Random().NextDouble().ToString();
            html = HttpHelper.GetHtml(littleLoginUrl, cookieContainer);
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(html);
            string cid = document.GetElementbyId("fm-cid").Attributes["value"].Value;
            string umidToken = document.GetElementbyId("fm-umid-token").Attributes["value"].Value;
            string pageId = document.GetElementbyId("fm-pageId").Attributes["value"].Value;
            string _csrf_token = document.GetElementbyId("fm-csrf-tk").Attributes["value"].Value;
            string site = document.GetElementbyId("fm-site").Attributes["value"].Value;
            string littleLoginRpcUrl = "https://passport.alipay.com/littleLogin/littleLoginRpc/doLogin.json";
            string postString =  "loginId="+ this.UserName;
            postString= postString + "&password="+this.Password;
            postString= postString + "&checkCode=";
            postString= postString + "&site=" + site;
            postString= postString + "&ua=undefined";
            postString= postString + "&cid="+ cid;
            postString= postString + "&rdsToken=";
            postString= postString + "&umidToken="+umidToken;
            postString= postString + "&lang=en_US";
            postString= postString + "&pageId=" + pageId;
            postString = postString + "&screenPixel=1280x800&navUserAgent=" + HttpHelper.UserAgent;
            postString= postString + "&navAppVersion=&navPlatform=Win32";
            postString= postString + "&_csrf_token=" + _csrf_token;
            html = HttpHelper.GetHtml(littleLoginRpcUrl, postString, cookieContainer);
            System.Diagnostics.Trace.WriteLine(html);
            return true;
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
            string manageHtml = HttpHelper.GetHtml(HttpClient.ManageHtml, cookieContainer);
            DataCache.Instance.CsrfToken = HttpClient.GetCsrfToken(manageHtml);
            DataCache.Instance.CheckCodeUrl = HttpClient.GetCheckCodeUrl(manageHtml);
            DataCache.Instance.AliID = FileUtils.GetAliId(cookieContainer, Constants.HomeUrl);
            ShareCookie.Instance.LoginCookieContainer = cookieContainer;
            return true;
        }

    }
}
