using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace AliRank
{
    public class ShareCookie
    {
        public static ShareCookie Instance = new ShareCookie();

        private ShareCookie() { }

        public string LoginCookie { get; set; }

        public List<Cookie> LoginCookies { get; set; }

    }
}
