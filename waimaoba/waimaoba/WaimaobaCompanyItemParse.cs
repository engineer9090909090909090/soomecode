using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Web;

namespace com.soomes
{
    class WaimaobaCompanyItemParse : WebParse
    {
        public void Parse(string url, HtmlDocument document)
        {
            if (url.IndexOf("/company/") > 0)
            {
                if (url.StartsWith("http://buyer.waimaoba.com"))
                {
                    HtmlNodeCollection fieldsetNodes = document.DocumentNode.SelectNodes("//fieldset[@class='fieldgroup group-information']/table/tbody/tr/td[2]/div");
                    if (fieldsetNodes.Count > 1)
                    {
                        HtmlNode node1 = fieldsetNodes[0].SelectSingleNode("div[2]");//公司
                        string context = node1.InnerText;
                        Console.WriteLine(context);

                        node1 = fieldsetNodes[0].SelectSingleNode("div[4]");//国家
                        context = node1.InnerText;
                        Console.WriteLine(context);

                        node1 = fieldsetNodes[0].SelectSingleNode("div[6]");//采购产品
                        context = node1.InnerText;
                        Console.WriteLine(context);

                        

                    }

                }
                else
                {
                    HtmlNodeCollection fieldsetNodes = document.DocumentNode.SelectNodes("//fieldset[@class='fieldgroup group-information']/table/tbody/tr/td[2]");
                    if (fieldsetNodes.Count > 1)
                    {
                        HtmlNode node1 = fieldsetNodes[0].SelectSingleNode("div/div[2]");//公司
                        string context = node1.InnerText;
                        Console.WriteLine(HttpUtility.HtmlDecode(context).Trim());

                        node1 = fieldsetNodes[0].SelectSingleNode("div[2]/div[2]");//国家
                        context = node1.InnerText;
                        Console.WriteLine(context);

                        node1 = fieldsetNodes[0].SelectSingleNode("div[3]/div[2]");//采购产品
                        context = node1.InnerText;
                        Console.WriteLine(context);




                    }
                }

            }
            
        }
    }
}
