using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soomes;
using System.Web;

namespace AliHelper
{
    public partial class NewItemForm : Form
    {
        public Product UpdatedProduct { set; get; }
        private MyItemManager manager;
        private Categories selectedCategory;
        public NewItemForm()
        {
            InitializeComponent();
            webBrowser1.Navigate(Application.StartupPath + "\\KindEditor\\Editor.htm");
            manager = new MyItemManager();
        }

        private void NewItemForm_Load(object sender, EventArgs e)
        {
            this.ProductStatus.DisplayMember = "Label";
            this.ProductStatus.ValueMember = "Key";
            this.ProductStatus.DataSource = manager.GetAppDicOptions(Constants.ProductStatus);
            this.PriceCate.DisplayMember = "CateName";
            this.PriceCate.ValueMember = "Id";
            QueryObject<PriceCate> query = new QueryObject<PriceCate>();
            query.IsExport = true;
            this.PriceCate.DataSource = manager.GetPriceCates(query).Result;
            LoadUpdatedProduct(UpdatedProduct);
            LoadTreeView();
        }

        protected void LoadUpdatedProduct(Product obj)
        {
            if (obj == null)
            {
                return;
            }
            this.ProductName.Text = obj.Name;
            this.ProductModel.Text = obj.Model;
            this.Weight.Text = obj.Weight;
            this.Size.Text = obj.Size;
            this.Minimum.Text = obj.Minimum.ToString();
            this.Packing.Text = obj.Packing;
            AliHelperUtils.LoadAppDicComboBoxValue(this.PriceCate, obj.PriceCate.ToString());
            AliHelperUtils.LoadAppDicComboBoxValue(this.ProductStatus, obj.Status);
            LoadPriceCateComboBoxValue(this.PriceCate, obj.PriceCate);
            this.selectedCategory = new Categories();
            this.selectedCategory.Id = obj.CategoryId;
            this.webBrowser1.Document.InvokeScript("SetData", new object[] { HttpUtility.HtmlDecode(obj.Description) });
        }
        
        public void LoadTreeView()
        {
            CategoryTreeBox.Nodes.Clear();
            List<Categories> cates = manager.GetAllCategories();
            foreach (Categories p in cates)
            {
                if (p.Level == 1)
                {
                    ComboTreeNode t1 = new ComboTreeNode(p.Name);
                    t1.Tag = p;
                    CategoryTreeBox.Nodes.Add(t1);
                    if (selectedCategory != null && selectedCategory.Id == p.Id)
                    {
                        CategoryTreeBox.SelectedNode = t1;
                    }

                    foreach (Categories c in cates)
                    {
                        if (c.ParentId == p.Id && c.Level == p.Level + 1)
                        {
                            ComboTreeNode t2 = new ComboTreeNode(c.Name);
                            t2.Tag = c;
                            t1.Nodes.Add(t2);
                            if (selectedCategory != null && selectedCategory.Id == c.Id)
                            {
                                CategoryTreeBox.SelectedNode = t2;
                            }

                            foreach (Categories f in cates)
                            {
                                if (f.ParentId == c.Id && f.Level == c.Level + 1)
                                {
                                    ComboTreeNode t3 = new ComboTreeNode(f.Name);
                                    t3.Tag = f;
                                    t2.Nodes.Add(t3);
                                    if (selectedCategory != null && selectedCategory.Id == f.Id)
                                    {
                                        CategoryTreeBox.SelectedNode = t3;
                                    }
                                }
                            }

                        }
                    }
                }
            }
            CategoryTreeBox.ExpandAll();
        }

        private void CategoryTreeBox_SelectedNodeChanged(object sender, EventArgs e)
        {
            selectedCategory = (Categories)this.CategoryTreeBox.SelectedNode.Tag;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (selectedCategory == null)
            {
                MessageBox.Show("产品分类必须选择!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (this.ProductName.Text.Trim() == null)
            {
                MessageBox.Show("产品名称必须填写!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (this.ProductModel.Text.Trim() == null)
            {
                MessageBox.Show("产品型号必须填写!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Product item = new Product();
            item.Name = this.ProductName.Text.Trim();
            item.Model = this.ProductModel.Text.Trim();
            item.Price = Convert.ToDouble(this.ProductPrice.Text);
            item.CategoryId = selectedCategory.Id;

            item.Weight = this.Weight.Text.Trim();
            item.Size = this.Size.Text.Trim();
            item.Minimum = Convert.ToInt32(this.Minimum.Text.Trim());
            item.Packing = this.Packing.Text.Trim();
            item.Status = ((AppDic)this.ProductStatus.SelectedItem).Key;
            item.Description = (string)this.webBrowser1.Document.InvokeScript("GetData", null);


            this.Close();
        }

        private void CannelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadPriceCateComboBoxValue(ComboBox combo, int val)
        {
            foreach (PriceCate item in combo.Items)
            {
                if (item.Id == val)
                {
                    combo.SelectedItem = item;
                    return;
                }
            }
        }
    }
}
