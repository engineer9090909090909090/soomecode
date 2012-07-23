using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace com.soomes.ali
{
    class Searcher
    {

        private static string BASE_URL = "http://www.alibaba.com/trade/search?SearchText={0}&Country=&CatId=0&IndexArea=product_en&fsb=y";

        public string searchUrl = "http://www.alibaba.com/trade/search?SearchText=usb+flash+drive&Country=&CatId=0&IndexArea=product_en&fsb=y";
        public string againSearchUrl = "http://www.alibaba.com/products/{0}/{1}.html";

        public string productUrlPrefix = "http://www.alibaba.com/product-gs/";
        public string againSearchKey = "usb_flash_drive";
        public string productId = "305893225";
        public string keyword = "usb flash drive";
        public string companyName = "";
        private WebBrowser webBrowser;
        private ToolStripStatusLabel statusLabel;
        private Boolean searchFinished;
        private Boolean clickedSuccess;
        private Boolean isStop;

        public Searcher(Form1 f, string keyword, string productId, string companyName)
        {
            this.webBrowser = f.webBrowser1;
            this.statusLabel = f.statusLabel;
            this.keyword = keyword;
            this.companyName = companyName;
            string searchKey = keyword.Replace(" ", "+");
            this.againSearchKey = keyword.Replace(" ", "_");
            String[] testArgs = new String[] { searchKey };
            this.searchUrl = string.Format(BASE_URL, testArgs);
            this.productId = productId;
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
            URLSecurityZoneAPI.InternetSetFeatureEnabled(URLSecurityZoneAPI.InternetFeaturelist.DISABLE_NAVIGATION_SOUNDS, URLSecurityZoneAPI.SetFeatureOn.PROCESS, false);
		    int pageNum = 1;
		    String url = searchUrl;
            this.PrintStautsLabel(url);
            this.webBrowser.Navigate(url);
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            WaitFinished();

            if (this.clickedSuccess)
            {
                this.statusLabel.Text = "You company product[" + this.productId + "] in " + pageNum + " Page.";
                Thread.Sleep(2000);
                return new int[] { 1, pageNum };
            }

            for (int i = 2; i < 30; i++)
            {
                if (this.isStop)
                {
                    return new int[] { 0, 0 };
                }
                pageNum = i;
                String[] testArgs = new String[] { againSearchKey, "" + pageNum };
                url = string.Format(againSearchUrl, testArgs);
                this.PrintStautsLabel(url);
                this.searchFinished = false;
                this.webBrowser.Navigate(url);
                this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
                WaitFinished();

                if (this.clickedSuccess)
                {
                    this.statusLabel.Text = "You company product[" + this.productId + "] in " + pageNum + " Page.";
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
            if (browser.ReadyState != System.Windows.Forms.WebBrowserReadyState.Complete)
                return;
            if (e.Url.ToString() != browser.Url.ToString())
                return;

            if (!browser.Url.ToString().StartsWith(productUrlPrefix))
            {
                HtmlElement productLink = browser.Document.GetElementById("lsubject_" + this.productId.Trim() + "");
                HtmlElementCollection alllinks = browser.Document.Links;
                if (productLink != null)
                {
                    productLink.SetAttribute("target", "_self");
                    browser.Document.InvokeScript("onProductClick('" + this.productId + "');");
                    productLink.InvokeMember("click");
                    this.searchFinished = false;
                }
                else
                {
                    this.searchFinished = true;
                    this.webBrowser.DocumentCompleted -= new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
                }
                
            }
            if (browser.Url.ToString().StartsWith(productUrlPrefix))
            {
                this.clickedSuccess = true;
                this.searchFinished = true;
                this.webBrowser.DocumentCompleted -= new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            }
           
        }

    }
}
