using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliRank
{
    public class InquiryDAO
    {
        private SQLiteDBHelper dbHelper;

        public InquiryDAO(SQLiteDBHelper dbHelper)
        { 
            this.dbHelper = dbHelper;
            CreateTable();
        }

        private void CreateTable()
        {
            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS AliAccounts("
            + "AccountId integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"
            + "Email varchar(100) NOT NULL,"
            + "Name varchar(200),"
            + "Password varchar(100) NOT NULL,"
            + "Country varchar(100) NOT NULL,"
            + "LoginIp varchar(50) NOT NULL)");

            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS InquiryInfos("
            + "AccountId integer NOT NULL,"
            + "ProductId integer NOT NULL,"
            + "MsgId integer NOT NULL,"
            + "Company varchar(100) NOT NULL,"
            + "InquiryIp varchar(50)  NOT NULL,"
            + "InquiryTime datetime NOT NULL)");
            dbHelper.ExecuteNonQuery("CREATE INDEX IF NOT EXISTS account_product on InquiryInfos(AccountId, ProductId, InquiryTime);");
            dbHelper.ExecuteNonQuery("CREATE INDEX IF NOT EXISTS idx_msgId on InquiryInfos(MsgId);");

            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS InquiryMessages("
            + "MsgId integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"
            + "Content varchar(8000),"
            + "Subject varchar(500) NOT NULL,"
            + "SendNum integer NOT NULL default 0)");
        }


    }
}
