using System;
using System.Collections.Generic;
using System.Text;

namespace com.soomes.model
{
    public class VpnModel
    {
        private long _id;
        private string _ip;
        private string _userId;
        private string _password;
        private string _type;
        private bool _enabled;

        /// <summary>
        /// ID
        /// </summary>
        public long Id 
        {
            get { return _id;  }
            set { _id = value; }
        }

        /// <summary>
        /// IP
        /// </summary>
        public string Ip
        {
            get { return _ip;  }
            set { _ip = value; }
        }

        /// <summary>
        /// UserId
        /// </summary>
        public string UserId
        {
            get { return _userId;  }
            set { _userId = value; }
        }

        /// <summary>
        /// Password
        /// </summary>
        public string Password
        {
            get { return _password;  }
            set { _password = value; }
        }

        /// <summary>
        /// Type
        /// </summary>
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// CompanyUrl
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
    }
}
