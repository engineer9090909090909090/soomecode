using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Database
{
    public class FinanceDaoMysql : IFinanceDao
    {
        private MysqlDBHelper dbHelper;

        public FinanceDaoMysql(MysqlDBHelper dbHelper)
        { 
            this.dbHelper = dbHelper;
            CreateTable();
        }

        private void CreateTable()
        {
            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS FinDetails("
            + "`DetailId` integer NOT NULL PRIMARY KEY AUTO_INCREMENT UNIQUE,"
            + "`FinId` integer NOT NULL default 0,"
            + "`FinDate` varchar(10) not null,"
            + "`Description` varchar(500) not null,"
            + "`EventType` varchar(50) not null,"
            + "`Amount` double not null,"
            + "`Rate` double not null default 1.00,"
            + "`Currency` varchar(3) not null,"
            + "`Association` varchar(50) not null,"
            + "`OrderNo` varchar(50),"
            + "`ItemType` varchar(50),"
            + "`Remark` varchar(500),"
            + "`CreatedTime` datetime,"
            + "`ModifiedTime` datetime) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin ");

            dbHelper.ExecuteNonQuery(
             "CREATE TABLE IF NOT EXISTS Finance("
           + "`FinId` integer NOT NULL PRIMARY KEY AUTO_INCREMENT UNIQUE,"
           + "`FinDate` varchar(10) not null,"
           + "`Description` varchar(100) not null,"
           + "`EventType` varchar(50) not null,"
           + "`ItemType` varchar(50) not null,"
           + "`Account` varchar(50) not null,"
           + "`ReferenceNo` varchar(50),"
           + "`ReceivePaymentor` varchar(100),"
           + "`Customer` varchar(50),"
           + "`Association` varchar(50) not null,"
           + "`Amount` double not null,"
           + "`Rate` double not null default 1.00,"
           + "`Currency` varchar(10) not null,"
           + "`Remark` varchar(500),"
           + "`CreatedTime` datetime,"
           + "`ModifiedTime` datetime) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin ");

        }

        public QueryObject<FinDetails> GetFinDetails(QueryObject<FinDetails> query)
        {
            string sql = "select DetailId, FinId, FinDate,Description,Amount,OrderNo,ItemType,Association,EventType,Remark, ";
            sql = sql + "Rate, Currency, Amount * Rate TotalAmount FROM FinDetails where 1 = 1 ";
            List<MySqlParameter> QueryParameters = new List<MySqlParameter>();
            if (query.Condition != null)
            {
                if (query.Condition.BeginTime != null)
                {
                    sql = sql + "and FinDate >= @BeginTime ";
                    QueryParameters.Add(new MySqlParameter("@BeginTime", query.Condition.BeginTime));
                }
                if (query.Condition.EndTime != null)
                {
                    sql = sql + "and FinDate <= @EndTime ";
                    QueryParameters.Add(new MySqlParameter("@EndTime", query.Condition.EndTime));
                }
                if (!string.IsNullOrEmpty(query.Condition.Description))
                {
                    sql = sql + "and Description like '%" + query.Condition.Description.Trim() + "%' ";
                }
                if (!string.IsNullOrEmpty(query.Condition.ItemType))
                {
                    sql = sql + "and ItemType = @ItemType ";
                    QueryParameters.Add(new MySqlParameter("@ItemType", query.Condition.ItemType));
                }
                if (!string.IsNullOrEmpty(query.Condition.EventType))
                {
                    sql = sql + "and EventType = @EventType ";
                    QueryParameters.Add(new MySqlParameter("@EventType", query.Condition.EventType));
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
                sql = sql + "order by FinDate asc limit " + query.Start + ", " + query.PageSize;
            }
            else 
            {
                sql = sql + " order by FinDate asc";
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
            using (MySqlConnection connection = dbHelper.GetConnection())
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    InsertOrUpdateDetails(transaction, list);
                    transaction.Commit();
                }
            }
        }

        public void InsertOrUpdateDetails(DbTransaction trans, List<FinDetails> list)
        {
            string InsSql = @"INSERT INTO FinDetails(FinDate,FinId,Description,EventType,Amount,Association,OrderNo,ItemType,Remark,Currency, Rate, CreatedTime,ModifiedTime)"
                            + "values(@FinDate,@FinId,@Description,@EventType,@Amount,@Association,@OrderNo,@ItemType, @Remark, @Currency, @Rate, @CreatedTime, @ModifiedTime)";
            string UpdSql = @"update FinDetails set FinDate=@FinDate,Description=@Description,EventType=@EventType,Amount=@Amount,Association=@Association,"
                            + "OrderNo=@OrderNo,ItemType=@ItemType,Remark=@Remark,Currency = @Currency, Rate = @Rate, ModifiedTime=@ModifiedTime "
                            + "where DetailId = @DetailId";

            string ExistRecordSql = "SELECT count(1) FROM FinDetails WHERE DetailId = ";

            List<MySqlParameter[]> InsertParameters = new List<MySqlParameter[]>();
            List<MySqlParameter[]> UpdateParameters = new List<MySqlParameter[]>();
            DateTime CurrentTime = DateTime.Now;
            foreach(FinDetails item in list)
            {

                MySqlParameter[] parameter = new MySqlParameter[]
                {
                    new MySqlParameter("@DetailId",item.DetailId),
                    new MySqlParameter("@FinId",item.FinId),
                    new MySqlParameter("@FinDate",item.FinDate),
                    new MySqlParameter("@Description",item.Description),
                    new MySqlParameter("@EventType",item.EventType),
                    new MySqlParameter("@Amount",item.Amount),
                    new MySqlParameter("@Association",item.Association),
                    new MySqlParameter("@OrderNo",item.OrderNo),
                    new MySqlParameter("@ItemType",item.ItemType),
                    new MySqlParameter("@Currency",item.Currency),
                    new MySqlParameter("@Rate",item.Rate),
                    new MySqlParameter("@Remark",item.Remark),
                    new MySqlParameter("@CreatedTime", CurrentTime),
                    new MySqlParameter("@ModifiedTime",CurrentTime)
                };
                int record = Convert.ToInt32(dbHelper.ExecuteScalar(trans, ExistRecordSql + item.DetailId, null));
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
                dbHelper.ExecuteNonQuery(trans, InsSql, InsertParameters);
            }
            if (UpdateParameters.Count > 0)
            {
                dbHelper.ExecuteNonQuery(trans, UpdSql, UpdateParameters);
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
            sql = sql + " order by FinDate asc";
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

            List<MySqlParameter[]> InsertParameters = new List<MySqlParameter[]>();
            List<MySqlParameter[]> UpdateParameters = new List<MySqlParameter[]>();
            DateTime CurrentTime = DateTime.Now;
            MySqlParameter[] parameter = new MySqlParameter[]
            {
                 new MySqlParameter("@FinId",finance.FinId),
                new MySqlParameter("@FinDate",finance.FinDate),
                new MySqlParameter("@Description",finance.Description),
                new MySqlParameter("@EventType",finance.EventType),
                new MySqlParameter("@Account",finance.Account),
                new MySqlParameter("@ItemType",finance.ItemType),
                new MySqlParameter("@ReferenceNo",finance.ReferenceNo),
                new MySqlParameter("@ReceivePaymentor",finance.ReceivePaymentor),
                new MySqlParameter("@Customer",finance.Customer),
                new MySqlParameter("@Association",finance.Association),
                new MySqlParameter("@Amount",finance.Amount),
                new MySqlParameter("@Rate",finance.Rate),
                new MySqlParameter("@Currency",finance.Currency),
                new MySqlParameter("@Remark",finance.Remark),
                new MySqlParameter("@CreatedTime", CurrentTime),
                new MySqlParameter("@ModifiedTime",CurrentTime)
            };
            int record = Convert.ToInt32(dbHelper.ExecuteScalar(ExistRecordSql + finance.FinId, null));
            using (MySqlConnection connection = dbHelper.GetConnection())
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    if (record == 0)
                    {
                        dbHelper.ExecuteNonQuery(transaction, InsSql, parameter);
                        int LasertInsertId = dbHelper.GetLastInsertId(transaction);
                        foreach (FinDetails detail in finance.Details)
                        {
                            detail.FinId = LasertInsertId;
                        }
                    }
                    else
                    {
                        dbHelper.ExecuteNonQuery(transaction, UpdSql, parameter);
                        dbHelper.ExecuteNonQuery(transaction, "delete from FinDetails where FinId =" + finance.FinId);
                    }
                    InsertOrUpdateDetails(transaction, finance.Details);
                    transaction.Commit();
                }
            }
        }


        public QueryObject<Finance> GetFinances(QueryObject<Finance> query)
        {
            string sql = "select *, Amount * Rate TotalAmount FROM Finance f where 1 = 1 ";
            List<MySqlParameter> QueryParameters = new List<MySqlParameter>();
            if (query.Condition != null)
            {
                if (query.Condition.BeginTime != null)
                {
                    sql = sql + "and f.FinDate >= @BeginTime ";
                    QueryParameters.Add(new MySqlParameter("@BeginTime", query.Condition.BeginTime));
                }
                if (query.Condition.EndTime != null)
                {
                    sql = sql + "and f.FinDate <= @EndTime ";
                    QueryParameters.Add(new MySqlParameter("@EndTime", query.Condition.EndTime));
                }
                if (!string.IsNullOrEmpty(query.Condition.Description))
                {
                    sql = sql + "and f.Description like '%" + query.Condition.Description.Trim() + "%' ";
                }
                if (!string.IsNullOrEmpty(query.Condition.ItemType))
                {
                    sql = sql + "and f.ItemType = @ItemType ";
                    QueryParameters.Add(new MySqlParameter("@ItemType", query.Condition.ItemType));
                }
                if (!string.IsNullOrEmpty(query.Condition.EventType))
                {
                    sql = sql + "and f.EventType = @EventType ";
                    QueryParameters.Add(new MySqlParameter("@EventType", query.Condition.EventType));
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
            sql = sql + " order by f.FinDate asc";
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
