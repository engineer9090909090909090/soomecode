using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Soomes;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AliHelper
{
    class ImpProductDetail
    {
        public ImpProductDetail()
        { 

        }

        public ProductDetail GetFormElements()
        {
            string url = "http://hz.productposting.alibaba.com/product/editing.htm?id=627992386";
            string html = HttpClient.RemoteRequest(url, null);
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            HtmlNode productFormEl = document.GetElementbyId("productForm");
            ProductDetail detail = PrintElementsValue(productFormEl);
            detail.SysAttr = GetSysAttr(html);
            detail.FixAttr = GetFixAttr(html);
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



            HtmlNodeCollection nodeTags = htmlNode.SelectNodes(@"//input[@type='hidden'] | //input[@type='text']");
            if (nodeTags != null)
            {
                foreach (HtmlNode node in nodeTags)
                {
                    string id = node.GetAttributeValue("id", "");
                    string type = node.GetAttributeValue("type", "");
                    string name = node.GetAttributeValue("name", "");
                    string value = node.GetAttributeValue("value", "");
                    System.Diagnostics.Trace.WriteLine("Id:" + id + "  type:" + type + "  name:" + name + "  value:" + value);

                    FormElement el = new FormElement();
                    el.Id = id;
                    el.Type = type;
                    el.Name = name;
                    el.Val = value;

                    string propertyName = GetPropertyName(id, name);
                    PropertyInfo pInfo = typeOfClass.GetProperty(propertyName);
                    if (!string.IsNullOrEmpty(value) && pInfo != null && pInfo.PropertyType.Name == "FormElement")
                    {
                        pInfo.SetValue(detail, el, null);
                    }
                }
            }
            detail.CustomAttr = new Dictionary<FormElement, FormElement>();
            HtmlNodeCollection customNameTags = htmlNode.SelectNodes(@"//tr[@class='custom-attr-item']/td/input[@name='_fmp.pr._0.u']");
            HtmlNodeCollection customValueTags = htmlNode.SelectNodes(@"//tr[@class='custom-attr-item']/td/input[@name='_fmp.pr._0.us']");
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
                    nameEl.Val = value;

                    HtmlNode valueNode = customValueTags[i];
                    string vid = valueNode.GetAttributeValue("id", "");
                    string vtype = valueNode.GetAttributeValue("type", "");
                    string vname = valueNode.GetAttributeValue("name", "");
                    string vvalue = valueNode.GetAttributeValue("value", "");
                    System.Diagnostics.Trace.WriteLine("Id:" + id + "  type:" + type + "  name:" + name + "  value:" + value);
                    FormElement valueEl = new FormElement();
                    valueEl.Id = vid;
                    valueEl.Type = vtype;
                    valueEl.Name = vname;
                    valueEl.Val = vvalue;
                    detail.CustomAttr.Add(nameEl, valueEl);
                }
            }
            
            nodeTags = htmlNode.SelectNodes(@"//input[@type='radio']");
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
                    System.Diagnostics.Trace.WriteLine("Id:" + id + "  type:" + type + "  name:" + name + "  checked:" + chk + "  value:" + value);
                    FormElement el = new FormElement();
                    el.Id = id;
                    el.Type = type;
                    el.Name = name;
                    el.Val = value;
                    el.Checked = chk;
                }
            }
            System.Diagnostics.Trace.WriteLine("radiobox===========================");

            nodeTags = htmlNode.SelectNodes(@"//input[@type='checkbox']");
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
                    el.Val = value;
                    el.Checked = chk;
                    string propertyName = GetPropertyName(id, name);
                    PropertyInfo pInfo = typeOfClass.GetProperty(propertyName);
                    if (!string.IsNullOrEmpty(value) && pInfo != null && pInfo.PropertyType.Name == "FormElement")
                    {
                        pInfo.SetValue(detail, el, null);
                    }
                }
            }

            System.Diagnostics.Trace.WriteLine("select===========================");
            nodeTags = htmlNode.SelectNodes(@"//select");
            if (nodeTags != null)
            {
                foreach (HtmlNode node in nodeTags)
                {
                    string id = node.GetAttributeValue("id", "");
                    string type = "select";
                    string name = node.GetAttributeValue("name", "");
                    string value = node.GetAttributeValue("value", "");
                    System.Diagnostics.Trace.WriteLine("Id:" + id + "  type:" + type + "  name:" + name + "  value:" + value);
                    FormElement el = new FormElement();
                    el.Id = id;
                    el.Type = type;
                    el.Name = name;
                    el.Val = value;
                    el.Options = GetOptions(node);
                    string propertyName = GetPropertyName(id, name);
                    PropertyInfo pInfo = typeOfClass.GetProperty(propertyName);
                    if (pInfo != null && pInfo.PropertyType.Name == "FormElement")
                    {
                        pInfo.SetValue(detail, el, null);
                    }
                    
                }
            }
            System.Diagnostics.Trace.WriteLine("select===========================");

            nodeTags = htmlNode.SelectNodes(@"//textarea");
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
                    el.Val = value;

                    string propertyName = GetPropertyName(id, name);
                    PropertyInfo pInfo = typeOfClass.GetProperty(propertyName);
                    if (!string.IsNullOrEmpty(value) && pInfo != null && pInfo.PropertyType.Name == "FormElement")
                    {
                        pInfo.SetValue(detail, el, null);
                    }
                }
            }
            return detail;
        }


        public List<FormElement> GetOptions(HtmlNode htmlNode)
        {
            List<FormElement> options = new List<FormElement>();
            HtmlNodeCollection nodeTags = htmlNode.SelectNodes(@"./option");
            if (nodeTags != null)
            {
                foreach (HtmlNode node in nodeTags)
                {
                    string id = node.GetAttributeValue("id", "");
                    string type = "option";
                    string name = node.GetAttributeValue("name", "");
                    string value = node.GetAttributeValue("value", "");
                    bool chk = node.Attributes["checked"] != null;
                    System.Diagnostics.Trace.WriteLine("Id:" + id + "  type:" + type + "  name:" + name + "  value:" + value);
                    FormElement el = new FormElement();
                    el.Id = id;
                    el.Type = type;
                    el.Name = name;
                    el.Val = value;
                    el.Type = type;
                    el.Label = node.NextSibling.InnerText;
                    el.Checked = chk;
                    options.Add(el);
                }
            }
            return options;
        }
    }
}
