using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Drawing;

namespace SooMailer
{
    class MailModelDAO
    {
        private SQLiteDBHelper dbHelper;

        public MailModelDAO(SQLiteDBHelper dbHelper)
        { 
            this.dbHelper = dbHelper;
            CreateTable();
        }

        private void CreateTable()
        {
                   dbHelper.ExecuteNonQuery
                    (
                      "CREATE TABLE IF NOT EXISTS mail_address("
                    + "id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"
                    + "Email varchar(100) NOT NULL,"
                    + "Company varchar(200) NOT NULL,"
                    + "Country varchar(200) NOT NULL,"
                    + "Username varchar(200) NOT NULL,"
                    + "Subject varchar(500),"
                    + "ProductType varchar(200) NOT NULL,"
                    + "Verify1 integer default 0 NOT NULL,"
                    + "Verify2 integer default 0 NOT NULL, "
                    + "SendDate varchar(200))"
                    );

                   dbHelper.ExecuteNonQuery("Create Index  IF NOT EXISTS Index_productId on mail_address(Email);");
                   dbHelper.ExecuteNonQuery("Create Index  IF NOT EXISTS Index_productType on mail_address(ProductType);");
        }



        public List<MailModel> GetMailModelList(MailModel searchItem)
        {
            DataTable dt = dbHelper.ExecuteDataTable(
                  "SELECT id, Email, Company, Country, Username, Subject, "
                + "ProductType, Verify1,Verify2, SendDate FROM mail_address",
                null);

            List<MailModel> list = new List<MailModel>();
            foreach (DataRow row in dt.Rows)
            {
                MailModel model = new MailModel();
                model.Id = Convert.ToInt32(row["id"]);
                model.Email = Convert.ToString(row["Email"]);
                model.Company = Convert.ToString(row["Company"]);
                model.Country = Convert.ToString(row["Country"]);
                model.Username = Convert.ToString(row["Username"]);
                model.Subject = Convert.ToString(row["Subject"]);
                model.ProductType = Convert.ToString(row["ProductType"]);
                model.Verify1 = Convert.ToInt32(row["Verify1"]);
                model.Verify2 = Convert.ToInt32(row["Verify2"]);
                model.SendDate = Convert.ToString(row["SendDate"]);
                list.Add(model);
            }
            return list;
        }


        public void Insert(List<MailModel> list)
        {
            string sql = @"INSERT INTO mail_address(Email, Company, Country, Username, Subject,ProductType, SendDate)"
                            + "values(@Email,@Company,@Country,@Username,@Subject,@ProductType, @SendDate)";
            List<SQLiteParameter[]> parameters = new List<SQLiteParameter[]>();
            foreach (MailModel item in list)
            {
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("@Email",item.Email), 
                    new SQLiteParameter("@Company", item.Company), 
                    new SQLiteParameter("@Country",item.Country), 
                    new SQLiteParameter("@Username",item.Username), 
                    new SQLiteParameter("@Subject",item.Subject), 
                    new SQLiteParameter("@ProductType",item.ProductType), 
                    new SQLiteParameter("@SendDate",item.SendDate), 
                };
                parameters.Add(parameter);
            }
            dbHelper.ExecuteNonQuery(sql, parameters);        
        }

    }
}
