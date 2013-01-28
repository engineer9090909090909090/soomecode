using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;

namespace AliHelper
{
    class DataCache
    {
        
        public static DataCache Instance = new DataCache();

        private DataCache() { }

        public string CsrfToken { get; set; }
        public string CheckCodeUrl;

        public List<FormElement> MinOrderUnitOptions { set; get; }
        public List<FormElement> MoneyTypeOptions { set; get; }
        public List<FormElement> PriceUnitOptions { set; get; }
        public List<FormElement> SupplyUnitOptions { set; get; }
        public List<FormElement> SupplyPeriodOptions { set; get; }
        public List<FormElement> GroupListOptions { set; get; }
    }
}
