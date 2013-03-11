using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using Soomes;
using MySql.Data.MySqlClient;

namespace Database
{
    public class AppDicDAOMysql : IAppDicDAO
    {
        private MysqlDBHelper dbHelper;

        public AppDicDAOMysql(MysqlDBHelper dbHelper)
        { 
            this.dbHelper = dbHelper;
            CreateTable();
        }

         private void CreateTable()
         {
             dbHelper.ExecuteNonQuery
              (
                "CREATE TABLE IF NOT EXISTS AppDics("
              + "`Id` integer NOT NULL PRIMARY KEY AUTO_INCREMENT UNIQUE,"
              + "`Type` varchar(50) NOT NULL,"
              + "`Key` varchar(50) NOT NULL,"
              + "`Value` varchar(100),"
              + " Index Index_Type (`Type`)) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin "
              );
         }

         public string GetValue(string type, string key)
         {
             string sql = "SELECT `Value` FROM AppDics WHERE `Type` = @Type and `Key` = @Key";
             List<MySqlParameter> parameters = new List<MySqlParameter>();
             parameters.Add(new MySqlParameter("@Type", type));
             parameters.Add(new MySqlParameter("@Key", key));
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
             string sql = "SELECT Id, `Type`, `Key`, `Value` FROM AppDics WHERE Type = @Type";
             List<MySqlParameter> parameters = new List<MySqlParameter>();
             parameters.Add(new MySqlParameter("@Type", type));
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
             string ExistRecordSql = "SELECT count(1) FROM AppDics WHERE `Type` = @Type and `Key` = @Key";
             List<MySqlParameter> queryparames = new List<MySqlParameter>();
             queryparames.Add(new MySqlParameter("@Type", type));
             queryparames.Add(new MySqlParameter("@Key", key));
            int record = Convert.ToInt32(dbHelper.ExecuteScalar(ExistRecordSql, queryparames.ToArray()));
            if (record > 0)
            {
                string UpdSql = @"UPDATE  AppDics SET `Value` =@Value where `Type` = @Type and `Key` = @Key";
                MySqlParameter[] parameter = new MySqlParameter[]
                {
                    new MySqlParameter("@Type", type), 
                    new MySqlParameter("@Key", key), 
                    new MySqlParameter("@Value",value)
                };
                dbHelper.ExecuteNonQuery(UpdSql, parameter);
            }
            else
            {
                string InsSql = @"INSERT INTO AppDics(`Type`, `Key`, `Value`) values(@Type, @Key,@Value)";
                MySqlParameter[] parameter = new MySqlParameter[]
                {
                    new MySqlParameter("@Type", type),
                    new MySqlParameter("@Key", key),
                    new MySqlParameter("@Value",value)
                };
                dbHelper.ExecuteNonQuery(InsSql, parameter);
            }
         }

         public void DeleteAppDic(string type, string key)
         {
             string DelSql = @"delete from AppDics WHERE `Type` = @Type and `Key` = @Key";
            MySqlParameter[] parameter = new MySqlParameter[]
            {
                new MySqlParameter("@Type", type), 
                new MySqlParameter("@Key", key)
            };
            dbHelper.ExecuteNonQuery(DelSql, parameter);
         }
    }


}
