using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliRank.Bussness
{
    class RemoteDataManager
    {
        private InquiryDAO inqDao;
        private VpnDAO vpnDao;
        private string CpuId;
        private string MacAdd;
        private string ComputeName;

        public static RemoteDataManager Instance = new RemoteDataManager();

        private RemoteDataManager()
        {
            inqDao = DAOFactory.Instance.GetInquiryDAO();
            vpnDao = DAOFactory.Instance.GetVpnDAO();
            CpuId = Tools.GetCpuID();
            MacAdd = Tools.getLocalMAC();
            ComputeName = Tools.GetComputerName();
        }

        public void UserLoginSystem()
        {
 
        }

        public void PostAccounts()
        {
            try
            {
                List<AliAccounts> accounts = inqDao.GetAccounts();
                string postString = "accounts=" + JsonConvert.ToJson(accounts);
                HttpHelper.GetHtml("http://rank.soomes.cn/accounts.php", postString, null);
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e.Message);
            }
        }

        public void PostVpns()
        {
            try
            {
                List<VpnModel> vpns = vpnDao.GetVpnModelList();
                string postString = "vpns=" + JsonConvert.ToJson(vpns);
                HttpHelper.GetHtml("http://rank.soomes.cn/vpns.php", postString, null);
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e.Message);
            }
        }
    }
}
