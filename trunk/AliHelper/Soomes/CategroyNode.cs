using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soomes
{
    public class CategroyDataJson
    { 
        public string Result {set ; get; }
        public string showtype {set ; get; }
        public List<CategroyNode> Category { set; get; }

    }

    public class CategroyNode
    {
        public int Id {set ; get; }
        public string Name {set ; get; }
        public string isLeaf {set ; get; }
        public string path {set ; get; }
        public string hasPrivilege {set ; get; }
        public string warnMessage {set ; get; }
        public List<Category> Children {set ; get; }
    }

}
