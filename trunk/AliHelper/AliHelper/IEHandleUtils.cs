using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Net;
using System.Windows.Forms;
using System.Security;
using System.Security.Permissions;
using System.ComponentModel;
using System.IO;

namespace AliHelper
{
    public class IEHandleUtils
    {
        #region public static void ClearIECache()
        /// <summary>
        /// 清除IE缓存
        /// </summary>
        public static void ClearIECache()
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c " + "del /f /s /q \"%userprofile%\\Local Settings\\Temporary Internet Files\\*.*\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
        }

        #endregion

        #region public static void ClearIECookie()
        /// <summary>
        /// 清除IE Cookie
        /// </summary>
        public static void ClearIECookie()
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c " + "del /f /s /q \"%userprofile%\\Cookies\\*.*\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
        }
        #endregion


        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetCookie(string lpszUrlName, string lbszCookieName, string lpszCookieData);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool InternetGetCookieEx(string pchURL, string pchCookieName, StringBuilder pchCookieData, ref int pcchCookieData, int dwFlags, object lpReserved);


        public static void WebBrowerSetCookies_NavigateToUrl(WebBrowser webBrowser1, string url)
        {
            string cookie_string = ShareCookie.Instance.LoginCookie;
            string[] cookstr = cookie_string.Split(';');
            foreach (string str in cookstr)
            {
                string[] cookieNameValue = str.Split('=');
                Cookie cook = new Cookie(cookieNameValue[0].Trim().ToString(), cookieNameValue[1].Trim().ToString());
                IEHandleUtils.InternetSetCookie(url, cook.Name, cook.Value);
            }
            webBrowser1.Navigate(url, "", null, "Cookie: " + cookie_string + Environment.NewLine);
        }

        public static string WebRequestGetUrlHtml(string url)
        {
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            myHttpWebRequest.Timeout = 20 * 1000; //连接超时
            myHttpWebRequest.Accept = "*/*";
            myHttpWebRequest.AllowAutoRedirect = false;
            myHttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0;)";
            myHttpWebRequest.Headers.Add("Cookie", ShareCookie.Instance.LoginCookie); //使用已经保存的cookies 方法二
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            Stream stream = myHttpWebResponse.GetResponseStream();
            stream.ReadTimeout = 15 * 1000; //读取超时
            StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("utf-8"));
            string strWebData = sr.ReadToEnd();
            return strWebData;
        }
    }
}
