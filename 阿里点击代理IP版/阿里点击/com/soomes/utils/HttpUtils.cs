using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Net;
using System.IO;
using log4net;
using System.Xml;
using System.Runtime.InteropServices;

namespace com.soomes.ali
{
    public class HttpUtils
    {

        private static readonly ILog Log = LogManager.GetLogger(typeof(HttpUtils));


        /// <summary>
        /// SetupProxy
        /// </summary>
        /// <param name="proxy"></param>
        public static void SetupProxy(string proxy)
        {
            string key = "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";
            RegistryKey RegKey = Registry.CurrentUser.OpenSubKey(key, true);
            if (proxy == null || "".Equals(proxy.Trim()))
            {
                RegKey.SetValue("ProxyServer", "");
                RegKey.SetValue("ProxyEnable", 0);
                RegKey.SetValue("ProxyOverride", "");
            }
            else 
            {
                RegKey.SetValue("ProxyServer", proxy);
                RegKey.SetValue("ProxyEnable", 1);
                RegKey.SetValue("ProxyOverride", "127.0.0.1;localhost;(local)");
            }
        }


        public struct Struct_INTERNET_PROXY_INFO
        {
            public int dwAccessType;
            public IntPtr proxy;
            public IntPtr proxyBypass;
        };

        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);

        public static void RefreshIESettings(string strProxy)
        {
            /*
            const int INTERNET_OPTION_PROXY = 38;
            const int INTERNET_OPEN_TYPE_PROXY = 3;
            const int INTERNET_OPEN_TYPE_DIRECT = 1;

            Struct_INTERNET_PROXY_INFO struct_IPI;
            //Filling in structure
            if (String.IsNullOrEmpty(strProxy))
            {
                struct_IPI.dwAccessType = INTERNET_OPEN_TYPE_DIRECT;
                struct_IPI.proxy = Marshal.StringToHGlobalAnsi("");
            }
            else
            {
                struct_IPI.dwAccessType = INTERNET_OPEN_TYPE_PROXY;
                struct_IPI.proxy = Marshal.StringToHGlobalAnsi(strProxy);
            }
            
            struct_IPI.proxyBypass = Marshal.StringToHGlobalAnsi("local");
            //Allocating memory   
            IntPtr intptrStruct = Marshal.AllocCoTaskMem(Marshal.SizeOf(struct_IPI));
            //Converting structure to IntPtr   
            Marshal.StructureToPtr(struct_IPI, intptrStruct, true);
            bool iReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_PROXY, intptrStruct, Marshal.SizeOf(struct_IPI));
          */
        }

        /// <summary>
        /// 读取网页HTML内容
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static string GetContent(string Url)
        {
            string strResult = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Proxy = null;
                request.Timeout = 30000;//设置连接超时时间
                request.Headers.Set("Pragma", "no-cache");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding("GB2312");
                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                strResult = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return strResult;
        }

        /// <summary>
        /// 检查IP是否为有效IP
        /// </summary>
        /// <param name="sIpAndPort"></param>
        /// <param name="remoteSite"></param>
        /// <returns></returns>
        public static bool CheckProxyIP(string sIpAndPort, string remoteSite)
        {
            string[] sIpPortArray = sIpAndPort.Split(':');
            if (sIpPortArray != null && sIpPortArray.Length == 2)
            {
                string sProxyIP = sIpPortArray[0];
                string sPort = sIpPortArray[1];
                return CheckProxyIP(sIpPortArray[0], Convert.ToInt32(sPort), 2000, remoteSite);
            }
            return false;
        }

        /// <summary>
        /// 检查IP是否为有效IP
        /// </summary>
        /// <param name="sProxyIP"></param>
        /// <param name="sPort"></param>
        /// <param name="iTimeOut"></param>
        /// <param name="remoteSite"></param>
        /// <returns></returns>
        public static bool CheckProxyIP(string sProxyIP, int sPort, int iTimeOut, string remoteSite)
        {
            if (sProxyIP == null || sProxyIP.Split('.').Length != 4
                || sPort > 65535 || sPort < 0)
            {
                Log.Error("" + sProxyIP + ":" + sPort + " 不正确或无效的端口号.");
                return false;
            }
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(remoteSite);
            try
            {
                request.Headers.Add("VIA", sProxyIP);
                request.Headers.Add("X_FORWARDED_FOR", sProxyIP);
                request.Timeout = iTimeOut;
                request.Proxy = new WebProxy(sProxyIP, sPort);
                request.GetResponse();
            }
            catch
            {
                request.Abort();
                Log.Error(sProxyIP + ":" + sPort + " validate fail.");
                return false;
            }
            Log.Debug(sProxyIP + ":" + sPort + " validate success.");
            request.Abort();
            return true;
        }

        /// <summary>
        /// 从有道IP接口读取IP Location
        /// </summary>
        /// <param name="sIp"></param>
        /// <returns></returns>
        public static string GetIPAddressLocationFormYouDao(string sIp)
        {
            WebRequest wr_req = WebRequest.Create("http://www.youdao.com/smartresult-xml/search.s?type=ip&q=" + sIp);
            wr_req.Proxy = null;
            WebResponse webResponse = wr_req.GetResponse();
            Stream receiveStream = webResponse.GetResponseStream();
            XmlDocument xml = new XmlDocument();
            xml.Load(receiveStream);
            XmlNode locationNode = xml.SelectSingleNode("/smartresult/product/location");
            if (locationNode != null)
            {
                return locationNode.InnerText; ;
            }
            else
            {
                return "";
            }
        } 
 

    }
}
