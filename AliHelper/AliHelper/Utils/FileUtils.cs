using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Net;
using Soomes;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;


namespace AliHelper
{
    class FileUtils
    {
        public static string GetUserDataFolder()
        {
            string AppDataFolder = Application.StartupPath + Path.DirectorySeparatorChar  + DataCache.Instance.AliID;
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
        

        public static string GetAppDataFolder()
        {
            string AppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                + Path.DirectorySeparatorChar + Application.ProductName;
            if (!Directory.Exists(AppDataFolder))
            {
                Directory.CreateDirectory(AppDataFolder);
            }
            return AppDataFolder;
        }

        public static string GetNewTempImagePath()
        {
            string temp = System.Environment.GetEnvironmentVariable("TEMP");
            return temp + Path.DirectorySeparatorChar + Guid.NewGuid().ToString() + ".jpg";
        }

        public static System.Drawing.Image GetImage(string path)
        {
            System.Drawing.Image result;
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                result = System.Drawing.Image.FromStream(fs);
            }   
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

        public static Color GetColor(double amount)
        {
            return (amount > 0) ? Color.Blue : Color.Red;
        }
       

        public static byte[] ImageFileToByteArray(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite);
            int streamLength = (int)fs.Length;
            byte[] image = new byte[streamLength];
            fs.Read(image, 0, streamLength);
            fs.Close();
            return image;
        }


        public static void ByteArrayToImageFile(byte[] byteArrayIn, string imageFile)
        {
            if (byteArrayIn == null || byteArrayIn.Length == 0) { return; }
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            returnImage.Save(imageFile);
            returnImage.Dispose();
        }

        public static string ResizeImageToLess1M(string fileName)
        {
            long size = new FileInfo(fileName).Length;
            if (size / 1024 < 600)
            {
                return fileName;
            }
            long multiple = size / 1024 / 500;
            string newImageFile = FileUtils.GetNewTempImagePath();
            Bitmap oldBmp = new Bitmap(fileName);
            int w = Convert.ToInt32(oldBmp.Size.Width / multiple);
            int h = Convert.ToInt32(oldBmp.Size.Height / multiple);
            Bitmap newBmp = ImageUtils.ResizeImage(oldBmp, w, h);
            oldBmp.Dispose();
            newBmp.Save(newImageFile, ImageFormat.Jpeg);
            newBmp.Dispose();
            return newImageFile;
        }
    }
}
