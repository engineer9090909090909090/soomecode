using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Net;
using Soomes;


namespace AliHelper
{
    class FileUtils
    {
        public static string GetUserDataFolder()
        {
            string AppDataFolder = Environment.CurrentDirectory + Path.DirectorySeparatorChar  + DataCache.Instance.AliID;
            if (!Directory.Exists(AppDataFolder))
            {
                Directory.CreateDirectory(AppDataFolder);
            }
            return AppDataFolder;
        }

        public static string GetPhotoBankFolder()
        {
            string imageDir = GetUserDataFolder() + Path.DirectorySeparatorChar + Constants.PhtotBank;
            if (!Directory.Exists(imageDir))
            {
                Directory.CreateDirectory(imageDir);
            }
            return imageDir;
        }

        public static string GetProductImagesFolder()
        {
            string imageDir = GetUserDataFolder() + Path.DirectorySeparatorChar + Constants.ProductImages;
            if (!Directory.Exists(imageDir))
            {
                Directory.CreateDirectory(imageDir);
            }
            return imageDir;
        }

        public static System.Drawing.Image GetImage(string path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
            System.Drawing.Image result = System.Drawing.Image.FromStream(fs);
            fs.Close();
            return result;
        } 

        public static string DownloadImage(WebClient webClient, string url, int id)
        {
            string imageFile = FileUtils.GetPhotoBankFolder() + Path.DirectorySeparatorChar + id + ".jpg";
            if (File.Exists(imageFile))
            {
                return imageFile;
            }
            try
            {
                webClient.DownloadFile(url, imageFile);
            }
            catch
            {
                imageFile = "";
            }
            return imageFile;
        }

        public static string DownloadProductImage(WebClient webClient, string url, int id)
        {
            string imageFile = FileUtils.GetProductImagesFolder() + Path.DirectorySeparatorChar + id + ".jpg";
            if (File.Exists(imageFile))
            {
                return imageFile;
            }
            try
            {
                webClient.DownloadFile(url, imageFile);
            }
            catch
            {
                imageFile = "";
            }
            return imageFile;
        }

        public static string DownloadProductImage(WebClient webClient, string url, int id, int order)
        {
            string imageFile = FileUtils.GetProductImagesFolder() + Path.DirectorySeparatorChar + id + "_" + order + ".jpg";
            if (File.Exists(imageFile))
            {
                return imageFile;
            }
            try
            {
                webClient.DownloadFile(url, imageFile);
            }
            catch
            {
                imageFile = "";
            }
            return imageFile;
        }

        public static string GetAliId(string s, string defaultDomain)
        {
            CookieContainer cc = new CookieContainer();
            if (string.IsNullOrEmpty(s) || s.Length < 5 || s.IndexOf("=") < 0) return string.Empty;
            s.TrimEnd(new char[] { ';' }).Trim();
            string[] cookstr = s.Split(';');
            Uri uri = new Uri(defaultDomain);
            foreach (string str in cookstr)
            {
                int index = str.IndexOf("=");
                Cookie ck = new Cookie(str.Substring(0, index).Trim(), str.Substring(index + 1).Trim());
                ck.Domain = uri.Host.ToString();
                cc.Add(ck);
            }
            return GetAliId(cc, defaultDomain);
        }

        public static string GetAliId(CookieContainer cc, string defaultDomain)
        {
            if (cc == null) return string.Empty;
            Uri uri = new Uri(defaultDomain);
            CookieCollection cookieCollection = cc.GetCookies(uri);
            if (cookieCollection == null) return string.Empty;
            string xman_us_tValue = cookieCollection["xman_us_t"].Value;
            if (string.IsNullOrEmpty(xman_us_tValue)) return string.Empty;
            string[] xman_us_tArray = xman_us_tValue.Split(new char[]{'&'});
            foreach (string signStr in xman_us_tArray)
            {
                if (signStr.IndexOf("x_lid=") > -1)
                {
                    return signStr.Substring(6);
                }
            }
            return string.Empty;
        }
    }
}
