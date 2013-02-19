using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SQLite;

namespace AliHelper.DAO
{
    class DAOFactory
    {
        public static DAOFactory Instance =  new DAOFactory();

        private SQLiteDBHelper dbHelper;
        private AliProductDao aliProductDao;
        private AliGroupDao aliGroupDao;
        private AliImageDao aliImageDao;
        private AliProductDetailDao aliProductDetailDao;
        private AppDicDAO appDicDAO;
        private FinanceDao financeDao;
        private OrderDao orderDao;

        private DAOFactory()
        {
            string DataBasePath = FileUtils.GetUserDataFolder() + Path.DirectorySeparatorChar + Constants.DB_FILE;
            //File.Delete(DataBasePath);
            if (!File.Exists(DataBasePath))
            {
                SQLiteConnection.CreateFile(DataBasePath);
            }
            dbHelper = new SQLiteDBHelper(DataBasePath);
        }

        public AliProductDao GetAliProductDao()
        {
            if (aliProductDao == null)
            {
                this.aliProductDao = new AliProductDao(dbHelper);
            }
            return aliProductDao;
        }

        public AliGroupDao GetAliGroupDao()
        {
            if (aliGroupDao == null)
            {
                aliGroupDao = new AliGroupDao(dbHelper);
            }
            return aliGroupDao;
        }


        public AliImageDao GetAliImageDao()
        {
            if (aliImageDao == null)
            {
                aliImageDao = new AliImageDao(dbHelper);
            }
            return aliImageDao;
        }

        public AliProductDetailDao GetAliProductDetailDao()
        {
            if (aliProductDetailDao == null)
            {
                aliProductDetailDao = new AliProductDetailDao(dbHelper);
            }
            return aliProductDetailDao;
        }

        public AppDicDAO GetAppDicDAO()
        {
            if (appDicDAO == null)
            {
                this.appDicDAO = new AppDicDAO(dbHelper);
            }
            return appDicDAO;
        }

        public FinanceDao GetFinanceDao()
        {
            if (financeDao == null)
            {
                this.financeDao = new FinanceDao(dbHelper);
            }
            return financeDao;
        }

        public OrderDao GetOrderDao()
        {
            if (orderDao == null)
            {
                orderDao = new OrderDao(dbHelper);
            }
            return orderDao;
        }
    }
}
