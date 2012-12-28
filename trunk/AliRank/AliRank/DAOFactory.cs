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
        private ProfileDAO profileDAO;

        private DAOFactory()
        {
            string DataBasePath = string.Empty;
#if DEBUG
            DataBasePath = FileUtils.GetAppDataFolder() + Path.DirectorySeparatorChar + Constants.DEBUG_DB_FILE;
#else
            DataBasePath = FileUtils.GetAppDataFolder() + Path.DirectorySeparatorChar + Constants.DB_FILE;
#endif
            //File.Delete(DataBasePath);
            if (!File.Exists(DataBasePath))
            {
                SQLiteConnection.CreateFile(DataBasePath);
                SQLiteDBHelper.EncryptDatabase(DataBasePath, Constants.DB_PASSWORD);
            }
            dbHelper = new SQLiteDBHelper(DataBasePath, Constants.DB_PASSWORD);
            keywordDAO = new KeywordDAO(dbHelper);
            vpnDAO = new VpnDAO(dbHelper);
            inquiryDAO = new InquiryDAO(dbHelper);
            profileDAO = new ProfileDAO(dbHelper);
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

        public ProfileDAO GetProfileDAO()
        {
            return profileDAO;
        }

    }
}
