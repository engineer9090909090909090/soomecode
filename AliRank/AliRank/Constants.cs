using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliRank
{
    class Constants
    {
        public static string DB_PASSWORD = "soomes2008";
        public static string INI_FILE = "company.ini";
        public static string DEBUG_DB_FILE = "AliRankDebug.db";
        public static string DB_FILE = "AliRankRealse.db";
        public static string IMAGE_DIR = "images";
        public static string DES_KEY = "83697732";

        public static string CLICK_SECTIONS = "Click Section";
        public static string AUTO_CLICK_NUM = "AutoClickNum";
        public static string MAX_PAUSE_TIME = "MaxPauseTime";
        public static string MIN_INTERVAL_TIME = "MinIntervalTime";
        public static string MAX_INTERVAL_TIME = "MaxIntervalTime";
        public static string MAX_QUERY_PAGE = "MaxQueryPage";
        public static string NETWORK_CHOICE = "Network Choice";
        public static string LOGIN_USER = "LoginUser";
        public static string LOGIN_PASS = "LoginPass";
        public static string LOGIN_REMINDE = "LoginRemind";
        public static string YES = "1";
        public static string NO =  "0";

        public static string NETWORK_VPN = "2";
        public static string NETWORK_AGENT = "1";
        public static string NETWORK_NONE = "0";

        public static string RUN_ONLY_CLICK = "0";
        public static string RUN_CLICK_INQUIRY = "1";

        public static string PPTP = "PPTP";
        public static string L2TP = "L2TP";

        public static int EFFECTIVE = 1;
        public static int INVALID = 0;

        public static string UserAgent = "User-Agent:Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; BTRS129735; chromeframe/23.0.1271.97;)" + Environment.NewLine;
            
    }
}
