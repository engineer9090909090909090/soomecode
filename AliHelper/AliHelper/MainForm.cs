using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using Soomes;

namespace AliHelper
{
    public partial class MainForm : Form
    {
        //alibaba vip manage url
        public static string url = "http://hz.productposting.alibaba.com/product/manage_products.htm#tab=approved";

        public static string GroupListRequest = "http://hz.productposting.alibaba.com/product/group_ajax.htm?event=listGroup&parentGroupId={0}&_csrf_token_={1}&pageSize=";

        public static string ProudctListRequest = "http://hz.productposting.alibaba.com/product/managementproducts/asyQueryProductList.do?status=approved&imageType=all&repositoryType=all&page={0}&size=50&changePageSize=Y&_csrf_token_={1}&groupId={2}&groupLevel={3}";

        public static string CsrfToken = string.Empty;

        public MainForm()
        {
            InitializeComponent();
            string html = IEHandleUtils.WebRequestGetUrlHtml(url);
            CsrfToken = GetCsrfToken(html);


            //  IEHandleUtils.WebBrowerSetCookies_NavigateToUrl(this.webBrowser1, url);
            //  this.webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
        }

        void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = (WebBrowser)sender;
            if (browser.ReadyState != System.Windows.Forms.WebBrowserReadyState.Complete)
                return;
            if (e.Url.ToString() != browser.Url.ToString())
                return;
            if (browser.Url.ToString() == url)
            {

            }
            System.Diagnostics.Trace.WriteLine("========================" + this.webBrowser1.Url.ToString());
        }


        public string GetCsrfToken(string html)
        {
            Regex r = new Regex("var _csrf_ = {'_csrf_token_':'(.*?)'};");
            GroupCollection gc = r.Match(html).Groups;
            if (gc != null && gc.Count > 1)
            {
                return gc[1].Value.Trim();
            }
            return "";
        }

        private void updateGroup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CsrfToken))
            {
                return;
            }
            string groupReqUrl = string.Format(GroupListRequest, "-1", CsrfToken);
            string groupJsonText = IEHandleUtils.WebRequestGetUrlHtml(groupReqUrl);
            AliGroupInfo groupInfo = JsonConvert.FromJson<AliGroupInfo>(groupJsonText);
            List<AliGroup> groups = groupInfo.Data;
            foreach (AliGroup g in groups)
            {
                if (g.HasChildren)
                {
                    groupReqUrl = string.Format(GroupListRequest, g.Id, CsrfToken);
                    groupJsonText = IEHandleUtils.WebRequestGetUrlHtml(groupReqUrl);
                    AliGroupInfo obj = JsonConvert.FromJson<AliGroupInfo>(groupJsonText);
                    g.Children = obj.Data;
                }
            }

            TreeNode t = new TreeNode("产品分组");//作为根节点。  
            treeView1.Nodes.Add(t);
            foreach (AliGroup p in groups)
            {
                TreeNode t1 = new TreeNode(p.Name);
                t1.Tag = p.Id;
                t.Nodes.Add(t1);
                if (p.HasChildren)
                {
                    foreach (AliGroup c in p.Children)
                    {
                        TreeNode t2 = new TreeNode(c.Name);
                        t2.Tag = c.Id;
                        t1.Nodes.Add(t2);
                    }
                }
            }
            treeView1.ExpandAll();
        }

        private void updateAllProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CsrfToken))
            {
                List<AliProduct> produtList = new List<AliProduct>();
                List<AliGroup> groups = null;
                foreach (AliGroup group in groups)
                {
                    produtList.AddRange(GetGroupProduct(group, CsrfToken));
                }
            }
        }

        public List<AliProduct> GetGroupProduct(AliGroup group, string csrfToken)
        {
            List<AliProduct> produtList = new List<AliProduct>();
            produtList.AddRange(GetProducts(group.Id.ToString(), group.Level.ToString(), csrfToken));
            if (group.HasChildren)
            {
                List<AliGroup> childGroups = group.Children;
                foreach (AliGroup child in childGroups)
                {
                    produtList.AddRange(GetGroupProduct(child, csrfToken));
                }
            }
            return produtList;
        }

        public List<AliProduct> GetProducts(string groupId, string groupLevel, string csrfToken)
        {
            List<AliProduct> produtList = new List<AliProduct>();
            string prodcutsReqUrl = string.Format(ProudctListRequest, "1", csrfToken, groupId, groupLevel);
            string prodcutsJsonText = IEHandleUtils.WebRequestGetUrlHtml(prodcutsReqUrl);
            AliProductInfo productsInfo = JsonConvert.FromJson<AliProductInfo>(prodcutsJsonText);
            produtList.AddRange(productsInfo.Products);
            int pageNumber = productsInfo.Count / 50 + ((productsInfo.Count % 50 > 0) ? 1 : 0);
            for (int i = 2; i <= pageNumber; i++)
            {
                prodcutsReqUrl = string.Format(ProudctListRequest, i, csrfToken, groupId, groupLevel);
                prodcutsJsonText = IEHandleUtils.WebRequestGetUrlHtml(prodcutsReqUrl);
                AliProductInfo obj = JsonConvert.FromJson<AliProductInfo>(prodcutsJsonText);
                produtList.AddRange(obj.Products);
            }
            return produtList;
        }

    }
}
