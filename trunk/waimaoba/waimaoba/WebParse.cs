using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace com.soomes
{
    interface WebParse: IDisposable
    {
        void Parse(string url, HtmlDocument document);
    }
}
