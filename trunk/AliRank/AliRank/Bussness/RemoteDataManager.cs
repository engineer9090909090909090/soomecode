using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AliRank.Bussness
{
    class RemoteDataManager
    {
        private InquiryDAO inqDao;
        private VpnDAO vpnDao;
        private string CpuId;
        private string MacAdd;
        private string ComputeName;
        private string OnlyToken;
        private string REMOTE_ADDRESS = "http://rank.soomes.com/Api/";
        public static RemoteDataManager Instance = new RemoteDataManager();

        private RemoteDataManager()
        {
            inqDao = DAOFactory.Instance.GetInquiryDAO();
            vpnDao = DAOFactory.Instance.GetVpnDAO();
            CpuId = Tools.GetCpuID();
            MacAdd = Tools.getLocalMAC();
            ComputeName = Tools.GetComputerName();
        }

        public string UserLoginSystem(string username, string password)
        {
            string postString = "username=" + username + "&password=" + password;
            postString = postString + "&cupId=" + CpuId + "&macAddress=" + MacAdd + "&ComputeName=" + ComputeName;
            string jsonString  = HttpHelper.GetHtml(REMOTE_ADDRESS + "login", postString, null);
            LoginInfo loginInfo = JsonConvert.FromJson<LoginInfo>(jsonString);
            if (string.IsNullOrEmpty(loginInfo.Message))
            {
                this.OnlyToken = loginInfo.Token;
            }
            return loginInfo.Message;
        }

        public void PostAccounts()
        {
            string returnstring = string.Empty;
            try
            {
                List<AliAccounts> accounts = inqDao.GetAccounts();
                if (accounts.Count> 0)
                {
                    string postString = "_token=" + this.OnlyToken;
                    postString = postString + "&accounts=" + HttpUtility.UrlEncode(JsonConvert.ToJson(accounts), Encoding.UTF8);
                    returnstring = HttpHelper.GetHtml(REMOTE_ADDRESS + "uploadAccounts", postString, null);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e.Message);
            }
            System.Diagnostics.Trace.WriteLine(returnstring);
        }

        public void PostVpns()
        {
            string returnstring = string.Empty;
            try
            {
                List<VpnModel> vpns = vpnDao.GetVpnModelList();
                if (vpns.Count > 0)
                {
                    string postString = "_token=" + this.OnlyToken;
                    postString = postString + "&vpns=" + HttpUtility.UrlEncode(JsonConvert.ToJson(vpns), Encoding.UTF8);
                    returnstring = HttpHelper.GetHtml(REMOTE_ADDRESS + "uploadVpns", postString, null);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e.Message);
            }
            System.Diagnostics.Trace.WriteLine(returnstring);
        }
    }
}
