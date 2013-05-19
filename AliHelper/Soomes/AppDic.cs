using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soomes
{
    public class AppDic
    {
        public AppDic() { }

        public AppDic(string type, string key, string label)
        {
            this.Type = type;
            this.Key = key;
            this.Label = label;
        }

        public string Type { set; get; }

        public string Key { set; get; }

        public string Label { set; get; }
    }
}
