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

        private DataCache() 
        {
            DicTypeOptions = new Dictionary<string, string>();
            DicTypeOptions.Add(Constants.DebitCredit, "收支类型");
            DicTypeOptions.Add(Constants.BussnessType, "业务类型");
            DicTypeOptions.Add(Constants.Employee, "人员");
            DicTypeOptions.Add(Constants.OrderStatusType, "订单状态类型");
        }
        public string AliID { get; set; }
        public string CsrfToken { get; set; }
        public string CheckCodeUrl;

        public Dictionary<string, string> DicTypeOptions { set; get; }

        public List<FormElement> MinOrderUnitOptions { set; get; }
        public List<FormElement> MoneyTypeOptions { set; get; }
        public List<FormElement> PriceUnitOptions { set; get; }
        public List<FormElement> SupplyUnitOptions { set; get; }
        public List<FormElement> SupplyPeriodOptions { set; get; }
        public List<FormElement> GroupListOptions { set; get; }
    }
}
