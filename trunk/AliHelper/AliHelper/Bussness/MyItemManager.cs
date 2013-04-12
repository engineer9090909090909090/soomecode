using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Database;
using soomes;

namespace AliHelper
{
    public class MyItemManager : BaseManager
    {
        public IProductDao productDao;
        public ISupplierDao supplierDao;

        public MyItemManager()
        {
            productDao = DAOFactory.Instance.GetProductDao();
            supplierDao = DAOFactory.Instance.GetSupplierDao();
        }

        public void DelteCategory(int id)
        {
            List<Categories> list = productDao.GetChildCategories(id);
            if (list.Count == 0)
            {
                productDao.DeleteCategory(id);
                return;
            }
            throw new Exception("包含子类，不能删除。");
        }


    }
}
