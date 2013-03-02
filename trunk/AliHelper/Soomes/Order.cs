using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soomes
{
    public class Order
    {
        public int Id { set; get; }
        public string BeginDate { set; get; }
        public string EndDate { set; get; }
        public string Description { set; get; }
        public string SalesMan { set; get; }
        public string OrderNo { set; get; }
        public string Status { set; get; }
        public string Remark { set; get; }
        public DateTime CreatedTime { set; get; }
        public DateTime ModifiedTime { set; get; }


        //query field
        public string BeginDateForm { set; get; }
        public string BeginDateTo { set; get; }
        public string EndDateForm { set; get; }
        public string EndDateTo { set; get; }
        public bool IsFinOrderView { set; get; }

        //result field
        public double TotalAmount { set; get; }
    }

    public class OrderTracking
    { 
        public int Id { set; get; }
        public int OrderId { set; get; }
        public string TrackingDate { set; get; }
        public string Description { set; get; }
        public string Tracker { set; get; }
        public string Status { set; get; }
        public DateTime CreatedTime { set; get; }
        public DateTime ModifiedTime { set; get; }
    }
}
