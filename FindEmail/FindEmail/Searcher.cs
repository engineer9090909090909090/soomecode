using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace FindEmail
{
    class Searcher
    {

        public Searcher() { }

        //深度3 //最大线程数10
        public void DoSearch(string url)
        {
            HtmlWeb clinet = new HtmlWeb();
            HtmlDocument document = clinet.Load(url);
            string html = document.DocumentNode.InnerText;

            Uri uri = new Uri(url);

            string regexEmail = "\\w{1,}@\\w{1,}\\.\\w{1,}";
            RegexOptions options = ((RegexOptions.IgnorePatternWhitespace| RegexOptions.Multiline)| RegexOptions.IgnoreCase);
            Regex r = new Regex(regexEmail,options);
            GroupCollection gc = r.Match(html).Groups;
            foreach (Group g in gc)
            {
                string email = g.Value;
                Console.WriteLine("mail="+ email);
            }
            HtmlNodeCollection linkNodes = document.DocumentNode.SelectNodes("//a");
            foreach (HtmlNode linkNode in linkNodes)
            {
                if (linkNode.Attributes["href"] != null)
                {
                    string currUrl = linkNode.Attributes["href"].Value;
                    if (currUrl == "" || currUrl == "#")
                    {
                        continue;
                    }
                    string currFullUrl = currUrl;
                    if (currUrl.StartsWith("/"))
                    {
                        currFullUrl = url.Replace(uri.PathAndQuery, "") + currUrl;
                        Console.WriteLine(currFullUrl);
                    }
                    
                }
                    
            }

        }
    }


}
