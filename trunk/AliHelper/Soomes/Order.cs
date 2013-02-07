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
        public string OrderName { set; get; }
        public string SalesMan { set; get; }
        public string OrderNo { set; get; }
        public string Status { set; get; }
        public string Remark { set; get; }
        public DateTime CreatedTime { set; get; }
        public DateTime ModifiedTime { set; get; }

    }
}
