using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SQLite;

namespace com.soomes
{
    public class DAOFactory
    {
        private static DAOFactory Instance;
        private static SQLiteDBHelper SQLiteDHelper;
        private static BuyerInfoDao buyerInfoDao;
        private static SearchUrlDao searchUrlDao;

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
                string DataBasePath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "DB.sqlite";
                if (!File.Exists(DataBasePath))
                {
                    SQLiteConnection.CreateFile(DataBasePath);
                }
                SQLiteDHelper = new SQLiteDBHelper(DataBasePath);
            }
        }

        public BuyerInfoDao GetBuyerInfoDao()
        {
            if (buyerInfoDao == null)
            {
                buyerInfoDao = new BuyerInfoDao(SQLiteDHelper);
            }
            return buyerInfoDao;
        }

        public SearchUrlDao GetSearchUrlDao()
        {
            if (searchUrlDao == null)
            {
                searchUrlDao = new SearchUrlDao(SQLiteDHelper);
            }
            return searchUrlDao;
        }

        
    }
}
