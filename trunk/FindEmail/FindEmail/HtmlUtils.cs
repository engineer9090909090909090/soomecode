using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace AliRank
{
    class HtmlUtils
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
    }
}
