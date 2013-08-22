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
            BuyerInfo buyer = GetBuyerInfo(url, document);
            


        }

        public BuyerInfo GetBuyerInfo(string url, HtmlDocument document)
        {
            BuyerInfo buyerInfo = new BuyerInfo();
            if (url.IndexOf("/company/") > 0)
            {
                if (url.StartsWith("http://buyer.waimaoba.com"))
                {
                    HtmlNodeCollection fieldsetNodes = document.DocumentNode.SelectNodes("//fieldset[@class='fieldgroup group-information']/table/tbody/tr/td[2]/div");
                    if (fieldsetNodes.Count > 1)
                    {
                        string companyInfo = GetHtmlNodeText(fieldsetNodes[0]);
                        Console.WriteLine("companyInfo==========================================\r\n" + companyInfo);
                        buyerInfo.CompanyInfo = companyInfo;

                        HtmlNode node1 = fieldsetNodes[0].SelectSingleNode("div[2]");//公司
                        string companyName = GetHtmlNodeText(node1);
                        Console.WriteLine("companyName==========================================\r\n" + companyName);
                        buyerInfo.CompanyName = companyName;

                        string contactInfo = GetHtmlNodeText(fieldsetNodes[1]);//Contact Method 
                        Console.WriteLine("contactInfo==========================================\r\n" + contactInfo);
                        buyerInfo.ContactInfo = contactInfo;

                        List<string> emails = HtmlUtils.getEmails(contactInfo);//Emails

                        node1 = fieldsetNodes[1].SelectSingleNode("div[2]");//Contact person
                        string person = GetHtmlNodeText(node1);
                        Console.WriteLine("person==========================================\r\n" + person);
                        buyerInfo.BuyerName = person;

                        node1 = fieldsetNodes[1].SelectSingleNode("div[12]");//Email Address 
                        string email = GetHtmlNodeText(node1);
                        Console.WriteLine("email==========================================\r\n" + email);
                        buyerInfo.Email = email;
                    }
                }
                else
                {
                    HtmlNodeCollection fieldsetNodes = document.DocumentNode.SelectNodes("//fieldset[@class='fieldgroup group-information']/table/tbody/tr/td[2]");
                    if (fieldsetNodes.Count > 1)
                    {
                        string companyInfo = GetHtmlNodeText(fieldsetNodes[0]);
                        Console.WriteLine("companyInfo==========================================\r\n" + companyInfo);
                        buyerInfo.CompanyInfo = companyInfo;

                        HtmlNode node1 = fieldsetNodes[0].SelectSingleNode("div/div[2]");//公司
                        string companyName = GetHtmlNodeText(node1);
                        Console.WriteLine("companyName==========================================\r\n" + companyName);
                        buyerInfo.CompanyName = companyName;

                        node1 = fieldsetNodes[1].SelectSingleNode("div/div[2]");//Contact Method 
                        string contactInfo = GetHtmlNodeText(node1);
                        Console.WriteLine("contactInfo==========================================\r\n" + contactInfo);
                        buyerInfo.ContactInfo = contactInfo;

                        Regex r = new Regex("Contact Person:(.*?)\\n");
                        GroupCollection gc = r.Match(contactInfo).Groups;
                        if (gc != null && gc.Count > 1)
                        {
                            string person = gc[1].Value.Trim();
                            Console.WriteLine("person==========================================\r\n" + person);
                            buyerInfo.BuyerName = person;
                        }
                        List<string> emails = HtmlUtils.getEmails(contactInfo);
                        node1 = fieldsetNodes[1].SelectSingleNode("div[2]/div[2]");//Email Address 
                        string email = GetHtmlNodeText(node1);
                        Console.WriteLine("email==========================================\r\n" + email);
                        buyerInfo.Email = email;
                    }
                }
            }

            if (url.IndexOf("/inquiry/") > 0)
            {

            }
            return buyerInfo;
        }

        public static string GetHtmlNodeText(HtmlNode node)
        {
            if (node == null)
            {
                return string.Empty;
            }
            string text = HttpUtility.HtmlDecode(node.InnerText).Trim();
            text = text.Replace("  ","").Replace("\t","");
            
            return text;
        }
    }
}
