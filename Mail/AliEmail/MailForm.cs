using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.PropertyGridInternal;

namespace AliEmail
{
    public partial class MailForm : Form
    {
        public MailForm()
        {
            InitializeComponent();
        }

        public void setDataGrigViewContext(DataTable dt)
        {
            this.dataGridView.DataSource = dt;
            this.dataGridView.Columns["IP"].HeaderText = "Message IP";
            this.dataGridView.Columns["Origin"].HeaderText = "Origin";
            this.dataGridView.Columns["Product"].HeaderText = "Product";
            this.dataGridView.Columns["Buyer Name"].HeaderText = "Name";
            this.dataGridView.Columns["Email"].HeaderText = "Email";
            this.dataGridView.Columns["Company"].HeaderText = "Company";
            this.dataGridView.Columns["Address"].HeaderText = "Address";
            this.dataGridView.Columns["Country"].HeaderText = "Country/Region";
            this.dataGridView.Columns["Telephone"].HeaderText = "Telephone";
            this.dataGridView.Columns["Fax"].HeaderText = "Fax";
            this.dataGridView.Columns["Date"].HeaderText = "Sent On";
            this.dataGridView.Columns["Source"].HeaderText = "Source";
            this.dataGridView.Columns["id"].FillWeight = 3;
            this.dataGridView.Columns["IP"].FillWeight = 5;
            this.dataGridView.Columns["Origin"].FillWeight = 5;
            this.dataGridView.Columns["Product"].FillWeight = 10;
            this.dataGridView.Columns["Buyer Name"].FillWeight = 10;
            this.dataGridView.Columns["Email"].FillWeight = 10;
            this.dataGridView.Columns["Country"].FillWeight = 7;
            this.dataGridView.Columns["Telephone"].FillWeight = 7;
            this.dataGridView.Columns["Company"].FillWeight = 10;
            this.dataGridView.Columns["Address"].FillWeight = 5;
            this.dataGridView.Columns["Fax"].FillWeight = 5;
            this.dataGridView.Columns["Date"].FillWeight = 10;
            this.dataGridView.Columns["Source"].FillWeight = 5;
        }
    }
}
