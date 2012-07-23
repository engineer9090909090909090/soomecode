using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using com.soomes.model;
using log4net;

namespace com.soomes.ali
{
    public class Searcher : Object
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Searcher));
        private static string BASE_URL = "http://www.alibaba.com/trade/search?SearchText={0}&Country=&CatId=0&IndexArea=product_en&fsb=y";
        private string AgainSearchUrl = "http://www.alibaba.com/products/{0}/{1}.html";

        private string searchUrl;
        private string productUrlPrefix = "http://www.alibaba.com/product-gs/";
        private string againSearchKey = "usb_flash_drive";
        private ClickerModel clickerModel;
        private WebBrowser webBrowser;
        private ToolStripStatusLabel statusLabel;
        private Boolean searchFinished;
        private Boolean clickedSuccess;
        private Boolean isStop;

        public Searcher(Form1 f, ClickerModel clickerModel)
        {
            this.webBrowser = f.webBrowser1;
            this.statusLabel = f.statusLabel;
            this.clickerModel = clickerModel;
            string searchKey = clickerModel.KeyWord.Replace(" ", "+");
            this.againSearchKey = clickerModel.KeyWord.Replace(" ", "_");
            this.searchUrl = string.Format(BASE_URL, new String[] { searchKey });
            this.searchFinished = false;
            this.clickedSuccess = false;
            this.isStop = false;
        }

        public void Stop()
        {
            this.isStop = true;
        }

        public int[] DoSearchClick()
        {
		    int pageNum = 1;
		    string url = searchUrl;
            this.PrintStautsLabel(url);
            this.webBrowser.Navigate(url);
            this.webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            WaitFinished();

            if (this.clickedSuccess)
            {
                this.statusLabel.Text = "You company product[" + this.clickerModel.ProductId + "] in " + pageNum + " Page.";
                Thread.Sleep(2000);
                return new int[] { 1, pageNum };
            }

            for (int i = 2; i <= 10; i++)
            {
                if (this.isStop)
                {
                    return new int[] { 0, 0 };
                }
                pageNum = i;
                url = string.Format(AgainSearchUrl, new String[] { againSearchKey, "" + pageNum });
                this.PrintStautsLabel(url);
                this.searchFinished = false;
                this.webBrowser.Navigate(url);
                this.webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
                WaitFinished();

                if (this.clickedSuccess)
                {
                    this.statusLabel.Text = "You company product[" + this.clickerModel.ProductId + "] in " + pageNum + " Page.";
                    Thread.Sleep(2000);
                    return new int[] { 1, pageNum };
                }
            }
            return new int[] { 0, 0 };
	    }

        public void WaitFinished()
        {
            Thread.Sleep(2000);
            if(this.isStop || this.searchFinished)
            {
                return;
            }
            else
            {
                WaitFinished();            
            }
        }

        private void PrintStautsLabel(string url)
	    {
            this.statusLabel.Text =  "Search to " + url;
	    }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = (WebBrowser)sender;
            if (browser.ReadyState != WebBrowserReadyState.Complete)
                return;
            if (e.Url.ToString() != browser.Url.ToString())
                return;

            if (!browser.Url.ToString().StartsWith(productUrlPrefix))
            {
                HtmlElement productLink = browser.Document.GetElementById("lsubject_" + this.clickerModel.ProductId.Trim() + "");
                HtmlElementCollection alllinks = browser.Document.Links;
                if (productLink != null)
                {
                    productLink.SetAttribute("target", "_self");
                    browser.Document.InvokeScript("onProductClick('" + this.clickerModel.ProductId + "');");
                    productLink.InvokeMember("click");
                    this.searchFinished = false;
                }
                else
                {
                    this.searchFinished = true;
                    this.webBrowser.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
                }
                
            }
            if (browser.Url.ToString().StartsWith(productUrlPrefix))
            {
                this.clickedSuccess = true;
                this.searchFinished = true;
                this.webBrowser.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            }
        }

    }
}
