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
    public interface ISupplierDao
    {
        QueryObject<Supplier> GetSuppliers(QueryObject<Supplier> query);

        List<SupplierItem> GetSupplierItems(int ProductId, int SupplierId);

        Supplier GetSupplierById(int id);

        byte[] GetSupplierItemImage(int SupplierItemId);

        void InsertOrUpdateSupplier(Supplier item);

        void InsertOrUpdateSupplierItem(SupplierItem item);

        void DeleteSupplier(int Id);

        void DeleteSupplierItem(int Id);
    }
}
