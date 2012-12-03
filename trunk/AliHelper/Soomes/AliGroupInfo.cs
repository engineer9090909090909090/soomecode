using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soomes
{
    public class AliGroupInfo
    {
        public string Message { set; get; }

        public bool Result { set; get; }

        public string Error { set; get; }

        public List<AliGroup> Data { set; get; }
    }
}
