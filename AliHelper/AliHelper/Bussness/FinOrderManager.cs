using System;
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
        public event NewEditItemEvent OnNewEditEvent;

        public virtual void FireNewEditEvent(object o)
        {
            if (OnNewEditEvent != null)
            {
                ItemEventArgs e = new ItemEventArgs(o);
                OnNewEditEvent(this, e);
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
            FireNewEditEvent(detail);
        }

        public void InsertOrUpdateDetails(List<FinDetails> list) 
        {
            financeDao.InsertOrUpdateDetails(list);
            FireNewEditEvent(list);
        }
    }
}
