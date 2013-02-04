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
        public FinOrderManager()
        {
            financeDao = DAOFactory.Instance.GetFinanceDao();
        }

        public QueryObject<FinDetails> GetFinDetails(QueryObject<FinDetails> query)
        {
            return financeDao.GetFinDetails(query);
        }

        public void InsertOrUpdateDetails(List<FinDetails> list) 
        {
            financeDao.InsertOrUpdateDetails(list);
        }
    }
}
