using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliRank
{
    public class VpnModel
    {
        public Int32 Id { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string VpnType { get; set; }
        public string Country { set; get; }
        public string Name { set; get; }
        public string L2tpSec { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
