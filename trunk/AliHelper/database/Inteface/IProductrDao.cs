using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Data.SQLite;
using System.Data;
using System.Data.Common;
using soomes;

namespace Database
{
    public interface IProductDao
    {

        List<Categories> GetAllCategories();

        List<Categories> GetChildCategories(int ParentId);

        void InsertOrUpdateCategory(Categories item);

        List<PriceCate> GetPriceCates();

        void InsertOrUpdatePriceCate(PriceCate item);

        void DeleteCategory(int Id);

        void DeletePriceCate(int Id);
    }
}
