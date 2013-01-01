using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace AliHelper
{
    class ShareCookie
    {
        private static ShareCookie instance;
        private ShareCookie() { }
        public static ShareCookie Instance
       {
          get 
          {
             if (instance == null)
             {
                 instance = new ShareCookie();
             }
             return instance;
          }
       }

        public string LoginCookie { get; set; }

        public CookieContainer LoginCookieContainer { get; set; }

    }
}
