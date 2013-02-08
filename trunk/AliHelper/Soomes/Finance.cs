using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soomes
{
    public class FinDetails
    {
        public int DetailId { set; get; }
        public string EventTime { set; get; }
        public string EventName { set; get; }
        public string EventType { set; get; }
        public string Currency { set; get; }
        public double Amount { set; get; }
        public double Rate { set; get; }
        public string Association { set; get; }
        public string OrderNo { set; get; }
        public string ItemType { set; get; }
        public string Remark { set; get; }
        public DateTime CreatedTime { set; get; }
        public DateTime ModifiedTime { set; get; }

        //query field
        public string BeginTime { set; get; }
        public string EndTime { set; get; }

        //qurey result field
        public double TotalAmount { set; get; }
    }


    public class Finance
    {
        public int FinId { set; get; }
        public string FinDate { set; get; }
        public string FinSource { set; get; }
        public string FinCompany { set; get; }
        public string Association { set; get; }
        public string Currency { set; get; }
        public double Amount { set; get; }
        public double Rate { set; get; }
        public string Remark { set; get; }
        public DateTime CreatedTime { set; get; }
        public DateTime ModifiedTime { set; get; }

        public List<FinDetails> Details { set; get; }
    }
}
