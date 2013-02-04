using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AliHelper.DAO;
using Soomes;

namespace AliHelper
{
    public class BaseManager
    {
        private AppDicDAO appDicDAO;
        public BaseManager()
        {
            appDicDAO = DAOFactory.Instance.GetAppDicDAO();
        }

        public string GetAppDicValue(string type, string key)
        {
            return appDicDAO.GetValue(type, key);
        }

        public List<AppDic> GetAppDicOptions(string type)
        {
            return appDicDAO.GetOptions(type);
        }

        public void SetAppDicValue(string type, string key, string value)
        {
            appDicDAO.SetValue(type, key, value);
        }
    }
}
