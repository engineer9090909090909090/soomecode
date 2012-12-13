using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliRank
{

    public delegate void RankSearchingEvent(object sender, RankEventArgs e);
    public delegate void RankSearchEndEvent(object sender, RankEventArgs e);


    public delegate void RankClickEndEvent(object sender, RankEventArgs e);
    public delegate void RankClickingEvent(object sender, RankEventArgs e);
    public delegate void RankInquiryEndEvent(object sender, InquiryEventArgs e);

    public class RankEventArgs : EventArgs
    {
        public ShowcaseRankInfo Item;
        public string Msg;

        public RankEventArgs(ShowcaseRankInfo _obj, string _msg)
        {
            this.Item = _obj;
            this.Msg = _msg;
        }
    }

    public class InquiryEventArgs : EventArgs
    {
        public InquiryInfos Item;
        public string Msg;

        public InquiryEventArgs(InquiryInfos _obj, string _msg)
        {
            this.Item = _obj;
            this.Msg = _msg;
        }
    }


    public delegate void TopFiveSearchingEvent(object sender, TopFiveEventArgs e);
    public delegate void TopFiveSearchEndEvent(object sender, TopFiveEventArgs e);

    public class TopFiveEventArgs : EventArgs
    {
        public TopFiveInfo Item;
        public string Msg;

        public TopFiveEventArgs(TopFiveInfo _obj, string _msg)
        {
            this.Item = _obj;
            this.Msg = _msg;
        }
    }

}
