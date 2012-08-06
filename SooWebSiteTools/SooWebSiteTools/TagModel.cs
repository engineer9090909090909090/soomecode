using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SooWebSiteTools
{
    class TagModel
    {
        public int TagId { set; get; }
        public int ProductId { set; get; }
        public int LanguageId { get; set; }
        public string Tag { get; set; }
    }
}
