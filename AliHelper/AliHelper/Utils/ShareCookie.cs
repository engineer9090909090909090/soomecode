using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace AliHelper
{
    class ShareCookie
    {
        public static ShareCookie Instance = new ShareCookie();

        private ShareCookie() { }

        public string LoginCookie { get; set; }

        public CookieContainer LoginCookieContainer { get; set; }

        public string CsrfToken { get; set; }
    }
}
