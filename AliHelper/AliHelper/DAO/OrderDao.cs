using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Data.SQLite;
using System.Data;

namespace AliHelper.DAO
{
    public class OrderDao
    {
        private SQLiteDBHelper dbHelper;

        public OrderDao(SQLiteDBHelper dbHelper)
        { 
            this.dbHelper = dbHelper;
            CreateTable();
        }

        private void CreateTable()
        {
            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS Orders("
            + "Id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"
            + "BeginDate varchar(10) not null,"
            + "EndDate varchar(10),"
            + "Description varchar(150) not null,"
            + "OrderNo varchar(10) not null,"
            + "SalesMan varchar(20) not null,"
            + "Status varchar(10) not null,"
            + "Remark varchar(500),"
            + "CreatedTime datetime,"
            + "ModifiedTime datetime)");

            dbHelper.ExecuteNonQuery("Create Index IF NOT EXISTS Index_key on Orders(OrderNo);");
        }

        public QueryObject<Order> GetOrders(QueryObject<Order> query)
        {
            string sql = string.Empty;
            if (!query.Condition.IsFinOrderView)
            {
                sql = "select t.* FROM Orders t where 1 = 1 ";
            }
            else {
                sql = "SELECT t.*, sum(d.Amount * d.Rate) TotalAmount FROM orders t left join findetails d on t.OrderNo = d.OrderNo where 1 = 1 ";
            }

            List<SQLiteParameter> QueryParameters = new List<SQLiteParameter>();
            if (query.Condition != null)
            {
                if (!string.IsNullOrEmpty(query.Condition.BeginDateForm))
                {
                    sql = sql + "and t.BeginDate >= @BeginDateForm ";
                    QueryParameters.Add(new SQLiteParameter("@BeginDateForm", query.Condition.BeginDateForm));
                }
                if (!string.IsNullOrEmpty(query.Condition.BeginDateTo))
                {
                    sql = sql + "and t.BeginDate <= @BeginDateTo ";
                    QueryParameters.Add(new SQLiteParameter("@BeginDateTo", query.Condition.BeginDateTo));
                }
                if (!string.IsNullOrEmpty(query.Condition.EndDateForm))
                {
                    sql = sql + "and t.EndDate >= @EndDateForm ";
                    QueryParameters.Add(new SQLiteParameter("@EndDateForm", query.Condition.EndDateForm));
                }
                if (!string.IsNullOrEmpty(query.Condition.EndDateTo))
                {
                    sql = sql + "and t.EndDate <= @EndDateTo ";
                    QueryParameters.Add(new SQLiteParameter("@EndDateTo", query.Condition.EndDateTo));
                }
                if (!string.IsNullOrEmpty(query.Condition.Description))
                {
                    sql = sql + "and t.Description like '%" + query.Condition.Description.Trim() + "%' ";
                }
                if (!string.IsNullOrEmpty(query.Condition.Status))
                {
                    sql = sql + "and t.Status = @Status ";
                    QueryParameters.Add(new SQLiteParameter("@Status", query.Condition.Status));
                }
                if (!string.IsNullOrEmpty(query.Condition.Remark))
                {
                    sql = sql + "and t.Remark like '%" + query.Condition.Remark.Trim() + "%' ";
                }
                if (!string.IsNullOrEmpty(query.Condition.SalesMan))
                {
                    sql = sql + "and t.SalesMan = @SalesMan ";
                    QueryParameters.Add(new SQLiteParameter("@SalesMan", query.Condition.SalesMan));
                }
            }
            if (query.Condition.IsFinOrderView)
            {
                sql = sql + "Group By t.Id";
            }
            query.dt = dbHelper.ExecuteDataTable(sql, QueryParameters.ToArray());
            query.Result = DataTableToList(query.dt);
            return query;
        }

        public List<Order> DataTableToList(DataTable dt)
        {
            List<Order> list = new List<Order>();
            foreach (DataRow row in dt.Rows)
            {
                Order info = new Order();
                info.Id = Convert.ToInt32(row["Id"]);
                info.BeginDate = (string)row["BeginDate"];
                info.EndDate = !Convert.IsDBNull(row["EndDate"]) ? (string)row["EndDate"] : string.Empty;
                info.Description = (string)row["Description"];
                info.OrderNo = (string)row["OrderNo"];
                info.SalesMan = (string)row["SalesMan"];
                info.Status = (string)row["Status"];
                info.Remark = !Convert.IsDBNull(row["Remark"]) ? (string)row["Remark"] : string.Empty;
                info.TotalAmount = dt.Columns["TotalAmount"] != null && !Convert.IsDBNull(row["TotalAmount"]) ? Convert.ToDouble(row["TotalAmount"]) : 0;
                info.CreatedTime = Convert.ToDateTime(row["CreatedTime"]);
                info.ModifiedTime = Convert.ToDateTime(row["ModifiedTime"]);
                list.Add(info);
            }
            return list;
        }

        public void InsertOrUpdateOrder(Order order)
        {
            string InsSql = @"INSERT INTO Orders(BeginDate,EndDate,Description,OrderNo,SalesMan,Status,Remark,CreatedTime,ModifiedTime) "
                            + "values(@BeginDate,@EndDate,@Description,@OrderNo,@SalesMan,@Status,@Remark,@CreatedTime,@ModifiedTime) ";
            string UpdSql = @"update Orders set OrderNo=@OrderNo, BeginDate=@BeginDate, EndDate=@EndDate, Description= @Description, "
                            + "SalesMan=@SalesMan, Status = @Status, Remark=@Remark, ModifiedTime=@ModifiedTime "
                            + "where Id = @Id";

            string ExistRecordSql = "SELECT count(1) FROM Orders WHERE Id = ";

            List<SQLiteParameter[]> InsertParameters = new List<SQLiteParameter[]>();
            List<SQLiteParameter[]> UpdateParameters = new List<SQLiteParameter[]>();
            DateTime CurrentTime = DateTime.Now;
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@Id",order.Id),
                new SQLiteParameter("@BeginDate",order.BeginDate),
                new SQLiteParameter("@EndDate",order.EndDate),
                new SQLiteParameter("@Description",order.Description),
                new SQLiteParameter("@OrderNo",order.OrderNo),
                new SQLiteParameter("@SalesMan",order.SalesMan),
                new SQLiteParameter("@Status",order.Status),
                new SQLiteParameter("@Remark",order.Remark),
                new SQLiteParameter("@CreatedTime", CurrentTime),
                new SQLiteParameter("@ModifiedTime",CurrentTime)
            };
            int record = Convert.ToInt32(dbHelper.ExecuteScalar(ExistRecordSql + order.Id, null));
            if (record == 0)
            {
                dbHelper.ExecuteNonQuery(InsSql, parameter);
            }
            else
            {
                dbHelper.ExecuteNonQuery(UpdSql, parameter);
            }
        }

        public Order GetOrderById(int id)
        {
            string sql = "select t.* FROM Orders t where id = " + id;
            DataTable dt = dbHelper.ExecuteDataTable(sql, null);
            List<Order> list = DataTableToList(dt);
            if (list.Count > 0)
                return list[0];
            else 
                return null;
        }
    }
}
