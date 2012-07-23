using System;
using System.Collections.Generic;
using System.Text;

namespace com.soomes.model
{
    public class ClickerModel
    {
        private long _id;
        private string _productId;
        private bool _operate;
        private string _companyUrl;
        private string _keyWord;
        private long _clickedNum;
        private long _pageRank;
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
        /// ProductId
        /// </summary>
        public string ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }

        /// <summary>
        /// Operate
        /// </summary>
        public bool Operate
        {
            get { return _operate; }
            set { _operate = value; }
        }

        /// <summary>
        /// CompanyUrl
        /// </summary>
        public string CompanyUrl
        {
            get { return _companyUrl; }
            set { _companyUrl = value; }
        }

        /// <summary>
        /// CompanyUrl
        /// </summary>
        public string KeyWord
        {
            get { return _keyWord; }
            set { _keyWord = value; }
        }

        /// <summary>
        /// CompanyUrl
        /// </summary>
        public long ClickedNum
        {
            get { return _clickedNum; }
            set { _clickedNum = value; }
        }

        /// <summary>
        /// CompanyUrl
        /// </summary>
        public long PageRank
        {
            get { return _pageRank; }
            set { _pageRank = value; }
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
