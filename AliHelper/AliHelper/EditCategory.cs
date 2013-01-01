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
    public partial class EditCategory : Form
    {
        public EditCategory()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string key = this.KeywordBox.Text.Trim();
            if (string.IsNullOrEmpty(key))
            {
                return;
            }
            List<CategroyNode> nodes = HttpClient.SearchCategories(key);
            this.CategoryListBox.DataBindings.Clear();
            this.CategoryListBox.DisplayMember = "Name";
            this.CategoryListBox.ValueMember = "Id";
            this.CategoryListBox.DataSource = nodes;
        }

        private void KeywordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                SearchButton_Click(sender, e);
            }
        }

        private void CategoryListBox_DoubleClick(object sender, EventArgs e)
        {
            string selectId =  this.CategoryListBox.SelectedValue.ToString();
            List<AttributeNode> nodes = HttpClient.GetSelectCategoryAttributes(selectId);
            foreach (AttributeNode node in nodes)
            {
                System.Diagnostics.Trace.WriteLine(node.Data.Id + " = " +node.Data.Value );
            }
            System.Diagnostics.Trace.WriteLine("=============");
        }
    }
}
