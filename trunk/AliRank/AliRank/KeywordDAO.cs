using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Drawing;

namespace AliRank
{
    class KeywordDAO
    {
        private SQLiteDBHelper dbHelper;

        public KeywordDAO(SQLiteDBHelper dbHelper)
        { 
            this.dbHelper = dbHelper;
            CreateTable();
            UpdateTable();
        }

        private void CreateTable()
        {
            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS keywords("
            + "id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"
            + "mainKey varchar(100) NOT NULL,"
            + "productId varchar(20) NOT NULL,"
            + "productName varchar(200) NOT NULL,"
            + "productImage varchar(300),"
            + "productUrl varchar(1000) NOT NULL,"
            + "companyUrl varchar(100) NOT NULL,"
            + "clicked integer default 0 NOT NULL, "
            + "rank integer default 0 NOT NULL,"     /*本产品的当前排名数*/
            + "prevRank integer default 0 NOT NULL,"     /*本产品的当前排名数*/
            + "keyAdNum integer default 0 NOT NULL," /*购买了本关键词排名的产品数*/
            + "keyP4Num integer default 0 NOT NULL," /*购买了本关键词直通车的产品数*/
            + "status integer default 0,"
            + "createTime datetime,"
            + "updateTime datetime)");

            dbHelper.ExecuteNonQuery("Create Index  IF NOT EXISTS Index_productId on keywords(productId);");
        }

        private void UpdateTable()
        {
            bool ExistColumnStatus = dbHelper.IsExistColumn("keywords", "status");
            if (!ExistColumnStatus)
            {
                dbHelper.ExecuteNonQuery("ALTER TABLE  keywords add COLUMN status integer default 1;");
            }
        }
        


        public List<Keywords> GetKeywordList()
        {
            DataTable dt = dbHelper.ExecuteDataTable(
                  "SELECT id, productId, productName, mainKey, companyUrl, productUrl, "
                + "productImage, prevRank,rank, keyAdNum, keyP4Num, clicked, updateTime FROM keywords where status = 1", null);

            List<Keywords> list = new List<Keywords>();
            foreach (DataRow row in dt.Rows)
            {
                Keywords kw = new Keywords();
                kw.Id = Convert.ToInt32(row["id"]);
                kw.ProductId = (string)row["productId"];
                kw.ProductName = (string)row["productName"];
                kw.MainKey = (string)row["mainKey"];
                kw.CompanyUrl = (string)row["companyUrl"];
                kw.ProductUrl = (string)row["productUrl"];
                kw.ProductImg = (string)row["productImage"];
                kw.PrevRank = Convert.ToInt32(row["prevRank"]);
                kw.Rank = Convert.ToInt32(row["rank"]);
                kw.KeyAdNum = Convert.ToInt32(row["keyAdNum"]);
                kw.KeyP4Num = Convert.ToInt32(row["keyP4Num"]);
                kw.Clicked = Convert.ToInt32(row["clicked"]);
                kw.UpdateTime = Convert.ToDateTime(row["updateTime"]);
                list.Add(kw);
            }
            return list;
        }


        public void Insert(List<Keywords> list)
        {
            string InsSql = @"INSERT INTO keywords(mainKey,productId,productName,productImage,productUrl,companyUrl, createTime, updateTime, status)"
                            + "values(@mainKey,@productId,@productName,@productImage,@productUrl,@companyUrl, @createTime, @updateTime, 1)";

            string UpdSql = @"Update keywords SET mainKey = @mainKey, productName = @productName, productImage = @productImage, "
                   + "productUrl = @productUrl, companyUrl = @companyUrl, updateTime = @updateTime,status=1 WHERE productId = @productId";

            string ExistRecordSql = "SELECT count(1) FROM keywords WHERE productId = ";
            List<SQLiteParameter[]> InsertParameters = new List<SQLiteParameter[]>();
            List<SQLiteParameter[]> UpdateParameters = new List<SQLiteParameter[]>();
            foreach (Keywords item in list)
            {
                int record = Convert.ToInt32(dbHelper.ExecuteScalar(ExistRecordSql + item.ProductId, null));
                if (record > 0)
                {
                    SQLiteParameter[] parameter = new SQLiteParameter[]
                    {
                        new SQLiteParameter("@mainKey",item.MainKey), 
                        new SQLiteParameter("@productName",item.ProductName), 
                        new SQLiteParameter("@productImage",item.ProductImg), 
                        new SQLiteParameter("@productUrl",item.ProductUrl), 
                        new SQLiteParameter("@companyUrl",item.CompanyUrl),
                        new SQLiteParameter("@updateTime",DateTime.Now),
                        new SQLiteParameter("@productId", item.ProductId)
                    };
                    UpdateParameters.Add(parameter);
                }
                else 
                {
                    SQLiteParameter[] parameter = new SQLiteParameter[]
                    {
                        new SQLiteParameter("@mainKey",item.MainKey), 
                        new SQLiteParameter("@productId", item.ProductId), 
                        new SQLiteParameter("@productName",item.ProductName), 
                        new SQLiteParameter("@productImage",item.ProductImg), 
                        new SQLiteParameter("@productUrl",item.ProductUrl), 
                        new SQLiteParameter("@companyUrl",item.CompanyUrl), 
                        new SQLiteParameter("@createTime",DateTime.Now), 
                        new SQLiteParameter("@updateTime",DateTime.Now) 
                    };
                    InsertParameters.Add(parameter);
                }
                
            }
            if (list != null && list.Count > 0)
            {
                dbHelper.ExecuteNonQuery("UPDATE keywords SET status = 0");
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

        public void UpdateRank(Keywords item)
        {

            dbHelper.ExecuteNonQuery(@"UPDATE keywords SET prevRank = rank where id = " + item.Id);

            string sql = @"UPDATE keywords SET rank = @rank, keyAdNum = @keyAdNum,keyP4Num = @keyP4Num,  updateTime = @updateTime where id = @id";
            List<SQLiteParameter[]> parameters = new List<SQLiteParameter[]>();

            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@rank",item.Rank), 
                new SQLiteParameter("@keyAdNum",item.KeyAdNum), 
                new SQLiteParameter("@keyP4Num",item.KeyP4Num), 
                new SQLiteParameter("@updateTime", DateTime.Now), 
                new SQLiteParameter("@id",item.Id)
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }

        public void UpdateClicked(Keywords kw)
        {
            string sql = @"UPDATE keywords SET clicked = @clicked, updateTime = @updateTime where id = @id";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@clicked",kw.Clicked),
                new SQLiteParameter("@updateTime", DateTime.Now),
                new SQLiteParameter("@id",kw.Id)
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }

        public void UpdateAllStatus(int status)
        {
            string sql = @"UPDATE keywords SET status = @status, updateTime = @updateTime";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@status", status),
                new SQLiteParameter("@updateTime", DateTime.Now)
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }

        public void DeleteAll()
        {
            dbHelper.ExecuteNonQuery(@"delete from  keywords");
        }
    }
}
