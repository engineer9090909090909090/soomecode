using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soomes
{
    public class AliProductInfo
    {
        public string Message { set; get; }

        public bool Result { set; get; }

        public bool CountLimit { set; get; }

        public int Count { set; get; }

        public List<AliProduct> Products { set; get; }

    }
}
