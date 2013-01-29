using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soomes
{
    public class MailQuery
    {
        public string feedbackType { set; get; }
        public string owner { set; get; }
        public string filter { set; get; }
        public string listType { set; get; }
        public string folderName { set; get; }
        public Int32 startRow { set; get; }
        public Int32 limit { set; get; }
        public string orderBy { set; get; }
        public Int32 orderType { set; get; }
        public Int32 totalNum { set; get; }
    }

    public class MailQueryCmdType
    {
        public const string GetList = "getList";
        public const string GetTotalNum = "getTotalNum";
    }

    public class MailQueryListType 
    {
        public const string Spam = "spam";
        public const string Sendbox = "sendbox";
        public const string Inbox = "inbox";
        public const string Trash = "trash";
    }

    


}
