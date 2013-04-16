using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soomes;

namespace AliHelper.MyItem
{
    public partial class CategoriesForm : Form
    {

        MyItemManager manager;
        TreeNode selectedNode;
        public CategoriesForm()
        {
            InitializeComponent();
            manager = new MyItemManager();
        }

        private void CategoriesForm_Load(object sender, EventArgs e)
        {
            this.CategoryGroup.Visible = false;
            LoadTreeView();
        }

        private void NewTopButton_Click(object sender, EventArgs e)
        {
            this.CategoryGroup.Visible = true;
            this.CategoryGroup.Text = "新增顶级分类";
            Categories cate = new Categories();
            cate.ParentId = 0;
            cate.Level = 1;
            this.CategoryName.Tag = cate;
            this.CategoryName.Text = string.Empty;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            Categories cate = (Categories)this.CategoryName.Tag;
            cate.Name = this.CategoryName.Text;
            if (string.IsNullOrEmpty(cate.Name))
            {
                MessageBox.Show("您没有填写分类名称！", "错误提示");
                return;
            }
            if (cate.Sort == 0)
            {
                cate.Sort = manager.GetCategoryNewSortNo(cate.ParentId, cate.Level);
            }
            manager.InsertOrUpdateCategory(cate);
            this.CategoryGroup.Visible = false;
            LoadTreeView();
        }

        private void RenameMenuItem_Click(object sender, EventArgs e)
        {
            this.CategoryGroup.Visible = true;
            Categories selected = (Categories)selectedNode.Tag;
            this.CategoryGroup.Text = "重命名分类【" + selected.Name + "】";
            this.CategoryName.Tag = selected;
            this.CategoryName.Text = selected.Name;
        }

        private void DeleMenuItem_Click(object sender, EventArgs e)
        {
            Categories selected = (Categories)selectedNode.Tag;
            if (MessageBox.Show("您确定要删除这个分类吗?", "提示信息", MessageBoxButtons.OKCancel, 
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading) == DialogResult.OK)
            {
                manager.DeleteCategory(selected.Id);
            }
        }

        private void MoveUpMenuItem_Click(object sender, EventArgs e)
        {
            Categories curCategory = (Categories)selectedNode.Tag;
            Categories prevCategory = (Categories)selectedNode.PrevNode.Tag;
            manager.CategoryMoveSort(curCategory, prevCategory);
            LoadTreeView();

        }

        private void MoveDownMenuItem_Click(object sender, EventArgs e)
        {
            Categories curCategory = (Categories)selectedNode.Tag;
            Categories nextCategory = (Categories)selectedNode.NextNode.Tag;
            manager.CategoryMoveSort(curCategory, nextCategory);
            LoadTreeView();
        }

        private void NewChildMenuItem_Click(object sender, EventArgs e)
        {
            this.CategoryGroup.Visible = true;
            Categories parent = (Categories)selectedNode.Tag;
            this.CategoryGroup.Text = "【" + parent.Name + "】新增子分类";
            Categories cate = new Categories();
            cate.ParentId = parent.Id;
            cate.Level = parent.Level + 1;
            this.CategoryName.Tag = cate;
            this.CategoryName.Text = string.Empty;
        }
    
        public void LoadTreeView()
        {
            CateTreeView.Nodes.Clear();
            List<Categories> cates = manager.GetAllCategories();
            foreach (Categories p in cates)
            {
                if (p.Level == 1)
                {
                    TreeNode t1 = new TreeNode(p.Name);
                    t1.Tag = p;
                    CateTreeView.Nodes.Add(t1);
                    foreach (Categories c in cates)
                    {
                        if (c.ParentId == p.Id && c.Level == p.Level + 1)
                        {
                            TreeNode t2 = new TreeNode(c.Name);
                            t2.Tag = c;
                            t1.Nodes.Add(t2);

                            foreach (Categories f in cates)
                            {
                                if (f.ParentId == c.Id && f.Level == c.Level + 1)
                                {
                                    TreeNode t3= new TreeNode(f.Name);
                                    t3.Tag = f;
                                    t2.Nodes.Add(t3);
                                }
                            }

                        }
                    }
                }
            }
            CateTreeView.ExpandAll();
        }

        private void CateTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            MoveUpMenuItem.Enabled = true;
            MoveDownMenuItem.Enabled = true;
            NewChildMenuItem.Enabled = true;
            DeleMenuItem.Enabled = true;
            selectedNode = e.Node;
            if (e.Button == MouseButtons.Right)
            {
                if (selectedNode.PrevNode == null)
                {
                    MoveUpMenuItem.Enabled = false;
                }
                if (selectedNode.NextNode == null)
                {
                    MoveDownMenuItem.Enabled = false;
                }
                if (selectedNode.Level < 3)
                {
                    NewChildMenuItem.Enabled = false;
                }
                if (selectedNode.GetNodeCount(true) > 0)
                {
                    DeleMenuItem.Enabled = false;
                }
                CateTreeView.ContextMenuStrip.Show();
            }
            if (e.Button == MouseButtons.Left)
            {
                this.CategoryGroup.Visible = false;
            }
        }

        

        
    }
}
