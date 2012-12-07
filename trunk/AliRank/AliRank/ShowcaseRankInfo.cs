using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliRank
{
    public class ShowcaseRankInfo : RankInfo
    {
        public string CompanyUrl { get; set; }
        public string ProductUrl { get; set; }
        public string ProductImg { get; set; }
        public string ProductName { get; set; }
        public string MainKey { get; set; }
        public Int32 PrevRank { get; set; }
        public Int32 Clicked { get; set; }
        public Int32 Status { get; set; }

        public string Image { get; set; }

        public static string GetRankInfo(ShowcaseRankInfo item)
        {
            if (item.Rank == 0)
            {
                return "无排名";
            }
            int page = item.Rank / 38 + 1;
            int location = ( item.Rank % 38 == 0 )? 38 :  item.Rank % 38;
            string msg = (item.Rank == 0) ? "前380名找不到本产品," : "第" + item.Rank + "名,第" + page + "页第" + location + "位,";
            if (item.Rank  > 0 && item.PrevRank > 0)
            {
                msg += "\r\n比上次提高了" + (item.PrevRank - item.Rank) + "名,";
            }
            msg += "\r\n"+ item.KeyAdNum + "个固定排名,";
            msg += "\r\n" + item.KeyP4Num + "个直通车排名";
            return msg;
        }

    }
}
