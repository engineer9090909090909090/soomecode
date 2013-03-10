using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Data.SQLite;
using System.Data;
using System.Data.Common;

namespace Database
{
    public interface IOrderDao
    {
        QueryObject<Order> GetOrders(QueryObject<Order> query);

        void InsertOrUpdateOrder(Order order, string tracker);

        Order GetOrderById(int id);

        OrderTracking GetOrderTrackingById(int id);

        void InsertOrUpdateTracking(OrderTracking tracking);
        
        void InsertOrUpdateTracking(SQLiteConnection connection, OrderTracking tracking);

        int TrackingCount(int OrderId);

        void UpdateOrderStatus(SQLiteConnection connection, Order order);

        List<OrderTracking> GetOrderTrackingList(int orderId);
    }
}
