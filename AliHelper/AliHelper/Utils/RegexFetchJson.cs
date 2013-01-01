using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AliHelper
{
    public class RegexFetchJson
    {

        public static string FetchJson(string reg, string html)
        {
            Regex r = new Regex(reg);
            GroupCollection gc = r.Match(html).Groups;
            if (gc != null && gc.Count > 1)
            {
                return gc[1].Value.Trim();
            }
            return "";
        }
    }
}
