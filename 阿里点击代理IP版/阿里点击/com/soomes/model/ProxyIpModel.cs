using System;
using System.Collections.Generic;
using System.Text;

namespace com.soomes.model
{
    public class ProxyIpModel
    {


        private long _id;
        private string _ip;
        private string _ipDesc;
        private long _useNumber;
        private DateTime _checkTime;
        private DateTime _lastUseTime;
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
            get { return _ip; }
            set { _ip = value; }
        }

        /// <summary>
        /// IpDesc
        /// </summary>
        public string IpDesc
        {
            get { return _ipDesc; }
            set { _ipDesc = value; }
        }

        /// <summary>
        /// UseNumber
        /// </summary>
        public long UseNumber
        {
            get { return _useNumber; }
            set { _useNumber = value; }
        }

        /// <summary>
        /// CheckTime
        /// </summary>
        public DateTime CheckTime
        {
            get { return _checkTime; }
            set { _checkTime = value; }
        }

        /// <summary>
        /// LastUseTime
        /// </summary>
        public DateTime LastUseTime
        {
            get { return _lastUseTime; }
            set { _lastUseTime = value; }
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
