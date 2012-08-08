using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace AliEmail
{
    public class ParseAliBetween24to30Mail: IfParseMail
    {
        private string subject;
        private HtmlNode senderInfo;
        private HtmlNode productInfo;
        private HtmlNode cantactInfo;
        public ParseAliBetween24to30Mail() { }


        public object[] Parse(string subject, string body)
        {
            this.subject = subject;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(body);
            this.senderInfo = doc.DocumentNode.SelectSingleNode(@"//body/table/tr[2]/td/div/table[2]");
            this.productInfo = doc.DocumentNode.SelectSingleNode(@"//body/table/tr[2]/td/h3");
            this.cantactInfo = doc.DocumentNode.SelectSingleNode(@"//body/table/tr[2]/td/div/table/tr/td[2]/h4");

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
            if (senderInfo != null)
            {
                return senderInfo.SelectSingleNode(@"tr[4]/td[2]").InnerText.Trim();
            }
            return "";
        }

        private string GetMsgIp()
        {
            if (senderInfo != null)
            {
                return senderInfo.SelectSingleNode(@"tr[8]/td[2]").InnerText.Trim();
            }
            return "";
        }
        private string GetOrigin()
        {
            if (senderInfo != null)
            {
                return senderInfo.SelectSingleNode(@"tr[7]/td[2]").InnerText.Trim();
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
                return cantactInfo.InnerText.Trim();
            }
            return "";
        }

        private string GetCountry()
        {
            if (senderInfo != null)
            {
                return senderInfo.SelectSingleNode(@"tr[2]/td[2]").InnerText.Trim();
            }
            return "";
        }
        private string GetTelephone()
        {
            if (senderInfo != null)
            {
                return senderInfo.SelectSingleNode(@"tr[5]/td[2]").InnerText.Trim();
            }
            return "";
        }

        private string GetCompany()
        {
            if (senderInfo != null)
            {
                return senderInfo.SelectSingleNode(@"tr[1]/td[2]").InnerText.Trim();
            }
            return "";
        }

        private string GetAddress()
        {
            if (senderInfo != null)
            {
                return senderInfo.SelectSingleNode(@"tr[3]/td[2]").InnerText.Trim();
            }
            return "";
        }
        private string GetFax()
        {
            if (senderInfo != null)
            {
                return senderInfo.SelectSingleNode(@"tr[6]/td[2]").InnerText.Trim();
            }
            return "";
        }

        public string getType()
        {
             return "Alibaba";
        }
    }
}
