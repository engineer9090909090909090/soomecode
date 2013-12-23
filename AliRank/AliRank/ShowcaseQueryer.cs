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
        private static string KeywordExpressions = "JSRUNDATA.productDetail.data.keywords = '(.*?)';";
        private static string PIDExpressions = ",pid:'(.*?)',sid:";
        private List<ShowcaseRankInfo> showCaseProducts = new List<ShowcaseRankInfo>();
        private int iCount = 0;
        private int MaxCount = 10;
        private ManualResetEvent eventX = new ManualResetEvent(false);
        public ShowcaseQueryer() { }

        public List<ShowcaseRankInfo> Seacher(string companyUrl)
        {
            HtmlWeb clinet = new HtmlWeb();
            HtmlDocument document = clinet.Load(companyUrl);
            HtmlNodeCollection nodes = document.DocumentNode.SelectNodes("//div[@id='products-show-normal']/ul/li/div[@class='products-small-info']");

            if (nodes != null)
            {
                foreach (HtmlNode node in nodes)
                {
                    ShowcaseRankInfo item = new ShowcaseRankInfo();
                    item.CompanyUrl = companyUrl;
                    HtmlNode linkNode = node.SelectSingleNode("div[@class='product-info']/a");
                    item.ProductName = linkNode.InnerText;
                    item.ProductUrl = linkNode.Attributes["href"].Value;
                    HtmlNode imgNode = node.SelectSingleNode("div[@class='products-img savm']/a/img");
                    item.Image = imgNode.Attributes["data-src"].Value;
                    showCaseProducts.Add(item);
                }
            }
            MaxCount = showCaseProducts.Count;
            if (MaxCount == 0)
            {
                return showCaseProducts;
            }
            ThreadPool.SetMinThreads(3, 40);
            ThreadPool.SetMaxThreads(6, 200);
            for (int i = 0; i < MaxCount; i++)
            {
                 
                 ThreadPool.QueueUserWorkItem(new WaitCallback(DoWork), (object)i);
            }

            eventX.WaitOne(Timeout.Infinite, true); //WaitOne()方法使调用它的线程等待直到eventX.Set()方法被调用

            WebClient webClient = new WebClient();
            foreach (ShowcaseRankInfo productItem in showCaseProducts)
            {
                try
                {
                    string imageFile = FileUtils.GetImageFolder() + Path.DirectorySeparatorChar + productItem.ProductId + ".jpg";
                    if (File.Exists(imageFile)) File.Delete(imageFile);
                    webClient.DownloadFile(productItem.Image, imageFile);
                    productItem.ProductImg = imageFile;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Trace.WriteLine(e.InnerException.Message);
                    productItem.ProductImg = "";
                }
            }
            webClient.Dispose();
            Console.WriteLine("线程池结束！");
            return showCaseProducts;
        }


        

        private void DoWork(object obj)
        {
            int index = (int)obj;
            ShowcaseRankInfo productItem = showCaseProducts[index];
            string url = productItem.CompanyUrl + productItem.ProductUrl;
            string ProductPageHtml = HtmlUtils.getContent(url);
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(ProductPageHtml);
            HtmlNodeCollection nodes = document.DocumentNode.SelectNodes("//div[@class='related-keywords-box clearfix']/ul/li/a");
            string jsKwString = string.Empty;
            if (nodes != null)
            {
                jsKwString = nodes[0].InnerText;
                if (nodes[1] != null)
                {
                    jsKwString = jsKwString + "," + nodes[1].InnerText;
                }
                if (nodes[2] != null)
                {
                    jsKwString = jsKwString + "," + nodes[2].InnerText;
                }
            }
            if (!string.IsNullOrEmpty(jsKwString))
            {
                productItem.MainKey = jsKwString;
                System.Diagnostics.Trace.WriteLine(url + " = " + jsKwString);
            }
            string pidString = Regex.Match(ProductPageHtml, PIDExpressions, RegexOptions.IgnoreCase).Groups[1].Value;
            if (!string.IsNullOrEmpty(pidString))
            {
                productItem.ProductId = Convert.ToInt32(pidString);
            }
            

            ProductPageHtml = null;
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
