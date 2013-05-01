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

        public static string GetMyItemImage(int productId, int imageId)
        {
            string imageDir = GetUserDataFolder() + Path.DirectorySeparatorChar + Constants.MyItemImages;
            if (!Directory.Exists(imageDir))
            {
                Directory.CreateDirectory(imageDir);
            }
            return imageDir + Path.DirectorySeparatorChar + productId + "_" + imageId + ".jpg";
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


        public static Image BufferToImage(byte[] Buffer)
        {
            if (Buffer == null || Buffer.Length == 0) { return null; }
            byte[] data = null;
            Image oImage = null;
            Bitmap oBitmap = null;
            data = (byte[])Buffer.Clone();//建立副本
            try
            {
                MemoryStream oMemoryStream = new MemoryStream(Buffer);//設定資料流位置
                oMemoryStream.Position = 0;
                oImage = System.Drawing.Image.FromStream(oMemoryStream);
                oBitmap = new Bitmap(oImage);//建立副本
            }
            catch
            {
                throw;
            }
            return oBitmap;
        }

        /// <summary>
        /// 將 Image 轉換為 Byte 陣列。
        /// </summary>
        /// <param name="Image">Image 。</param>
        /// <param name="imageFormat">指定影像格式。System.Drawing.Imaging.ImageFormat.JPEG</param>  
        public static byte[] ImageToBuffer(Image Image, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            if (Image == null) { return null; }
            byte[] data = null;
            using (MemoryStream oMemoryStream = new MemoryStream())
            {
                using (Bitmap oBitmap = new Bitmap(Image))//建立副本
                {
                    //儲存圖片到 MemoryStream 物件，並且指定儲存影像之格式
                    oBitmap.Save(oMemoryStream, imageFormat);//設定資料流位置
                    oMemoryStream.Position = 0; //設定 buffer 長度
                    data = new byte[oMemoryStream.Length];//將資料寫入 buffer
                    oMemoryStream.Read(data, 0, Convert.ToInt32(oMemoryStream.Length));
                    oMemoryStream.Flush();//將所有緩衝區的資料寫入資料流
                }
            }
            return data;
        }

        public static byte[] ImageFileToToBuffer(string imageFile)
        {
            if (string.IsNullOrEmpty(imageFile) || !File.Exists(imageFile))
            {
                return null;
            }
            return FileUtils.ImageToBuffer(Image.FromFile(imageFile), System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        public static void BufferToImageFile( byte[] buffer, string imageFile)
        {
            Image image = FileUtils.BufferToImage(buffer);
            image.Save(imageFile);
            image.Dispose();
        }
    }
}
