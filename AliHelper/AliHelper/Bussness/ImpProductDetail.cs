using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Soomes;
using System.Reflection;

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
            return PrintElementsValue(productFormEl);
            
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
    }
}
