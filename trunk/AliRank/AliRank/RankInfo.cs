using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliRank
{
    public class RankInfo
    {
        public Int32 ProductId { get; set; }
        public Int32 Rank { get; set; }
        public string RankKeyword { get; set; }
        public Int32 KeyAdNum { get; set; }
        public Int32 KeyP4Num { get; set; }
        public DateTime UpdateTime { get; set; }
        public Int32 QueryStatus { get; set; }
    }
}
