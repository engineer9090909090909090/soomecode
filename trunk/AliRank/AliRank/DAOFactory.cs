using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SQLite;

namespace AliRank
{
    class DAOFactory
    {
        public static DAOFactory Instance =  new DAOFactory();

        private SQLiteDBHelper dbHelper;
        private KeywordDAO keywordDAO;
        private VpnDAO vpnDAO;
        private InquiryDAO inquiryDAO;

        private DAOFactory()
        {
            string DataBasePath = FileUtils.GetAppDataFolder() + Path.DirectorySeparatorChar + Constants.DB_FILE;
            File.Delete(DataBasePath);
            if (!File.Exists(DataBasePath))
            {
                SQLiteConnection.CreateFile(DataBasePath);
                SQLiteDBHelper.EncryptDatabase(DataBasePath, Constants.DB_PASSWORD);
            }
            dbHelper = new SQLiteDBHelper(DataBasePath, Constants.DB_PASSWORD);
            keywordDAO = new KeywordDAO(dbHelper);
            vpnDAO = new VpnDAO(dbHelper);
            inquiryDAO = new InquiryDAO(dbHelper);
        }

        public KeywordDAO GetKeywordDAO()
        {
            return keywordDAO;
        }

        public VpnDAO GetVpnDAO()
        {
            return vpnDAO;
        }
        public InquiryDAO GetInquiryDAO()
        {
            return inquiryDAO;
        }

    }
}
