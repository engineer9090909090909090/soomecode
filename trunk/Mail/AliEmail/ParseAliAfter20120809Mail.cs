using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace AliEmail
{
    public class ParseAliAfter20120809Mail: IfParseMail
    {
        private string subject;
        private HtmlNode messageInfo;
        private string messageText = string.Empty;
        private HtmlNode productInfo;
        private HtmlNode cantactInfo;
        public ParseAliAfter20120809Mail() { }


        public object[] Parse(string subject, string body)
        {
            this.subject = subject;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(body);
            this.messageInfo = doc.DocumentNode.SelectSingleNode(@"//table/tr/td/h1");
            if (messageInfo != null)
            {
                messageText = messageInfo.ParentNode.InnerText.Replace("\t", "").Replace("\r\n", " ");
            }
            HtmlNodeCollection links = doc.DocumentNode.SelectNodes(@"//a");

            object[] dataItem = new object[13];
            dataItem[0] = 0;
            dataItem[1] = string.Empty;
            dataItem[2] = string.Empty;
            dataItem[3] = string.Empty;
            dataItem[4] = GetName(links);
            dataItem[5] = GetMail(subject);
            dataItem[6] = string.Empty;
            dataItem[7] = string.Empty;
            dataItem[8] = string.Empty;
            dataItem[9] = string.Empty;
            dataItem[10] = string.Empty;
            dataItem[11] = string.Empty;
            dataItem[12] = string.Empty;
            return dataItem;
        }
        private string GetMail(string subject)
        {
            Regex r = new Regex(@"\[(.*?)\]");
            GroupCollection gc = r.Match(subject).Groups;
            if (gc != null && gc.Count > 1)
            {
                return gc[1].Value.Trim();
            }
            return "";
        }


        private string GetName(HtmlNodeCollection links)
        {
            if (links != null)
            {
                foreach (HtmlNode n in links)
                {
                    if (n.Attributes["href"] == null) continue;

                    string href = n.Attributes["href"].Value;
                    if (href.StartsWith("http://message.alibaba.com/customer/customer_detail.htm") && n.InnerText.Trim()!="")
                    {
                        string name = n.InnerText;
                        name = name.Replace("Mr. ", "").Replace("Ms. ", "");
                        return name;
                    }
                }
            }
            return "";
        }


        public string getType()
        {
             return "Alibaba";
        }
    }
}
