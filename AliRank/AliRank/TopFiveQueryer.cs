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
    

    class TopFiveQueryer
    {
        public event TopFiveSearchEndEvent OnTopFiveSearchEndEvent;
        public event TopFiveSearchingEvent OnTopFiveSearchingEvent;
        private string KeywordExpressions = "var kw = encodeURIComponent\\(\\\"(.*?)\\\"\\);";
        private string SEARCH_URL = "http://www.alibaba.com/products/F0/{0}/----------------------38/{1}.html";
        private string IMAGE_PATH = "div[@class='pic']/a/img";
        private string SUBJECT_PATH = "div[@class='percent-wrap']/div[@class='attr']/h2/a[@class='qrPTitle']";
        private string PATH = "//div[@class='list-items']";
        private string AD_PATH = "//div[@class='list-items AD']";
        private string P4P_PATH = "//div[@class='list-items p4p']";


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
            
            string mainKey = key.Replace(" ", "_");
            HtmlDocument document = null;
            int p4pNum = 0;
            int adaNum = 0;
            string url = string.Format(SEARCH_URL, mainKey, 1);
            HtmlWeb clinet = new HtmlWeb();
            document = clinet.Load(url);
            HtmlNodeCollection itemNodes = document.DocumentNode.SelectNodes(PATH);
            HtmlNodeCollection adNodes = document.DocumentNode.SelectNodes(AD_PATH);
            HtmlNodeCollection p4pNodes = document.DocumentNode.SelectNodes(P4P_PATH);
            if (adNodes != null)
            {
                adaNum = adNodes.Count;
            }
            if (p4pNodes != null)
            {
                p4pNum = p4pNodes.Count;
            }
            int itemCount = itemNodes.Count;
            int kcount = itemCount >= 8 ? 8 : itemCount;
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
                    string imageFile = FileUtils.GetImageFolder() + Path.DirectorySeparatorChar + productId + ".jpg";
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
                string href = aLinkNode.Attributes["href"].Value;
                document = clinet.Load(href);
                string ProductPageHtml = document.DocumentNode.InnerHtml;
                string jsKwString = Regex.Match(ProductPageHtml, KeywordExpressions, RegexOptions.IgnoreCase).Groups[1].Value;
                if (!string.IsNullOrEmpty(jsKwString))
                {
                    string[] keywords = jsKwString.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
                    item.Key1 = keywords[0];
                    item.Key2 = keywords[1];
                    item.Key3 = keywords[2];
                 }
                string descrption = document.DocumentNode.SelectSingleNode("//div[@id='richTextDescription']").PreviousSibling.PreviousSibling.InnerHtml;
                item.Desc = descrption.Replace("<br>", "");
                ProductPageHtml = null;
                FinishedEvent(item, "Rank search finished.");
            }
           
        }
    }
}
