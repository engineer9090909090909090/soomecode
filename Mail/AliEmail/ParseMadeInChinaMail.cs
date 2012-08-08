using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace AliEmail
{
    public class ParseMadeInChinaMail: IfParseMail
    {
        private string subject;
        private HtmlNode buyerDetail;
        private HtmlNode productInfo;
        public ParseMadeInChinaMail() { }


        public object[] Parse(string subject, string body)
        {
            this.subject = subject;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(body);
            this.buyerDetail = doc.DocumentNode.SelectSingleNode(@"//div[@id='mail_cont']/div[4]/table");
            this.productInfo = doc.DocumentNode.SelectSingleNode(@"//div[@id='mail_cont']/div[2]/table");

            object[] dataItem = new object[13];
            dataItem[0] = 0;
            dataItem[1] = GetMsgIp();
            dataItem[2] = GetOrigin();
            dataItem[3] = GetProduct();
            dataItem[4] = GetName();
            dataItem[5] = GetMail();
            dataItem[6] = GetCountry();
            dataItem[7] = GetTelephone();
            dataItem[8] = GetCompany();
            dataItem[9] = GetAddress();
            dataItem[10] = GetFax();
            dataItem[11] = string.Empty;
            dataItem[12] = string.Empty;
            return dataItem;
        }
        private string GetMail()
        {
            return getBuyerInfo("Email");
        }

        private string GetMsgIp()
        {
            return getBuyerInfo("Sender's IP Address");
        }
        private string GetOrigin()
        {
            return getBuyerInfo("Sender's IP Location");
        }

        private string GetProduct()
        {
            if (productInfo != null)
            {
                HtmlNode productDetail = productInfo.SelectSingleNode(@"tr/td[2]/p/span");
                if (productDetail != null)
                {
                    return productDetail.InnerText.Trim();
                }                
            }
            return "";
            
        }

        private string GetName()
        {
            return getBuyerInfo("Sender");
        }

        private string GetCountry()
        {
            return getBuyerInfo("Country/Region");
        }

        private string GetTelephone()
        {
            return getBuyerInfo("Telephone");
        }

        private string GetCompany()
        {
            return getBuyerInfo("Company");
        }

        private string GetAddress()
        {
            return getBuyerInfo("Homepage").Replace("n/a", "");
        }

        private string GetFax()
        {
            return getBuyerInfo("Fax").Replace("n/a", "");
        }

        public string getBuyerInfo(string keystring)
        {
            if (buyerDetail != null)
            {
                HtmlNodeCollection nodes = buyerDetail.SelectNodes(@"tr");
                foreach (HtmlNode node in nodes)
                {
                    if (node.SelectSingleNode(@"td/p/span") == null)
                    {
                        continue;
                    }
                    string key = node.SelectSingleNode(@"td/p/span").InnerText.Trim();
                  
                    if (key != string.Empty 
                        && key.Equals(keystring, StringComparison.CurrentCultureIgnoreCase)
                        && node.SelectSingleNode(@"td[2]/p/span") != null)
                    {
                        Console.WriteLine(key + " : " + node.SelectSingleNode(@"td[2]").InnerText.Trim());
                        return node.SelectSingleNode(@"td[2]/p/span").InnerText.Trim();
                    }
                }
            }
            return "";
        }


        public string getType()
        { 
            return "Made-In-China";
        }
    }
}
