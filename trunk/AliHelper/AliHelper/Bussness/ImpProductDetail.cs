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
            List<FormElement> elments = PrintElementsValue(productFormEl);
            ProductDetail detail = new ProductDetail();
            Type typeOfClass = detail.GetType();
            foreach (FormElement el in elments)
            {
                string propertyName = el.Id;
                if (string.IsNullOrEmpty(propertyName))
                {
                    propertyName = el.Name.Replace("_", "").Replace(".", "");
                }
                if (string.IsNullOrEmpty(propertyName))
                {
                    continue;
                }
                PropertyInfo pInfo = typeOfClass.GetProperty(propertyName);
                if (pInfo != null)
                {
                    if (pInfo.PropertyType.Name == "FormElement")
                    {
                        pInfo.SetValue(detail, el, null);
                    }
                    else {
                        System.Diagnostics.Trace.WriteLine(pInfo.PropertyType.Name);
                    }
                }
            }
            return detail;
        }

        public List<FormElement> PrintElementsValue(HtmlNode htmlNode)
        {
            List<FormElement> elements = new List<FormElement>();
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
                    elements.Add(el);
                }
            }
            nodeTags = htmlNode.SelectNodes(@"//input[@type='checkbox'] | //input[@type='radio']");
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
                    elements.Add(el);
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
                    elements.Add(el);
                }
            }
            return elements;
        }
    }
}
