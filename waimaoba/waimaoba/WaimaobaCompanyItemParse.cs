using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Web;
using System.Text.RegularExpressions;

namespace com.soomes
{
    class WaimaobaCompanyItemParse : WebParse
    {
        private Log loger;

        public WaimaobaCompanyItemParse()
        {
            this.loger = new Log(@".\log\", LogType.Daily);
        }

        public void Parse(string url, HtmlDocument document)
        {
            if (url.IndexOf(".waimaoba.com") == -1)
            {
                return;
            }
            if ((url.IndexOf("/company/") == -1 && url.IndexOf("/inquiry/") == -1 && url.IndexOf("/content/") == -1))
            {
                return;
            }
            BuyerInfo buyer = GetBuyerInfo(url, document);
            if (buyer.Emails.Count == 0)
            {
                loger.Write("Cannot get the email, Url = " + url, MsgType.Error);
                return;
            }
            DAOFactory.GetInstance().GetBuyerInfoDao().Insert(buyer);
        }

        public BuyerInfo GetBuyerInfo(string url, HtmlDocument document)
        {
            BuyerInfo buyerInfo = new BuyerInfo();
            buyerInfo.Url = url;
            buyerInfo.UrlTitle = HtmlUtils.GetHtmlNodeText(document.DocumentNode.SelectSingleNode("/html/head/title"));
            if (url.IndexOf("/company/") > 0)
            {
                if (url.StartsWith("http://buyer.waimaoba.com"))
                {
                    buyerInfo.Type = "Buyer";
                    HtmlNodeCollection fieldsetNodes = document.DocumentNode.SelectNodes("//fieldset[@class='fieldgroup group-information']/table/tbody/tr/td[2]/div");
                    if (fieldsetNodes !=null && fieldsetNodes.Count > 1)
                    {
                        string companyInfo = HtmlUtils.GetHtmlNodeText(fieldsetNodes[0]);
                        HtmlUtils.Log("companyInfo==========================================\r\n" + companyInfo);
                        buyerInfo.CompanyInfo = companyInfo;

                        HtmlNode node1 = fieldsetNodes[0].SelectSingleNode("div[2]");//公司
                        string companyName = HtmlUtils.GetHtmlNodeText(node1);
                        HtmlUtils.Log("companyName==========================================\r\n" + companyName);
                        buyerInfo.CompanyName = companyName;

                        string contactInfo = HtmlUtils.GetHtmlNodeText(fieldsetNodes[1]);//ContactInfo 
                        HtmlUtils.Log("contactInfo==========================================\r\n" + contactInfo);
                        buyerInfo.ContactInfo = contactInfo;

                        buyerInfo.Emails = HtmlUtils.getEmails(contactInfo);//Emails
                        Regex r = new Regex(@"Contact Person \(联系人\):(.*?)\n");
                        GroupCollection gc = r.Match(contactInfo).Groups;
                        if (gc != null && gc.Count > 1)
                        {
                            string person = gc[1].Value.Trim();
                            HtmlUtils.Log("person==========================================\r\n" + person);
                            buyerInfo.BuyerName = person;
                        }
                        if (string.IsNullOrEmpty(buyerInfo.BuyerName))
                        {
                            r = new Regex(@"Contact Person:(.*?)\n");
                            gc = r.Match(contactInfo).Groups;
                            if (gc != null && gc.Count > 1)
                            {
                                string person = gc[1].Value.Trim();
                                HtmlUtils.Log("person==========================================\r\n" + person);
                                buyerInfo.BuyerName = person;
                            }
                        }
                    }
                    HtmlNode categoryNode = document.DocumentNode.SelectSingleNode("//fieldset[@class='fieldgroup group-industry']/table/tbody/tr/td/div/div[2]");
                    string categoryInfo = HtmlUtils.GetHtmlNodeText(categoryNode);
                    buyerInfo.Category = categoryInfo;
                }
                else
                {
                    buyerInfo.Type = "Company";
                    HtmlNodeCollection fieldsetNodes = document.DocumentNode.SelectNodes("//fieldset[@class='fieldgroup group-information']/table/tbody/tr/td[2]");
                    if (fieldsetNodes != null && fieldsetNodes.Count > 1)
                    {
                        string companyInfo = HtmlUtils.GetHtmlNodeText(fieldsetNodes[0]);
                        HtmlUtils.Log("companyInfo==========================================\r\n" + companyInfo);
                        buyerInfo.CompanyInfo = companyInfo;

                        HtmlNode node1 = fieldsetNodes[0].SelectSingleNode("div/div[2]");//公司
                        string companyName = HtmlUtils.GetHtmlNodeText(node1);
                        HtmlUtils.Log("companyName==========================================\r\n" + companyName);
                        buyerInfo.CompanyName = companyName;

                        node1 = fieldsetNodes[1].SelectSingleNode("div/div[2]");//Contact Method 
                        string contactInfo = HtmlUtils.GetHtmlNodeText(node1);
                        HtmlUtils.Log("contactInfo==========================================\r\n" + contactInfo);
                        buyerInfo.ContactInfo = contactInfo;

                        Regex r = new Regex(@"Contact Person:(.*?)\n");
                        GroupCollection gc = r.Match(contactInfo).Groups;
                        if (gc != null && gc.Count > 1)
                        {
                            string person = gc[1].Value.Trim();
                            HtmlUtils.Log("person==========================================\r\n" + person);
                            buyerInfo.BuyerName = person;
                        }
                        buyerInfo.Emails = HtmlUtils.getEmails(contactInfo);
                    }
                    HtmlNode categoryNode = document.DocumentNode.SelectSingleNode("//fieldset[@class='fieldgroup group-industry']/table/tbody/tr/td/div/div/div[2]");
                    string categoryInfo = HtmlUtils.GetHtmlNodeText(categoryNode);
                    buyerInfo.Category = categoryInfo;
                }
            }

            if (url.IndexOf("/inquiry/") > 0 || url.IndexOf("/content/") > 0 )
            {
                buyerInfo.Type = "Inquiry";
                HtmlNodeCollection fieldsetNodes = document.DocumentNode.SelectNodes("//fieldset[@class='fieldgroup group-information']/table/tbody/tr/td[2]");
                if (fieldsetNodes != null && fieldsetNodes.Count > 1)
                {
                    string companyInfo = HtmlUtils.GetHtmlNodeText(fieldsetNodes[0]);
                    HtmlUtils.Log("companyInfo==========================================\r\n" + companyInfo);
                    buyerInfo.CompanyInfo = companyInfo;

                    HtmlNode node1 = fieldsetNodes[1].SelectSingleNode("div/div[2]");//Contact Method 
                    string contactInfo = HtmlUtils.GetHtmlNodeText(node1);
                    HtmlUtils.Log("contactInfo==========================================\r\n" + contactInfo);
                    buyerInfo.ContactInfo = contactInfo;

                    Regex r = new Regex(@"Contact Person:(.*?)\n");
                    GroupCollection gc = r.Match(contactInfo).Groups;
                    if (gc != null && gc.Count > 1)
                    {
                        string person = gc[1].Value.Trim();
                        HtmlUtils.Log("person==========================================\r\n" + person);
                        buyerInfo.BuyerName = person;
                    }
                    r = new Regex(@"Company:(.*?)\n");
                    gc = r.Match(contactInfo).Groups;
                    if (gc != null && gc.Count > 1)
                    {
                        string company = gc[1].Value.Trim();
                        HtmlUtils.Log("Company:==========================================\r\n" + company);
                        buyerInfo.CompanyName = company;
                    }
                    if (string.IsNullOrEmpty(buyerInfo.CompanyName))
                    {
                        r = new Regex(@"Co:(.*?)\n");
                        gc = r.Match(contactInfo).Groups;
                        if (gc != null && gc.Count > 1)
                        {
                            string company = gc[1].Value.Trim();
                            HtmlUtils.Log("Company:==========================================\r\n" + company);
                            buyerInfo.CompanyName = company;
                        }
                    }
                    buyerInfo.Emails = HtmlUtils.getEmails(contactInfo);
                }
                HtmlNode categoryNode = document.DocumentNode.SelectSingleNode("//fieldset[@class='fieldgroup group-industry']/table/tbody/tr/td/div/div[2]");
                string categoryInfo = HtmlUtils.GetHtmlNodeText(categoryNode);
                buyerInfo.Category = categoryInfo;

            }
            return buyerInfo;
        }

        public void Dispose()
        {
            this.loger.Dispose();
        }
    }
}
