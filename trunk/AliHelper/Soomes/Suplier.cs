using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soomes
{
    public class Suplier
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Contact { set; get; }
        public string Remark { set; get; }
        public string Address { set; get; }
    }

    public class SuplierItem
    {
        public int Id { set; get; }
        public int ProductId { set; get; }
        public int SuplierId { set; get; }
        public string Name { set; get; }
        public Byte[] Image { set; get; }
        public double Price { set; get; }
        public string PriceDesc { set; get; }
        public string Remark { set; get; }
    }
}
