using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AliRank
{
    class FormatValidation
    {
        public static bool IsEmail(string strEmail)
        {
            Regex   rCode   =   new   Regex(@"^\w+((-\w+)|(\.\w+))*\@\w+((\.|-)\w+)*\.\w+$"); 
            if(!rCode.IsMatch(strEmail)) 
            { 
                return   false; 
            }
            return   true; 
        }
    }
}
