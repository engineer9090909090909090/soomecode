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
        //private string REMOTE_ADDRESS = "http://localhost/AliRank/Api/";
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
            LoginInfo loginInfo = null;
            SoomsUser user = new SoomsUser();
            user.Username = username;
            user.Password = password;
            user.CpuId = CpuId;
            user.MacAddress = MacAdd;
            user.ComputeName = ComputeName;
            string data = JsonConvert.ToJson(user);
            string postString = "data=" + FileUtils.DesEncrypt(data, Constants.DES_KEY);
            string jsonString  = HttpHelper.GetHtml(REMOTE_ADDRESS + "login", postString, null);
            try
            {
                loginInfo = JsonConvert.FromJson<LoginInfo>(jsonString);
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e.Message);
            }
            if (loginInfo == null)
            {
                return "登录失败，请联系管理员";
            }
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
                    string data =  JsonConvert.ToJson(accounts);
                    postString = postString + "&data=" + FileUtils.DesEncrypt(data, Constants.DES_KEY); 
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
                    string data = JsonConvert.ToJson(vpns);
                    postString = postString + "&data=" + FileUtils.DesEncrypt(data, Constants.DES_KEY); 
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
