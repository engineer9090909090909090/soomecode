using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Drawing;

namespace AliRank
{
    class RankInfoDAO
    {
        private SQLiteDBHelper dbHelper;

        public RankInfoDAO(SQLiteDBHelper dbHelper)
        { 
            this.dbHelper = dbHelper;
            CreateTable();
            UpdateTable();
        }

        private void CreateTable()
        {
            
            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS RankInfo("
            + "id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"
            + "rankKeyword varchar(300) NOT NULL,"
            + "rank integer default 0,"         /*本产品的当前排名数*/
            + "keyAdNum integer default 0 NOT NULL," /*购买了本关键词排名的产品数*/
            + "keyP4Num integer default 0 NOT NULL," /*购买了本关键词直通车的产品数*/
            + "productId varchar(20),"
            + "productName varchar(200),"
            + "status integer default 0,"
            + "orderBy integer default 0,"
            + "queryStatus integer default 0,"
            + "createTime datetime,"
            + "updateTime datetime)");

            dbHelper.ExecuteNonQuery("Create Index  IF NOT EXISTS Index_key on RankInfo(rankKeyword);");
        }

        private void UpdateTable()
        {
            bool ExistColumnStatus = dbHelper.IsExistColumn("RankInfo", "queryStatus");
            if (!ExistColumnStatus)
            {
                dbHelper.ExecuteNonQuery("ALTER TABLE  RankInfo add COLUMN queryStatus integer default 0;");
            }
            bool ExistColumnOrderBy = dbHelper.IsExistColumn("RankInfo", "orderBy");
            if (!ExistColumnOrderBy)
            {
                dbHelper.ExecuteNonQuery("ALTER TABLE  RankInfo add COLUMN orderBy integer default 0;");
            }
        }

        public List<RankInfo> GetRankInfoList()
        {
            DataTable dt = dbHelper.ExecuteDataTable(
                  "SELECT id, rankKeyword,rank, keyAdNum, keyP4Num, productId, productName, updateTime FROM RankInfo where status = 1 order by orderBy asc", null);

            List<RankInfo> list = new List<RankInfo>();
            foreach (DataRow row in dt.Rows)
            {
                RankInfo kw = new RankInfo();
                kw.Id = Convert.ToInt32(row["id"]);
                if (!Convert.IsDBNull(row["productId"]))
                    kw.ProductId = (string)row["productId"];
                if (!Convert.IsDBNull(row["productName"]))
                    kw.ProductName = (string)row["productName"];
                kw.RankKeyword = (string)row["rankKeyword"];
                kw.Rank = Convert.ToInt32(row["rank"]);
                kw.KeyAdNum = Convert.ToInt32(row["keyAdNum"]);
                kw.KeyP4Num = Convert.ToInt32(row["keyP4Num"]);
                kw.UpdateTime = Convert.ToDateTime(row["updateTime"]);
                list.Add(kw);
            }
            return list;
        }


        public void Insert(List<RankInfo> list)
        {
            string InsSql = @"INSERT INTO RankInfo(rankKeyword,createTime, updateTime, status, orderBy)"
                            + "values(@rankKeyword, @createTime, @updateTime, 1, @orderBy)";

            string UpdSql = @"Update RankInfo SET status = 1, orderBy = @orderBy  WHERE id = @id";

            string ExistRecordSql = "SELECT id FROM RankInfo WHERE rankKeyword =@rankKeyword LIMIT 0,1";
            List<SQLiteParameter[]> InsertParameters = new List<SQLiteParameter[]>();
            List<SQLiteParameter[]> UpdateParameters = new List<SQLiteParameter[]>();
            for (int i = 0; i < list.Count; i++ )
            {
                RankInfo item = list[i];
                Object id = dbHelper.ExecuteScalar
                (
                    ExistRecordSql, new SQLiteParameter[] { new SQLiteParameter("@rankKeyword", item.RankKeyword) }
                );
                if (!Convert.IsDBNull(id) && Convert.ToInt32(id) > 0)
                {
                    SQLiteParameter[] parameter = new SQLiteParameter[]
                    {
                        new SQLiteParameter("@id",Convert.ToInt32(id)),
                        new SQLiteParameter("@orderBy",i)
                    };
                    UpdateParameters.Add(parameter);
                }
                else
                {
                    SQLiteParameter[] parameter = new SQLiteParameter[]
                    {
                        new SQLiteParameter("@rankKeyword",item.RankKeyword),
                        new SQLiteParameter("@createTime",DateTime.Now), 
                        new SQLiteParameter("@updateTime",DateTime.Now),
                        new SQLiteParameter("@orderBy",i) 
                    };
                    InsertParameters.Add(parameter);
                }

            }
            if (list != null && list.Count > 0)
            {
                dbHelper.ExecuteNonQuery("UPDATE RankInfo SET status = 0");
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


        public int UpdateRankInfo(RankInfo item)
        {
            string sql = @"UPDATE RankInfo SET rank = @rank, keyAdNum = @keyAdNum,keyP4Num = "
                + "@keyP4Num,productId= @productId, productName=@productName,  updateTime = @updateTime, queryStatus = 1 "
                +"where rankKeyword = @rankKeyword and queryStatus = 0";
            List<SQLiteParameter[]> parameters = new List<SQLiteParameter[]>();

            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@rank",item.Rank), 
                new SQLiteParameter("@keyAdNum",item.KeyAdNum), 
                new SQLiteParameter("@keyP4Num",item.KeyP4Num), 
                new SQLiteParameter("@productId",item.ProductId),
                new SQLiteParameter("@productName",item.ProductName),
                new SQLiteParameter("@updateTime", DateTime.Now), 
                new SQLiteParameter("@rankKeyword",item.RankKeyword), 
            };
            return dbHelper.ExecuteNonQuery(sql, parameter);
        }

        public void UpdateAllQueryStatus()
        {
            dbHelper.ExecuteNonQuery("UPDATE RankInfo SET queryStatus = 0");
        }
    }
}
