using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soomes;

namespace AliHelper
{
    public partial class NewItemForm : Form
    {
        MyItemManager manager;
        Categories selectedCategory;
        public NewItemForm()
        {
            InitializeComponent();
            manager = new MyItemManager();
        }

        private void NewItemForm_Load(object sender, EventArgs e)
        {
            //LoadTreeView();
            this.ProductStatus.DisplayMember = "Label";
            this.ProductStatus.ValueMember = "Key";
            //this.ProductStatus.DataSource = manager.GetAppDicOptions(Constants.ProductStatus);
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

        private void Image_Click(object sender, EventArgs e)
        {

        }

        private void AddImageButton_Click(object sender, EventArgs e)
        {

        }
        private void CategoryTreeBox_SelectedNodeChanged(object sender, EventArgs e)
        {
            selectedCategory = (Categories)this.CategoryTreeBox.SelectedNode.Tag;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            
        }

        private void CannelButton_Click(object sender, EventArgs e)
        {

        }

        

        
    }
}
