using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliHelper
{
    class Constants
    {
        public const string LoginUrl = "https://login.alibaba.com/";

        public const string HomeUrl = "http://www.alibaba.com/";

        public const string DB_FILE = "data.db";

        public const string PhtotBank = "PhtotBank";

        public const string ProductImages = "ProductImages";

        public const string YES = "1";
        public const string NO =  "0";

        public static string UserAgent = "User-Agent:Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; BTRS129735; chromeframe/23.0.1271.97;)" + Environment.NewLine;

        public const string DateFormat = "yyyy-MM-dd";
        public const string PageType_Post = "post";
        public const string PageType_Edit = "edit";

        public const int AttrType_SysAttr = 1;
        public const int AttrType_FixAttr = 2;

        public const string DebitCredit = "DebitCreditType";
        public const string Employee = "Employee";
        public const string BussnessType = "BussnessType";
        public const string OrderStatusType = "OrderStatusType";
        public const string CurrencyType = "CurrencyType";
        public const string RecivePaymentAccounts = "RecivePaymentAccounts";

        public const string FinanceBaseView = "FinanceBaseView";
        public const string FinanceBizView = "FinanceBizView";
        public const string FinanceWaterView = "FinanceWaterView";

        public const string OrderBaseView = "OrderBaseView";
        public const string OrderTrackView = "OrderTrackView";
    }
}
