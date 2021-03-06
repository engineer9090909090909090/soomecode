﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Data.SQLite;
using System.Data;
using System.Data.Common;

namespace Database
{
    public class OrderDao : IOrderDao
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

            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS OrderTracking("
            + "Id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"
            + "OrderId integer not null,"
            + "TrackingDate varchar(10),"
            + "Description varchar(2000) not null,"
            + "Tracker varchar(20) not null,"
            + "Status varchar(10) not null,"
            + "CreatedTime datetime,"
            + "ModifiedTime datetime)");

            dbHelper.ExecuteNonQuery("Create Index IF NOT EXISTS Index_key on OrderTracking(OrderId);");
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
            sql = sql + " Order by BeginDate asc";
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

        public void InsertOrUpdateOrder(Order order, string tracker)
        {
            string InsSql = @"INSERT INTO Orders(BeginDate,EndDate,Description,OrderNo,SalesMan,Status,Remark,CreatedTime,ModifiedTime) "
                            + "values(@BeginDate,@EndDate,@Description,@OrderNo,@SalesMan,@Status,@Remark,@CreatedTime,@ModifiedTime) ";
            string UpdSql = @"update Orders set OrderNo=@OrderNo, BeginDate=@BeginDate, EndDate=@EndDate, Description= @Description, "
                            + "SalesMan=@SalesMan, Status = @Status, Remark=@Remark, ModifiedTime=@ModifiedTime "
                            + "where Id = @Id";

            string ExistRecordSql = "SELECT count(1) FROM Orders WHERE Id = ";
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

            using (SQLiteConnection connection = dbHelper.GetConnection())
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    int record = Convert.ToInt32(dbHelper.ExecuteScalar(transaction, ExistRecordSql + order.Id, null));
                    int orderId = 0;
                    if (record == 0)
                    {
                        dbHelper.ExecuteNonQuery(transaction, InsSql, parameter);
                        orderId = dbHelper.GetLastInsertId(transaction);
                        
                    }
                    else
                    {
                        dbHelper.ExecuteNonQuery(transaction, UpdSql, parameter);
                        orderId = order.Id;
                    }
                    string TrackingCountSql = "SELECT count(1) FROM OrderTracking WHERE Id = " + orderId;
                    int rrackingCount = Convert.ToInt32(dbHelper.ExecuteScalar(transaction, TrackingCountSql, null));
                    if (rrackingCount == 0)
                    {
                        OrderTracking tracking = new OrderTracking();
                        tracking.OrderId = orderId;
                        tracking.Status = order.Status;
                        tracking.TrackingDate = order.BeginDate;
                        tracking.Description = order.Description + " - " + order.Status;
                        tracking.Tracker = tracker;
                        InsertOrUpdateTracking(transaction, tracking);
                    }
                    transaction.Commit();
                }
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

        public OrderTracking GetOrderTrackingById(int id)
        {
            string sql = "select t.* FROM OrderTracking t where id = " + id;
            DataTable dt = dbHelper.ExecuteDataTable(sql, null);
            List<OrderTracking> list = new List<OrderTracking>();
            foreach (DataRow row in dt.Rows)
            {
                OrderTracking info = new OrderTracking();
                info.Id = Convert.ToInt32(row["Id"]);
                info.OrderId = Convert.ToInt32(row["OrderId"]);
                info.TrackingDate = (string)row["TrackingDate"];
                info.Description = (string)row["Description"];
                info.Tracker = (string)row["Tracker"];
                info.Status = (string)row["Status"];
                info.CreatedTime = Convert.ToDateTime(row["CreatedTime"]);
                info.ModifiedTime = Convert.ToDateTime(row["ModifiedTime"]);
                list.Add(info);
            }
            if (list.Count > 0)
                return list[0];
            else
                return null;
        }

        public void InsertOrUpdateTracking(OrderTracking tracking)
        {
            InsertOrUpdateTracking(null, tracking);
        }

        public void InsertOrUpdateTracking(DbTransaction trans, OrderTracking tracking)
        {
            string InsSql = @"INSERT INTO OrderTracking(OrderId, TrackingDate, Description, Tracker, Status, CreatedTime, ModifiedTime) "
                            + "values(@OrderId, @TrackingDate, @Description, @Tracker, @Status, @CreatedTime, @ModifiedTime) ";
            string UpdSql = @"update OrderTracking set OrderId=@OrderId, TrackingDate=@TrackingDate, Description=@Description,"
                            + " Tracker=@Tracker, Status=@Status, ModifiedTime=@ModifiedTime "
                            + "where Id = @Id";

            string ExistRecordSql = "SELECT count(1) FROM OrderTracking WHERE Id = " + tracking.Id;
            
            DateTime CurrentTime = DateTime.Now;
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@Id",tracking.Id),
                new SQLiteParameter("@OrderId",tracking.OrderId),
                new SQLiteParameter("@TrackingDate",tracking.TrackingDate),
                new SQLiteParameter("@Description",tracking.Description),
                new SQLiteParameter("@Tracker",tracking.Tracker),
                new SQLiteParameter("@Status",tracking.Status),
                new SQLiteParameter("@CreatedTime", CurrentTime),
                new SQLiteParameter("@ModifiedTime",CurrentTime)
            };
            if (trans == null)
            {
                using (SQLiteConnection connection = dbHelper.GetConnection())
                {
                    connection.Open();
                    using (DbTransaction transaction = connection.BeginTransaction())
                    {

                        int ExistTrackingId = Convert.ToInt32(dbHelper.ExecuteScalar(ExistRecordSql, null));
                        if (ExistTrackingId == 0)
                        {
                            dbHelper.ExecuteNonQuery(transaction, InsSql, parameter);
                        }
                        else
                        {
                            dbHelper.ExecuteNonQuery(transaction, UpdSql, parameter);
                        }
                        Order order = new Order();
                        order.Id = tracking.OrderId;
                        order.Status = tracking.Status;
                        order.EndDate = tracking.IsClosed?tracking.TrackingDate:string.Empty;
                        UpdateOrderStatus(transaction, order);
                        transaction.Commit();
                    }
                }
            }
            else 
            {
                int ExistTrackingId = Convert.ToInt32(dbHelper.ExecuteScalar(trans, ExistRecordSql, null));
                if (ExistTrackingId == 0)
                {
                    dbHelper.ExecuteNonQuery(trans, InsSql, parameter);
                }
                else
                {
                    dbHelper.ExecuteNonQuery(trans, UpdSql, parameter);
                }
            }
        }


        public int TrackingCount(int OrderId)
        {
            string ExistTrackingSql = "SELECT count(1) FROM OrderTracking WHERE OrderId = " + OrderId;
            return Convert.ToInt32(dbHelper.ExecuteScalar(ExistTrackingSql, null));
        }


        public void UpdateOrderStatus(DbTransaction trans, Order order)
        {
            string UpdSql = @"update Orders set EndDate=@EndDate, Status = @Status, ModifiedTime=@ModifiedTime where Id = @Id";
            DateTime CurrentTime = DateTime.Now;
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("@Id",order.Id),
                new SQLiteParameter("@EndDate",order.EndDate),
                new SQLiteParameter("@Status",order.Status),
                new SQLiteParameter("@ModifiedTime",CurrentTime)
            };
            dbHelper.ExecuteNonQuery(trans, UpdSql, parameter);
        }

        public List<OrderTracking> GetOrderTrackingList(int orderId)
        {
            string sql = "select * FROM OrderTracking t where OrderId = " + orderId;
            DataTable dt = dbHelper.ExecuteDataTable(sql, null);
            List<OrderTracking> list = new List<OrderTracking>();
            foreach (DataRow row in dt.Rows)
            {
                OrderTracking info = new OrderTracking();
                info.Id = Convert.ToInt32(row["Id"]);
                info.OrderId = Convert.ToInt32(row["OrderId"]);
                info.TrackingDate = (string)row["TrackingDate"];
                info.Description = (string)row["Description"];
                info.Tracker = (string)row["Tracker"];
                info.Status = (string)row["Status"];
                info.CreatedTime = Convert.ToDateTime(row["CreatedTime"]);
                info.ModifiedTime = Convert.ToDateTime(row["ModifiedTime"]);
                list.Add(info);
            }
            return list;
        }
    }
}
