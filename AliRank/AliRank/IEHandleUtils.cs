using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Net;
using System.Collections;

namespace AliRank
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


        public static void WebBrowerSetCookies_NavigateToUrl(WebBrowser webBrowser1, string url, CookieContainer cookieContainer)
        {
            CookieCollection cookies = cookieContainer.GetCookies(new Uri(""));
            string cookie_string = string.Empty;
            foreach (Cookie cook in cookies)
            {
                cookie_string = cook.Name + "=" + cook.Value + ";" + cookie_string;
                IEHandleUtils.InternetSetCookie(url, cook.Name, cook.Value);
            }
            webBrowser1.Navigate(url, "", null, "Cookie: " + cookie_string + Environment.NewLine);
        }


        public static void Navigate(WebBrowser webBrowser1, string url, string postString, string additionalHeaders)
        {
            List<Cookie> cookies = ShareCookie.Instance.LoginCookies;
            if (cookies != null)
            {
                foreach (Cookie cook in cookies)
                {
                    IEHandleUtils.InternetSetCookie(url, cook.Name, cook.Value);
                }
            }
            byte[] postData = null;
            if (!string.IsNullOrEmpty(postString))
            {
                postData = Encoding.Default.GetBytes(postString);
            }
            webBrowser1.Navigate(url, "_self", postData, additionalHeaders);
        }

        /// <summary>
        /// 遍历CookieContainer
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        public static List<Cookie> GetAllCookies(CookieContainer cc)
        {
            List<Cookie> lstCookies = new List<Cookie>();

            Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
                System.Reflection.BindingFlags.Instance, null, cc, new object[] { });

            foreach (object pathList in table.Values)
            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                    | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                foreach (CookieCollection colCookies in lstCookieCol.Values)
                    foreach (Cookie c in colCookies) lstCookies.Add(c);
            }
            return lstCookies;
        }
    }
}
