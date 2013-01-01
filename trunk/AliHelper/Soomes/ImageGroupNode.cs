using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soomes
{

    public class ImageGroupNode
    {
        public ImageGroup Node { set; get; }
        public string Branch { set; get; }
    }


    public class ImageGroup
    {
        public string Text { set; get; }
        public int Value { set; get; }
        public string LevelCode { set; get; }
        public int CompanyId { set; get; }
    }
}
