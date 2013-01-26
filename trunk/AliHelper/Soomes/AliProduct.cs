using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soomes
{
    public class AliProduct
    {

        public int Id { set; get; }

        public string Keywords { set; get; }

        public bool IsKeywords { set; get; }

        public string Status { set; get; }

        public string GroupName1 { set; get; }

        public string GroupName2 { set; get; }

        public string GroupName3 { set; get; }

        public string Subject { set; get; }

        public string RedModel { set; get; }

        public string DetailUrl { set; get; }

        public string AbsImageUrl { set; get; }

        public string AbsSummImageUrl { set; get; }

        public bool IsWindowProduct { set; get; }
        ///====
        public string GmtModified { set; get; }

        public string Type { set; get; }

        public string IsDisplay { set; get; }

        public string OwnerMemberId { set; get; }

        public string OwnerMemberName { set; get; }

        public bool IsLowScore { set; get; }
        ///=====
        public string GroupId { set; get; }
    }
}