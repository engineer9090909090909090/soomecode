using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Database;
using Soomes;
using System.Windows.Forms;

namespace AliHelper
{
    public class BaseManager
    {
        private IAppDicDAO appDicDAO;
        public BaseManager()
        {
        }

        public string GetConfigValue(string type, string key)
        {
            return DAOFactory.GetAppConfigDAO().GetValue(type, key);
        }

        public void SetConfigValue(string type, string key, string value)
        {
            DAOFactory.GetAppConfigDAO().SetValue(type, key, value);
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
                string dbUrl = GetConfigValue(Constants.Db_Config, Constants.Db_Url);
                string dbName = GetConfigValue(Constants.Db_Config, Constants.Db_Name);
                string dbUser = GetConfigValue(Constants.Db_Config, Constants.Db_User);
                string dbPass = GetConfigValue(Constants.Db_Config, Constants.Db_Pass);
                string connection_str = "server=" + dbUrl + ";uid=" + dbUser + ";pwd=" + dbPass
                + ";database=" + dbName + ";Charset=utf8;Allow Zero Datetime=true";
                MysqlDBHelper dbHelper = new MysqlDBHelper(connection_str);
                bool Success = dbHelper.ConnectionTest();
                if (Success)
                {
                    DataCache.Instance.MySqlConnection = connection_str;
                    DataCache.Instance.OpenMySqlDb = true;
                }
                else 
                {
                    MessageBox.Show("不能正常连接到指定的MySql数据库，请检查!");
                }
            }
        }

        public string GetAppDicValue(string type, string key)
        {
            return DAOFactory.Instance.GetAppDicDAO().GetValue(type, key);
        }

        public List<AppDic> GetAppDicOptions(string type)
        {
            return DAOFactory.Instance.GetAppDicDAO().GetOptions(type);
        }

        public List<AppDic> GetQueryAppDicOptions(string type)
        {
            List<AppDic> list = DAOFactory.Instance.GetAppDicDAO().GetOptions(type);
            list.Insert(0, new AppDic());
            return list;
        }

        public void SetAppDicValue(string type, string key, string value)
        {
            DAOFactory.Instance.GetAppDicDAO().SetValue(type, key, value);
        }

        public void DeleteAppDic(string type, string key)
        {
            DAOFactory.Instance.GetAppDicDAO().DeleteAppDic(type, key);
        }
    }
}
