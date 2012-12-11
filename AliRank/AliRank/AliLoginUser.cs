using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliRank
{
    public class AliLoginUser
    {
        public List<string> xlogin_urls { get; set; }
        public AliPerson person_data { get; set; }
        public string time_out { get; set; }
        public List<string> proxy_cookies { get; set; }
    }

    public class AliPerson
    {
        public string login_id { get; set; }
        public string p_status { get; set; }
        public string first_name { get; set; }
        public string email { get; set; }
        public string last_name { get; set; }
        public string v_status { get; set; }
        public string country { get; set; }
    }
}