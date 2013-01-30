using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Reflection;
using System.Data.SQLite;
using System.Data;

namespace AliHelper.DAO
{
    public class AliProductDetailDao
    {
        private SQLiteDBHelper dbHelper;

        public AliProductDetailDao(SQLiteDBHelper dbHelper)
        { 
            this.dbHelper = dbHelper;
            CreateTable();
            UpdateTable();
        }

        private void CreateTable()
        {
            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS AliProductDetail("
            + "pid integer NOT NULL PRIMARY KEY UNIQUE,"
            + "id varchar(1000),"
            + "fromType2 varchar(1000),"
            + "commonCategoryName varchar(1000),"
            + "commonCategoryIds varchar(1000),"
            + "canChangeWaterMark varchar(1000),"
            + "versionId varchar(1000),"
            + "categoryIdsPathStr varchar(1000),"
            + "categoryDisappeared varchar(1000),"
            + "trashDOId varchar(1000),"
            + "isFromTrash varchar(1000),"
            + "trashXmlPath varchar(1000),"
            + "event_submit_do_change_category varchar(1000),"
            + "productQueryURL varchar(1000),"
            + "productSketchId varchar(1000),"
            + "hiddenCatPath varchar(1000),"
            + "groupId varchar(1000),"
            + "groupStatusId varchar(1000),"
            + "keyFromSmartKeywords varchar(1000),"
            + "pageType varchar(1000),"
            + "productId varchar(1000),"
            + "productName varchar(1000),"
            + "productKeyword varchar(1000),"
            + "keywords2 varchar(1000),"
            + "keywords3 varchar(1000),"
            + "imageFiles varchar(4000),"
            + "paymentMethod1 varchar(1000),"
            + "paymentMethod2 varchar(1000),"
            + "paymentMethod3 varchar(1000),"
            + "paymentMethod4 varchar(1000),"
            + "paymentMethod5 varchar(1000),"
            + "paymentMethod6 varchar(1000),"
            + "paymentMethodOther varchar(1000),"
            + "paymentMethodOtherDesc varchar(1000),"
            + "minOrderQuantity varchar(1000),"
            + "minOrderUnit varchar(1000),"
            + "moneyType varchar(1000),"
            + "minOrderOther varchar(1000),"
            + "priceRangeMin varchar(1000),"
            + "priceRangeMax varchar(1000),"
            + "priceUnit varchar(1000),"
            + "port varchar(1000),"
            + "static_and_dyn0 varchar(1000),"
            + "static_and_dyn1 varchar(1000),"
            + "staticImageWaterMarkId varchar(1000),"
            + "staticImageToBankId varchar(1000),"
            + "fmppr0stati_y varchar(1000),"
            + "fmppr0stati_n varchar(1000),"
            + "fmppr0static_center varchar(1000),"
            + "fmppr0static_down varchar(1000),"
            + "dynamicImageWaterMarkId varchar(1000),"
            + "dynamicImageToBankId varchar(1000),"
            + "fmppr0dyn_y varchar(1000),"
            + "fmppr0dyn_n varchar(1000),"
            + "fmppr0dyna_center varchar(1000),"
            + "fmppr0dyna_down varchar(1000),"
            + "supplyQuantity varchar(1000),"
            + "supplyPeriod varchar(1000),"
            + "supplyUnit varchar(1000),"
            + "supplyOther varchar(1000),"
            + "consignmentTerm varchar(1000),"
            + "productTeamInputBox varchar(1000),"
            + "productGroupId1 varchar(1000),"
            + "productGroupId2 varchar(1000),"
            + "productGroupId3 varchar(1000),"
            + "summary varchar(1000),"
            + "productDescriptionTemp varchar(9000),"
            + "packagingDesc varchar(1000),"
            + "fmppr0r varchar(1000),"
            + "fmppr0is varchar(1000),"
            + "dynamicImageTbdFlag varchar(1000),"
            + "dynamicImageOriginFlag varchar(1000),"
            + "dynamicImageChangedFlag varchar(1000),"
            + "fromvirtualsite varchar(1000),"
            + "userReviseCategory varchar(1000),"
            + "backUrl varchar(1000)"
            + ")");

            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS Detail_CustomAttr("
            + "pid integer NOT NULL PRIMARY KEY UNIQUE,"
            + "custAttrName varchar(1000),"
            + "custAttrValue varchar(1000)"
            + ")");

            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS Detail_SystemAttr("
            + "pid integer NOT NULL PRIMARY KEY UNIQUE,"
            + "type integer NOT NULL,"
            + "data varchar(1000),"
            + "nodes varchar(5000)"
            + ")");
        }

        

        private void UpdateTable()
        {

        }

        public void InsertOrUpdate(ProductDetail detail)
        {
            string sql = "select count(1) from AliProductDetail where pid = " + detail.pid;
            int count = Convert.ToInt32(dbHelper.ExecuteScalar(sql, null));
            if (count > 0)
            {
                Update(detail);
            }
            else {
                Insert(detail);
            }
            UpdateCustomAttr(detail.pid, detail.CustomAttr);
            UpdateSystemAttr(detail.pid, Constants.AttrType_SysAttr, detail.SysAttr);
            UpdateSystemAttr(detail.pid, Constants.AttrType_FixAttr, detail.FixAttr);
        }

        public void Insert(ProductDetail detail)
        {
            Type typeOfClass = detail.GetType();
            PropertyInfo[] pInfo = typeOfClass.GetProperties();
            string sqlCondi = "INSERT INTO AliProductDetail(pid,";
            string sqlValue = ")values(@pid,";
            string sqlEnd = ");";
            List<SQLiteParameter> parameter = new List<SQLiteParameter>();
            parameter.Add(new SQLiteParameter("@Id",detail.pid));
            foreach (PropertyInfo info in pInfo)
            {
                if (info.PropertyType.Name == "FormElement")
                {
                    sqlCondi = sqlCondi + ", " + info.Name;
                    sqlValue = sqlValue + ", @" + info.Name;
                    object val = info.GetValue(detail, null);
                    string json = JsonConvert.ToJson(val);
                    parameter.Add(new SQLiteParameter("@" + info.Name, json));
                }
            }
            string InsSql = sqlCondi + sqlValue + sqlEnd;
            dbHelper.ExecuteNonQuery(InsSql, parameter.ToArray());
        }

        public void Update(ProductDetail detail)
        {
            Type typeOfClass = detail.GetType();
            PropertyInfo[] pInfo = typeOfClass.GetProperties();
            string sqlCondi = "Update AliProductDetail SET ";
            string sqlEnd = " where pid =" + detail.pid;
            List<SQLiteParameter> parameter = new List<SQLiteParameter>();
            foreach (PropertyInfo info in pInfo)
            {
                if (info.PropertyType.Name == "FormElement")
                {
                    sqlCondi = sqlCondi + info.Name + " = @" + info.Name + ",";
                    object val = info.GetValue(detail, null);
                    string json = JsonConvert.ToJson(val);
                    parameter.Add(new SQLiteParameter("@" + info.Name, json));
                }
            }
            string UpdateSql = sqlCondi.Substring(0, sqlCondi.Length - 1) + sqlEnd;
            dbHelper.ExecuteNonQuery(UpdateSql, parameter.ToArray());
        }

        public void UpdateCustomAttr(int pid, Dictionary<FormElement, FormElement> CustomAttr)
        {
            if (CustomAttr == null || CustomAttr.Count ==0) return;
            string InsSql = "INSERT INTO Detail_CustomAttr(pid,custAttrName,custAttrValue)values(@pid,@custAttrName,@custAttrValue);";
            List<SQLiteParameter[]> InsertParameters = new List<SQLiteParameter[]>();
            foreach (FormElement custonName in CustomAttr.Keys)
            {
                FormElement customValue = CustomAttr[custonName];
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("@pid",pid),
                    new SQLiteParameter("@custAttrName",JsonConvert.ToJson(custonName)), 
                    new SQLiteParameter("@custAttrValue",JsonConvert.ToJson(customValue))
                };
                InsertParameters.Add(parameter);
            }

            dbHelper.ExecuteNonQuery("delete from Detail_CustomAttr where pid = " + pid);
            if (InsertParameters.Count > 0)
            {
                dbHelper.ExecuteNonQuery(InsSql, InsertParameters);
            }
        }

        public void UpdateSystemAttr(int pid, int Type, List<AttributeNode> systemAttr)
        {
            if (systemAttr == null || systemAttr.Count == 0) return;
            string InsSql = "INSERT INTO Detail_SystemAttr(pid,type, data,nodes)values(@pid, @type, @data, @nodes);";
            List<SQLiteParameter[]> InsertParameters = new List<SQLiteParameter[]>();
            foreach (AttributeNode attr in systemAttr)
            {
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("@pid", pid),
                    new SQLiteParameter("@type", Type), 
                    new SQLiteParameter("@data",JsonConvert.ToJson(attr.Data)),
                    new SQLiteParameter("@nodes",JsonConvert.ToJson(attr.Nodes))
                };
                InsertParameters.Add(parameter);
            }

            dbHelper.ExecuteNonQuery("delete from Detail_SystemAttr where pid = " + pid + " and type =" +Type);
            if (InsertParameters.Count > 0)
            {
                dbHelper.ExecuteNonQuery(InsSql, InsertParameters);
            }
        }

        public ProductDetail GetProductDetail(int pid)
        {
            ProductDetail detail = new ProductDetail();
            Type typeOfClass = detail.GetType();
            DataTable dt = dbHelper.ExecuteDataTable("select * from AliProductDetail where pid = " + pid, null);
            if (dt == null || dt.Rows.Count == 0) return null;
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    string propertyName = col.ColumnName;
                    PropertyInfo pInfo = typeOfClass.GetProperty(propertyName);
                    if (pInfo != null && pInfo.PropertyType.Name == "FormElement")
                    {
                        string element = (string)row[propertyName];
                        FormElement el = JsonConvert.FromJson<FormElement>(element);
                        pInfo.SetValue(detail, el, null);
                    }
                }
            }
            detail.pid = pid;
            detail.CustomAttr = GetCustomAttr(pid);
            detail.SysAttr = GetSystemAttr(pid, Constants.AttrType_SysAttr);
            detail.FixAttr = GetSystemAttr(pid, Constants.AttrType_FixAttr);
            return null;
        }

        public Dictionary<FormElement, FormElement> GetCustomAttr(int pid)
        {
            string sql = "select * from Detail_CustomAttr where pid = " + pid;
            DataTable dt = dbHelper.ExecuteDataTable(sql, null);
            Dictionary<FormElement, FormElement> dic = new Dictionary<FormElement, FormElement>();
            foreach (DataRow row in dt.Rows)
            {
                FormElement custAttrName = JsonConvert.FromJson<FormElement>((string)row["custAttrName"]);
                FormElement custAttrValue = JsonConvert.FromJson<FormElement>((string)row["custAttrValue"]);
                dic.Add(custAttrName, custAttrValue);
            }
            return dic;
        }


        public List<AttributeNode> GetSystemAttr(int pid, int Type)
        {
            string sql = "select * from Detail_SystemAttr where pid = " + pid + " and type =" + Type;
            DataTable dt = dbHelper.ExecuteDataTable(sql, null);
            List<AttributeNode> list = new List<AttributeNode>();
            foreach (DataRow row in dt.Rows)
            {
                AttributeNode kw = new AttributeNode();
                kw.Data = JsonConvert.FromJson<Soomes.Attribute>((string)row["data"]);
                kw.Nodes = JsonConvert.FromJson<List<AttributeNode>>((string)row["nodes"]);
                list.Add(kw);
            }
            return list;
        }
    }
}
