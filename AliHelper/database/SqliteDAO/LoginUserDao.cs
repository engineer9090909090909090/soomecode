﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Data.SQLite;
using System.Data;

namespace Database
{
    public class LoginUserDao : ILoginUserDao
    {
        private SQLiteDBHelper dbHelper;

        public LoginUserDao(SQLiteDBHelper dbHelper)
        { 
            this.dbHelper = dbHelper;
            CreateTable();
            UpdateTable();
        }

        protected void CreateTable()
        {

            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS LoginUser("
            + "Id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"
            + "UserId varchar(100) NOT NULL,"
            + "Password varchar(100) NOT NULL,"
            + "Name varchar(300) NOT NULL,"
            + "Description varchar(500),"
            + "Status varchar(10) default 'A')");

            dbHelper.ExecuteNonQuery("Create Index  IF NOT EXISTS Index_key on LoginUser(UserId);");
            dbHelper.ExecuteNonQuery("Insert Into LoginUser(UserId, Password, Name, Description) values('admin','admin','admin','admin');");
        }

        protected void UpdateTable()
        {

        }


        public List<LoginUser> GetLoginUserList()
        {
            DataTable dt = dbHelper.ExecuteDataTable(
                  "SELECT Id, UserId, Password, Name, Description, Status FROM LoginUser", null);
            List<LoginUser> list = new List<LoginUser>();
            foreach (DataRow row in dt.Rows)
            {
                LoginUser kw = new LoginUser();
                kw.Id = Convert.ToInt32(row["Id"]);
                kw.UserId = (string)row["UserId"];
                kw.Password = (string)row["Password"];
                kw.Name = (string)row["Name"];
                kw.Description = (string)row["Description"];
                kw.Status = (string)row["Status"];
                list.Add(kw);
            }
            return list;
        }


        public LoginUser GetUserByUserId(string userId)
        {
            string sql = "SELECT Id, UserId, Password, Name, Description, Status FROM LoginUser where UserId = @UserId";
            SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("@UserId",userId)
                };
            DataTable dt = dbHelper.ExecuteDataTable(sql, parameter);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                LoginUser kw = new LoginUser();
                kw.Id = Convert.ToInt32(row["Id"]);
                kw.UserId = (string)row["UserId"];
                kw.Password = (string)row["Password"];
                kw.Name = (string)row["Name"];
                kw.Description = (string)row["Description"];
                kw.Status = (string)row["Status"];
                return kw;
            }
            return null;
        }


        public void Insert(LoginUser item)
        {
            string InsSql = @"Insert Into LoginUser(UserId, Password, Name, Description) "
                            + "values(@UserId,@Password, @Name, @Description)";

                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("@UserId",item.UserId),
                    new SQLiteParameter("@Password",item.Password), 
                    new SQLiteParameter("@Name",item.Name),
                    new SQLiteParameter("@Description",item.Description)
                };
                dbHelper.ExecuteNonQuery(InsSql, parameter);
        }

        public void Update(LoginUser item)
        {
            string UpdSql = @"Update LoginUser SET UserId = @UserId, Password = @Password, Name = @Name, "
                   + "Description = @Description, Status = @Status WHERE Id = @Id";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@UserId",item.UserId), 
                new SQLiteParameter("@Password",item.Password),
                new SQLiteParameter("@Name",item.Name),
                new SQLiteParameter("@Description",item.Description),
                new SQLiteParameter("@Status",item.Status),
                new SQLiteParameter("@Id",item.Id)
            };
            dbHelper.ExecuteNonQuery(UpdSql, parameter);
        }

        public void DeleteGroups(int Id)
        {
            dbHelper.ExecuteNonQuery(@"delete from AliGroups WHERE Id = " + Id);
        }
    }
}
