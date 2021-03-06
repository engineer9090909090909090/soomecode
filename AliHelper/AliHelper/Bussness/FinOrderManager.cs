﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using Database;

namespace AliHelper
{
    public class FinOrderManager : BaseManager
    {
        public IFinanceDao financeDao;
        public IOrderDao orderDao;
        static public event NewEditItemEvent OnEditFinDetailEvent;
        static public event NewEditItemEvent OnEditFinanceEvent;
        static public event NewEditItemEvent OnEditOrderEvent;
        static public event NewEditItemEvent OnEditTrackingEvent;

        public virtual void FireEditFinDetailEvent(object o)
        {
            if (FinOrderManager.OnEditFinDetailEvent != null)
            {
                ItemEventArgs e = new ItemEventArgs(o);
                OnEditFinDetailEvent(this, e);
            }
        }
        public virtual void FireEditFinanceEvent(object o)
        {
            if (FinOrderManager.OnEditFinanceEvent != null)
            {
                ItemEventArgs e = new ItemEventArgs(o);
                OnEditFinanceEvent(this, e);
            }
        }

        public virtual void FireEditOrderEvent(object o)
        {
            if (FinOrderManager.OnEditOrderEvent != null)
            {
                ItemEventArgs e = new ItemEventArgs(o);
                OnEditOrderEvent(this, e);
            }
        }

        public virtual void FireEditTrackingEvent(object o)
        {
            if (FinOrderManager.OnEditTrackingEvent != null)
            {
                ItemEventArgs e = new ItemEventArgs(o);
                OnEditTrackingEvent(this, e);
            }
        }

        public FinOrderManager()
        {
            financeDao = DAOFactory.GetInstance().GetFinanceDao();
            orderDao = DAOFactory.GetInstance().GetOrderDao();
        }

        public QueryObject<FinDetails> GetFinDetails(QueryObject<FinDetails> query)
        {
            return financeDao.GetFinDetails(query);
        }

        public void InsertOrUpdateDetails(FinDetails detail) 
        {
            List<FinDetails> list = new List<FinDetails>();
            list.Add(detail);
            financeDao.InsertOrUpdateDetails(list);
            list.Clear();
            list = null;
            FireEditFinDetailEvent(detail);
        }

        public void InsertOrUpdateDetails(List<FinDetails> list) 
        {
            financeDao.InsertOrUpdateDetails(list);
            FireEditFinDetailEvent(list);
        }

        public FinDetails GetFinDetail(int id)
        {
            return financeDao.GetFinDetail(id);
        }

        public void InsertOrUpdateFinance(Finance obj)
        {
            financeDao.InsertOrUpdateFinance(obj);
            FireEditFinanceEvent(obj);
        }

        public QueryObject<Finance> GetFinances(QueryObject<Finance> query)
        {
            return financeDao.GetFinances(query);
        }

        public Finance GetFinance(int finId)
        {
            return financeDao.GetFinance(finId);
        }

        public QueryObject<Order> GetOrders(QueryObject<Order> query)
        {
            return orderDao.GetOrders(query);
        }

        public Order GetOrderById(int id)
        {
            return orderDao.GetOrderById(id);
        }

        public void InsertOrUpdateOrder(Order o)
        {
            string tracker = o.SalesMan;
            orderDao.InsertOrUpdateOrder(o, tracker);
            FireEditOrderEvent(o);
        }

        public void InsertOrUpdateTracking(OrderTracking o)
        {
            orderDao.InsertOrUpdateTracking(o);
            FireEditTrackingEvent(o);
        }

        public List<OrderTracking> GetOrderTrackingList(int orderId)
        {
            return orderDao.GetOrderTrackingList(orderId);
        }

        public OrderTracking GetOrderTrackingById(int id)
        {
            return orderDao.GetOrderTrackingById(id);
        }
    }
}
