using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace AliEmail
{
    public class ParseGlobelSourceMail: IfParseMail
    {
        private string subject;
        private HtmlNode buyerDetail;
        private HtmlNode productInfo;
        public ParseGlobelSourceMail() { }

        public object[] Parse(string subject, string body)
        {
            this.subject = subject;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(body);
            this.buyerDetail = doc.DocumentNode.SelectSingleNode(@"//body/div/table[2]/tr[2]/td/table[3]");
            if (this.buyerDetail == null)
            {
                this.buyerDetail = doc.DocumentNode.SelectSingleNode(@"//body/table[2]/tr[2]/td/table[3]");
            }
            this.productInfo = doc.DocumentNode.SelectSingleNode(@"//body/div/table[2]/tr[2]/td/table[3]/tr[4]/td/table");
            if (this.productInfo == null)
            {
                this.productInfo = doc.DocumentNode.SelectSingleNode(@"//body/table[2]/tr[2]/td/table[3]/tr[4]/td/table");
            }

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
            return getBuyerInfo("Business Email");
        }

        private string GetMsgIp()
        {
            return "";
        }
        private string GetOrigin()
        {
            return getBuyerInfo("Inquiry was sent from");
        }

        private string GetProduct()
        {
            string products = string.Empty;
            if (productInfo != null)
            {
                HtmlNodeCollection nodes = productInfo.SelectNodes(@"tr");
                foreach (HtmlNode node in nodes)
                {
                    if (node.SelectSingleNode(@"td[2]/p/b/span") != null)
                    {
                        string productName = node.SelectSingleNode(@"td[2]/p/b/span").InnerText.Trim();
                        products = products + ", " + productName;
                    }
                    if (node.SelectSingleNode(@"td[2]/font/b") != null)
                    {
                        string productName = node.SelectSingleNode(@"td[2]/font/b").InnerText.Trim();
                        products = products + ", " + productName;
                    }
                }
                if (products != string.Empty)
                {
                   return products.Substring(2);
                }
            }
            return products;
            
        }

        private string GetName()
        {
            return getBuyerInfo("Name");
        }

        private string GetCountry()
        {
            return getBuyerInfo("Country/Territory");
        }

        private string GetTelephone()
        {
            return getBuyerInfo("Phone Number");
        }

        private string GetCompany()
        {
            return getBuyerInfo("Company Name");
        }

        private string GetAddress()
        {
            return getBuyerInfo("Company Website");
        }

        private string GetFax()
        {
            return getBuyerInfo("Business Type");
        }

        public string getBuyerInfo(string keystring)
        {
            if (buyerDetail != null)
            {
                HtmlNodeCollection nodes = buyerDetail.SelectNodes(@"tr");
                foreach (HtmlNode node in nodes)
                {
                    if (node.SelectSingleNode(@"td/p/span/b") == null)
                    {
                        continue;
                    }
                    string key = node.SelectSingleNode(@"td/p/span/b").InnerText.Trim();
                    if (key != string.Empty && key.Equals(keystring, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return node.SelectSingleNode(@"td[2]/p/span").InnerText.Trim();
                    }
                }
                //another format email
                foreach (HtmlNode node in nodes)
                {
                    if (node.SelectSingleNode(@"td/font/b") == null)
                    {
                        continue;
                    }
                    string key = node.SelectSingleNode(@"td/font/b").InnerText.Trim();
                    if (key != string.Empty && key.Equals(keystring, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return node.SelectSingleNode(@"td[2]/font").InnerText.Trim();
                    }
                }
            }
            return "";
        }


        public string getType()
        {
            return "GlobalSources"; 
        }
    }
}
