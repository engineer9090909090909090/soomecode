using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace AliRank
{
    class ProfileDAO
    {
         private SQLiteDBHelper dbHelper;

         public ProfileDAO(SQLiteDBHelper dbHelper)
        { 
            this.dbHelper = dbHelper;
            CreateTable();
        }

         private void CreateTable()
         {
             dbHelper.ExecuteNonQuery
              (
                "CREATE TABLE IF NOT EXISTS profiles("
              + "Id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"
              + "Key varchar(50) NOT NULL,"
              + "Value varchar(100))"
              );
         }

         public string GetValue(string key)
         {
             string ExistRecordSql = "SELECT Value FROM profiles WHERE Key = '{0}'";
             object val = dbHelper.ExecuteScalar(string.Format(ExistRecordSql, key),null);
             if (Convert.IsDBNull(val))
             {  
                 return string.Empty;
             }
             return Convert.ToString(val);
         }

         public void SetValue(string key, string value)
         {
             string ExistRecordSql = "SELECT count(1) FROM profiles WHERE Key = '{0}'";
            int record = Convert.ToInt32(dbHelper.ExecuteScalar(string.Format(ExistRecordSql, key), null));
            if (record > 0)
            {
                string UpdSql = @"UPDATE  profiles SET Value =@Value where Key = @Key ";
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("@Key", key), 
                    new SQLiteParameter("@Value",value)
                };
                dbHelper.ExecuteNonQuery(UpdSql, parameter);
            }
            else
            {
                string InsSql = @"INSERT INTO profiles(Key, Value) values(@Key,@Value)";
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                        new SQLiteParameter("@Key", key), 
                    new SQLiteParameter("@Value",value)
                };
                dbHelper.ExecuteNonQuery(InsSql, parameter);
            }
         }
    }


}
