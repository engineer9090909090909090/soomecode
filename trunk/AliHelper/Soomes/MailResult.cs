using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soomes
{
    public class MailResult
    {
        public string Message { set; get; }
        public int Code { set; get; }
        public bool XssFilter { set; get; }
        public bool IncludeNull { set; get; }
        public MailData Data { set; get; }
    }

    public class MailData
    {
        public int Limit { set; get; }
        public int TotalNum { set; get; }
        public int StartRow { set; get; }
        public List<MailItem> List { set; get; }
    }

    public class MailItem
    { 
        public Int64 Id { set; get; }

        public string Subject { set; get; }

        public string OperatorName { set; get; }

        public Int64 MessageId { set; get; }

        public Int64 GmtCreate { set; get; }

        public Int64 gmtModified { set; get; }

        public Int64 ReceiverType { set; get; }

        public string OperatorId { set; get; }

        public string AlitalkEncrypt { set; get; }

        public string FeedbackType { set; get; }

        public Int64 TargetId { set; get; }

        public Int64 SenderVacount { set; get; }
        
        public Int64 SenderType { set; get; }
        
        public Int64 AttachmentStatus { set; get; }

        public Int64 ReceiverVacount { set; get; }

        public Int64 SenderStatus { set; get; }

        public Int64 SpamStatus { set; get; }

        public Int64 ReceiverStatus { set; get; }

        public Int64 ReplyStatus { set; get; }

        public Int64 DeleteStatus { set; get; }

        public string ReceiverCountry { set; get; }

        public Int64 FollowStatus { set; get; }

        public string SenderCountry { set; get; }

        public Int64 TradeId { set; get; }

        public Int64 SenderId { set; get; }

        public string SenderDisplayName { set; get; }

        public Int64 ReceiverId { set; get; }

        public string ReceiverDisplayName { set; get; }
        
        public string AppSource { set; get; }

        public string AppFrom { set; get; }

        public string AppTo { set; get; }

        public string RejectRelatedFeedbackId { set; get; }

        public Int64 SendBoxFolderId { set; get; }

        public string FollowTime { set; get; }

        public bool IsContact { set; get; }

        public string ReceiverCompanyName { set; get; }

        public string ReceiverIdEncrypt { set; get; }

        public string ReceiverCountryCode { set; get; }
    
    }
}
