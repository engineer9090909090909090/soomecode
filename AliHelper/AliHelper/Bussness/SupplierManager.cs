using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Database;
using Soomes;

namespace AliHelper
{
    class SupplierManager : BaseManager
    {
        public ISupplierDao supplierDao;

        public SupplierManager()
        {
            supplierDao = DAOFactory.GetInstance().GetSupplierDao();
        }

        public QueryObject<Supplier> GetSupplierList(QueryObject<Supplier> query)
        {
            return supplierDao.GetSuppliers(query);
        }

        public Supplier GetSupplierById(int id)
        {
            return supplierDao.GetSupplierById(id);
        }
    }
}
