using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
namespace AliRank
{
    public class Tools
    {
        public static string getLocalMAC()
        {
            string mac = null;
            ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection queryCollection = query.Get();
            foreach (ManagementObject mo in queryCollection)
            {
                if (mo["IPEnabled"].ToString() == "True")
                {
                    mac = mo["MacAddress"].ToString();
                    break;
                }
            }
            return (mac);
        }

        public static string GetCpuID()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
                string strCPUID = "";
                foreach (ManagementObject mo in searcher.Get())
                {
                    strCPUID = mo["ProcessorId"].ToString().Trim();
                    break;
                }
                return strCPUID;
            }
            catch
            {
                return "读取CPU序列号失败!";
            }
        }

        public static string GetComputerName()
        {
            return System.Environment.GetEnvironmentVariable("ComputerName");
        }
    }
}
