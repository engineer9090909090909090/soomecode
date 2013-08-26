using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Net;

namespace com.soomes
{

    public delegate void SearchEvent(object sender, SearchEventArgs e);

    public class SearchEventArgs : EventArgs
    {
        public string Msg;
        public SearchEventArgs(string _msg)
        {
            this.Msg = _msg;
        }
    }

    public class Searcher
    {
        private int SearchDepth;
        private HtmlWeb webClient;
        private HtmlDocument document;
        private WebParse webParse;
        public event SearchEvent DoSearchEvent;
        private bool IsStop;
        private Log loger;

        public Searcher(int searchDepth)
        {
            this.SearchDepth = searchDepth;
            this.IsStop = false;
            this.webClient = new HtmlWeb();
            this.loger = new Log(FileUtils.GetLogFolder(), LogType.Daily);
            HtmlWeb.PreRequestHandler handler = delegate(HttpWebRequest request)
            {

                request.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate";
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                request.CookieContainer = new System.Net.CookieContainer();
                return true;
            };
            this.webClient.PreRequest += handler;
            this.document = new HtmlDocument();
            this.webParse = new WaimaobaCompanyItemParse();
        }

        public void Stop()
        {
            this.IsStop = true;
        }

        public void DoSearch(string url)
        {
            DoSearch(url, 0);
            this.webParse.Dispose();
            loger.Dispose();
        }
        

        private void DoSearch(string url, int depth)
        {
            if (!url.ToLower().StartsWith("http://"))
            {
                return;
            }
            if (url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length-1);
            }
            if (DAOFactory.GetInstance().GetSearchUrlDao().ExistUrl(url))
            {
                return;
            }
            
            if (url.IndexOf(".waimaoba.com") >= 0)
            {

                try
                {
                    document = this.webClient.Load(url);
                }
                catch (Exception e)
                {
                    loger.Write("Open " + url + @"\r\n " + e.Message, MsgType.Error);
                    DAOFactory.GetInstance().GetSearchUrlDao().Insert(url);
                    return;
                }
                webParse.Parse(url, document);
                DAOFactory.GetInstance().GetSearchUrlDao().Insert(url);
                List<string> documentLinks = new List<string>();
                GetLinks(url, document, ref documentLinks);
                List<string> childrenLinks = new List<string>();
                foreach (string link in documentLinks)
                {
                    if (DAOFactory.GetInstance().GetSearchUrlDao().ExistUrl(link))
                    {
                        continue;
                    }
                    loger.Write(link, MsgType.Info);
                    SearchingEvent(link);
                    try
                    {
                        document = this.webClient.Load(link);
                    }
                    catch (Exception e)
                    {
                        loger.Write("Open " + link + @"\r\n " + e.Message, MsgType.Error);
                        DAOFactory.GetInstance().GetSearchUrlDao().Insert(url);
                        continue;
                    }
                    webParse.Parse(link, document);
                    DAOFactory.GetInstance().GetSearchUrlDao().Insert(link);
                    GetLinks(link, document, ref childrenLinks);
                    if (this.IsStop)
                    {
                        return;
                    }
                }
                documentLinks.Clear();
                documentLinks = null;
                foreach (string childLink in childrenLinks)
                {
                    if (DAOFactory.GetInstance().GetSearchUrlDao().ExistUrl(childLink))
                    {
                        continue;
                    }
                    DoSearch(childLink, depth + 1);
                    if (this.IsStop)
                    {
                        return;
                    }
                }
                childrenLinks.Clear();
                childrenLinks = null;
            }
            
        }

        public void GetLinks(string url, HtmlDocument document, ref List<string> links)
        {
            HtmlNodeCollection linkNodes = document.DocumentNode.SelectNodes("//a");
            if (linkNodes == null)
            {
                return;
            }
            foreach (HtmlNode linkNode in linkNodes)
            {
                if (linkNode.Attributes["href"] != null)
                {
                    string currUrl = linkNode.Attributes["href"].Value;
                    if (currUrl == "" || currUrl == "#" || currUrl.StartsWith("#"))
                    {
                        continue;
                    }
                    string currFullUrl = currUrl;
                    if (currUrl.StartsWith("/"))
                    {
                        currFullUrl = "http://" + new Uri(url).Host + currUrl;
                        HtmlUtils.Log(currFullUrl);
                    }
                    else
                    {
                        HtmlUtils.Log(currFullUrl);
                    }
                    if (!links.Contains(currFullUrl))
                    {
                        links.Add(currFullUrl);
                    }
                }
            }
        }

        public virtual void SearchingEvent(string msg)
        {
            if (DoSearchEvent != null)
            {
                SearchEventArgs e = new SearchEventArgs(msg);
                DoSearchEvent(this, e);
            }
        }
    }


}
