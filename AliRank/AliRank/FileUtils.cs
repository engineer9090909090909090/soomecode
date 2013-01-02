using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace AliRank
{
    class FileUtils
    {
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

        public static string GetImageFolder()
        {
            string imageDir = GetAppDataFolder() + Path.DirectorySeparatorChar + Constants.IMAGE_DIR;
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

        public static string CreateAppDataFolderEmptyTextFile(string txtFileName)
        {
            string txtFile = GetAppDataFolder() + Path.DirectorySeparatorChar + txtFileName;
            if (!File.Exists(txtFile)) {
                File.CreateText(txtFile);
            }
            return txtFile;
        }


        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /*
        public static string IniReadValue(string Section, string Key, string filePath)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, filePath);
            return temp.ToString();
        }
        public static void IniWriteValue(string Section, string Key, string val, string filePath)
        {
            WritePrivateProfileString(Section, Key, val, filePath);
        }
        */


        //public string key = "98765432";//任意组合的8位数字  
        //private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>  
        /// DESEnCode DES加密  
        /// </summary>  
        /// <param name=”pToEncrypt”></param>  
        /// <param name=”sKey”></param>  
        /// <returns></returns>  
        public static string DesEncrypt(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.GetEncoding("UTF-8").GetBytes(pToEncrypt);
            //建立加密对象的密钥和偏移量
            //原文使用ASCIIEncoding.ASCII方法的GetBytes方法     
            //使得输入密码必须输入英文文本
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:x2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }

        /// <summary>  
        /// DESDeCode DES解密  
        /// </summary>  
        /// <param name=”pToDecrypt”> 待解密的字符串</param>  
        /// <param name=”sKey”> 解密密钥,要求为8字节,和加密密钥相同</param>  
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>         
        public static string DesDncrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.Default.GetString(ms.ToArray()); 
        } 
    }



}
