using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliRank
{
    public class Keywords
    {
        public Int32 Id { get; set; }
        public string ProductId { get; set; }
        public string CompanyUrl { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public string ProductImg { get; set; }
        public string MainKey { get; set; }
        public Int32 Rank { get; set; }
        public Int32 KeyAdNum { get; set; }
        public Int32 KeyP4Num { get; set; }
        public Int32 Clicked { get; set; }
        public DateTime UpdateTime { get; set; }

        public string Image { get; set; }

        public static string GetRankInfo(Keywords item)
        {
            string msg = (item.Rank == 0) ? "前500位没有本产品排名," : "该产品当前排在" + item.Rank + "位,";
            msg += item.KeyAdNum + "个产品购买了该关键词排名,";
            msg += item.KeyP4Num + "个产品买了直通车服务";
            return msg;
        }

    }
}
