using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliRank
{
    public class AliAccounts
    {
        public Int32 AccountId { set; get; }
        public string Email { set; get; }
        public string Name { set; get; }
        public string Password { set; get; }
        public string Country { set; get; }
        public string LoginIp  { set; get; }
    }

    public class InquiryInfos
    {
        public Int32 AccountId { set; get; }
        public Int32 ProductId { set; get; }
        public Int32 MsgId { set; get; }
        public string Company { set; get; }
        public string InquiryIp { set; get; }
        public DateTime InquiryTime { set; get; }
    }

    public class InquiryMessages
    {
        public Int32 MsgId { set; get; }
        public string Content { set; get; }
        public string Subject { set; get; }
    }
}
