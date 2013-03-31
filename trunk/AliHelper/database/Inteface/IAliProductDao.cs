using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using Soomes;
using System.Data;

namespace Database
{
    public interface IAliProductDao
    {
        void DeleteProduct4GroupId(int groupId);

        void InsertOrUpdate(List<AliProduct> list);

        QueryObject<AliProduct> GetAliProductList(QueryObject<AliProduct> query);

        AliProduct GetAliProduct(int Id);

        bool IsNeedUpdateDetail(int id);
    }
}
