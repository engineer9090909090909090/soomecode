using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net;
using System.IO;

namespace AliRank
{
    

    class RankQueryer
    {
        public event RankSearchEndEvent OnRankSearchEndEvent;
        public event RankSearchingEvent OnRankSearchingEvent;

        private string SEARCH_URL = "http://www.alibaba.com/products/F0/{0}/----------------------50/{1}.html";
        private string PERCENT_PATH = "//div[@class='percent-wrap']";
        private string SUBJECT_PATH = "div[@class='attr']/h2/a";
        private string AD_PATH = "//div[@class='list-items AD']";
        private string P4P_PATH = "//div[@class='list-items p4p']";

        private Keywords item;

        public RankQueryer() { }


        public virtual void FinishedEvent(Keywords kw, string msg)
        {
            if (OnRankSearchEndEvent != null)
            {
                RankEventArgs e = new RankEventArgs(kw, msg);
                OnRankSearchEndEvent(this, e);
            }
        }
        public virtual void SearchingEvent(Keywords kw, string msg)
        {
            if (OnRankSearchingEvent != null)
            {
                RankEventArgs e = new RankEventArgs(kw, msg);
                OnRankSearchingEvent(this, e);
            }
        }

        public void Seacher(Keywords kw)
        {
            item = kw;
            item.Rank = 0;
            string mainKey = item.MainKey.Replace(" ", "_");
            HtmlDocument document = null;

            for (int i = 0; i < 10; i++)
            {
                string url = string.Format(SEARCH_URL, mainKey, (i + 1));
                HtmlWeb clinet = new HtmlWeb();
                SearchingEvent(item, "Searching " + i + " page...");
                document = clinet.Load(url);
                System.Diagnostics.Trace.WriteLine(url + " = " + mainKey);
                if (i == 0)
                {
                    HtmlNodeCollection adNodes = document.DocumentNode.SelectNodes(AD_PATH);
                    HtmlNodeCollection p4pNodes = document.DocumentNode.SelectNodes(P4P_PATH);
                    if (adNodes != null)
                    {
                        item.KeyAdNum = adNodes.Count;
                    }
                    if (p4pNodes != null)
                    {
                        item.KeyP4Num = p4pNodes.Count;
                    }
                }
                string comUrlPath = "//div[@class='supplier']/span/a[@href='" + item.CompanyUrl + "']";
                HtmlNodeCollection comUrlNode = document.DocumentNode.SelectNodes(comUrlPath);
                if (comUrlNode == null || comUrlNode.Count == 0)
                {
                    continue;
                }
                HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(PERCENT_PATH);
                for (int k = 0; k < nodes.Count; k++)
                {
                    HtmlNode percentNode = nodes[k];
                    HtmlNode aLinkNode = percentNode.SelectSingleNode(SUBJECT_PATH);
    
                    string lsubject = aLinkNode.Id.ToLower();
                    string subjectId = "lsubject_" + item.ProductId;
                    if (subjectId.Equals(lsubject.ToLower()))
                    {
                        item.Rank = i * 50 + (k + 1);
                        break;
                    }
                }
                if (item.Rank > 0)
                {
                    break;                
                }
            }
            FinishedEvent(item, "Rank search finished.");
        }
    }
}
