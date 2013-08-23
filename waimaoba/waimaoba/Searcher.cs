using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace com.soomes
{
    class Searcher
    {
        private int SearchDepth;
        private HtmlDocument document;
        public Searcher(int searchDepth)
        {
            this.SearchDepth = searchDepth;
            this.document = new HtmlDocument();
        }

        public void DoSearch(string url)
        {
            DoSearch(url, 0);
        }
        

        private void DoSearch(string url, int depth)
        {
            if (depth > this.SearchDepth)
            {
                return;
            }
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

            try
            {
                
                string html = HttpHelper.GetHtml(url);
                document.LoadHtml(html);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            List<string> links = GetLinks(url, document);
            if (url.IndexOf(".waimaoba.com") >= 0)
            {
                WebParse webParse = new WaimaobaCompanyItemParse();
                webParse.Parse(url, document);
                DAOFactory.GetInstance().GetSearchUrlDao().Insert(url);

                foreach (string link in links)
                {
                    webParse.Parse(url, document);
                    DAOFactory.GetInstance().GetSearchUrlDao().Insert(url);
                }
                foreach (string link in links)
                {
                    DoSearch(link, depth + 1);
                }
            }
        }

        public List<string> GetLinks(string url, HtmlDocument document)
        {
            List<string> links = new List<string>();
            HtmlNodeCollection linkNodes = document.DocumentNode.SelectNodes("//a");
            if (linkNodes == null)
            {
                return links;
            }
            foreach (HtmlNode linkNode in linkNodes)
            {
                if (linkNode.Attributes["href"] != null)
                {
                    string currUrl = linkNode.Attributes["href"].Value;
                    if (currUrl == "" || currUrl == "#")
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
            return links;
        }
    }


}
