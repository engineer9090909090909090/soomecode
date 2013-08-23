using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;

namespace com.soomes
{
    public class HtmlUtils
    {
        public static string getContent(string Url)
        {
            string strResult = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Timeout = 30000;//设置连接超时时间  
                request.Headers.Set("Pragma", "no-cache");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(streamReceive, Encoding.UTF8);
                strResult = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch
            {
                //throw;
                return strResult;
            }
            return strResult;
        }

        public static List<string> getEmails(string html)
        {
            List<string> emailList = new List<string>();
            string regexEmail = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            RegexOptions options = ((RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline) | RegexOptions.IgnoreCase);
            Regex r = new Regex(regexEmail, options);
            MatchCollection mc = r.Matches(html);
            if (mc.Count > 0)
            {
                foreach (Match m in mc)
                {
                    GroupCollection gc = m.Groups;
                    if (gc.Count > 0)
                    {
                        string email = gc[0].Value;
                        if (!emailList.Contains(email))
                        {
                            emailList.Add(email);
                        }
                    }
                }
            }
            return emailList;
        }

        public static string GetHtmlNodeText(HtmlNode node)
        {
            if (node == null)
            {
                return string.Empty;
            }
            string text = node.InnerText.Replace("&nbsp;", " ");
            text = HttpUtility.HtmlDecode(text).Trim();
            text = text.Replace("  ", "").Replace("\t", "");

            return text;
        }

        public static void Log(string text)
        {
            Console.WriteLine(text);
        }
    }
}
