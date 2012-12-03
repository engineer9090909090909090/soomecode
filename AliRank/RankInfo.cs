using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliRank
{
    public class RankInfo
    {
        public Int32 Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public Int32 Rank { get; set; }
        public string RankKeyword { get; set; }
        public Int32 KeyAdNum { get; set; }
        public Int32 KeyP4Num { get; set; }
        public DateTime UpdateTime { get; set; }
        public Int32 QueryStatus { get; set; }
    }
}
