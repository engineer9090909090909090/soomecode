using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.soomes
{
    public class BuyerInfo
    {
        public Int32 Id { set; get; }

        public string Type { set; get; }

        public string CompanyName { set; get; }

        public string CompanyInfo { set; get; }

        public string Email { set; get; }

        public string BuyerName { set; get; }

        public string ContactInfo { set; get; }

        public string Url { set; get; }

        public string  UrlTitle { set; get; }

        public int Status { set; get; }

        public List<string> Emails { set; get; }
    }
}
