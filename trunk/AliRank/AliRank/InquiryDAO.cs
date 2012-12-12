using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

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
            + "LoginIp varchar(50))");

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

 


        public List<AliAccounts> GetAccounts()
        {
            DataTable dt = dbHelper.ExecuteDataTable("SELECT AccountId, Email, Name, Password, Country, LoginIp from AliAccounts", null);
            List<AliAccounts> list = new List<AliAccounts>();
            foreach (DataRow row in dt.Rows)
            {
                AliAccounts kw = new AliAccounts();
                kw.AccountId = Convert.ToInt32(row["AccountId"]);
                kw.Email = (string)row["Email"];
                kw.Name = (string)row["Name"];
                kw.Password = (string)row["Password"];
                kw.Country = (string)row["Country"];
                kw.LoginIp = (string)row["LoginIp"];
                list.Add(kw);
            }
            return list;
        }

        public void InsertAccount(AliAccounts model)
        {
            string sql = @"INSERT INTO AliAccounts(Email, Name, Password, Country)values(@Email,@Name,@Password,@Country)";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@Email",model.Email), 
                new SQLiteParameter("@Name", model.Name), 
                new SQLiteParameter("@Password",model.Password), 
                new SQLiteParameter("@Country",model.Country)
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }

        public void ImportAccounts(List<AliAccounts> list)
        {
            string InsSql = @"INSERT INTO AliAccounts(Email, Name, Password, Country)values(@Email,@Name,@Password,@Country)";

            string UpdSql = @"Update AliAccounts SET Password = @Password, Name = @Name, Country = @Country WHERE Email = @Email";

            string ExistRecordSql = "SELECT count(1) FROM AliAccounts WHERE Email = ";
            List<SQLiteParameter[]> InsertParameters = new List<SQLiteParameter[]>();
            List<SQLiteParameter[]> UpdateParameters = new List<SQLiteParameter[]>();
            foreach (AliAccounts item in list)
            {
                int record = Convert.ToInt32(dbHelper.ExecuteScalar(ExistRecordSql + item.Email, null));
                if (record > 0)
                {
                    SQLiteParameter[] parameter = new SQLiteParameter[]
                    {
                        new SQLiteParameter("@Password",item.Password), 
                        new SQLiteParameter("@Name",item.Name), 
                        new SQLiteParameter("@Country",item.Country), 
                        new SQLiteParameter("@Email",item.Email)
                    };
                    UpdateParameters.Add(parameter);
                }
                else
                {
                    SQLiteParameter[] parameter = new SQLiteParameter[]
                    {
                        new SQLiteParameter("@Email",item.Email), 
                        new SQLiteParameter("@Name",item.Name), 
                        new SQLiteParameter("@Password", item.Password), 
                        new SQLiteParameter("@Country",item.Country)
                    };
                    InsertParameters.Add(parameter);
                }

            }
            if (InsertParameters.Count > 0)
            {
                dbHelper.ExecuteNonQuery(InsSql, InsertParameters);
            }
            if (UpdateParameters.Count > 0)
            {
                dbHelper.ExecuteNonQuery(UpdSql, UpdateParameters);
            }
        }

        public void DeleteAccount(int accountId)
        {
            string sql = @"delete from AliAccounts where AccountId = @AccountId";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@AccountId",accountId)
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }

        public void InsertInquiryInfos(InquiryInfos model)
        {
            string sql = @"INSERT INTO InquiryInfos(AccountId, ProductId, MsgId, Company, InquiryIp,InquiryTime)values(@AccountId,@ProductId,@MsgId,@Company,@InquiryIp,@InquiryTime)";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@AccountId",model.AccountId), 
                new SQLiteParameter("@ProductId", model.ProductId), 
                new SQLiteParameter("@MsgId",model.MsgId), 
                new SQLiteParameter("@Company",model.Company),
                new SQLiteParameter("@InquiryIp",model.InquiryIp),
                new SQLiteParameter("@InquiryTime",model.InquiryTime)
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }

        public void InsertInquiryMessages(InquiryMessages model)
        {
            string sql = @"INSERT INTO InquiryMessages(Content, Subject)values(@Content,@Subject)";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@Content", model.Content), 
                new SQLiteParameter("@Subject",model.Subject)
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }

        public void DeleteInquiryMessages(int msgId)
        {
            string sql = @"delete from InquiryMessages where MsgId = @MsgId";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@MsgId",msgId)
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }

        public List<InquiryMessages> GetInquiryMessages()
        {
            DataTable dt = dbHelper.ExecuteDataTable("SELECT MsgId, Content, Subject, SendNum from InquiryMessages", null);
            List<InquiryMessages> list = new List<InquiryMessages>();
            foreach (DataRow row in dt.Rows)
            {
                InquiryMessages kw = new InquiryMessages();
                kw.MsgId = Convert.ToInt32(row["MsgId"]);
                kw.Content = (string)row["Content"];
                kw.Subject = (string)row["Subject"];
                kw.SendNum = Convert.ToInt32(row["SendNum"]);
                list.Add(kw);
            }
            return list;
        }
    }
}
