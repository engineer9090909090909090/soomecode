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


        private string SEARCH_URL = "http://www.alibaba.com/products/F0/{0}/----------------------50/{1}.html";
        private string PURL_PREFIX = "http://www.alibaba.com/product-gs/";
        int currentPage = 1;
        private Keywords item;
        private string currentRequestUrl;
        public ProductClicker(WebBrowser b) 
        {
            browser = b;
        }


        public virtual void ClickedEvent(Keywords kw, string msg)
        {
            if (OnRankClickEndEvent != null)
            {
                RankEventArgs e = new RankEventArgs(kw, msg);
                OnRankClickEndEvent(this, e);
            }
        }
        public virtual void ClickingEvent(Keywords kw, string msg)
        {
            if (OnRankClickingEvent != null)
            {
                RankEventArgs e = new RankEventArgs(kw, msg);
                OnRankClickingEvent(this, e);
            }
        }

        public void DoClick(Keywords kw)
        {
            item = kw;
            if (item.Rank == 0)
            {
                string productUrl = item.ProductUrl.Substring(item.ProductUrl.LastIndexOf("/"));
                currentRequestUrl = PURL_PREFIX + item.ProductId +  productUrl;
            }
            else
            {
                currentPage = item.Rank % 50 == 0 ? item.Rank / 50 : item.Rank / 50 + 1;
                if (currentPage > 1)
                {
                    currentPage = currentPage - 1;
                }
                string mainKey = item.MainKey.Replace(" ", "_");
                currentRequestUrl = string.Format(SEARCH_URL, mainKey, (currentPage));
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
                HtmlElement productLink = browser.Document.GetElementById("lsubject_" + this.item.ProductId);
                if (productLink != null)
                {
                    productLink.SetAttribute("target", "_self");
                    productLink.InvokeMember("click");
                }
                else 
                {
                    if (currentPage > 10)
                    {
                        ClickedEvent(item, "The system does not find the product you need to click.");
                        browser.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);
                        eventX.Set();
                    }
                    currentPage++;
                    string mainKey = item.MainKey.Replace(" ", "_");
                    currentRequestUrl = string.Format(SEARCH_URL, mainKey, (currentPage));
                    ClickingEvent(item, "Clicking " + currentRequestUrl);
                    browser.Navigate(currentRequestUrl);
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
