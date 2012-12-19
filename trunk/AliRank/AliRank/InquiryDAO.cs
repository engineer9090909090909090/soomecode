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
            UpdateTable();
        }

        private void CreateTable()
        {
            //dbHelper.ExecuteNonQuery(" drop table IF NOT EXISTS AliAccounts");
            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS AliAccounts("
            + "AccountId integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"
            + "Account varchar(100) NOT NULL,"
            + "Password varchar(100) NOT NULL,"
            + "Country varchar(100) NOT NULL,"
            + "LoginIp varchar(50) default '',"
            + "LoginTime datetime,"
            + "Enable integer default 1) ");

            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS InquiryInfos("
            + "Account varchar(100) NOT NULL,"
            + "ProductId integer NOT NULL,"
            + "MsgId integer NOT NULL,"
            + "Company varchar(100) NOT NULL,"
            + "InquiryIp varchar(50)  NOT NULL,"
            + "InquiryDate datetime NOT NULL)");
            dbHelper.ExecuteNonQuery("CREATE INDEX IF NOT EXISTS account_product on InquiryInfos(Account, ProductId, InquiryDate);");
            dbHelper.ExecuteNonQuery("CREATE INDEX IF NOT EXISTS idx_msgId on InquiryInfos(MsgId);");

            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS InquiryMessages("
            + "MsgId integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"
            + "Content varchar(8000) NOT NULL,"
            + "SendNum integer NOT NULL default 0)");
        }

        private void UpdateTable()
        {
            bool ExistColumnEnable = dbHelper.IsExistColumn("AliAccounts", "Enable");
            if (!ExistColumnEnable)
            {
                dbHelper.ExecuteNonQuery("alter table AliAccounts add column Enable integer default 1;");
            }
        }

        public bool ExistAccount(string account)
        {
            string sql = "select count(1) from AliAccounts where Account= @Account";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@Account", account),
            };
            int  r = Convert.ToInt32(dbHelper.ExecuteScalar(sql, parameter));
            if (r > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        

        public List<AliAccounts> GetAccounts()
        {
            DataTable dt = dbHelper.ExecuteDataTable(
                "SELECT a.AccountId, a.Account, "
                +" a.Password, a.Country, a.LoginIp, a.Enable,"
                +" (select count(1) from InquiryInfos i where a.Account=i.Account) as InquiryNum "
                +" from AliAccounts a", null);
            List<AliAccounts> list = new List<AliAccounts>();
            foreach (DataRow row in dt.Rows)
            {
                AliAccounts kw = new AliAccounts();
                kw.AccountId = Convert.ToInt32(row["AccountId"]);
                kw.Account = (string)row["Account"];
                kw.Password = (string)row["Password"];
                kw.Country = (string)row["Country"];
                kw.LoginIp = (string)row["LoginIp"];
                kw.Enable = Convert.ToInt32(row["Enable"]);
                kw.InquiryNum = Convert.ToInt32(row["InquiryNum"]);
                list.Add(kw);
            }
            return list;
        }

        public void InsertAccount(AliAccounts model)
        {
            string sql = @"INSERT INTO AliAccounts(Account, Password, Country)values(@Account, @Password, @Country)";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@Account",model.Account), 
                new SQLiteParameter("@Password",model.Password), 
                new SQLiteParameter("@Country",model.Country)
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }

        public void ImportAccounts(List<AliAccounts> list)
        {
            string InsSql = @"INSERT INTO AliAccounts(Account, Password, Country, LoginIp)values(@Account,@Password,@Country, @LoginIp)";

            string UpdSql = @"Update AliAccounts SET Password = @Password, Country = @Country, LoginIp=@LoginIp, Enable = 1 WHERE Account = @Account";

            string ExistRecordSql = "SELECT count(1) FROM AliAccounts WHERE Account = '{0}'";
            List<SQLiteParameter[]> InsertParameters = new List<SQLiteParameter[]>();
            List<SQLiteParameter[]> UpdateParameters = new List<SQLiteParameter[]>();
            foreach (AliAccounts item in list)
            {
                int record = Convert.ToInt32(dbHelper.ExecuteScalar(string.Format(ExistRecordSql, item.Account), null));
                if (record > 0)
                {
                    SQLiteParameter[] parameter = new SQLiteParameter[]
                    {
                        new SQLiteParameter("@Password",item.Password), 
                        new SQLiteParameter("@Country",item.Country), 
                        new SQLiteParameter("@Account",item.Account),
                        new SQLiteParameter("@LoginIp",item.LoginIp)
                    };
                    UpdateParameters.Add(parameter);
                }
                else
                {
                    SQLiteParameter[] parameter = new SQLiteParameter[]
                    {
                        new SQLiteParameter("@Account",item.Account), 
                        new SQLiteParameter("@Password", item.Password), 
                        new SQLiteParameter("@Country",item.Country),
                         new SQLiteParameter("@LoginIp",item.LoginIp)
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

        public void DisableAccount(string account)
        {
            string sql = @"Update AliAccounts set Enable = 0 where Account=@Account";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@Account", account)
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }

        public void UpdateAccountLoginIp(string account, string ip)
        {
            string sql = @"Update AliAccounts set LoginIp = @LoginIp, LoginTime=@LoginTime where Account=@Account";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@LoginIp", ip),
                new SQLiteParameter("@LoginTime", DateTime.Now),
                new SQLiteParameter("@Account", account)
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }

        public void DeleteAccount(List<string> idList)
        {
            foreach (string id in idList)
            {
                string sql = @"DELETE FROM AliAccounts WHERE AccountId = @AccountId";
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("@AccountId", Convert.ToInt32(id))
                };
                dbHelper.ExecuteNonQuery(sql, parameter);
            }
        }

        public void DeleteInquiryMessages(List<string> idList)
        {
            foreach (string id in idList)
            {
                string sql = @"DELETE FROM InquiryMessages WHERE MsgId = @MsgId";
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("@MsgId", Convert.ToInt32(id))
                };
                dbHelper.ExecuteNonQuery(sql, parameter);
            }
        }

        public void InsertInquiryInfos(InquiryInfos model)
        {
            string sql = @"INSERT INTO InquiryInfos(Account, ProductId, MsgId, Company, InquiryIp, InquiryDate)values(@Account,@ProductId,@MsgId,@Company,@InquiryIp,@InquiryDate)";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@Account",model.Account), 
                new SQLiteParameter("@ProductId", model.ProductId), 
                new SQLiteParameter("@MsgId",model.MsgId), 
                new SQLiteParameter("@Company",model.Company),
                new SQLiteParameter("@InquiryIp",model.InquiryIp),
                new SQLiteParameter("@InquiryDate",model.InquiryDate)
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }

        
        public void InsertInquiryMessages(InquiryMessages model)
        {
            string sql = @"INSERT INTO InquiryMessages(Content)values(@Content)";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@Content", model.Content)
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }

        public void AddInqMessageSendNum(int msgId)
        {
            string sql = @"Update InquiryMessages set SendNum = SendNum + 1 where MsgId=@MsgId";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@MsgId", msgId)
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }

        public List<InquiryMessages> GetInquiryMessages()
        {
            DataTable dt = dbHelper.ExecuteDataTable("SELECT MsgId, Content, SendNum from InquiryMessages", null);
            List<InquiryMessages> list = new List<InquiryMessages>();
            foreach (DataRow row in dt.Rows)
            {
                InquiryMessages kw = new InquiryMessages();
                kw.MsgId = Convert.ToInt32(row["MsgId"]);
                kw.Content = (string)row["Content"];
                kw.SendNum = Convert.ToInt32(row["SendNum"]);
                list.Add(kw);
            }
            return list;
        }

        public InquiryMessages GetInquiryMinMessage()
        {
            DataTable dt = dbHelper.ExecuteDataTable(
                "SELECT MsgId, Content, SendNum from InquiryMessages order by SendNum asc limit 0,1", null);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                InquiryMessages kw = new InquiryMessages();
                kw.MsgId = Convert.ToInt32(row["MsgId"]);
                kw.Content = (string)row["Content"];
                kw.SendNum = Convert.ToInt32(row["SendNum"]);
                return kw;
            }
            return null;
        }

        /// <summary>
        /// 查询今天可用来发询盘的帐号
        /// </summary>
        /// <param name="yesterday"></param>
        /// <returns></returns>
        public AliAccounts GetCanInquiryAccount(int yesterday, string loginIp)
        {
            DataTable dt = dbHelper.ExecuteDataTable(
                "select distinct a.AccountId, a.Account, a.Password,a.Country from aliaccounts  a "
                + "left join inquiryInfos i on a.Account=i.Account "
                + "where a.Enable = 1 and (a.loginIp = '' or a.loginIp ='" + loginIp 
                + "') and i.Account is null  and (i.inquiryDate > " + yesterday 
                + " or i.inquiryDate is null)", null);
            List<AliAccounts> list = new List<AliAccounts>();
            foreach (DataRow row in dt.Rows)
            {
                AliAccounts kw = new AliAccounts();
                kw.AccountId = Convert.ToInt32(row["AccountId"]);
                kw.Account = (string)row["Account"];
                kw.Password = (string)row["Password"];
                kw.Country = (string)row["Country"];
                list.Add(kw);
            }
            if (list.Count > 0)
            {
                int randomNumber = new Random().Next(0, list.Count - 1);
                return list[randomNumber];
            }

            string sql = "select a.AccountId, a.Account, a.Password, a.Country from aliaccounts a  "
                + "where a.LoginTime is null or a.LoginTime <= @LoginTime";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@LoginTime", DateTime.Now.AddDays(-1))
            };
            dt = dbHelper.ExecuteDataTable(sql, parameter);
            foreach (DataRow row in dt.Rows)
            {
                AliAccounts kw = new AliAccounts();
                kw.AccountId = Convert.ToInt32(row["AccountId"]);
                kw.Account = (string)row["Account"];
                kw.Password = (string)row["Password"];
                kw.Country = (string)row["Country"];
                list.Add(kw);
            } 
            if (list.Count > 0)
            {
                int randomNumber = new Random().Next(0, list.Count - 1);
                return list[randomNumber];
            }
            return null;
        }
        
        /// <summary>
        /// 查询产品今天已经发送询盘的数量
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="todayDate"></param>
        /// <returns></returns>
        public int TodayInquiryQty4Product(int productId)
        {
            string sql = "select count(1) from  inquiryInfos where productId = @productId "
                + " and inquiryDate= @inquiryDate ";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@productId", productId),
                new SQLiteParameter("@inquiryDate", DateTime.Now.ToString("yyyyMMdd"))
            };
            return Convert.ToInt32(dbHelper.ExecuteScalar(sql, parameter));
        }
        


    }
}
