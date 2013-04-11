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
        public int Sort { set; get; }
        public int ParentId { set; get; }
    }

    public class PriceCate
    {
        public int Id { set; get; }
        public bool UsePrice1 { set; get; }
        public string Price1Name { set; get; }
        public double Price1Val { set; get; }
        public bool UsePrice2 { set; get; }
        public string Price2Name { set; get; }
        public double Price2Val { set; get; }
        public bool UsePrice3 { set; get; }
        public string Price3Name { set; get; }
        public double Price3Val { set; get; }
        public bool UsePrice4 { set; get; }
        public string Price4Name { set; get; }
        public double Price4Val { set; get; }
        public bool UsePrice5 { set; get; }
        public string Price5Name { set; get; }
        public double Price5Val { set; get; }
        public string Status { set; get; }
    }


    public class Product
    {
        public int Id { set; get; }
        public int CategoryId { set; get; }
        public string Name { set; get; }
        public string Model { set; get; }
        public double Price { set; get; }
        public int PriceCate { set; get; }
        public string Size { set; get; }
        public string Weight { set; get; }
        public int Minimum { set; get; }
        public string Packing { set; get; }
        public int Sort { set; get; }
        public string Status { set; get; }
        public DateTime CreatedTime { set; get; }
        public DateTime ModifiedTime { set; get; }
    }
    public class ProductImage
    {
        public int Id { set; get; }
        public int ProductId { set; get; }
        public bool IsMain { set; get; }
        public byte[] Image { set; get; }
        public DateTime CreatedTime { set; get; }
        public DateTime ModifiedTime { set; get; }
    }
}
