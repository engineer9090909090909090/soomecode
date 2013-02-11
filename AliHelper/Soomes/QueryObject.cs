using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Soomes
{
    public class QueryObject<T>
    {
        public int PageSize { set; get; }
        public int Page { set; get; }

        public int Start
        {
            get
            {
                return (Page - 1)*PageSize;
            }
        }
        public int RecordCount { set; get; }
        public bool IsExport { set; get; }
        public T Condition { set; get; }
        public List<T> Result { set; get; }
        public DataTable dt { set; get; }
    }
}
