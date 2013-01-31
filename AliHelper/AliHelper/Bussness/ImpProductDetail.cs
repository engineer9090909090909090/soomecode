using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Soomes;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace AliHelper
{
    class ImpProductDetail
    {
        public ImpProductDetail()
        {

        }

        public void InitDataCacheFormOptions()
        {
            string url = "http://hz.productposting.alibaba.com/product/posting.htm";
            string html = HttpClient.RemoteRequest(url, null);
            html = html.Replace("\r","").Replace("\n","").Replace("\t","");
            HtmlNode.ElementsFlags.Remove("form");
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            HtmlNode productFormEl = document.GetElementbyId("productForm");
            DataCache.Instance.MinOrderUnitOptions = GetProductDetailOptions(productFormEl, "minOrderUnit");
            DataCache.Instance.MoneyTypeOptions = GetProductDetailOptions(productFormEl, "moneyType");
            DataCache.Instance.PriceUnitOptions = GetProductDetailOptions(productFormEl, "priceUnit");
            DataCache.Instance.SupplyUnitOptions = GetProductDetailOptions(productFormEl, "supplyUnit");
            DataCache.Instance.SupplyPeriodOptions = GetProductDetailOptions(productFormEl, "supplyPeriod");
        }

        public ProductDetail GetEditFormElements(AliProduct product)
        {
            string url = "http://hz.productposting.alibaba.com/product/editing.htm?id=" + product.Id;
            string html = HttpClient.RemoteRequest(url, null);
            HtmlNode.ElementsFlags.Remove("form");
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            HtmlNode productFormEl = document.GetElementbyId("productForm");
            ProductDetail detail = PrintElementsValue(productFormEl);
            detail.pid = product.Id;
            detail.gmtModified = product.GmtModified;
            html = html.Replace("\r", "").Replace("\n", "").Replace("\t", "");
            detail.SysAttr = GetSysAttr(html);
            detail.FixAttr = GetFixAttr(html);
            detail.imageFiles.Value = GetImageFilesJsonString(html);
            html = null;
            return detail;
        }

        private string GetPropertyName(string elId, string elName)
        {
            string propertyName = elId;
            if (string.IsNullOrEmpty(propertyName))
            {
                propertyName = elName.Replace("_", "").Replace(".", "");
            }
            return propertyName;
        }

        private string GetRadioBoxPropertyName(string elId, string elName,string value)
        {
            string propertyName = elId;
            if (string.IsNullOrEmpty(propertyName))
            {
                propertyName = elName.Replace("_", "").Replace(".", "") + "_" + value.ToLower();
            }
            return propertyName;
        }

        private List<AttributeNode> GetSysAttr(string html)
        {
            Regex r = new Regex("POSTDATAMAP.attrData.systemAttr =(.*?);");
            GroupCollection gc = r.Match(html).Groups;
            if (gc == null || gc.Count == 0)
            {
                return new List<AttributeNode>();
            }
            string json = gc[1].Value.Trim();
            List<AttributeNode> sysAttrList = JsonConvert.FromJson<List<AttributeNode>>(json);
            return sysAttrList;
        }

        private static string GetImageFilesJsonString(string html)
        {
            Regex r = new Regex("POSTDATAMAP.uploader.imageObjects =(.*?);");
            GroupCollection gc = r.Match(html).Groups;
            if (gc == null || gc.Count == 0)
            {
                return string.Empty;
            }
            return gc[1].Value.Trim();
        }
       
        private List<AttributeNode> GetFixAttr(string html)
        {
            Regex r = new Regex("POSTDATAMAP.attrData.fixAttr =(.*?);");
            GroupCollection gc = r.Match(html).Groups;
            if (gc == null || gc.Count == 0)
            {
                return new List<AttributeNode>();
            }
            string json = gc[1].Value.Trim();
            List<AttributeNode> sysAttrList = JsonConvert.FromJson<List<AttributeNode>>(json);
            return sysAttrList;
        }

        public ProductDetail PrintElementsValue(HtmlNode htmlNode)
        {
            ProductDetail detail = new ProductDetail();
            Type typeOfClass = detail.GetType();
            IEnumerable<HtmlNode> nodeTags = htmlNode.SelectNodes(@".//input[@type='hidden'] | .//input[@type='text']");
            if (nodeTags != null)
            {
                foreach (HtmlNode node in nodeTags)
                {
                    string id = node.GetAttributeValue("id", "");
                    string type = node.GetAttributeValue("type", "");
                    string name = node.GetAttributeValue("name", "");
                    string value = node.GetAttributeValue("value", "");
                    if ("_fmp.pr._0.u" == name || "_fmp.pr._0.us" == name)
                    {
                        continue;
                    }
                    System.Diagnostics.Trace.WriteLine("Id:" + id + "  type:" + type + "  name:" + name + "  value:" + value);

                    FormElement el = new FormElement();
                    el.Id = id;
                    el.Type = type;
                    el.Name = name;
                    el.Value = value;

                    string propertyName = GetPropertyName(id, name);
                    PropertyInfo pInfo = typeOfClass.GetProperty(propertyName);
                    if (pInfo != null && pInfo.PropertyType.Name == "FormElement")
                    {
                        pInfo.SetValue(detail, el, null);
                    }
                }
            }
            detail.CustomAttr = new Dictionary<FormElement, FormElement>();
            HtmlNodeCollection customNameTags = htmlNode.SelectNodes(@".//tr[@class='custom-attr-item']/td/input[@name='_fmp.pr._0.u']");
            HtmlNodeCollection customValueTags = htmlNode.SelectNodes(@".//tr[@class='custom-attr-item']/td/input[@name='_fmp.pr._0.us']");
            if (customNameTags != null)
            {
                for (int i = 0; i < customNameTags.Count; i ++ )
                {
                    HtmlNode nameNode = customNameTags[i];
                    string id = nameNode.GetAttributeValue("id", "");
                    string type = nameNode.GetAttributeValue("type", "");
                    string name = nameNode.GetAttributeValue("name", "");
                    string value = nameNode.GetAttributeValue("value", "");
                    System.Diagnostics.Trace.WriteLine("Id:" + id + "  type:" + type + "  name:" + name + "  value:" + value);
                    FormElement nameEl = new FormElement();
                    nameEl.Id = id;
                    nameEl.Type = type;
                    nameEl.Name = name;
                    nameEl.Value = value;

                    HtmlNode valueNode = customValueTags[i];
                    string vid = valueNode.GetAttributeValue("id", "");
                    string vtype = valueNode.GetAttributeValue("type", "");
                    string vname = valueNode.GetAttributeValue("name", "");
                    string vvalue = valueNode.GetAttributeValue("value", "");
                    System.Diagnostics.Trace.WriteLine("Id:" + vid + "  type:" + vtype + "  name:" + vname + "  value:" + vvalue);
                    FormElement valueEl = new FormElement();
                    valueEl.Id = vid;
                    valueEl.Type = vtype;
                    valueEl.Name = vname;
                    valueEl.Value = vvalue;
                    detail.CustomAttr.Add(nameEl, valueEl);
                }
            }
            
            nodeTags = htmlNode.SelectNodes(@".//input[@type='radio']");
            System.Diagnostics.Trace.WriteLine("radiobox===========================");
            
            if (nodeTags != null)
            {
                foreach (HtmlNode node in nodeTags)
                {
                    string id = node.GetAttributeValue("id", "");
                    string type = node.GetAttributeValue("type", "");
                    string name = node.GetAttributeValue("name", "");
                    string value = node.GetAttributeValue("value", "");
                    bool chk = node.Attributes["checked"] != null;
                    FormElement el = new FormElement();
                    el.Id = id;
                    el.Type = type;
                    el.Name = name;
                    el.Value = value;
                    el.Checked = chk;
                    System.Diagnostics.Trace.WriteLine("Id:" + id + "  type:" + type + "  name:" + name + "  checked:" + chk + "  value:" + value);
                    string propertyName = GetRadioBoxPropertyName(id, name, value);
                    PropertyInfo pInfo = typeOfClass.GetProperty(propertyName);
                    if (pInfo != null && pInfo.PropertyType.Name == "FormElement")
                    {
                        pInfo.SetValue(detail, el, null);
                    }
                }
            }
            System.Diagnostics.Trace.WriteLine("radiobox===========================");

            nodeTags = htmlNode.SelectNodes(@".//input[@type='checkbox']");
            if (nodeTags != null)
            {
                foreach (HtmlNode node in nodeTags)
                {
                    string id = node.GetAttributeValue("id", "");
                    string type = node.GetAttributeValue("type", "");
                    string name = node.GetAttributeValue("name", "");
                    string value = node.GetAttributeValue("value", "");
                    bool chk = node.Attributes["checked"] != null;
                    System.Diagnostics.Trace.WriteLine("Id:" + id + "  type:" + type + "  name:" + name + "  checked:" + chk + "  value:" + value);
                    FormElement el = new FormElement();
                    el.Id = id;
                    el.Type = type;
                    el.Name = name;
                    el.Value = value;
                    el.Checked = chk;
                    string propertyName = GetPropertyName(id, name);
                    PropertyInfo pInfo = typeOfClass.GetProperty(propertyName);
                    if (pInfo != null && pInfo.PropertyType.Name == "FormElement")
                    {
                        pInfo.SetValue(detail, el, null);
                    }
                }
            }

            System.Diagnostics.Trace.WriteLine("select===========================");
            nodeTags = htmlNode.SelectNodes(@".//select");
            if (nodeTags != null)
            {
                foreach (HtmlNode node in nodeTags)
                {
                    string id = node.GetAttributeValue("id", "");
                    string type = "select";
                    string name = node.GetAttributeValue("name", "");
                    string value = GetSelectValue(node);
                    System.Diagnostics.Trace.WriteLine("Id:" + id + "  type:" + type + "  name:" + name + "  value:" + value);
                    FormElement el = new FormElement();
                    el.Id = id;
                    el.Type = type;
                    el.Name = name;
                    el.Value = value;
                    string propertyName = GetPropertyName(id, name);
                    PropertyInfo pInfo = typeOfClass.GetProperty(propertyName);
                    if (pInfo != null && pInfo.PropertyType.Name == "FormElement")
                    {
                        pInfo.SetValue(detail, el, null);
                    }
                    
                }
            }
            System.Diagnostics.Trace.WriteLine("select===========================");

            nodeTags = htmlNode.SelectNodes(@".//textarea");
            if (nodeTags != null)
            {
                foreach (HtmlNode node in nodeTags)
                {
                    string id = node.GetAttributeValue("id", "");
                    string type = node.GetAttributeValue("type", "");
                    string name = node.GetAttributeValue("name", "");
                    string value = node.InnerHtml;
                    System.Diagnostics.Trace.WriteLine("Id:" + id + "  type:" + type + "  name:" + name + "  value:" + value);
                    FormElement el = new FormElement();
                    el.Id = id;
                    el.Type = "textarea";
                    el.Name = name;
                    el.Value = value;
                    string propertyName = GetPropertyName(id, name);
                    PropertyInfo pInfo = typeOfClass.GetProperty(propertyName);
                    if (pInfo != null && pInfo.PropertyType.Name == "FormElement")
                    {
                        pInfo.SetValue(detail, el, null);
                    }
                }
            }
            return detail;
        }

        public string GetSelectValue(HtmlNode htmlNode)
        {
            HtmlNodeCollection nodeTags = htmlNode.SelectNodes(@".//option");
            if (nodeTags != null)
            {
                foreach (HtmlNode node in nodeTags)
                {
                    string value = node.GetAttributeValue("value", "");
                    bool chk = (value != "0" && node.Attributes["selected"] != null);
                    if (chk)
                    {
                        return value;
                    }
                }
            }
            return string.Empty;
        }

        public List<FormElement> GetOptions(HtmlNode htmlNode)
        {
            List<FormElement> options = new List<FormElement>();
            HtmlNodeCollection nodeTags = htmlNode.SelectNodes(@".//option");
            if (nodeTags != null)
            {
                foreach (HtmlNode node in nodeTags)
                {
                    string id = node.GetAttributeValue("id", "");
                    string type = "option";
                    string name = node.GetAttributeValue("name", "");
                    string value = node.GetAttributeValue("value", "");
                    bool chk = node.Attributes["selected"] != null;
                    FormElement el = new FormElement();
                    el.Id = id;
                    el.Type = type;
                    el.Name = node.NextSibling.InnerText;
                    el.Value = value;
                    el.Type = type;
                    el.Checked = chk;
                    options.Add(el);
                }
            }
            return options;
        }

        public List<FormElement> GetProductDetailOptions(HtmlNode htmlNode, string tagId)
        {
            List<FormElement> options = new List<FormElement>();
            HtmlNodeCollection nodeTags = htmlNode.SelectNodes(@".//select[@id='" + tagId + "']/option");
            if (nodeTags != null)
            {
                foreach (HtmlNode node in nodeTags)
                {
                    string id = node.GetAttributeValue("id", "");
                    string type = "option";
                    string name = node.GetAttributeValue("name", "");
                    string value = node.GetAttributeValue("value", "");
                    FormElement el = new FormElement();
                    el.Id = id;
                    el.Type = type;
                    el.Name = node.NextSibling.InnerText;
                    el.Value = value;
                    el.Type = type;
                    options.Add(el);
                }
            }
            return options;
        }
    }
}
