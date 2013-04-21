using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;

namespace Soomes
{
    public class DataCache
    {
        
        public static DataCache Instance = new DataCache();

        private DataCache() 
        {
            DicTypeOptions = new Dictionary<string, string>();
            DicTypeOptions.Add(Constants.DebitCredit, "收支类型");
            DicTypeOptions.Add(Constants.BussnessType, "业务类型");
            DicTypeOptions.Add(Constants.Employee, "人员");
            DicTypeOptions.Add(Constants.OrderStatusType, "订单状态类型");
            DicTypeOptions.Add(Constants.CurrencyType, "币种");
            DicTypeOptions.Add(Constants.RecivePaymentAccounts, "收付款账户");
            DicTypeOptions.Add(Constants.ProductStatus, "产品状态");
        }

        public bool OpenMySqlDb { get; set; }
        public string MySqlConnection { get; set; }
        public string AliID { get; set; }
        public string CsrfToken { get; set; }
        public string CheckCodeUrl { get; set; }

        public Dictionary<string, string> DicTypeOptions { set; get; }

        public List<FormElement> MinOrderUnitOptions { set; get; }
        public List<FormElement> MoneyTypeOptions { set; get; }
        public List<FormElement> PriceUnitOptions { set; get; }
        public List<FormElement> SupplyUnitOptions { set; get; }
        public List<FormElement> SupplyPeriodOptions { set; get; }
        public List<FormElement> GroupListOptions { set; get; }
    }
}
