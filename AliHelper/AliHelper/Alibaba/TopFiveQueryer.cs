using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net;
using System.IO;

namespace AliHelper
{
    class TopFiveQueryer
    {
        public event TopFiveSearchEndEvent OnTopFiveSearchEndEvent;
        public event TopFiveSearchingEvent OnTopFiveSearchingEvent;
        private string KeywordExpressions = "var kw = encodeURIComponent\\(\\\"(.*?)\\\"\\);";
        private string SEARCH_URL = "http://www.alibaba.com/trade/search?fsb=y&IndexArea=product_en&CatId=&SearchText={0}";
        private string IMAGE_PATH = "div[@class='pic']/a/img";
        private string SUBJECT_PATH = "div[@class='percent-wrap']/div[@class='attr']/h2/a[@class='qrPTitle']";
        private string PATH = "//div[@class='list-items']";
        private string AD_PATH = "//div[@class='list-items AD']";
        private string P4P_PATH = "//div[@class='list-items  p4p']";
        private string P4PV2_PATH = "//div[@class='list-items  p4pv2']";

        public TopFiveQueryer() 
        {
        }


        public virtual void FinishedEvent(TopFiveInfo kw, string msg)
        {
            if (OnTopFiveSearchEndEvent != null)
            {
                TopFiveEventArgs e = new TopFiveEventArgs(kw, msg);
                OnTopFiveSearchEndEvent(this, e);
            }
        }
        public virtual void SearchingEvent(TopFiveInfo kw, string msg)
        {
            if (OnTopFiveSearchingEvent != null)
            {
                TopFiveEventArgs e = new TopFiveEventArgs(kw, msg);
                OnTopFiveSearchingEvent(this, e);
            }
        }

        public void Seacher(string key)
        {
            HtmlDocument document = null;
            int p4pNum = 0;
            int adaNum = 0;
            string url = string.Format(SEARCH_URL, key.Replace(" ", "+"), 1);
            HtmlWeb clinet = new HtmlWeb();
            document = clinet.Load(url);
            HtmlNodeCollection itemNodes = document.DocumentNode.SelectNodes(PATH);
            HtmlNodeCollection adNodes = document.DocumentNode.SelectNodes(AD_PATH);
            HtmlNodeCollection p4pNodes = document.DocumentNode.SelectNodes(P4P_PATH);
            HtmlNodeCollection p4pv2Nodes = document.DocumentNode.SelectNodes(P4PV2_PATH);
            if (adNodes != null)
            {
                adaNum = adNodes.Count;
            }
            if (p4pNodes != null)
            {
                p4pNum = p4pNodes.Count;
            }
            if (p4pv2Nodes != null)
            {
                p4pNum = p4pNum + p4pv2Nodes.Count;
            }
            int itemCount = itemNodes.Count;
            int kcount = itemCount >= 10 ? 10 : itemCount;
            for (int k = 0; k < kcount; k++)
            {
                TopFiveInfo item = new TopFiveInfo();
                item.KeyP4Num = p4pNum;
                item.KeyAdNum = adaNum;
                HtmlNode percentNode = itemNodes[k];

                HtmlNode ImageNode = percentNode.SelectSingleNode(IMAGE_PATH);
                string src = ImageNode.Attributes["image-src"].Value;
                string productId = ImageNode.Id.Replace("limage_", "");
                try
                {
                    WebClient webClient = new WebClient();
                    string imageFile = FileUtils.GetPhotoBankFolder() + Path.DirectorySeparatorChar + productId + ".jpg";
                    if (File.Exists(imageFile)) File.Delete(imageFile);
                    webClient.DownloadFile(src, imageFile);
                    item.Image = imageFile;
                    webClient.Dispose();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Trace.WriteLine(e.InnerException.Message);
                    item.Image = "";
                }

                HtmlNode aLinkNode = percentNode.SelectSingleNode(SUBJECT_PATH);
                item.Name = aLinkNode.InnerText;
                item.Href = aLinkNode.Attributes["href"].Value;
                HtmlDocument document1 = new HtmlWeb().Load(item.Href);
                string ProductPageHtml = document1.DocumentNode.InnerHtml;
                string jsKwString = Regex.Match(ProductPageHtml, KeywordExpressions, RegexOptions.IgnoreCase).Groups[1].Value;
                if (!string.IsNullOrEmpty(jsKwString))
                {
                    item.Key = jsKwString.Replace(",", ",\n");
                 }
                string category = document1.DocumentNode.SelectSingleNode("//div[@class='crumb global']").InnerText;
                item.Category = category.Replace("\t", "").Replace("\n", "").Replace("&gt;", ">").Replace("  ", "").Replace("&amp;","&");
                string descrption = document1.DocumentNode.SelectSingleNode("//div[@id='richTextDescription']").PreviousSibling.PreviousSibling.InnerHtml;
                item.Desc = descrption.Replace("<br>", "");
                ProductPageHtml = null;
                document1 = null;
                FinishedEvent(item, "Rank search finished.");
            }
           
        }
    }
}
