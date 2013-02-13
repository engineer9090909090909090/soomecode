using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Data.SQLite;
using System.Data;
using System.Data.Common;

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
            + "FinId integer NOT NULL default 0,"
            + "FinDate varchar(10) not null,"
            + "Description varchar(500) not null,"
            + "EventType varchar(50) not null,"
            + "Amount double not null,"
            + "Rate double not null default 1.00,"
            + "Currency varchar(3) not null,"
            + "Association varchar(50) not null,"
            + "OrderNo varchar(50),"
            + "ItemType varchar(50),"
            + "Remark varchar(500),"
            + "CreatedTime datetime,"
            + "ModifiedTime datetime)");

            dbHelper.ExecuteNonQuery(
             "CREATE TABLE IF NOT EXISTS Finance("
           + "FinId integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"
           + "FinDate varchar(10) not null,"
           + "Description varchar(100) not null,"
           + "EventType varchar(50) not null,"
           + "ItemType varchar(50) not null,"
           + "Account varchar(50) not null,"
           + "ReferenceNo varchar(50),"
           + "ReceivePaymentor varchar(100),"
           + "Customer varchar(50),"
           + "Association varchar(50) not null,"
           + "Amount double not null,"
           + "Rate double not null default 1.00,"
           + "Currency varchar(10) not null,"
           + "Remark varchar(500),"
           + "CreatedTime datetime,"
           + "ModifiedTime datetime)");

        }

        public QueryObject<FinDetails> GetFinDetails(QueryObject<FinDetails> query)
        {
            string sql = "select DetailId, FinId, FinDate,Description,Amount,OrderNo,ItemType,Association,EventType,Remark, ";
            sql = sql + "Rate, Currency, Amount * Rate TotalAmount FROM FinDetails where 1 = 1 ";
            List<SQLiteParameter> QueryParameters = new List<SQLiteParameter>();
            if (query.Condition != null)
            {
                if (query.Condition.BeginTime != null)
                {
                    sql = sql + "and FinDate >= @BeginTime ";
                    QueryParameters.Add(new SQLiteParameter("@BeginTime", query.Condition.BeginTime));
                }
                if (query.Condition.EndTime != null)
                {
                    sql = sql + "and FinDate <= @EndTime ";
                    QueryParameters.Add(new SQLiteParameter("@EndTime", query.Condition.EndTime));
                }
                if (!string.IsNullOrEmpty(query.Condition.FinDate))
                {
                    sql = sql + "and Description like '%" + query.Condition.Description.Trim() + "%' ";
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
                sql = sql + "limit " + query.Start + ", " + query.PageSize;
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
                info.FinId = Convert.ToInt32(row["FinId"]);
                info.FinDate = (string)row["FinDate"];
                info.Description = (string)row["Description"];
                info.EventType = (string)row["EventType"];
                info.Amount = Convert.ToDouble(row["Amount"]);
                info.Currency = (string)row["Currency"];
                info.TotalAmount = Convert.ToDouble(row["TotalAmount"]);
                info.Rate = Convert.ToDouble(row["Rate"]);
                info.Association = (string)row["Association"];
                info.OrderNo = (string)row["OrderNo"];
                info.ItemType = (string)row["ItemType"];
                info.Remark = Convert.IsDBNull(row["Remark"])?"": (string)row["Remark"];
                list.Add(info);
            }
            return list;
        }

        public void InsertOrUpdateDetails(List<FinDetails> list)
        {
            using (SQLiteConnection connection = dbHelper.GetConnection())
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    InsertOrUpdateDetails(connection, list);
                    transaction.Commit();
                }
            }
        }

        public void InsertOrUpdateDetails(SQLiteConnection connection, List<FinDetails> list)
        {
            string InsSql = @"INSERT INTO FinDetails(FinDate,FinId,Description,EventType,Amount,Association,OrderNo,ItemType,Remark,Currency, Rate, CreatedTime,ModifiedTime)"
                            + "values(@FinDate,@FinId,@Description,@EventType,@Amount,@Association,@OrderNo,@ItemType, @Remark, @Currency, @Rate, @CreatedTime, @ModifiedTime)";
            string UpdSql = @"update FinDetails set FinDate=@FinDate,Description=@Description,EventType=@EventType,Amount=@Amount,Association=@Association,"
                            + "OrderNo=@OrderNo,ItemType=@ItemType,Remark=@Remark,Currency = @Currency, Rate = @Rate, ModifiedTime=@ModifiedTime "
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
                    new SQLiteParameter("@FinId",item.FinId),
                    new SQLiteParameter("@FinDate",item.FinDate),
                    new SQLiteParameter("@Description",item.Description),
                    new SQLiteParameter("@EventType",item.EventType),
                    new SQLiteParameter("@Amount",item.Amount),
                    new SQLiteParameter("@Association",item.Association),
                    new SQLiteParameter("@OrderNo",item.OrderNo),
                    new SQLiteParameter("@ItemType",item.ItemType),
                    new SQLiteParameter("@Currency",item.Currency),
                    new SQLiteParameter("@Rate",item.Rate),
                    new SQLiteParameter("@Remark",item.Remark),
                    new SQLiteParameter("@CreatedTime", CurrentTime),
                    new SQLiteParameter("@ModifiedTime",CurrentTime)
                };
                int record = Convert.ToInt32(dbHelper.ExecuteScalar(connection, ExistRecordSql + item.DetailId, null));
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
                dbHelper.ExecuteNonQuery(connection, InsSql, InsertParameters);
            }
            if (UpdateParameters.Count > 0)
            {
                dbHelper.ExecuteNonQuery(connection, UpdSql, UpdateParameters);
            }
        }

        public FinDetails GetFinDetail(int id)
        {
            string sql = "select *, Amount * Rate TotalAmount from FinDetails where DetailId = " + id;

            DataTable dt = dbHelper.ExecuteDataTable(sql, null);
            List<FinDetails> list =  DetailTableToList(dt);
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        public List<FinDetails> GetFinDetails(int finId)
        {
            string sql = "select *, Amount * Rate TotalAmount from FinDetails where FinId = " + finId;
            DataTable dt = dbHelper.ExecuteDataTable(sql, null);
            return DetailTableToList(dt);
        }



        public void InsertOrUpdateFinance(Finance finance)
        {
            
            string InsSql = @"INSERT INTO Finance(FinDate, Description, EventType, ItemType, Account, ReferenceNo, ReceivePaymentor, Customer, Association,Amount,Rate,Currency,Remark, CreatedTime,ModifiedTime)"
                            + "values(@FinDate, @Description, @EventType, @ItemType, @Account, @ReferenceNo, @ReceivePaymentor, @Customer, @Association,@Amount,@Rate,@Currency,@Remark, @CreatedTime, @ModifiedTime)";
            string UpdSql = @"update Finance set Description=@Description,FinDate=@FinDate,EventType=@EventType,Amount=@Amount,Association=@Association,"
                            + "ReceivePaymentor=@ReceivePaymentor,Account=@Account,ItemType=@ItemType,ReferenceNo=@ReferenceNo,Customer=@Customer,Remark=@Remark,Currency=@Currency,Rate =@Rate,ModifiedTime=@ModifiedTime "
                            + "WHERE FinId=@FinId";

            string ExistRecordSql = "SELECT count(1) FROM Finance WHERE FinId = ";

            List<SQLiteParameter[]> InsertParameters = new List<SQLiteParameter[]>();
            List<SQLiteParameter[]> UpdateParameters = new List<SQLiteParameter[]>();
            DateTime CurrentTime = DateTime.Now;
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                 new SQLiteParameter("@FinId",finance.FinId),
                new SQLiteParameter("@FinDate",finance.FinDate),
                new SQLiteParameter("@Description",finance.Description),
                new SQLiteParameter("@EventType",finance.EventType),
                new SQLiteParameter("@Account",finance.Account),
                new SQLiteParameter("@ItemType",finance.ItemType),
                new SQLiteParameter("@ReferenceNo",finance.ReferenceNo),
                new SQLiteParameter("@ReceivePaymentor",finance.ReceivePaymentor),
                new SQLiteParameter("@Customer",finance.Customer),
                new SQLiteParameter("@Association",finance.Association),
                new SQLiteParameter("@Amount",finance.Amount),
                new SQLiteParameter("@Rate",finance.Rate),
                new SQLiteParameter("@Currency",finance.Currency),
                new SQLiteParameter("@Remark",finance.Remark),
                new SQLiteParameter("@CreatedTime", CurrentTime),
                new SQLiteParameter("@ModifiedTime",CurrentTime)
            };
            int record = Convert.ToInt32(dbHelper.ExecuteScalar(ExistRecordSql + finance.FinId, null));
            using (SQLiteConnection connection = dbHelper.GetConnection())
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    if (record == 0)
                    {
                        dbHelper.ExecuteNonQuery(connection, InsSql, parameter);
                        int LasertInsertId = dbHelper.GetLastInsertId(connection);
                        foreach (FinDetails detail in finance.Details)
                        {
                            detail.FinId = LasertInsertId;
                        }
                    }
                    else
                    {
                        dbHelper.ExecuteNonQuery(connection, UpdSql, parameter);
                        dbHelper.ExecuteNonQuery(connection, "delete from FinDetails where FinId =" + finance.FinId);
                    }
                    InsertOrUpdateDetails(connection, finance.Details);
                    transaction.Commit();
                }
            }
        }


        public QueryObject<Finance> GetFinances(QueryObject<Finance> query)
        {
            string sql = "select *, Amount * Rate TotalAmount FROM Finance f where 1 = 1 ";
            List<SQLiteParameter> QueryParameters = new List<SQLiteParameter>();
            if (query.Condition != null)
            {
                if (query.Condition.BeginTime != null)
                {
                    sql = sql + "and f.FinDate >= @BeginTime ";
                    QueryParameters.Add(new SQLiteParameter("@BeginTime", query.Condition.BeginTime));
                }
                if (query.Condition.EndTime != null)
                {
                    sql = sql + "and f.FinDate <= @EndTime ";
                    QueryParameters.Add(new SQLiteParameter("@EndTime", query.Condition.EndTime));
                }
                if (!string.IsNullOrEmpty(query.Condition.Description))
                {
                    sql = sql + "and f.Description like '%" + query.Condition.Description.Trim() + "%' ";
                }
                if (!string.IsNullOrEmpty(query.Condition.ItemType))
                {
                    sql = sql + "and f.ItemType = @ItemType ";
                    QueryParameters.Add(new SQLiteParameter("@ItemType", query.Condition.ItemType));
                }
                if (!string.IsNullOrEmpty(query.Condition.EventType))
                {
                    sql = sql + "and f.EventType = @EventType ";
                    QueryParameters.Add(new SQLiteParameter("@EventType", query.Condition.EventType));
                }
                if (!string.IsNullOrEmpty(query.Condition.Association))
                {
                    sql = sql + "and f.Association like '%" + query.Condition.Association.Trim() + "%' ";
                }
                if (!string.IsNullOrEmpty(query.Condition.ReceivePaymentor))
                {
                    sql = sql + "and f.ReceivePaymentor like '%" + query.Condition.ReceivePaymentor.Trim() + "%' ";
                }
            }
            query.dt = dbHelper.ExecuteDataTable(sql, QueryParameters.ToArray());
            List<Finance> FinanceList = FinanceTableToList(query.dt);
            foreach(Finance fin in FinanceList)
            {
                if (fin.FinId > 0)
                {
                    fin.Details = GetFinDetails(fin.FinId);
                }
            }
            query.Result = FinanceList;
            return query;
        }

        public Finance GetFinance(int finId)
        {
            string sql = "select *, Amount * Rate TotalAmount from Finance where FinId = " + finId;
            DataTable dt = dbHelper.ExecuteDataTable(sql, null);
            List<Finance> list =  FinanceTableToList(dt);
            if (list.Count > 0)
            {
                Finance finance = list[0];
                finance.Details = this.GetFinDetails(finance.FinId);
                return finance;
            }
            else
            {
                return null;
            }
        }

        public List<Finance> FinanceTableToList(DataTable dt)
        {
            List<Finance> list = new List<Finance>();
            foreach (DataRow row in dt.Rows)
            {
                Finance info = new Finance();
                info.FinId = Convert.ToInt32(row["FinId"]);
                info.FinDate = (string)row["FinDate"];
                info.Description = (string)row["Description"];
                info.EventType = (string)row["EventType"];
                info.Amount = Convert.ToDouble(row["Amount"]);
                info.Currency = (string)row["Currency"];
                info.TotalAmount = Convert.ToDouble(row["TotalAmount"]);
                info.Rate = Convert.ToDouble(row["Rate"]);
                info.Association = (string)row["Association"];
                info.Account = (string)row["Account"];
                info.ReferenceNo = Convert.IsDBNull(row["ReferenceNo"]) ? "" : (string)row["ReferenceNo"];
                info.ReceivePaymentor = (string)row["ReceivePaymentor"];
                info.Customer = Convert.IsDBNull(row["Customer"])?"":(string)row["Customer"];
                info.ItemType = (string)row["ItemType"];
                info.Remark = Convert.IsDBNull(row["Remark"])?"":(string)row["Remark"];
                list.Add(info);
            }
            return list;
        }

    }
}
