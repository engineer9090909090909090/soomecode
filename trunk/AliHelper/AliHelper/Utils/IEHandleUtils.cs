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
using System.Collections;

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

        public static string GetHtml(string url)
        {
            return GetHtml(url, null);
        }

        public static string GetHtml(string url, string postString)
        {
            string html = string.Empty;
            bool IsPost = true;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.DefaultConnectionLimit = HttpHelper.DefaultConnectionLimit;
            //设置并发连接数限制上额 
            HttpHelper.DefaultConnectionLimit++;
            if (string.IsNullOrEmpty(postString)) IsPost = false;
            HttpWebRequest httpWebRequest = null;

            HttpWebResponse httpWebResponse = null;
            try
            {
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);//创建连接请求
                httpWebRequest.Method = IsPost ? "POST" : "GET";
                httpWebRequest.AllowAutoRedirect = true;//【注意】这里有个时候在特殊情况下要设置为否，否则会造成cookie丢失
                httpWebRequest.ContentType = HttpHelper.ContentType;
                httpWebRequest.Accept = HttpHelper.Accept;
                httpWebRequest.UserAgent = HttpHelper.UserAgent;
                if (ShareCookie.Instance.LoginCookieContainer == null)
                {
                    httpWebRequest.Headers.Add("Cookie", ShareCookie.Instance.LoginCookie); //使用已经保存的cookies 方法二
                }
                else 
                {
                    httpWebRequest.CookieContainer = ShareCookie.Instance.LoginCookieContainer;
                }
                if (IsPost)  //如果是Post递交数据，则写入传的字符串数据 
                {
                    byte[] byteRequest = Encoding.Default.GetBytes(postString);
                    httpWebRequest.ContentLength = byteRequest.Length;
                    Stream stream = httpWebRequest.GetRequestStream();
                    stream.Write(byteRequest, 0, byteRequest.Length);
                    stream.Close();
                }
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();//开始获取响应流 
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                html = streamReader.ReadToEnd();//注意这里是直接将所有的字节从头读到尾，也可以一行一行的控制，节省时间 
                streamReader.Close();
                responseStream.Close();
                httpWebRequest.Abort();
                httpWebResponse.Close();//到这里为止，所有的对象都要释放掉，以免内存像滚雪球一样
                return html;
            }
            catch (Exception e)
            {
                HttpHelper.DefaultConnectionLimit--;
                System.Diagnostics.Trace.WriteLine("Open " + url + "\r\n " + e.Message);
                //我这里就没做任何处理了，这里最好还是处理一下
                return string.Empty;
            }
        
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
