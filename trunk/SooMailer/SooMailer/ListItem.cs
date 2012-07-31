using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SooMailer
{
    public class ListItem
    {
        public string Id { set; get; }
        public string Name { set; get; }

        public ListItem(string id, string sname)
        {
            Id = id;
            Name = sname;
        }

        public override string ToString()
        {
            return this.Name;
        }

        
    }

}
