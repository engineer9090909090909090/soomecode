﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using Soomes;

namespace Database
{
    public class AppDicDAO : IAppDicDAO
    {
         private SQLiteDBHelper dbHelper;

         public AppDicDAO(SQLiteDBHelper dbHelper)
        { 
            this.dbHelper = dbHelper;
            CreateTable();
        }

         private void CreateTable()
         {
             dbHelper.ExecuteNonQuery
              (
                "CREATE TABLE IF NOT EXISTS AppDics("
              + "Id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"
              + "Type varchar(50) NOT NULL,"
              + "Key varchar(50) NOT NULL,"
              + "Value varchar(100))"
              );
             dbHelper.ExecuteNonQuery("Create Index  IF NOT EXISTS Index_key on AppDics(Type);");
         }

         public string GetValue(string type, string key)
         {
             string sql = "SELECT Value FROM AppDics WHERE Type = @Type and Key = @Key";
             List<SQLiteParameter> parameters = new List<SQLiteParameter>();
             parameters.Add(new SQLiteParameter("@Type", type));
             parameters.Add(new SQLiteParameter("@Key", key));
             object val = dbHelper.ExecuteScalar(sql,parameters.ToArray());
             if (Convert.IsDBNull(val))
             {  
                 return string.Empty;
             }
             return Convert.ToString(val);
         }

         public List<AppDic> GetOptions(string type)
         {
             List<AppDic> list = new List<AppDic>();
             string sql = "SELECT Id, Type, Key, Value FROM AppDics WHERE Type = @Type";
             List<SQLiteParameter> parameters = new List<SQLiteParameter>();
             parameters.Add(new SQLiteParameter("@Type", type));
             DataTable dt = dbHelper.ExecuteDataTable(sql, parameters.ToArray());
             Dictionary<string, string> dic = new Dictionary<string, string>();
             foreach (DataRow row in dt.Rows)
             {
                 AppDic item = new AppDic();
                 item.Type = (string)row["Type"];
                 item.Key = (string)row["Key"];
                 item.Label = (string)row["Value"];
                 list.Add(item);
             }
             return list;
         }

         public void SetValue(string type, string key, string value)
         {
             string ExistRecordSql = "SELECT count(1) FROM AppDics WHERE Type = @Type and Key = @Key";
             List<SQLiteParameter> queryparames = new List<SQLiteParameter>();
             queryparames.Add(new SQLiteParameter("@Type", type));
             queryparames.Add(new SQLiteParameter("@Key", key));
            int record = Convert.ToInt32(dbHelper.ExecuteScalar(ExistRecordSql, queryparames.ToArray()));
            if (record > 0)
            {
                string UpdSql = @"UPDATE  AppDics SET Value =@Value where Type = @Type and Key = @Key";
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("@Type", type), 
                    new SQLiteParameter("@Key", key), 
                    new SQLiteParameter("@Value",value)
                };
                dbHelper.ExecuteNonQuery(UpdSql, parameter);
            }
            else
            {
                string InsSql = @"INSERT INTO AppDics(Type, Key, Value) values(@Type, @Key,@Value)";
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("@Type", type),
                    new SQLiteParameter("@Key", key),
                    new SQLiteParameter("@Value",value)
                };
                dbHelper.ExecuteNonQuery(InsSql, parameter);
            }
         }

         public void DeleteAppDic(string type, string key)
         {
             string DelSql = @"delete from AppDics WHERE Type = @Type and Key = @Key";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@Type", type), 
                new SQLiteParameter("@Key", key)
            };
            dbHelper.ExecuteNonQuery(DelSql, parameter);
         }
    }


}
