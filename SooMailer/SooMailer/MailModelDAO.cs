﻿using System;
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
                    + "Company varchar(200),"
                    + "Country varchar(200),"
                    + "Username varchar(200),"
                    + "Subject varchar(500),"
                    + "ProductType varchar(200) NOT NULL,"
                    + "Source varchar(200),"
                    + "Verify1 integer default 0 NOT NULL,"
                    + "Verify2 integer default 0 NOT NULL, "
                    + "SendDate varchar(200))"
                    );

                   dbHelper.ExecuteNonQuery("Create Index  IF NOT EXISTS Index_productId on mail_address(Email);");
                   dbHelper.ExecuteNonQuery("Create Index  IF NOT EXISTS Index_productType on mail_address(ProductType);");
        }



        public List<MailModel> GetMailModelList(MailModel searchItem)
        {
            string sql = "SELECT id, Email, Company, Country, Username, Subject,ProductType, Source, Verify1,Verify2, SendDate FROM mail_address";
            SQLiteParameter[] parameter = null;
            if (searchItem != null)
            {
                List<SQLiteParameter> parameterList = new List<SQLiteParameter>();
                string condition = " where 1 = 1";
                if (!string.IsNullOrEmpty(searchItem.Email))
                {
                    condition += " and Email like '%"+searchItem.Email+"%'";

                }
                if (!string.IsNullOrEmpty(searchItem.Username)) {
                    condition += " and Username like '%" + searchItem.Username + "%'";
                }
                if (!string.IsNullOrEmpty(searchItem.ProductType))
                {
                    condition += " and ProductType = @ProductType";
                    parameterList.Add(new SQLiteParameter("@ProductType", searchItem.ProductType));
                }
                if (!string.IsNullOrEmpty(searchItem.Source))
                {
                    condition += " and Source = @Source";
                    parameterList.Add(new SQLiteParameter("@Source", searchItem.Source));
                }
                if (searchItem.Verify1 >= 0)
                {
                    condition += " and Verify1 = " + searchItem.Verify1;
                }
                if (!string.IsNullOrEmpty(searchItem.Country))
                {
                    condition += " and Country = @Country";
                    parameterList.Add(new SQLiteParameter("@Country", searchItem.Country));
                }
                sql = sql + condition;
                parameter = parameterList.ToArray();

            }

            DataTable dt = dbHelper.ExecuteDataTable(sql, parameter);

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
                model.Source = Convert.ToString(row["Source"]);
                model.Verify1 = Convert.ToInt32(row["Verify1"]);
                model.Verify2 = Convert.ToInt32(row["Verify2"]);
                model.SendDate = Convert.ToString(row["SendDate"]);
                list.Add(model);
            }
            return list;
        }


        public void Insert(List<MailModel> list)
        {
            string existSql = @"select count(1) from mail_address where Email= @Email";
            List<string> existEmail = new List<string>();
            List<SQLiteParameter[]> parameters = new List<SQLiteParameter[]>();
            foreach (MailModel item in list)
            {
                string emailAndProductType = item.Email + "_" + item.ProductType;
                if (existEmail.Contains(emailAndProductType))
                {
                    continue;
                }
                SQLiteParameter[] existParameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("@Email",item.Email)
                };
                int count = Convert.ToInt32(dbHelper.ExecuteScalar(existSql, existParameter));
                if (count > 0)
                {
                    continue;
                }
                existEmail.Add(emailAndProductType);
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("@Email",item.Email), 
                    new SQLiteParameter("@Company", item.Company), 
                    new SQLiteParameter("@Country",item.Country), 
                    new SQLiteParameter("@Username",item.Username), 
                    new SQLiteParameter("@Subject",item.Subject), 
                    new SQLiteParameter("@ProductType",item.ProductType), 
                    new SQLiteParameter("@Source",item.Source), 
                    new SQLiteParameter("@SendDate",item.SendDate), 
                };
                parameters.Add(parameter);
            }

            string sql = @"INSERT INTO mail_address(Email, Company, Country, Username, Subject,ProductType, Source, SendDate)"
                + "values(@Email,@Company,@Country,@Username,@Subject,@ProductType,@Source, @SendDate)";
            dbHelper.ExecuteNonQuery(sql, parameters);

        }

        public void UpdateAllMailVerifyToZreo()
        {
            string sql = @"update mail_address set Verify1 = 0";
            dbHelper.ExecuteNonQuery(sql);
        }

        public void UpdateMailVerify(MailModel item)
        {
            string sql = @"update mail_address set Verify1 = " + item.Verify1 + " where id = " + item.Id;
            dbHelper.ExecuteNonQuery(sql);     
        }

        public List<ListItem> GetComboBoxData(string ColumnName)
        {
            string sql = "SELECT distinct " + ColumnName + " FROM mail_address where " 
                + ColumnName + " is not null and " + ColumnName + " != ''";
            DataTable dt = dbHelper.ExecuteDataTable(sql, null);
            List<ListItem> list = new List<ListItem>();
            foreach (DataRow row in dt.Rows)
            {
                string v = Convert.ToString(row[ColumnName]);
                list.Add(new ListItem(v,v));
            }
            return list;
        }

    }
}
