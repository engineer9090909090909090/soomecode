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
            UpdateTable();
            CreateTable();
        }

        private void CreateTable()
        {
            
            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS keywords("
            + "productId integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"
            + "productImage varchar(300),"
            + "productName varchar(200) NOT NULL,"
            + "mainKey varchar(100) NOT NULL,"
            + "productUrl varchar(1000) NOT NULL,"
            + "companyUrl varchar(100) NOT NULL,"
            + "clicked integer default 0 NOT NULL, "
            + "maxInquiryQty integer default 0 NOT NULL, "
            + "factInquiryQty integer default 0 NOT NULL, "
            + "rankKeyword varchar(300) default '',"
            + "keyAdNum integer default 0 NOT NULL," /*购买了本关键词排名的产品数*/
            + "keyP4Num integer default 0 NOT NULL," /*购买了本关键词直通车的产品数*/
            + "rank integer default 0,"         /*本产品的当前排名数*/
            + "prevRank integer default 0,"     /*本产品的当前排名数*/
            + "status integer default 0,"
            + "queryStatus integer default 0,"
            + "createTime datetime,"
            + "updateTime datetime)");

        }

        private void UpdateTable()
        {
            bool ExistColumnStatus = dbHelper.IsExistColumn("keywords", "status");
             bool ExistColumnId = dbHelper.IsExistColumn("keywords", "id");
             if (ExistColumnId)
             {
                 dbHelper.ExecuteNonQuery("Drop TABLE keywords");
             }
        }

        public List<ShowcaseRankInfo> GetKeywordList()
        {
            return GetProducts("");
        }

        public List<ShowcaseRankInfo> GetClickProducts()
        {
            return GetProducts("clicked asc");
        }

        public List<ShowcaseRankInfo> GetProducts(string orderBy)
        {
            string SelectSql = "SELECT productId, productName, mainKey, rankKeyword, companyUrl, productUrl, "
                + "productImage, prevRank,rank, keyAdNum, keyP4Num, clicked, updateTime, maxInquiryQty,factInquiryQty "
                + "FROM keywords where status = 1";
            if (!string.IsNullOrEmpty(orderBy))
            {
                SelectSql = SelectSql + " order by " + orderBy;
            }
            DataTable dt = dbHelper.ExecuteDataTable(SelectSql, null);

            List<ShowcaseRankInfo> list = new List<ShowcaseRankInfo>();
            foreach (DataRow row in dt.Rows)
            {
                ShowcaseRankInfo kw = new ShowcaseRankInfo();
                kw.ProductId = Convert.ToInt32(row["productId"]);
                kw.ProductName = (string)row["productName"];
                kw.MainKey = (string)row["mainKey"];
                kw.RankKeyword = (string)row["rankKeyword"];
                kw.CompanyUrl = (string)row["companyUrl"];
                kw.ProductUrl = (string)row["productUrl"];
                kw.ProductImg = (string)row["productImage"];
                kw.PrevRank = Convert.ToInt32(row["prevRank"]);
                kw.Rank = Convert.ToInt32(row["rank"]);
                kw.MaxInquiryQty = Convert.ToInt32(row["maxInquiryQty"]);
                kw.FactInquiryQty = Convert.ToInt32(row["factInquiryQty"]);
                kw.KeyAdNum = Convert.ToInt32(row["keyAdNum"]);
                kw.KeyP4Num = Convert.ToInt32(row["keyP4Num"]);
                kw.Clicked = Convert.ToInt32(row["clicked"]);
                kw.UpdateTime = Convert.ToDateTime(row["updateTime"]);
                list.Add(kw);
            }
            return list;
        }

        public ShowcaseRankInfo GetShowcaseRankInfo(int ProductId)
        {
            DataTable dt = dbHelper.ExecuteDataTable(
                  "SELECT productId, productName, mainKey, rankKeyword, companyUrl, productUrl, "
                + "productImage, prevRank,rank, keyAdNum, keyP4Num, clicked, updateTime , maxInquiryQty,factInquiryQty  "
                + "FROM keywords where productId = " + ProductId, null);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0]; 
                ShowcaseRankInfo kw = new ShowcaseRankInfo();
                kw.ProductId = Convert.ToInt32(row["productId"]);
                kw.ProductName = (string)row["productName"];
                kw.MainKey = (string)row["mainKey"];
                kw.RankKeyword = (string)row["rankKeyword"];
                kw.CompanyUrl = (string)row["companyUrl"];
                kw.ProductUrl = (string)row["productUrl"];
                kw.ProductImg = (string)row["productImage"];
                kw.PrevRank = Convert.ToInt32(row["prevRank"]);
                kw.Rank = Convert.ToInt32(row["rank"]);
                kw.MaxInquiryQty = Convert.ToInt32(row["maxInquiryQty"]);
                kw.FactInquiryQty = Convert.ToInt32(row["factInquiryQty"]);
                kw.KeyAdNum = Convert.ToInt32(row["keyAdNum"]);
                kw.KeyP4Num = Convert.ToInt32(row["keyP4Num"]);
                kw.Clicked = Convert.ToInt32(row["clicked"]);
                kw.UpdateTime = Convert.ToDateTime(row["updateTime"]);
                return kw;
            }
            return null;
        }

        public void Insert(List<ShowcaseRankInfo> list)
        {
            string InsSql = @"INSERT INTO keywords(mainKey,rankKeyword, productId,productName,productImage,productUrl,companyUrl, createTime, updateTime, status, maxInquiryQty)"
                            + "values(@mainKey,@rankKeyword, @productId,@productName,@productImage,@productUrl,@companyUrl, @createTime, @updateTime, 1, 3)";

            string UpdSql = @"Update keywords SET mainKey = @mainKey, productName = @productName, productImage = @productImage, "
                   + "productUrl = @productUrl, companyUrl = @companyUrl, updateTime = @updateTime,status=1,clicked = 0 WHERE productId = @productId";

            string ExistRecordSql = "SELECT count(1) FROM keywords WHERE productId = ";
            List<SQLiteParameter[]> InsertParameters = new List<SQLiteParameter[]>();
            List<SQLiteParameter[]> UpdateParameters = new List<SQLiteParameter[]>();
            foreach (ShowcaseRankInfo item in list)
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
                    string sRankKeyword = string.Empty;
                    if (string.IsNullOrEmpty(item.MainKey) == false && item.MainKey.Split(',').Length > 1)
                    {
                        sRankKeyword = item.MainKey.Split(',')[0];
                    }
                    SQLiteParameter[] parameter = new SQLiteParameter[]
                    {
                        new SQLiteParameter("@mainKey",item.MainKey), 
                        new SQLiteParameter("@rankKeyword",sRankKeyword), 
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
            if (InsertParameters.Count > 0)
            {
                dbHelper.ExecuteNonQuery(InsSql, InsertParameters);
            }
            if (UpdateParameters.Count > 0)
            {
                dbHelper.ExecuteNonQuery(UpdSql, UpdateParameters);
            }
        }

        public void UpdateAllQueryStatus()
        {
            dbHelper.ExecuteNonQuery("UPDATE keywords SET queryStatus = 0");
        }

        public ShowcaseRankInfo UpdateRank(ShowcaseRankInfo item)
        {
            Object prank = dbHelper.ExecuteScalar(@"select rank from keywords where productId = " + item.ProductId, null);
            if (Convert.IsDBNull(prank))
            {
                item.PrevRank = 0;
            }else{
                item.PrevRank = Convert.ToInt32(prank);
            }
            string sql = @"UPDATE keywords SET prevRank= @prevRank,  "
                + "rank = @rank, keyAdNum = @keyAdNum, keyP4Num = @keyP4Num, queryStatus = 1,  "
                + "updateTime = @updateTime where productId = @productId ";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@prevRank",item.PrevRank), 
                new SQLiteParameter("@rank",item.Rank), 
                new SQLiteParameter("@keyAdNum",item.KeyAdNum), 
                new SQLiteParameter("@keyP4Num",item.KeyP4Num), 
                new SQLiteParameter("@updateTime", DateTime.Now), 
                new SQLiteParameter("@productId",item.ProductId),
                 new SQLiteParameter("@productId",item.ProductId)
            };
            item.QueryStatus = dbHelper.ExecuteNonQuery(sql, parameter);
            return item;
        }

        public void UpdateClicked(ShowcaseRankInfo kw)
        {
            string sql = @"UPDATE keywords SET clicked = @clicked, updateTime = @updateTime where productId = @productId";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@clicked",kw.Clicked),
                new SQLiteParameter("@updateTime", DateTime.Now),
                new SQLiteParameter("@productId",kw.ProductId)
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

        public void AddProductFactInquiryQty(int productId)
        {
            string sql = @"UPDATE keywords SET factInquiryQty = factInquiryQty + 1 where productId= @productId";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
               new SQLiteParameter("@productId",productId)
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }

        public void Delete(int productId)
        {
            string sql = @"delete from  keywords where productId = @productId";
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@productId", productId)
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }

        public void UpdateRankKeyword(int productId, string rankKeyword)
        {
            string sql = @"UPDATE keywords SET rankKeyword = @rankKeyword where productId= @productId";
            List<SQLiteParameter[]> parameters = new List<SQLiteParameter[]>();

            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@rankKeyword", rankKeyword),
                new SQLiteParameter("@productId",productId),
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }

        public void UpdateMaxInquiryQty(int productId, int maxInquiryQty)
        {
            string sql = @"UPDATE keywords SET maxInquiryQty = @maxInquiryQty where productId= @productId";
            List<SQLiteParameter[]> parameters = new List<SQLiteParameter[]>();

            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@maxInquiryQty", maxInquiryQty),
                new SQLiteParameter("@productId",productId),
            };
            dbHelper.ExecuteNonQuery(sql, parameter);
        }

        public void DeleteAll()
        {
            dbHelper.ExecuteNonQuery("drop TABLE IF EXISTS keywords");
            CreateTable();
            UpdateTable();
        }
    }
}
