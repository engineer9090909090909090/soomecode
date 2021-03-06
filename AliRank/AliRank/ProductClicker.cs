﻿using System;
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
        public event RankInquiryEndEvent OnInquiryEndEvent;
        private WebBrowser browser;
        private ManualResetEvent eventX = new ManualResetEvent(false);

        private string SEARCH_URL1 = @"http://www.alibaba.com/trade/search?fsb=y&IndexArea=product_en&CatId=&SearchText={0}";
        private string SEARCH_URL2 = @"http://www.alibaba.com/products/F0/{0}/{1}.html";
        private string PURL_PREFIX = @"http://www.alibaba.com/product-gs/";

        private string LOGOUT_URL = @"http://us.my.alibaba.com/user/sign/sign_out.htm";
        private string SEND_MESSAGE = @"http://message.alibaba.com/msgsend/contact.htm";

        //http://us.message.alibaba.com/msgsend/memberInquirySuccess.htm
        //http://cn.message.alibaba.com/msgsend/memberInquirySuccess.htm
        private string INQUIRY_SUCCESS = "msgsend/memberInquirySuccess.htm";
        private string additionalHeaders = Constants.UserAgent;

        private int currentPage = 1;
        private int maxQueryPage = 10;
        private ShowcaseRankInfo item;
        private AliAccounts aliAccount;
        private InquiryMessages inquiryMessage;
        private bool canInquiry;
        private string clickKey = string.Empty;
        private string searchProductUrl;
        private string detailProductUrl;
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

        public virtual void InquiryEndEvent(InquiryInfos kw, string msg)
        {
            if (OnInquiryEndEvent != null)
            {
                InquiryEventArgs e = new InquiryEventArgs(kw, msg);
                OnInquiryEndEvent(this, e);
            }
        }

        public void Stop()
        {
            browser.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);
            browser.Stop();
            browser.Navigate("about:blank");
            eventX.Set();
        }

        public void Click(ShowcaseRankInfo kw, int maxQueryPageNumber, AliAccounts account, bool canInquiry, InquiryMessages msg)
        {
            this.item = kw;
            this.currentPage = 1;
            this.aliAccount = account;
            this.inquiryMessage = msg;
            this.canInquiry = canInquiry;
            this.maxQueryPage = maxQueryPageNumber;
            this.eventX = new ManualResetEvent(false);
            if (this.aliAccount == null)
            {
                this.canInquiry = false;
            }
            this.clickKey = item.RankKeyword;
            searchProductUrl = string.Format(SEARCH_URL1, clickKey.Replace(" ", "+"));
            ClickingEvent(item, @"Clicking " + searchProductUrl);
            browser.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);
            browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);
            IEHandleUtils.Navigate(browser, searchProductUrl, null, additionalHeaders);
            //browser.Navigate(currentRequestUrl, "_self", null, additionalHeaders);
            eventX.WaitOne(Timeout.Infinite, true);
            item = null;
            aliAccount = null;
            inquiryMessage = null;
            Console.WriteLine("线程池结束！");
        }

        void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            browser = (WebBrowser)sender;
            if (browser.ReadyState != System.Windows.Forms.WebBrowserReadyState.Complete)
                return;
            if (e.Url.ToString() != browser.Url.ToString())
                return;
            if (browser.Url.ToString() == searchProductUrl)
            {

                detailProductUrl = null;
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(browser.DocumentText);
                HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(RankQueryer.PRODUCT_LINK_PATH);
                for (int k = 0; k < nodes.Count; k++)
                {
                    HtmlNode aLinkNode = nodes[k];
                    string proId = aLinkNode.Attributes["data-pid"].Value;
                    int rankProductId = Convert.ToInt32(proId);
                    if (rankProductId == item.ProductId)
                    {
                        detailProductUrl = aLinkNode.Attributes["href"].Value;
                        break;
                    }
                }

                if (detailProductUrl != null && detailProductUrl != string.Empty)
                {
                    IEHandleUtils.Navigate(browser, detailProductUrl, null, additionalHeaders);
                }
                else
                {
                    if (currentPage == maxQueryPage)
                    {
                        this.canInquiry = false;
                        ClickedEvent(item, "The system does not find the product you need to click.");
                        string productUrl = item.ProductUrl.Substring(item.ProductUrl.LastIndexOf("/"));
                        detailProductUrl = PURL_PREFIX + item.ProductId + productUrl;
                        ClickingEvent(item, "Enforce clicking " + detailProductUrl);
                        browser.Document.InvokeScript("onProductClick", new string[] { item.ProductId.ToString() });
                        IEHandleUtils.Navigate(browser, detailProductUrl, null, additionalHeaders);
                    }
                    else
                    {
                        currentPage++;
                        searchProductUrl = string.Format(SEARCH_URL2, clickKey.Replace(" ", "_"), currentPage);
                        Thread.Sleep(new Random().Next(1000, 10000));
                        ClickingEvent(item, @"Clicking " + searchProductUrl);
                        IEHandleUtils.Navigate(browser, searchProductUrl, null, additionalHeaders);
                    }
                }
            }
            if (detailProductUrl != null && browser.Url.ToString() == detailProductUrl)
            {
                item.Clicked = item.Clicked + 1;
                ClickedEvent(item, "This product has been successfully click.");
                if (this.canInquiry)
                {
                    string messageUrl = "http://message.alibaba.com/msgsend/contact.htm?action=contact_action&domain=1&id="+ this.item.ProductId +"&tracelog=tracedetailfeedback";
                    IEHandleUtils.Navigate(browser, messageUrl, null, additionalHeaders);

                }
                else
                {
                    browser.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);
                    eventX.Set();
                }
            }
            if (browser.Url.ToString().StartsWith(SEND_MESSAGE))
            {
                string postUrl = "http://message.alibaba.com/msgsend/inquiry.htm";
                //string html = browser.Document.Body.InnerHtml;
                string html = browser.DocumentText;
                string msgContent = inquiryMessage.Content + "\r\n." + DateTime.Now.ToString("yyyyMMddHHmmss");
                int randomNumber = new Random().Next(1, 20) * 100;
                string token = "_csrf_token_=" + browser.Document.All["_csrf_token_"].GetAttribute("value");
                string action = "action=SendMemberInquiryAction";
                string sh = "_fmm.b._0.sh=false";
                string pageId = "pageId=" + GetDmtrackPageid(html);
                string chkProductIds = "chkProductIds="+ browser.Document.All["chkProductIds"].GetAttribute("value");
                string s = "_fmm.b._0.s=" + browser.Document.GetElementById("subject").GetAttribute("value");
                string c = "_fmm.b._0.c=<p>" + msgContent.Replace("\r\n", "<br>") + "<p>";
                string o = "_fmm.a._0.o=" + randomNumber + " Piece/Pieces";
                string attachs = "attachs=";
                string eventSubmitDoSend = "eventSubmitDoSend=Send";
                string postString = token + "&" + action + "&" + sh + "&" + pageId + "&" + chkProductIds + "&" + s + "&"
                       + c + "&" + o + "&" + attachs + "&" + eventSubmitDoSend;
                string headers = additionalHeaders + Environment.NewLine + 
                       "Content-Type: application/x-www-form-urlencoded" + Environment.NewLine;
                IEHandleUtils.Navigate(browser, postUrl, postString, headers);
            }
            if (browser.Url.ToString().IndexOf(INQUIRY_SUCCESS) >= 0)
            {
                InquiryInfos info = new InquiryInfos();
                info.ProductId = item.ProductId;
                info.Account = aliAccount.Account;
                info.MsgId = inquiryMessage.MsgId;
                info.InquiryDate = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
                info.Company = item.CompanyUrl;
                info.InquiryIp = aliAccount.LoginIp;
                InquiryEndEvent(info, "This product has been send a Rank Inquiry.");
                browser.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);
                IEHandleUtils.Navigate(browser, LOGOUT_URL, null, additionalHeaders);
                eventX.Set();
            }
        }

        private string GetDmtrackPageid(string html)
        {
            Regex r = new Regex("var dmtrack_pageid='(.*?)';");
            GroupCollection gc = r.Match(html).Groups;
            if (gc != null && gc.Count > 1)
            {
                return gc[1].Value.Trim();
            }
            return "";
        }
       
    }
}
