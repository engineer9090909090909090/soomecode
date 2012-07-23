using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace AliEmail
{
    public class ParseAliBefor20110224Mail: IfParseMail
    {
        private string subject;
        private HtmlNode senderInfo;
        private HtmlNode productInfo;
        private HtmlNode messageInfo;
        public ParseAliBefor20110224Mail(){}


        public object[] Parse(string subject, string body)
        {
            this.subject = subject;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(body);
            this.senderInfo  = doc.DocumentNode.SelectSingleNode(@"//body/table[2]/tr[3]/td/div/div/div[2]/table");
            HtmlNode table1Node = doc.DocumentNode.SelectSingleNode(@"//body/table[2]/tr[3]/td/div/table");
            HtmlNode table2Node = doc.DocumentNode.SelectSingleNode(@"//body/table[2]/tr[3]/td/div/table[2]");
            if (table2Node != null)
            {
                this.productInfo = table1Node;
                this.messageInfo = table2Node.SelectSingleNode(@"tr[3]/td/div");
            }
            else if (table1Node != null)
            {
                this.messageInfo = table1Node.SelectSingleNode(@"tr[3]/td/div");
                this.productInfo = table1Node.SelectSingleNode(@"tr[2]/td/strong");
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
            return getBuyerInfo("Business Email:");
        }


        public string getBuyerInfo(string keystring)
        {
            if (senderInfo != null)
            {
                HtmlNodeCollection nodes = senderInfo.SelectNodes(@"tr");
                foreach (HtmlNode node in nodes)
                {
                    if (node.SelectSingleNode(@"td") == null)
                    {
                        continue;
                    }
                    string key = node.SelectSingleNode(@"td").InnerText.Trim();
                    if (key != string.Empty && key.Equals(keystring, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (keystring.Equals("Country/Region:"))
                        {
                            return node.SelectSingleNode(@"td[2]").FirstChild.InnerText.Trim().Replace("&nbsp;", "");
                        }
                        else
                        {
                            return node.SelectSingleNode(@"td[2]").InnerText.Trim();
                        }
                    }
                }
            }
            return "";
        }

        private string GetMsgIp()
        {
            string result = string.Empty;
            if (messageInfo != null)
            {  
                Regex r = new Regex("Message IP Address:(.*?)<br>");
                GroupCollection gc = r.Match(messageInfo.InnerHtml).Groups;
                if (gc != null && gc.Count > 1)
                {
                    result = gc[1].Value.Trim();
                }
            }
            return result;
        }
        private string GetOrigin()
        {
            string result = string.Empty;
            if (messageInfo != null)
            {
                Regex r = new Regex("Message Origin:(.*?)\r\n");
                GroupCollection gc = r.Match(messageInfo.InnerHtml).Groups;
                if (gc != null && gc.Count > 1)
                {
                    result = gc[1].Value.Trim();
                }
            }
            return result;
        }
        private string GetProduct()
        {
            if (productInfo != null)
            {
                return productInfo.InnerText.Trim();
            }
            return "";
        }

        private string GetName()
        {
            return getBuyerInfo("Contact Name:");
        }

        private string GetCountry()
        {
            return getBuyerInfo("Country/Region:");
        }

        private string GetTelephone()
        {
            return getBuyerInfo("Telephone:");
        }

        private string GetCompany()
        {
            return getBuyerInfo("Company:");
        }

        private string GetAddress()
        {
            return getBuyerInfo("Address:");
        }

        private string GetFax()
        {
            return getBuyerInfo("Fax:");
        }

        public string getType()
        {
             return "Alibaba.com";
        }
    }
}
