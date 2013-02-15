﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AliHelper.DAO;
using Soomes;

namespace AliHelper.Bussness
{
    public class FinOrderManager : BaseManager
    {
        public FinanceDao financeDao;
        static public event NewEditItemEvent OnEditFinDetailEvent;
        static public event NewEditItemEvent OnEditFinanceEvent;
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

        public FinOrderManager()
        {
            financeDao = DAOFactory.Instance.GetFinanceDao();
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
    }
}