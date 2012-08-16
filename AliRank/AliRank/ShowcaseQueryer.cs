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
    class ShowcaseQueryer:IDisposable
    {
        private static string KeywordExpressions = "var kw = encodeURIComponent\\(\\\"(.*?)\\\"\\);";
        private List<Keywords> showCaseProducts = new List<Keywords>();
        private int iCount = 0;
        private int MaxCount = 10;
        private ManualResetEvent eventX = new ManualResetEvent(false);
        public ShowcaseQueryer() { }

        public List<Keywords> Seacher(string companyUrl)
        {
            HtmlWeb clinet = new HtmlWeb();
            HtmlDocument document = clinet.Load(companyUrl);
            HtmlNodeCollection nodes = document.DocumentNode.SelectNodes("//div[@class='featureSelectProduct']");
            
            foreach (HtmlNode node in nodes)
            {
                Keywords item = new Keywords();
                item.CompanyUrl = companyUrl;
                item.ProductId = node.Id.Replace("featureSelectProduct", "");
                HtmlNode linkNode = node.SelectSingleNode("div[@class='featureSelectProductName']/a");
                item.ProductName = linkNode.InnerText;
                item.ProductUrl = linkNode.Attributes["href"].Value;
                HtmlNode imgNode = node.SelectSingleNode("div[@class='featureSelectProductPhoto']/div/a/img");
                item.Image = imgNode.Attributes["src"].Value;
                showCaseProducts.Add(item);
            }

            MaxCount = showCaseProducts.Count;
            if (MaxCount == 0)
            {
                return showCaseProducts;
            }
            ThreadPool.SetMinThreads(4, 40);
            ThreadPool.SetMaxThreads(10, 200);
            for (int i = 0; i < MaxCount; i++)
            {
                 
                 ThreadPool.QueueUserWorkItem(new WaitCallback(DoWork), (object)i);
            }
            eventX.WaitOne(Timeout.Infinite, true); //WaitOne()方法使调用它的线程等待直到eventX.Set()方法被调用
            Console.WriteLine("线程池结束！");

            return showCaseProducts;
        }


        

        private void DoWork(object obj)
        {
            int index = (int)obj;
            Keywords productItem = showCaseProducts[index];
            string url = productItem.CompanyUrl + productItem.ProductUrl;
            string ProductPageHtml = HtmlUtils.getContent(url);
            string jsKwString = Regex.Match(ProductPageHtml, KeywordExpressions, RegexOptions.IgnoreCase).Groups[1].Value;
            if (!string.IsNullOrEmpty(jsKwString))
            {
                string mainKey = jsKwString.Split(',')[0];
                productItem.MainKey = mainKey;
                System.Diagnostics.Trace.WriteLine(url + " = " + mainKey);
            }
            ProductPageHtml = null;

            try
            {
                WebClient webClient = new WebClient();
                string imageFile = FileUtils.GetImageFolder() + Path.DirectorySeparatorChar + productItem.ProductId + ".jpg";
                if (File.Exists(imageFile)) File.Delete(imageFile);
                webClient.DownloadFile(productItem.Image, imageFile);
                productItem.ProductImg = imageFile;
                webClient.Dispose();
            }
            catch (Exception e){
                System.Diagnostics.Trace.WriteLine(e.InnerException.Message);
                productItem.ProductImg = "";
            }

            Interlocked.Increment(ref iCount);
            if (iCount == MaxCount)
            {
                eventX.Set();//设置ManualResetEvent为有信号状态，Setting eventX
            }　
        }

        public void Dispose()
        {
            showCaseProducts.Clear();
            showCaseProducts = null;
            eventX = null;
        }
    }

    
}
