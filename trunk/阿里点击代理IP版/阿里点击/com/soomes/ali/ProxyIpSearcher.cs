using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Xml;
using System.IO;
using log4net;
using DBUtility.SQLite;
using com.soomes.model;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace com.soomes.ali
{
    public delegate void CheckSuccessEventHandler(object sender, MyEventArgs e);
    public delegate void WriteLogEventHandler(object sender, MyEventArgs e);

    public class ProxyIpSearcher
    {
        public event CheckSuccessEventHandler CheckSuccessEvent;
        public event WriteLogEventHandler WriteLogEvent;
        private Boolean IsStop;

        private static readonly ILog Log = LogManager.GetLogger(typeof(ProxyIpSearcher));
        static string PROXYCN_REGEX = "onDblClick=\"clip\\(\\'(.*?)\\'\\);alert";
        static string CNPROXY_REGEX = "<tr><td>(.*?)<SCRIPT type=text/javascript>document.write\\(\":\"(.*?)\\)</SCRIPT></td>";

        public void Stop()
        {
            this.IsStop = true;
        }

        public void DoSearch(bool cnproxyChecked, bool minidailiChecked, bool proxycnChecked)
        {
            IsStop = false;
            string remoteSite = "http://www.alibaba.com";
            IList<string> ipList = new List<string>();
            if (cnproxyChecked && !IsStop)
            {
                ipList = SearchIpAddressFromCnproxy(ipList);
                LogToLabelAndFile( "www.cnproxy.com search proxy number " + ipList.Count);
            }
            if (minidailiChecked && !IsStop)
            {
                ipList = SearchIpAddressFromMinidaili(ipList);
                LogToLabelAndFile("www.minidaili.com search proxy number " + ipList.Count);
            }
            if (proxycnChecked && !IsStop)
            {
                ipList = SearchIpAddressFromProxycn(ipList);
                LogToLabelAndFile("www.proxycn.com search proxy number " + ipList.Count);
            }
            IList<string> validIpList = new List<string>();
            LogToLabelAndFile(ipList.Count + " == valid IP Address.");
            DAO.UpdateAllProxyToDisabled();
            foreach (string ipAddress in ipList)
            {
                LogToLabelAndFile("start check proxy address " + ipAddress + "...");
                Boolean isValid = HttpUtils.CheckProxyIP(ipAddress, remoteSite);
                if (isValid)
                {
                    ProxyIpModel model = new ProxyIpModel();
                    model.Ip = ipAddress;
                    model.IpDesc = HttpUtils.GetIPAddressLocationFormYouDao(ipAddress.Split(':')[0]);
                    model.UseNumber = 0;
                    model.LastUseTime = DateTime.Now;
                    model.CheckTime = DateTime.Now;
                    model.Enabled = true;
                    validIpList.Add(ipAddress);
                    CheckSuccessEvent(this, new MyEventArgs(model));
                    LogToLabelAndFile(ipAddress + " is valid proxy address, location is [" + model.IpDesc + "]");
                }
                if (IsStop)
                {
                    validIpList = null;
                    ipList = null;
                    return;
                }
            }

            LogToLabelAndFile("Search proxy address end. total:" + validIpList.Count + "/" + ipList.Count);
            validIpList = null;
            ipList = null;
        }

        public void LogToLabelAndFile(string msg)
        {
            Log.Debug(msg);
            WriteLogEvent(this, new MyEventArgs(msg));
        }

        public IList<string> SearchIpAddress()
        {
            string remoteSite = "http://www.alibaba.com";
            IList<string> ipList = new List<string>();
            ipList = SearchIpAddressFromCnproxy(ipList);
            ipList = SearchIpAddressFromMinidaili(ipList);
            ipList = SearchIpAddressFromProxycn(ipList);
            IList<string> validIpList = new List<string>();
            Log.Debug(ipList.Count + " == valid proxy Address.");
            
            foreach (string ipAddress in ipList)
            {
                Boolean isValid = HttpUtils.CheckProxyIP(ipAddress, remoteSite);
                if (isValid)
                {
                    String location = HttpUtils.GetIPAddressLocationFormYouDao(ipAddress.Split(':')[0]);
                    validIpList.Add(ipAddress);
                    Log.Debug(ipAddress + " is valid address, location is [" + location + "]");
                }
            }
            Log.Debug("Search proxy address end. total:" + validIpList.Count + "/" + ipList.Count);
            return validIpList;        
        }

        /// <summary>
        /// 从www.proxycn.com搜索IP
        /// </summary>
        /// <param name="ipList"></param>
        /// <returns></returns>
        private IList<string> SearchIpAddressFromProxycn(IList<string> ipList)
        {
            List<string> urlList = new List<string>();
            urlList.Add("http://www.proxycn.com/html_proxy/30fastproxy-1.html");
            for (int i = 1; i <= 1; i++) 
            {
                urlList.Add("http://www.proxycn.com/html_proxy/http-" + i + ".html");
            }
            Regex mRegex = new Regex(PROXYCN_REGEX);
            foreach (string url in urlList)
            {
                string html = HttpUtils.GetContent(url);
                ipList = SearchIpAddressFormHtml(mRegex, html, ipList);
            }
            urlList.Clear();
            urlList = null;
            return ipList;
        }

        /// <summary>
        /// 从www.minidaili.com搜索IP
        /// </summary>
        /// <param name="ipList"></param>
        /// <returns></returns>
        private IList<string> SearchIpAddressFromMinidaili(IList<string> ipList)
        {
            List<string> urlList = new List<string>();
            //from www.minidaili.com
            urlList.Add("http://www.minidaili.com/http/");
            Regex mRegex = new Regex(PROXYCN_REGEX);
            foreach (string url in urlList)
            {
                string html = HttpUtils.GetContent(url);
                ipList = SearchIpAddressFormHtml(mRegex, html, ipList);
            }
            urlList.Clear();
            urlList = null;
            return ipList;
        }

        /// <summary>
        /// 从www.cnproxy.com搜索IP
        /// </summary>
        /// <param name="ipList"></param>
        /// <returns></returns>
        private IList<string> SearchIpAddressFromCnproxy(IList<string> ipList)
        {
            List<string> urlList = new List<string>();
            urlList.Add("http://www.cnproxy.com/proxy1.html");
            /*
            for (int i = 1; i <= 1; i++)
            {
                urllist.add("http://www.cnproxy.com/proxy" + i + ".html");
            }
             */
         
            Regex mRegex = new Regex(CNPROXY_REGEX);
            foreach (string url in urlList)
            {
                string html = HttpUtils.GetContent(url);
                MatchCollection mMactchCol = mRegex.Matches(html);
                foreach (Match mMatch in mMactchCol)
                {
                    string ipAddress = mMatch.Groups[1].ToString();
                    string portCode = mMatch.Groups[2].ToString();
                    portCode = portCode.Replace("+z", "3");
                    portCode = portCode.Replace("+m", "4");
                    portCode = portCode.Replace("+k", "2");
                    portCode = portCode.Replace("+l", "9");
                    portCode = portCode.Replace("+d", "0");
                    portCode = portCode.Replace("+b", "5");
                    portCode = portCode.Replace("+i", "7");
                    portCode = portCode.Replace("+w", "6");
                    portCode = portCode.Replace("+r", "8");
                    portCode = portCode.Replace("+c", "1");
                    String proxyIp = ipAddress + ":" + portCode;
                    if (!ipList.Contains(proxyIp))
                    {
                        ipList.Add(proxyIp);
                        Console.WriteLine(proxyIp);
                    }
                    else
                    {
                        Log.Debug(proxyIp + "===be contained in List.");
                    }
                }
            }
            urlList.Clear();
            urlList = null;
            return ipList;
        }



        private IList<string> SearchIpAddressFormHtml(Regex mRegex, string html, IList<string> ipList)
        {
            if (ipList == null)
            {
                ipList = new List<string>();
            }
            MatchCollection mMactchCol = mRegex.Matches(html);
            foreach (Match mMatch in mMactchCol)
            {
                string proxyIp = mMatch.Groups[1].ToString();
                if (!ipList.Contains(proxyIp))
                {
                    ipList.Add(proxyIp);
                    Console.WriteLine(proxyIp);
                }
                else 
                {
                    Log.Debug(proxyIp + "===be contained in List.");
                }
            }
            return ipList;
        }
         
    }
}