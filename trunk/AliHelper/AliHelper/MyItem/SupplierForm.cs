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
    public partial class SupplierForm : Form
    {
        SupplierManager manager;
        public Supplier UpdateSupplier { set; get; }
        public SupplierForm()
        {
            InitializeComponent();
            manager = new SupplierManager();
        }

        private void SupplierForm_Load(object sender, EventArgs e)
        {
            LoadUpdateSupplier(UpdateSupplier);
        }


        private void LoadUpdateSupplier(Supplier obj)
        {
            if (obj == null) return;
            this.Tag = obj;
            this.NameTxt.Text = obj.Name;
            this.ContactTxt.Text = obj.Contact;
            this.AddressTxt.Text = obj.Address;
            this.RemarkTxt.Text = obj.Remark;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.NameTxt.Text))
            {
                MessageBox.Show("供应商名称不能为空。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.Tag == null)
            {
                UpdateSupplier = new Supplier();
            }
            UpdateSupplier.Name = this.NameTxt.Text;
            UpdateSupplier.Contact = this.ContactTxt.Text;
            UpdateSupplier.Address = this.AddressTxt.Text;
            UpdateSupplier.Remark = this.RemarkTxt.Text;
            manager.InsertOrUpdateSupplier(UpdateSupplier);
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
