using System;
using System.Data;
using System.Collections.Generic;
using com.soomes.model;

namespace DBUtility.SQLite
{
    public class DAO
    {
        public static string connectionString = "Data Source=AliClicker.sqlite";

        public static DataSet GetAllClickerList()
        {
            string sql = "select id,operate,company_url,key_word,product_id,enabled,clicked_num,page_rank  from clicker_list";
            return SQLiteHelper.ExecuteDataSet(connectionString, sql, null);        
        }

        public static IList<ClickerModel> GetClickerModelList()
        {
            string sql = "select id,operate,company_url,key_word,product_id,enabled,clicked_num,page_rank  from clicker_list";
            DataSet ds = SQLiteHelper.ExecuteDataSet(connectionString, sql, null);
            IList<ClickerModel> list = new List<ClickerModel>();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ClickerModel model = null;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    model = new ClickerModel();
                    model.Id = (Int64)dr[0];
                    model.Operate = (bool)dr[1];
                    model.CompanyUrl = (string)dr[2];
                    model.KeyWord = (string)dr[3];
                    model.ProductId = (string)dr[4];
                    model.Enabled = (bool)dr[5];
                    model.ClickedNum = (Int64)dr[6];
                    model.PageRank = (Int64)dr[7];
                    list.Add(model);
                }
                model = null;
            }
            return list;
        }
         

        public static void AddClicker(ClickerModel model)
        {
            string sql = "insert into clicker_list (product_id, operate, company_url, key_word, enabled,clicked_num,page_rank) values(@1, @2, @3, @4, @5, @6, @7)";
            object[] parampters = new object[]
            {
                model.ProductId,
                model.Operate,
                model.CompanyUrl,
                model.KeyWord,
                model.Enabled,
                0,
                0
            };
            SQLiteHelper.ExecuteNonQuery(connectionString, sql, parampters);
        }

        public static int UpdateClicker(ClickerModel model)
        {
            string sql = "update clicker_list  set product_id = @product_id, company_url = @company_url, key_word = @key_word, enabled = @enabled ,clicked_num = @clicked_num ,page_rank = @page_rank where id= @id";
            object[] parampters = new object[]
            {
                model.ProductId,
                model.CompanyUrl,
                model.KeyWord,
                model.Enabled,
                model.ClickedNum,
                model.PageRank,
                model.Id
            };
            return SQLiteHelper.ExecuteNonQuery(connectionString, sql, parampters);
        }

        public static void DeleteClicker(long id)
        {
            string sql = "delete from clicker_list where id = @id";
            object[] parampters = new object[] { id };
            SQLiteHelper.ExecuteNonQuery(connectionString, sql, parampters);
        }


        

        public static IList<VpnModel> GetAllVpnAddress()
        {
            string sql = "SELECT id,ip,userid,password,type,enabled FROM vpn_list";
            DataSet ds = SQLiteHelper.ExecuteDataSet(connectionString, sql, null);
            IList<VpnModel> list = new List<VpnModel>();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                VpnModel model = null;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    model = new VpnModel();
                    model.Id = (Int64)dr[0];
                    model.Ip = (string)dr[1];
                    model.UserId = (string)dr[2];
                    model.Password = (string)dr[3];
                    model.Type = (string)dr[4];
                    model.Enabled = (bool)dr[5];
                    list.Add(model);
                }
            }
            return list;
        }

        public static bool HasExistIpAddress(string ip)
        {
            string sql = "SELECT id FROM vpn_list where ip = @ip";
            object[] parampters = new object[]{ip};
            object o = SQLiteHelper.ExecuteScalar(connectionString, sql, parampters);
            if (o != null)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        public static void AddVpn(VpnModel model)
        {
            string sql = "insert into vpn_list (ip,userid,password,type,enabled) values(@1, @2, @3, @4, @5)";
            object[] parampters = new object[]
            {
                model.Ip.Trim(),
                model.UserId,
                model.Password,
                model.Type,
                model.Enabled
            };
            SQLiteHelper.ExecuteNonQuery(connectionString, sql, parampters);
        }


        public static void AddProxy(ProxyIpModel model)
        {
            string sql = "insert into proxy_ip_list (ip,ip_desc,check_time,use_number,last_use_time,enabled) values(@1, @2, @3, @4, @5, @6)";
            object[] parampters = new object[]
            {
                model.Ip.Trim(),
                model.IpDesc.Trim(),
                model.CheckTime,
                model.UseNumber,
                model.LastUseTime,
                model.Enabled
            };
            SQLiteHelper.ExecuteNonQuery(connectionString, sql, parampters);
        }

        public static int UpdateAllProxyToDisabled()
        {
            string sql = "update proxy_ip_list set enabled = @enabled";
            object[] parampters = new object[] { false };
            return SQLiteHelper.ExecuteNonQuery(connectionString, sql, parampters);
        }

        public static int UpdateProxy(ProxyIpModel model)
        {
            string sql = "update proxy_ip_list  set check_time = @1, enabled = @2 where ip= @3";
            object[] parampters = new object[]{ model.CheckTime, model.Enabled, model.Ip};
            return SQLiteHelper.ExecuteNonQuery(connectionString, sql, parampters);
        }

        public static int UpdateProxyToUsed(int id)
        {
            string sql = "update proxy_ip_list  set use_number = use_number + 1, last_use_time = @1 where id= @2";
            object[] parampters = new object[] { DateTime.Now, id };
            return SQLiteHelper.ExecuteNonQuery(connectionString, sql, parampters);
        }

        public static bool HasExistProxy(string ip)
        {
            string sql = "SELECT id FROM proxy_ip_list where ip = @ip";
            object[] parampters = new object[] { ip };
            object o = SQLiteHelper.ExecuteScalar(connectionString, sql, parampters);
            if (o != null)
            {
                return true;
            }else{
                return false;
            }
        }

        public static IList<ProxyIpModel> GetEnableProxyList()
        {
            string sql = "select id,ip,ip_desc from proxy_ip_list where enabled = 1 order by last_use_time asc ";
            DataSet ds =  SQLiteHelper.ExecuteDataSet(connectionString, sql, null);
            IList<ProxyIpModel> list = new List<ProxyIpModel>();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ProxyIpModel model = null;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    model = new ProxyIpModel();
                    model.Id = (Int64)dr[0];
                    model.Ip = (string)dr[1];
                    model.IpDesc = (string)dr[2];
                    list.Add(model);
                }
            }
            return list;
        }
    }
}
