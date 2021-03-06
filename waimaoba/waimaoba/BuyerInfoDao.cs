﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace com.soomes
{
    public class BuyerInfoDao
    {
        private SQLiteDBHelper dbHelper;

        public BuyerInfoDao(SQLiteDBHelper dbHelper)
        { 
            this.dbHelper = dbHelper;
            CreateTable();
            UpdateTable();
        }

        protected void CreateTable()
        {
            //Type, CompanyName, CompanyInfo, Email,BuyerName, ContactInfo, Url, UrlTitle, Status
            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS BuyerInfo("
            + "Id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"
            + "Type varchar(500),"
            + "CompanyName varchar(100),"
            + "CompanyInfo varchar(500),"
            + "ContactInfo varchar(500),"
            + "Category varchar(100) not null,"
            + "BuyerName varchar(100),"
            + "Url varchar(200),"
            + "UrlTitle varchar(500),"
            + "Mail varchar(100) not null,"
            + "Status integer default 0)");

            dbHelper.ExecuteNonQuery("Create Index  IF NOT EXISTS Index_key on BuyerInfo(Email);");
        }

        protected void UpdateTable()
        {

        }


        public List<BuyerInfo> GetBuyerInforList(QueryObject<BuyerInfo> query)
        {
            string sql = "SELECT Id, Type, CompanyName, CompanyInfo, Category, Mail,BuyerName, ContactInfo, Url, UrlTitle, Status FROM BuyerInfo";
            if (query.IsPager)
            {
                query.RecordCount = dbHelper.GetItemCount(sql, null);
                sql = sql + " order by Id asc limit " + query.Start + ", " + query.PageSize;
            }
            DataTable dt = dbHelper.ExecuteDataTable(sql, null);
            List<BuyerInfo> list = new List<BuyerInfo>();
            foreach (DataRow row in dt.Rows)
            {
                BuyerInfo kw = new BuyerInfo();
                kw.Id = Convert.ToInt32(row["Id"]);
                kw.Type = (string)row["Type"];
                kw.CompanyName = (string)row["CompanyName"];
                kw.CompanyInfo = (string)row["CompanyInfo"];
                kw.Category = (string)row["Category"];
                kw.Mail = (string)row["Mail"];
                kw.BuyerName = (string)row["BuyerName"];
                kw.ContactInfo = (string)row["ContactInfo"];
                kw.Url = (string)row["Url"];
                kw.UrlTitle = (string)row["UrlTitle"];
                kw.Status = Convert.ToInt32(row["Status"]);
                list.Add(kw);
            }
            return list;
        }


        public bool ExistEmail(string email)
        {
            string sql = "SELECT count(1) FROM BuyerInfo where Mail = '" + email.Trim() + "'";
            int result = Convert.ToInt32(dbHelper.ExecuteScalar(sql, null));
            if (result > 0)
            {
                return true;
            }
            return false;
        }


        public void Insert(BuyerInfo item)
        {
            if (item.Emails.Count == 0)
            {
                return;
            }
            string InsSql = @"Insert into BuyerInfo(Type, CompanyName, CompanyInfo, Category, Mail,BuyerName, ContactInfo, Url, UrlTitle) "
                            + "values(@Type, @CompanyName, @CompanyInfo, @Category, @Mail, @BuyerName, @ContactInfo, @Url, @UrlTitle)";


            foreach (string mail in item.Emails)
            {
                if (ExistEmail(mail))
                {
                    continue;
                }
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("@Type",item.Type),
                    new SQLiteParameter("@CompanyName",item.CompanyName),
                    new SQLiteParameter("@CompanyInfo",item.CompanyInfo),
                    new SQLiteParameter("@Category",string.IsNullOrEmpty(item.Category)?"-":item.Category),
                    new SQLiteParameter("@Mail", mail),
                    new SQLiteParameter("@BuyerName",item.BuyerName),
                    new SQLiteParameter("@ContactInfo",item.ContactInfo), 
                    new SQLiteParameter("@Url",item.Url),
                    new SQLiteParameter("@UrlTitle",item.UrlTitle)
                };
                dbHelper.ExecuteNonQuery(InsSql, parameter);
            }
                
        }
    }
}
