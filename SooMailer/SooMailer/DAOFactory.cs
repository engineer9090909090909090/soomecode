using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SQLite;

namespace SooMailer
{
    class DAOFactory
    {
        public static DAOFactory Instance =  new DAOFactory();

        private SQLiteDBHelper dbHelper;

        private DAOFactory()
        {
            string DataBasePath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + Constants.DB_FILE;
            //File.Delete(DataBasePath);
            if (!File.Exists(DataBasePath))
            {
                SQLiteConnection.CreateFile(DataBasePath);
            }
            dbHelper = new SQLiteDBHelper(DataBasePath);
        }

        public MailModelDAO GetMailModelDAO()
        {
            return new MailModelDAO(dbHelper);
        }

    }
}
