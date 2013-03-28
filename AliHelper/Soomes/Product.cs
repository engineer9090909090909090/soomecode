using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace soomes
{
    public class Categories
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int ChildrenCount { set; get; }
        public int ProductCount { set; get; }
        public int Level { set; get; }
        public int ParentId { set; get; }
    }

    public class Product
    {
        public int Id { set; get; }
        public int CategoryId { set; get; }
        public string Name { set; get; }
        public string Model { set; get; }
        public Byte[] Image { set; get; }
        public double Price { set; get; }
        public int PriceCate { set; get; }
        public string Size { set; get; }
        public string Weight { set; get; }
        public int Minimum { set; get; }
        public string Packing { set; get; }
        public int Sort { set; get; }
        public string Status { set; get; }
    }
    public class ProductImage
    {
        public int Id { set; get; }
        public int ProductId { set; get; }
        public Byte[] Image { set; get; }
    }
}
