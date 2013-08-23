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
        public void Parse(string url, HtmlDocument document)
        {
            if (url.IndexOf(".waimaoba.com") == -1)
            {
                return;
            }
            BuyerInfo buyer = GetBuyerInfo(url, document);
            if (string.IsNullOrEmpty(buyer.Email))
            {
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

                        List<string> emails = HtmlUtils.getEmails(contactInfo);//Emails

                        node1 = fieldsetNodes[1].SelectSingleNode("div[2]");//Contact person
                        string person = HtmlUtils.GetHtmlNodeText(node1);
                        HtmlUtils.Log("person==========================================\r\n" + person);
                        buyerInfo.BuyerName = person;

                        node1 = fieldsetNodes[1].SelectSingleNode("div[12]");//Email Address 
                        string email = HtmlUtils.GetHtmlNodeText(node1);
                        HtmlUtils.Log("email==========================================\r\n" + email);
                        buyerInfo.Email = email;
                    }
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

                        Regex r = new Regex("Contact Person:(.*?)\\n");
                        GroupCollection gc = r.Match(contactInfo).Groups;
                        if (gc != null && gc.Count > 1)
                        {
                            string person = gc[1].Value.Trim();
                            HtmlUtils.Log("person==========================================\r\n" + person);
                            buyerInfo.BuyerName = person;
                        }
                        List<string> emails = HtmlUtils.getEmails(contactInfo);
                        node1 = fieldsetNodes[1].SelectSingleNode("div[2]/div[2]");//Email Address 
                        string email = HtmlUtils.GetHtmlNodeText(node1);
                        HtmlUtils.Log("email==========================================\r\n" + email);
                        buyerInfo.Email = email;
                    }
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

                    Regex r = new Regex("Contact Person:(.*?)\\n");
                    GroupCollection gc = r.Match(contactInfo).Groups;
                    if (gc != null && gc.Count > 1)
                    {
                        string person = gc[1].Value.Trim();
                        HtmlUtils.Log("person==========================================\r\n" + person);
                        buyerInfo.BuyerName = person;
                    }
                    r = new Regex("Company:(.*?)\\n");
                    gc = r.Match(contactInfo).Groups;
                    if (gc != null && gc.Count > 1)
                    {
                        string company = gc[1].Value.Trim();
                        HtmlUtils.Log("Company:==========================================\r\n" + company);
                        buyerInfo.CompanyName = company;
                    }
                    if (string.IsNullOrEmpty(buyerInfo.CompanyName))
                    {
                        r = new Regex("Co:(.*?)\\n");
                        gc = r.Match(contactInfo).Groups;
                        if (gc != null && gc.Count > 1)
                        {
                            string company = gc[1].Value.Trim();
                            HtmlUtils.Log("Company:==========================================\r\n" + company);
                            buyerInfo.CompanyName = company;
                        }
                    }

                    List<string> emails = HtmlUtils.getEmails(contactInfo);
                    node1 = fieldsetNodes[1].SelectSingleNode("div[2]/div[2]");//Email Address 
                    string email = HtmlUtils.GetHtmlNodeText(node1);
                    HtmlUtils.Log("email==========================================\r\n" + email);
                    buyerInfo.Email = email;
                }

            }
            return buyerInfo;
        }


    }
}
