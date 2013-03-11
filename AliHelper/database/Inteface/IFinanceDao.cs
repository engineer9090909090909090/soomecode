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
    public interface IFinanceDao
    {
        QueryObject<FinDetails> GetFinDetails(QueryObject<FinDetails> query);

        void InsertOrUpdateDetails(List<FinDetails> list);

        void InsertOrUpdateDetails(DbTransaction trans, List<FinDetails> list);

        FinDetails GetFinDetail(int id);

        List<FinDetails> GetFinDetails(int finId);

        void InsertOrUpdateFinance(Finance finance);

        QueryObject<Finance> GetFinances(QueryObject<Finance> query);

        Finance GetFinance(int finId);

    }
}
