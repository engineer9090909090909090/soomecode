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
using AliHelper.Bussness;

namespace AliHelper
{
    public partial class MainForm : Form
    {
        //alibaba vip manage url
        public static string url = "http://hz.productposting.alibaba.com/product/manage_products.htm#tab=approved";

        public static string GroupListRequest = "http://hz.productposting.alibaba.com/product/group_ajax.htm?event=listGroup&parentGroupId={0}&_csrf_token_={1}&pageSize=";

        public static string ProudctListRequest = "http://hz.productposting.alibaba.com/product/managementproducts/asyQueryProductList.do?status=approved&imageType=all&repositoryType=all&page={0}&size=50&changePageSize=Y&_csrf_token_={1}&groupId={2}&groupLevel={3}";

        public static string CsrfToken = string.Empty;

        public ProductsManager productsManager;
        
        public MainForm()
        {
            InitializeComponent();
            productsManager = new ProductsManager();
            string html = IEHandleUtils.WebRequestGetUrlHtml(url);
            CsrfToken = GetCsrfToken(html);

            //  IEHandleUtils.WebBrowerSetCookies_NavigateToUrl(this.webBrowser1, url);
            //  this.webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            List<AliGroup> groups = productsManager.GetGroupList();
            UpdateGroupUI(groups);
            
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

        public void UpdateGroupUI(List<AliGroup> groups)
        {
            treeView1.Nodes.Clear();
            TreeNode t = new TreeNode("产品分组");//作为根节点
            treeView1.Nodes.Add(t);
            foreach (AliGroup p in groups)
            {
                if (p.Level == 1)
                {
                    TreeNode t1 = new TreeNode(p.Name);
                    t1.Tag = p.Id;
                    t.Nodes.Add(t1);
                    if (p.HasChildren)
                    {
                        foreach (AliGroup c in groups)
                        {
                            if (c.ParentId == p.Id && c.Level == p.Level + 1)
                            {
                                TreeNode t2 = new TreeNode(c.Name);
                                t2.Tag = c.Id;
                                t1.Nodes.Add(t2);
                            }
                        }
                    }
                }
                
            }
            treeView1.ExpandAll();
        }


        private void updateGroup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CsrfToken))
            {
                return;
            }
            List<AliGroup> groups = GetGroups(-1, 0, CsrfToken);
            productsManager.UpdateGroups(groups);
            UpdateGroupUI(groups);
        }

        private void updateAllProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CsrfToken))
            {
                return;
            }
            List<AliGroup> groups = productsManager.GetGroupList();
            List<AliProduct> produtList = GetGroupProduct(groups, CsrfToken);
        }

        public List<AliProduct> GetGroupProduct(List<AliGroup> groups, string csrfToken)
        {
            List<AliProduct> produtList = new List<AliProduct>(); 
            foreach (AliGroup group in groups)
            {
                List<AliProduct> products = GetProducts(group.Id, group.Level, csrfToken);
                productsManager.UpdateGroupProdcuts(group.Id, products);
                produtList.AddRange(products);
            }
            return produtList;
        }

        public List<AliGroup> GetGroups(int parentId, int parentLevel, string csrfToken)
        {
            List<AliGroup> groups = new List<AliGroup>();
            string groupReqUrl = string.Format(GroupListRequest, parentId, csrfToken);
            string groupJsonText = IEHandleUtils.WebRequestGetUrlHtml(groupReqUrl);
            AliGroupInfo groupInfo = JsonConvert.FromJson<AliGroupInfo>(groupJsonText);
            foreach (AliGroup g in groupInfo.Data)
            {
                g.ParentId = parentId;
                g.Level = parentLevel + 1;
                groups.Add(g);
            }
            foreach (AliGroup g in groupInfo.Data)
            {
                if (g.HasChildren)
                {
                    groups.AddRange(GetGroups(g.Id, g.Level, CsrfToken));
                }
            }
            return groups;
        }


        public List<AliProduct> GetProducts(int groupId, int groupLevel, string csrfToken)
        {
            List<AliProduct> produtList = new List<AliProduct>();
            string prodcutsReqUrl = string.Format(ProudctListRequest, groupId, csrfToken, groupId, groupLevel);
            string prodcutsJsonText = IEHandleUtils.WebRequestGetUrlHtml(prodcutsReqUrl);
            AliProductInfo productsInfo = JsonConvert.FromJson<AliProductInfo>(prodcutsJsonText);
            foreach (AliProduct p in productsInfo.Products)
            {
                p.GroupId = groupId;
                produtList.Add(p);
            }
            int pageNumber = productsInfo.Count / 50 + ((productsInfo.Count % 50 > 0) ? 1 : 0);
            for (int i = 2; i <= pageNumber; i++)
            {
                prodcutsReqUrl = string.Format(ProudctListRequest, i, csrfToken, groupId, groupLevel);
                prodcutsJsonText = IEHandleUtils.WebRequestGetUrlHtml(prodcutsReqUrl);
                AliProductInfo obj = JsonConvert.FromJson<AliProductInfo>(prodcutsJsonText);
                foreach (AliProduct p in obj.Products)
                {
                    p.GroupId = groupId;
                    produtList.Add(p);
                }
            }
            return produtList;
        }

        

    }
}
