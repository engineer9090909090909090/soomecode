using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

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
                        Console.WriteLine("mail=" + email);
                    }
                }
            }
            return emailList;
        }


        public static string ExecRepaceHTML(string Htmlstring)   
        {   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);  
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);   
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);  
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*\n", "", RegexOptions.IgnoreCase);   
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);   
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);   
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);  
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);   
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);   
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);  
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);  
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);  
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);   
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);   
            Htmlstring = Regex.Replace(Htmlstring, @"\s", ""); Htmlstring.Replace("<", ""); Htmlstring.Replace(">", "");     
            Htmlstring = HttpUtility.HtmlEncode(Htmlstring).Trim();  
            return Htmlstring;  
        }  

    }
}
