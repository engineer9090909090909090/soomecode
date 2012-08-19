using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace AliRank
{
    

    class ProductClicker
    {
        public event RankClickEndEvent OnRankClickEndEvent;
        public event RankClickingEvent OnRankClickingEvent;
        private WebBrowser browser;
        ManualResetEvent eventX = new ManualResetEvent(false);

        private string SEARCH_URL1 = "http://www.alibaba.com/trade/search?SearchText={0}&IndexArea=product_en&fsb=y";
        private string SEARCH_URL2 = "http://www.alibaba.com/products/F0/{0}/{1}.html";
        private string PURL_PREFIX = "http://www.alibaba.com/product-gs/";
        int currentPage = 1;
        private ShowcaseRankInfo item;
        private string currentRequestUrl;
        public ProductClicker(WebBrowser b) 
        {
            browser = b;
        }


        public virtual void ClickedEvent(ShowcaseRankInfo kw, string msg)
        {
            if (OnRankClickEndEvent != null)
            {
                RankEventArgs e = new RankEventArgs(kw, msg);
                OnRankClickEndEvent(this, e);
            }
        }
        public virtual void ClickingEvent(ShowcaseRankInfo kw, string msg)
        {
            if (OnRankClickingEvent != null)
            {
                RankEventArgs e = new RankEventArgs(kw, msg);
                OnRankClickingEvent(this, e);
            }
        }

        public void DoClick(ShowcaseRankInfo kw)
        {
            item = kw;
            if (item.Rank == 0)
            {

                string mainKey = item.MainKey.Split(',')[0].Replace(" ", "+");
                currentRequestUrl = string.Format(SEARCH_URL1, mainKey);
            }
            else
            {
                currentPage = item.Rank % 38 == 0 ? item.Rank / 38 : item.Rank / 38 + 1;
                if (currentPage > 1)
                {
                    currentPage = currentPage - 1;
                }
                if (currentPage == 1)
                {
                    string mainKey = item.MainKey.Split(',')[0].Replace(" ", "+");
                    currentRequestUrl = string.Format(SEARCH_URL1, mainKey);
                }
                else {
                    string mainKey = item.MainKey.Split(',')[0].Replace(" ", "_");
                    currentRequestUrl = string.Format(SEARCH_URL2, mainKey, currentPage);
                }
                
            }

            ClickingEvent(item, "Clicking " + currentRequestUrl);
            browser.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);
            browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);
            browser.Navigate(currentRequestUrl);

            eventX.WaitOne(Timeout.Infinite, true);
            Console.WriteLine("线程池结束！");
        }

        void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = (WebBrowser)sender;
            if (browser.ReadyState != System.Windows.Forms.WebBrowserReadyState.Complete)
                return;
            if (e.Url.ToString() != browser.Url.ToString())
                return;
            if (browser.Url.ToString() == currentRequestUrl)
            {
                if (item.Rank == 0)
                {
                    string productUrl = item.ProductUrl.Substring(item.ProductUrl.LastIndexOf("/"));
                    currentRequestUrl = PURL_PREFIX + item.ProductId + productUrl;
                    ClickingEvent(item, "Clicking " + currentRequestUrl);
                    browser.Document.InvokeScript("onProductClick", new string[] { item.ProductId });
                    browser.Navigate(currentRequestUrl);
                }
                else
                {
                    HtmlElement productLink = browser.Document.GetElementById("lsubject_" + this.item.ProductId);
                    if (productLink != null)
                    {
                        productLink.SetAttribute("target", "_self");
                        browser.Document.InvokeScript("onProductClick", new string[] { item.ProductId });
                        productLink.InvokeMember("click");
                    }
                    else
                    {
                        if (currentPage == 10)
                        {
                            ClickedEvent(item, "The system does not find the product you need to click.");
                            string productUrl = item.ProductUrl.Substring(item.ProductUrl.LastIndexOf("/"));
                            currentRequestUrl = PURL_PREFIX + item.ProductId + productUrl;
                            ClickingEvent(item, "Enforce clicking " + currentRequestUrl);
                            browser.Document.InvokeScript("onProductClick", new string[] { item.ProductId });
                            browser.Navigate(currentRequestUrl);
                        }
                        else
                        {
                            currentPage++;
                            string mainKey = item.MainKey.Split(',')[0].Replace(" ", "_");
                            currentRequestUrl = string.Format(SEARCH_URL2, mainKey, currentPage);
                            ClickingEvent(item, "Clicking " + currentRequestUrl);
                            browser.Navigate(currentRequestUrl);
                        }
                    }
                }
            }
            if (browser.Url.ToString().StartsWith(PURL_PREFIX + item.ProductId))
            {
                item.Clicked = item.Clicked + 1;
                ClickedEvent(item, "This product has been successfully click.");
                browser.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);
                eventX.Set();
            }
        }
    }
}
