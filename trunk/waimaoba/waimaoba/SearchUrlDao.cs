using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace com.soomes
{
    public class SearchUrlDao
    {
        private SQLiteDBHelper dbHelper;

        public SearchUrlDao(SQLiteDBHelper dbHelper)
        { 
            this.dbHelper = dbHelper;
            CreateTable();
            UpdateTable();
        }

        protected void CreateTable()
        {
            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS SearchUrls("
            + "Id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"
            + "Url varchar(500) not null)");

            dbHelper.ExecuteNonQuery("Create Index  IF NOT EXISTS Index_key on SearchUrls(Url);");
        }

        protected void UpdateTable()
        {

        }


        public List<string> GetBuyerInforList(QueryObject<string> query)
        {
            string sql = "SELECT Url FROM SearchUrls";
            if (query.IsPager)
            {
                query.RecordCount = dbHelper.GetItemCount(sql, null);
                sql = sql + " order by Id asc limit " + query.Start + ", " + query.PageSize;
            }
            DataTable dt = dbHelper.ExecuteDataTable(sql, null);
            List<string> list = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                string url = (string)row["Url"];
                list.Add(url);
            }
            return list;
        }


        public bool ExistUrl(string url)
        {
            string sql = "SELECT count(1) FROM SearchUrls where Url = @Url";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@Url",url)
            };
            int result = Convert.ToInt32(dbHelper.ExecuteScalar(sql, parameter));
            if (result > 0)
            {
                return true;
            }
            return false;
        }


        public void Insert(string url)
        {
            string InsSql = @"Insert into SearchUrls ( Url) values(@Url)";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@Url",url)
            };
            dbHelper.ExecuteNonQuery(InsSql, parameter);
        }
    }
}
