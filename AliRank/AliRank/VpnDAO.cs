﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace AliRank
{
    class VpnDAO
    {
        private SQLiteDBHelper dbHelper;

        public VpnDAO(SQLiteDBHelper dbHelper)
        { 
            this.dbHelper = dbHelper;
            //DropTable();
            CreateTable();
        }
        private void DropTable()
        {
            dbHelper.ExecuteNonQuery("DROP TABLE IF EXISTS vpns");
        }
        private void CreateTable()
        {
                   dbHelper.ExecuteNonQuery
                    (
                      "CREATE TABLE IF NOT EXISTS vpns("
                    + "Id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"
                    + "Address varchar(20) NOT NULL,"
                    + "Username varchar(50) NOT NULL,"
                    + "Password varchar(50) NOT NULL,"
                    + "Country varchar(100) NOT NULL,"
                    + "Name varchar(100),"
                    + "VpnType varchar(50),"
                    + "L2tpSec varchar(20),"
                    + "ConnQty integer default 0,"
                    + "Status integer default 0,"
                    + "createTime datetime,"
                    + "updateTime datetime)"
                    );
        }


        public bool ExistAddress(string address, string type)
        {
            string sql = "select count(1) from vpns where Address= @Address and VpnType=@VpnType";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@Address", address),
                new SQLiteParameter("@VpnType", type)
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


        public List<VpnModel> GetVpnModelList()
        {
            DataTable dt = dbHelper.ExecuteDataTable(
                "SELECT Id, Address, Username, Password, Country,Name, VpnType, L2tpSec,updateTime,Status FROM vpns order by updateTime desc", null);

            List<VpnModel> list = new List<VpnModel>();
            foreach (DataRow row in dt.Rows)
            {
                VpnModel model = new VpnModel();
                model.Id = Convert.ToInt32(row["id"]);
                model.Address = (string)row["Address"];
                model.Username = (string)row["Username"];
                model.Password = (string)row["Password"];
                model.Country = (string)row["Country"];
                model.Name = (string)row["Name"];
                model.VpnType = (string)row["VpnType"];
                model.L2tpSec = (string)row["L2tpSec"];
                model.Status = Convert.ToInt32(row["Status"]);
                model.UpdateTime = Convert.ToDateTime(row["updateTime"]);
                list.Add(model);
            }
            return list;
        }


        public void Insert(VpnModel model)
        {
            string sql = @"INSERT INTO vpns(Address, Username, Password, Country, Name, VpnType, L2tpSec, createTime, updateTime)"
                            + "values(@Address,@Username,@Password, @Country, @Name, @VpnType,@L2tpSec, @createTime, @updateTime)";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@Address",model.Address), 
                new SQLiteParameter("@Username", model.Username), 
                new SQLiteParameter("@Password",model.Password), 
                new SQLiteParameter("@Country",model.Country), 
                new SQLiteParameter("@Name",model.Name), 
                new SQLiteParameter("@VpnType",model.VpnType), 
                new SQLiteParameter("@L2tpSec",model.L2tpSec), 
                new SQLiteParameter("@createTime",DateTime.Now), 
                new SQLiteParameter("@updateTime",DateTime.Now) 
            };
            dbHelper.ExecuteNonQuery(sql, parameter);        
        }

        public void UpdateUserPassword(VpnModel model)
        {
            string sql = @"UPDATE  vpns SET Username =@Username, Password =@Password, updateTime = @updateTime where id = @id";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@Username", model.Username), 
                new SQLiteParameter("@Password",model.Password), 
                new SQLiteParameter("@updateTime",DateTime.Now),
                new SQLiteParameter("@id",model.Id)
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }


        public void ImportVpns(List<VpnModel> list)
        {
            string InsSql = @"INSERT INTO vpns(Address, Username, Password, Country, Name, VpnType, L2tpSec, createTime, updateTime)"
                            + "values(@Address,@Username,@Password, @Country, @Name, @VpnType,@L2tpSec, @createTime, @updateTime)";

            string UpdSql = @"UPDATE  vpns SET Username =@Username, Password =@Password, Country = @Country, Name = @Name,"
                + " VpnType = @VpnType, L2tpSec = @L2tpSec, updateTime = @updateTime where Address = @Address ";

            string ExistRecordSql = "SELECT count(1) FROM vpns WHERE Address = '{0}'";
            List<SQLiteParameter[]> InsertParameters = new List<SQLiteParameter[]>();
            List<SQLiteParameter[]> UpdateParameters = new List<SQLiteParameter[]>();
            foreach (VpnModel model in list)
            {
                int record = Convert.ToInt32(dbHelper.ExecuteScalar(string.Format(ExistRecordSql, model.Address), null));
                if (record > 0)
                {
                    SQLiteParameter[] parameter = new SQLiteParameter[]
                    {
                        new SQLiteParameter("@Username", model.Username), 
                        new SQLiteParameter("@Password",model.Password), 
                        new SQLiteParameter("@Country",model.Country), 
                        new SQLiteParameter("@Name",model.Name), 
                        new SQLiteParameter("@VpnType",model.VpnType), 
                        new SQLiteParameter("@L2tpSec",model.L2tpSec), 
                        new SQLiteParameter("@updateTime",DateTime.Now),
                        new SQLiteParameter("@Address",model.Address)
                    };
                    UpdateParameters.Add(parameter);
                }
                else
                {
                    SQLiteParameter[] parameter = new SQLiteParameter[]
                    {
                       new SQLiteParameter("@Address",model.Address), 
                        new SQLiteParameter("@Username", model.Username), 
                        new SQLiteParameter("@Password",model.Password), 
                        new SQLiteParameter("@Country",model.Country), 
                        new SQLiteParameter("@Name",model.Name), 
                        new SQLiteParameter("@VpnType",model.VpnType), 
                        new SQLiteParameter("@L2tpSec",model.L2tpSec), 
                        new SQLiteParameter("@createTime",DateTime.Now), 
                        new SQLiteParameter("@updateTime",DateTime.Now) 
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

        public void UpdateAllVPNStatus(int status)
        {
            string sql = @"UPDATE  vpns SET Status =@Status";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@Status",status)
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }

        public VpnModel GetEffctiveVPN()
        {
            DataTable dt = dbHelper.ExecuteDataTable(
            "SELECT Id, Address, Username, Password, Country,Name, VpnType, L2tpSec,updateTime, ConnQty, Status "
            +"FROM vpns where status = 1 order by ConnQty asc, updateTime asc limit 0, 1", null);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0]; 
                VpnModel model = new VpnModel();
                model.Id = Convert.ToInt32(row["id"]);
                model.Address = (string)row["Address"];
                model.Username = (string)row["Username"];
                model.Password = (string)row["Password"];
                model.Country = (string)row["Country"];
                model.Name = (string)row["Name"];
                model.VpnType = (string)row["VpnType"];
                model.L2tpSec = (string)row["L2tpSec"];
                model.ConnQty = Convert.ToInt32(row["ConnQty"]);
                model.Status = Convert.ToInt32(row["Status"]);
                model.UpdateTime = Convert.ToDateTime(row["updateTime"]);
                return model;
            }
            return null;
        }

        public VpnModel GetVpnModelByIpAddress(string ipAddress)
        {
            string sql = "SELECT Id, Address, Username, Password, Country,Name, VpnType, L2tpSec,updateTime, ConnQty, Status "
            + "FROM vpns where status = 1 Address = @Address";
            DataTable dt = dbHelper.ExecuteDataTable(sql, 
                new SQLiteParameter[]{
                    new SQLiteParameter("@Address",ipAddress)
                }
            );
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0]; 
                VpnModel model = new VpnModel();
                model.Id = Convert.ToInt32(row["id"]);
                model.Address = (string)row["Address"];
                model.Username = (string)row["Username"];
                model.Password = (string)row["Password"];
                model.Country = (string)row["Country"];
                model.Name = (string)row["Name"];
                model.VpnType = (string)row["VpnType"];
                model.L2tpSec = (string)row["L2tpSec"];
                model.ConnQty = Convert.ToInt32(row["ConnQty"]);
                model.Status = Convert.ToInt32(row["Status"]);
                model.UpdateTime = Convert.ToDateTime(row["updateTime"]);
                return model;
            }
            return GetEffctiveVPN();
        }

        public void UpdateVPNStatus(string Address, int status)
        {
            string sql = @"UPDATE  vpns SET Status =@Status, updateTime = @updateTime where Address = @Address";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@Status",status),
                new SQLiteParameter("@updateTime",DateTime.Now),
                new SQLiteParameter("@Address",Address)
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }

        public void AddVPNConnQty(string Address, int status)
        {
            string sql = @"UPDATE vpns SET ConnQty = ConnQty + 1, Status =1, updateTime = @updateTime where Address = @Address";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@updateTime",DateTime.Now),
                new SQLiteParameter("@Address",Address)
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }


        public void DeleteVpn(List<string> idList)
        {
            foreach (string id in idList)
            {
                string sql = @"DELETE FROM vpns WHERE id = @id";
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("@id", Convert.ToInt32(id))
                };
                dbHelper.ExecuteNonQuery(sql, parameter);
            }
        }

    }
}
