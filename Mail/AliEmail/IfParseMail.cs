using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace AliEmail
{
    public interface IfParseMail
    {
        object[] Parse(string subject, string body);

        string getType();
    }
}
