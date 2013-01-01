using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliHelper
{
    public class DateUtils
    {
        public static long DateTimeToInt(DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;            //除10000调整为13位
            return t;
        }
    }
}
