using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace AliEmail
{
    public class ParseAliAfter20120701Mail: IfParseMail
    {
        private string subject;
        private HtmlNode messageInfo;
        private string messageText = string.Empty;
        private HtmlNode productInfo;
        private HtmlNode cantactInfo;
        public ParseAliAfter20120701Mail() { }


        public object[] Parse(string subject, string body)
        {
            this.subject = subject;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(body);
            this.messageInfo = doc.DocumentNode.SelectSingleNode(@"//body/table/tr[2]/td/div/div[2]");
            if (messageInfo != null)
            {
                messageText = messageInfo.InnerText.Replace("\t", "").Replace("\r\n", " ");
            }
            this.productInfo = doc.DocumentNode.SelectSingleNode(@"//body/table/tr[2]/td/table/tr[2]/td[2]/a/strong");
            this.cantactInfo = doc.DocumentNode.SelectSingleNode(@"//body/table/tr[2]/td/table[3]/tr/td");

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
            if (cantactInfo != null)
            {
                return cantactInfo.SelectSingleNode(@"table[2]/tr[4]/td[2]").InnerText.Trim();
            }
            return "";
        }

        private string GetMsgIp()
        {
            if (messageText != null)
            {
                Regex r = new Regex("Message IP:(.*?)\\*");
                GroupCollection gc = r.Match(messageText).Groups;
                if (gc != null && gc.Count > 1)
                {
                    string msgIp = gc[1].Value.Trim()+"*";
                    return msgIp;
                }
            }
            return "";
        }
        private string GetOrigin()
        {
            if (messageText != null)
            {
                Regex r = new Regex("Message Origin:(.*?)Message IP:");
                GroupCollection gc = r.Match(messageText).Groups;
                if (gc != null && gc.Count > 1)
                {
                    string msgIp = gc[1].Value.Trim();
                    return msgIp;
                }
            }
            return "";
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
            if (cantactInfo != null)
            {
                return cantactInfo.SelectSingleNode(@"table/tr/td[2]/a/h4").InnerText.Trim();
            }
            return "";
        }

        private string GetCountry()
        {
            if (cantactInfo != null)
            {
                return cantactInfo.SelectSingleNode(@"table[2]/tr[2]/td[2]").InnerText.Trim();
            }
            return "";
        }
        private string GetTelephone()
        {
            if (cantactInfo != null)
            {
                return cantactInfo.SelectSingleNode(@"table[2]/tr[5]/td[2]").InnerText.Trim();
            }
            return "";
        }

        private string GetCompany()
        {
            if (cantactInfo != null)
            {
                return cantactInfo.SelectSingleNode(@"table[2]/tr/td[2]").InnerText.Trim();
            }
            return "";
        }

        private string GetAddress()
        {
            if (cantactInfo != null)
            {
                return cantactInfo.SelectSingleNode(@"table[2]/tr[3]/td[2]").InnerText.Trim();
            }
            return "";
        }
        private string GetFax()
        {
            if (cantactInfo != null)
            {
                return cantactInfo.SelectSingleNode(@"table[2]/tr[6]/td[2]").InnerText.Trim();
            }
            return "";
        }

        public string getType()
        {
             return "Alibaba";
        }
    }
}
