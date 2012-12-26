using System;  
using System.Collections.Generic; 
using System.Text; 
using System.Net; 
using System.IO;
using System.Net.NetworkInformation;  

namespace AliRank

{ 
    /// <summary> 
    ///http协议模拟请求类 
    //////主要考虑请求后的具体结果 
    ///此类出自 uu102.com，请尊重原作者，转载请加上此链接 
    /// </summary>
    class HttpHelper
    { 
        /// <summary>
        /// 请求并发限制数目
        /// </summary> 
        private static int DefaultConnectionLimit=1; 
     
        private const string Accept = "*/*";
        private const string UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)"; 
        private const string ContentType = "application/x-www-form-urlencoded"; 
        
        /// <summary>
        /// 发送资源请求。返回请求到的响应文本
        /// 之所以看到这么多形参，只是本人的水平很菜的体现^.^
        /// 注意，这个类并没有处理https加密请求
        /// </summary> 
 /// <param name="url">发送请求的url地址</param> 
        /// <param name="postString"></param>
        /// <param name="IsPost"></param> 
        /// <param name="cookieContainer"></param> 
        /// <param name="referer"></param> 
        /// <param name="encoding"></param>
        /// <returns></returns> 
        public static string GetHtml(string url, string postString, bool IsPost, CookieContainer cookieContainer, string referer, Encoding encoding) 
        {
            string html = string.Empty;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.DefaultConnectionLimit = DefaultConnectionLimit;
            //设置并发连接数限制上额 
            DefaultConnectionLimit++; 
            if (string.IsNullOrEmpty(postString)) IsPost = false;
            HttpWebRequest httpWebRequest = null;
            
            HttpWebResponse httpWebResponse = null;
            try
            { 
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);//创建连接请求
                httpWebRequest.Method = IsPost ? "POST" : "GET"; 
                if (cookieContainer != null) httpWebRequest.CookieContainer = cookieContainer; 
                httpWebRequest.AllowAutoRedirect = true;//【注意】这里有个时候在特殊情况下要设置为否，否则会造成cookie丢失
                httpWebRequest.ContentType = ContentType;
                httpWebRequest.Accept = Accept;
                httpWebRequest.UserAgent = UserAgent;
                if (!string.IsNullOrEmpty(referer)) httpWebRequest.Referer = referer;
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
                StreamReader streamReader = new StreamReader(responseStream, encoding);
                html = streamReader.ReadToEnd();//注意这里是直接将所有的字节从头读到尾，也可以一行一行的控制，节省时间 
                streamReader.Close();
                responseStream.Close(); 
                httpWebRequest.Abort();
                foreach (Cookie cookie in httpWebResponse.Cookies) //获取cookie
                { 
                    cookieContainer.Add(cookie);
                }
                httpWebResponse.Close();
                //到这里为止，所有的对象都要释放掉，以免内存像滚雪球一样
                return html;
            }
            catch(Exception e)
            {
                DefaultConnectionLimit--;
                System.Diagnostics.Trace.WriteLine("Open " + url + "\r\n " + e.Message);
                //我这里就没做任何处理了，这里最好还是处理一下
                return string.Empty;
            }
        }
        
        
        #region 重载方法 
        public static string GetHtml(string url, string postString, CookieContainer cookieContainer, string referer)
        {
            return GetHtml(url, postString, true, cookieContainer, referer, Encoding.UTF8);
        }
        
        public static string GetHtml(string url, string postString, CookieContainer cookieContainer)
        {
            return GetHtml(url, postString, true, cookieContainer, url, Encoding.UTF8);
        }
        
        public static string GetHtml(string url, CookieContainer cookieContainer, string referer)
        {
            return GetHtml(url, "", false, cookieContainer, referer, Encoding.UTF8);
        }
        
        public static string GetHtml(string url, CookieContainer cookieContainer)
        {
            return GetHtml(url, "", false, cookieContainer, url, Encoding.UTF8);
        }
        
        public static string GetHtml(string url)
        {
            return GetHtml(url, "", true, null, url, Encoding.UTF8);
        }
        #endregion
        
        /// <summary>
        /// 这个方法主要用来获取非文本的html响应正文
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <returns></returns>
        public static Stream GetStream(string url,CookieContainer cookieContainer)
        {
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            try
            {
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();
                return responseStream;
            }
            catch
            {
                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                } if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
                return null;
            }
        }

        public static string GetWebAccessIpAddress()
        {
            string address = string.Empty;
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface item in interfaces)
            {
                if (item.NetworkInterfaceType == NetworkInterfaceType.Ppp)
                {
                    address = item.GetIPProperties().UnicastAddresses[0].Address.ToString();
                }
            }
            if (string.IsNullOrEmpty(address))
            {
                address = Ip138GetIp();
            }
            return address;
        }

        public static string Ip138GetIp()
        {
            string address = string.Empty;
            try
            {
                string strUrl = "http://iframe.ip138.com/ic.asp";//获得IP的网址
                Uri uri = new Uri(strUrl);
                WebRequest webreq = WebRequest.Create(uri);
                Stream s = webreq.GetResponse().GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.Default);
                string all = sr.ReadToEnd(); //读取网站返回的数据  格式：您的IP地址是：[x.x.x.x] 
                int i = all.IndexOf("[") + 1;
                string tempip = all.Substring(i, 15);
                address =  tempip.Replace("]", "").Replace(" ", "").Replace("<", "");
            }
            catch(Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e.Message);
                address = "";
            }
            return address;
        }

    }
}