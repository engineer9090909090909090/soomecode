using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Database;
using Soomes;

namespace AliHelper
{
    public class BaseManager
    {
        private AppConfigDAO configDAO;
        private IAppDicDAO appDicDAO;
        public BaseManager()
        {
            appDicDAO = DAOFactory.Instance.GetAppDicDAO();
            configDAO = DAOFactory.Instance.GetAppConfigDAO();
        }

        public string GetConfigValue(string type, string key)
        {
            return configDAO.GetValue(type, key);
        }

        public void SetConfigValue(string type, string key, string value)
        {
            configDAO.SetValue(type, key, value);
        }

        public void SetDbConfig(string dbType, string dbUrl, string dbName, string dbUser, string dbPass)
        {
            SetConfigValue(Constants.Db_Config, Constants.Db_Type, dbType);
            SetConfigValue(Constants.Db_Config, Constants.Db_Url, dbUrl);
            SetConfigValue(Constants.Db_Config, Constants.Db_Name, dbName);
            SetConfigValue(Constants.Db_Config, Constants.Db_User, dbUser);
            SetConfigValue(Constants.Db_Config, Constants.Db_Pass, dbPass);
        }

        public void InitDbConfig()
        {
            DataCache.Instance.OpenMySqlDb = false;
            DataCache.Instance.MySqlConnection = string.Empty;
            string dbType = GetConfigValue(Constants.Db_Config, Constants.Db_Type);
            if (dbType == Constants.DbType_MySql)
            {
                DataCache.Instance.OpenMySqlDb = true;
                string dbUrl = GetConfigValue(Constants.Db_Config, Constants.Db_Url);
                string dbName = GetConfigValue(Constants.Db_Config, Constants.Db_Name);
                string dbUser = GetConfigValue(Constants.Db_Config, Constants.Db_User);
                string dbPass = GetConfigValue(Constants.Db_Config, Constants.Db_Pass);
                string connection_str = "server=" + dbUrl + ";uid=" + dbUser + ";pwd=" + dbPass
                + ";database=" + dbName + ";Charset=utf8;Allow Zero Datetime=true";
                DataCache.Instance.MySqlConnection = connection_str;
            }
        }

        public string GetAppDicValue(string type, string key)
        {
            return appDicDAO.GetValue(type, key);
        }

        public List<AppDic> GetAppDicOptions(string type)
        {
            return appDicDAO.GetOptions(type);
        }

        public List<AppDic> GetQueryAppDicOptions(string type)
        {
            List<AppDic> list =  appDicDAO.GetOptions(type);
            list.Insert(0, new AppDic());
            return list;
        }

        public void SetAppDicValue(string type, string key, string value)
        {
            appDicDAO.SetValue(type, key, value);
        }

        public void DeleteAppDic(string type, string key)
        {
            appDicDAO.DeleteAppDic(type, key);
        }
    }
}
