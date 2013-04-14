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
        private static DAOFactory Instance;
        private static SQLiteDBHelper SQLiteDHelper;
        private static AppConfigDAO appConfigDAO;
        private static MysqlDBHelper MySqlDbHelper;
        private IAliProductDao aliProductDao;
        private IAliGroupDao aliGroupDao;
        private IAliImageDao aliImageDao;
        private IAliProductDetailDao aliProductDetailDao;
        private IAppDicDAO appDicDAO;
        private IFinanceDao financeDao;
        private IOrderDao orderDao;
        private IProductDao productDao;
        private ISupplierDao supplierDao;

        public static DAOFactory GetInstance()
        {
            if (Instance == null)
            {
                Instance = new DAOFactory();
            }
            return Instance;
        }

        private DAOFactory()
        {
            if (SQLiteDHelper == null)
            {
                string DataBasePath = GetUserDataFolder() + Path.DirectorySeparatorChar + Constants.DB_FILE;
                if (!File.Exists(DataBasePath))
                {
                    SQLiteConnection.CreateFile(DataBasePath);
                }
                SQLiteDHelper = new SQLiteDBHelper(DataBasePath);
            }
            if (!string.IsNullOrEmpty(DataCache.Instance.MySqlConnection) && DataCache.Instance.OpenMySqlDb)
            {
                MySqlDbHelper = new MysqlDBHelper(DataCache.Instance.MySqlConnection);
            }
        }

        public static AppConfigDAO GetAppConfigDAO()
        {
            if (SQLiteDHelper == null)
            {
                string DataBasePath = GetUserDataFolder() + Path.DirectorySeparatorChar + Constants.DB_FILE;
                if (!File.Exists(DataBasePath))
                {
                    SQLiteConnection.CreateFile(DataBasePath);
                }
                SQLiteDHelper = new SQLiteDBHelper(DataBasePath);
            }
            if (appConfigDAO == null)
            {
                appConfigDAO = new AppConfigDAO(SQLiteDHelper);
            }
            return appConfigDAO;
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

        
        
        public IAliProductDao GetAliProductDao()
        {
            if (aliProductDao == null)
            {
                this.aliProductDao = new AliProductDao(SQLiteDHelper);
            }
            return aliProductDao;
        }

        public IAliGroupDao GetAliGroupDao()
        {
            if (aliGroupDao == null)
            {
                aliGroupDao = new AliGroupDao(SQLiteDHelper);
            }
            return aliGroupDao;
        }


        public IAliImageDao GetAliImageDao()
        {
            if (aliImageDao == null)
            {
                aliImageDao = new AliImageDao(SQLiteDHelper);
            }
            return aliImageDao;
        }

        public IAliProductDetailDao GetAliProductDetailDao()
        {
            if (aliProductDetailDao == null)
            {
                aliProductDetailDao = new AliProductDetailDao(SQLiteDHelper);
            }
            return aliProductDetailDao;
        }

        public IAppDicDAO GetAppDicDAO()
        {
            if (appDicDAO == null)
            {
                if (!DataCache.Instance.OpenMySqlDb)
                {
                    this.appDicDAO = new AppDicDAO(SQLiteDHelper);
                }
                else 
                {
                    this.appDicDAO = new AppDicDAOMysql(MySqlDbHelper);
                }
            }
            return appDicDAO;
        }

        public IFinanceDao GetFinanceDao()
        {
            if (financeDao == null)
            {
                if (!DataCache.Instance.OpenMySqlDb)
                {
                    this.financeDao = new FinanceDao(SQLiteDHelper);
                }
                else
                {
                    this.financeDao = new FinanceDaoMysql(MySqlDbHelper);
                }
            }
            return financeDao;
        }

        public IOrderDao GetOrderDao()
        {
            if (orderDao == null)
            {
                if (!DataCache.Instance.OpenMySqlDb)
                {
                    this.orderDao = new OrderDao(SQLiteDHelper);
                }
                else
                {
                    this.orderDao = new OrderDaoMysql(MySqlDbHelper);
                }
            }
            return orderDao;
        }

        public IProductDao GetProductDao()
        {
            if (orderDao == null)
            {
                if (!DataCache.Instance.OpenMySqlDb)
                {
                    //this.productDao = new ProductDaoMysql(dbHelper);
                }
                else
                {
                    this.productDao = new ProductDaoMysql(MySqlDbHelper);
                }
            }
            return productDao;
        }

        public ISupplierDao GetSupplierDao()
        {
            if (supplierDao == null)
            {
                if (!DataCache.Instance.OpenMySqlDb)
                {
                    //this.productDao = new ProductDaoMysql(dbHelper);
                }
                else
                {
                    this.supplierDao = new SuplierDaoMysql(MySqlDbHelper);
                }
            }
            return supplierDao;
        }
    }
}
