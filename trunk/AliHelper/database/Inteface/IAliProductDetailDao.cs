using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Reflection;
using System.Data.SQLite;
using System.Data;

namespace Database
{
    public interface IAliProductDetailDao
    {
        void InsertOrUpdate(ProductDetail detail);

        void Insert(ProductDetail detail);

        void Update(ProductDetail detail);

        void UpdateCustomAttr(int pid, Dictionary<FormElement, FormElement> CustomAttr);

        void UpdateSystemAttr(int pid, int Type, List<AttributeNode> systemAttr);

        ProductDetail GetProductDetail(int pid);

        Dictionary<FormElement, FormElement> GetCustomAttr(int pid);

        List<AttributeNode> GetSystemAttr(int pid, int Type);
    }
}
