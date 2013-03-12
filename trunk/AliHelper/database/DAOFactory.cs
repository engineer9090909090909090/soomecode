using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SQLite;
using Soomes;

namespace Database
{
    public class DAOFactory
    {
        public static DAOFactory Instance =  new DAOFactory();
        private SQLiteDBHelper dbHelper;
        private MysqlDBHelper mysqlDbHelper;
        private AppConfigDAO appConfigDAO;
        private IAliProductDao aliProductDao;
        private IAliGroupDao aliGroupDao;
        private IAliImageDao aliImageDao;
        private IAliProductDetailDao aliProductDetailDao;
        private IAppDicDAO appDicDAO;
        private IFinanceDao financeDao;
        private IOrderDao orderDao;

        private DAOFactory()
        {
            string DataBasePath = GetUserDataFolder() + Path.DirectorySeparatorChar + Constants.DB_FILE;
            if (!File.Exists(DataBasePath))
            {
                SQLiteConnection.CreateFile(DataBasePath);
            }
            dbHelper = new SQLiteDBHelper(DataBasePath);

            if (!string.IsNullOrEmpty(DataCache.Instance.MySqlConnection))
            {
                mysqlDbHelper = new MysqlDBHelper(DataCache.Instance.MySqlConnection);
            }
        }

        private static string GetUserDataFolder()
        {
            string AppDataFolder = Environment.CurrentDirectory + Path.DirectorySeparatorChar + DataCache.Instance.AliID;
            if (!Directory.Exists(AppDataFolder))
            {
                Directory.CreateDirectory(AppDataFolder);
            }
            return AppDataFolder;
        }

        public AppConfigDAO GetAppConfigDAO()
        {
            if (appConfigDAO == null)
            {
                this.appConfigDAO = new AppConfigDAO(dbHelper);
            }
            return appConfigDAO;
        }
        
        public IAliProductDao GetAliProductDao()
        {
            if (aliProductDao == null)
            {
                this.aliProductDao = new AliProductDao(dbHelper);
            }
            return aliProductDao;
        }

        public IAliGroupDao GetAliGroupDao()
        {
            if (aliGroupDao == null)
            {
                aliGroupDao = new AliGroupDao(dbHelper);
            }
            return aliGroupDao;
        }


        public IAliImageDao GetAliImageDao()
        {
            if (aliImageDao == null)
            {
                aliImageDao = new AliImageDao(dbHelper);
            }
            return aliImageDao;
        }

        public IAliProductDetailDao GetAliProductDetailDao()
        {
            if (aliProductDetailDao == null)
            {
                aliProductDetailDao = new AliProductDetailDao(dbHelper);
            }
            return aliProductDetailDao;
        }

        public IAppDicDAO GetAppDicDAO()
        {
            if (appDicDAO == null)
            {
                if (this.mysqlDbHelper == null)
                {
                    this.appDicDAO = new AppDicDAO(dbHelper);
                }
                else 
                {
                    this.appDicDAO = new AppDicDAOMysql(mysqlDbHelper);
                }
            }
            return appDicDAO;
        }

        public IFinanceDao GetFinanceDao()
        {
            if (financeDao == null)
            {
                if (this.mysqlDbHelper == null)
                {
                    this.financeDao = new FinanceDao(dbHelper);
                }
                else
                {
                    this.financeDao = new FinanceDaoMysql(mysqlDbHelper);
                }
            }
            return financeDao;
        }

        public IOrderDao GetOrderDao()
        {
            if (orderDao == null)
            {
                if (this.mysqlDbHelper == null)
                {
                    this.orderDao = new OrderDao(dbHelper);
                }
                else
                {
                    this.orderDao = new OrderDaoMysql(mysqlDbHelper);
                }
            }
            return orderDao;
        }
    }
}
