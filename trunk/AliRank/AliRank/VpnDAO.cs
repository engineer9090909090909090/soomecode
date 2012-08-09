using System;
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
            CreateTable();
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
                    + "VpnType varchar(50),"
                    + "L2tpSec varchar(20) NOT NULL,"
                    + "createTime datetime,"
                    + "updateTime datetime)"
                    );
        }


        public bool ExistAddress(string address)
        {
            string sql = "select count(1) from vpns where Addrsss= @Address";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@Address", address)
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
                "SELECT Id, Address, Username, Password, VpnType, L2tpSec,updateTime FROM vpns", null);

            List<VpnModel> list = new List<VpnModel>();
            foreach (DataRow row in dt.Rows)
            {
                VpnModel model = new VpnModel();
                model.Id = Convert.ToInt32(row["id"]);
                model.Address = (string)row["Address"];
                model.Username = (string)row["Username"];
                model.Password = (string)row["Password"];
                model.Password = (string)row["Password"];
                model.L2tpSec = (string)row["L2tpSec"];
                model.UpdateTime = Convert.ToDateTime(row["updateTime"]);
                list.Add(model);
            }
            return list;
        }


        public void Insert(VpnModel model)
        {
            string sql = @"INSERT INTO vpns(Address, Username, Password, VpnType, L2tpSec, createTime, updateTime)"
                            + "values(@Address,@Username,@Password,@VpnType,@L2tpSec, @createTime, @updateTime)";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@Address",model.Address), 
                new SQLiteParameter("@Username", model.Username), 
                new SQLiteParameter("@Password",model.Password), 
                new SQLiteParameter("@VpnType",model.VpnType), 
                new SQLiteParameter("@L2tpSec",model.L2tpSec), 
                new SQLiteParameter("@createTime",DateTime.Now), 
                new SQLiteParameter("@updateTime",DateTime.Now) 
            };
            dbHelper.ExecuteNonQuery(sql, parameter);        
        }

        public void UpdateUserPassword(VpnModel model)
        {
            string sql = @"UPDATE  vpns SET Username =@Username, Password =@Password, updateTime = @updateTime";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@Username", model.Username), 
                new SQLiteParameter("@Password",model.Password), 
                new SQLiteParameter("@updateTime",DateTime.Now) 
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }

        public void DeleteVpn(List<string> idList)
        {
            foreach (string id in idList)
            {
                string sql = @"delete form vpns where id = @id";
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("@id", id)
                };
                dbHelper.ExecuteNonQuery(sql, parameter);
            }
        }

    }
}
