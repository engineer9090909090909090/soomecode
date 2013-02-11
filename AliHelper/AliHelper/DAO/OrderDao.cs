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
            string sql = "select Id,BeginDate,EndDate,Description,OrderNo,SalesMan,Status,Remark,CreatedTime,ModifiedTime ";
            sql = sql + "FROM Orders where 1 = 1 ";
            query.dt = dbHelper.ExecuteDataTable(sql, null);
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
                info.EndDate = (string)row["EndDate"];
                info.Description = (string)row["Description"];
                info.OrderNo = (string)row["OrderNo"];
                info.SalesMan = (string)row["SalesMan"];
                info.Status = (string)row["Status"];
                info.Remark = (string)row["Remark"];
                info.CreatedTime = Convert.ToDateTime(row["CreatedTime"]);
                info.ModifiedTime = Convert.ToDateTime(row["ModifiedTime"]);
                list.Add(info);
            }
            return list;
        }

        public void InsertOrUpdateOrder(Order order)
        {
            string InsSql = @"INSERT INTO Orders(BeginDate,EndDate,Description,OrderNo,SalesMan,Status,Remark,CreatedTime,ModifiedTime)"
                            + "values(@BeginDate,@EndDate,@Description,@OrderNo,@SalesMan,@Status,@Remark,@CreatedTime,@ModifiedTime)";
            string UpdSql = @"update Orders set OrderName= @OrderName, @EndDate,Status = @Status, Remark=@Remark, ModifiedTime=@ModifiedTime"
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
    }
}
