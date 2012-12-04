using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soomes
{
    public class AliGroup
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public int ChildrenCount { set; get; }

        public bool HasChildren { set; get; }

        public int ProductCount { set; get; }

        public List<AliGroup> Children { set; get; }

        public int Level { set; get; }

        public int ParentId { set; get; }
    }
}