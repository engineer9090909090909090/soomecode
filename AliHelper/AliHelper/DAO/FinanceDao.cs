using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Data.SQLite;
using System.Data;

namespace AliHelper.DAO
{
    public class FinanceDao
    {
        private SQLiteDBHelper dbHelper;

        public FinanceDao(SQLiteDBHelper dbHelper)
        { 
            this.dbHelper = dbHelper;
            CreateTable();
        }

        private void CreateTable()
        {
            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS FinDetails("
            + "DetailId integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"
            + "EventTime datetime not null,"
            + "EventName varchar(500) not null,"
            + "EventType varchar(50) not null,"
            + "Amount double not null,"
            + "Association varchar(50) not null,"
            + "OrderNo varchar(50),"
            + "ItemType varchar(50),"
            + "Remark varchar(500),"
            + "CreatedTime datetime,"
            + "ModifiedTime datetime)");

            dbHelper.ExecuteNonQuery(
             "CREATE TABLE IF NOT EXISTS Finance("
           + "FinId integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"
           + "FinDate datetime not null,"
           + "FinSource varchar(100) not null,"
           + "FinCompany varchar(100) not null,"
           + "Association varchar(50) not null,"
           + "Amount double not null,"
           + "Rate double not null default 1.00,"
           + "Currency varchar(3) not null,"
           + "Remark varchar(500),"
           + "CreatedTime datetime,"
           + "ModifiedTime datetime)");

            dbHelper.ExecuteNonQuery(
             "CREATE TABLE IF NOT EXISTS Finance_FinDetails("
           + "FinId integer NOT NULL,"
           + "DetailId integer NOT NULL)");

            dbHelper.ExecuteNonQuery("Create Index IF NOT EXISTS Index_key on Finance_FinDetails(FinId, DetailId);");
        }

        public QueryObject<FinDetails> GetFinDetails(QueryObject<FinDetails> query)
        {
            string sql = "select DetailId,EventTime,EventName,EventType,Amount,Association,OrderNo,ItemType,Remark ";
            sql = sql + "FROM FinDetails where 1 = 1 ";
            List<SQLiteParameter> QueryParameters = new List<SQLiteParameter>();
            if (query.Condition != null)
            {
                if (query.Condition.BeginTime != null)
                {
                    sql = sql + "and EventTime >= @BeginTime ";
                    QueryParameters.Add(new SQLiteParameter("@BeginTime", query.Condition.BeginTime));
                }
                if (query.Condition.EndTime != null)
                {
                    sql = sql + "and EventTime <= @EndTime ";
                    QueryParameters.Add(new SQLiteParameter("@EndTime", query.Condition.EndTime));
                }
                if (!string.IsNullOrEmpty(query.Condition.EventName))
                {
                    sql = sql + "and EventName like '%" + query.Condition.EventName.Trim() + "%' ";
                }
                if (!string.IsNullOrEmpty(query.Condition.ItemType))
                {
                    sql = sql + "and ItemType = @ItemType ";
                    QueryParameters.Add(new SQLiteParameter("@ItemType", query.Condition.ItemType));
                }
                if (!string.IsNullOrEmpty(query.Condition.EventType))
                {
                    sql = sql + "and EventType = @EventType ";
                    QueryParameters.Add(new SQLiteParameter("@EventType", query.Condition.EventType));
                }
                if (!string.IsNullOrEmpty(query.Condition.OrderNo))
                {
                    sql = sql + "and OrderNo like '%" + query.Condition.OrderNo.Trim() + "%' ";
                }
                if (!string.IsNullOrEmpty(query.Condition.Association))
                {
                    sql = sql + "and Association like '%" + query.Condition.Association.Trim() + "%' ";
                }
            }
            if (query.IsExport == false)
            {
                query.RecordCount = dbHelper.GetItemCount(sql, QueryParameters.ToArray());
                sql = sql + "limit " + query.Page + ", " + query.PageSize;
            }
            query.dt = dbHelper.ExecuteDataTable(sql, QueryParameters.ToArray());
            query.Result = DetailTableToList(query.dt);
            return query;
        }

        public List<FinDetails> DetailTableToList(DataTable dt)
        {
            List<FinDetails> list = new List<FinDetails>();
            foreach (DataRow row in dt.Rows)
            {
                FinDetails info = new FinDetails();
                info.DetailId = Convert.ToInt32(row["DetailId"]);
                info.EventTime = Convert.ToDateTime(row["EventTime"]);
                info.EventName = (string)row["EventName"];
                info.EventType = (string)row["EventType"];
                info.Amount = Convert.ToDouble(row["Amount"]);
                info.Association = (string)row["Association"];
                info.OrderNo = (string)row["OrderNo"];
                info.ItemType = (string)row["ItemType"];
                info.Remark = (string)row["Remark"];
                list.Add(info);
            }
            return list;
        }

        public void InsertOrUpdateDetails(List<FinDetails> list)
        {
            string InsSql = @"INSERT INTO FinDetails(SEventTime,EventName,EventType,Amount,Association,OrderNo,ItemType,Remark,CreatedTime,ModifiedTime)"
                            + "values(@EventTime,@EventName,@EventType,@Amount,@Association,@OrderNo,@ItemType,@Remark,@CreatedTime,@ModifiedTime)";
            string UpdSql = @"update FinDetails set EventTime=@EventTime,EventName=@EventName,EventType=@EventType,Amount=@Amount,Association=@Association,"
                            + "OrderNo=@OrderNo,ItemType=@ItemType,Remark=@Remark,ModifiedTime=@ModifiedTime"
                            + "where DetailId = @DetailId";

            string ExistRecordSql = "SELECT count(1) FROM FinDetails WHERE DetailId = ";

            List<SQLiteParameter[]> InsertParameters = new List<SQLiteParameter[]>();
            List<SQLiteParameter[]> UpdateParameters = new List<SQLiteParameter[]>();
            DateTime CurrentTime = DateTime.Now;
            foreach(FinDetails item in list)
            {

                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("@DetailId",item.DetailId),
                    new SQLiteParameter("@EventTime",item.EventTime),
                    new SQLiteParameter("@EventName",item.EventName),
                    new SQLiteParameter("@EventType",item.EventType),
                    new SQLiteParameter("@Amount",item.Amount),
                    new SQLiteParameter("@Association",item.Association),
                    new SQLiteParameter("@OrderNo",item.OrderNo),
                    new SQLiteParameter("@ItemType",item.ItemType),
                    new SQLiteParameter("@Remark",item.Remark),
                    new SQLiteParameter("@CreatedTime", CurrentTime),
                    new SQLiteParameter("@ModifiedTime",CurrentTime)
                };
                int record = Convert.ToInt32(dbHelper.ExecuteScalar(ExistRecordSql + item.DetailId, null));
                if (record == 0)
                {
                    InsertParameters.Add(parameter);
                }
                else
                {
                    UpdateParameters.Add(parameter);
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




    }
}
