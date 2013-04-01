using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soomes
{
    public class Supplier
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Contact { set; get; }
        public string Remark { set; get; }
        public string Address { set; get; }
    }

    public class SupplierItem
    {
        public int Id { set; get; }
        public int ProductId { set; get; }
        public int SupplierId { set; get; }
        public string Name { set; get; }
        public Byte[] Image { set; get; }
        public double Price { set; get; }
        public string PriceDesc { set; get; }
        public string Remark { set; get; }
    }
}
