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
            this.dataGridView.Columns["ip"].HeaderText = "Message IP";
            this.dataGridView.Columns["origin"].HeaderText = "Origin";
            this.dataGridView.Columns["product"].HeaderText = "Product";
            this.dataGridView.Columns["name"].HeaderText = "Name";
            this.dataGridView.Columns["email"].HeaderText = "Email";
            this.dataGridView.Columns["company"].HeaderText = "Company";
            this.dataGridView.Columns["address"].HeaderText = "Address";
            this.dataGridView.Columns["country"].HeaderText = "Country/Region";
            this.dataGridView.Columns["telephone"].HeaderText = "Telephone";
            this.dataGridView.Columns["fax"].HeaderText = "Fax";
            this.dataGridView.Columns["sendTime"].HeaderText = "Sent On";
            this.dataGridView.Columns["source"].HeaderText = "Source";
            this.dataGridView.Columns["id"].FillWeight = 3;
            this.dataGridView.Columns["ip"].FillWeight = 5;
            this.dataGridView.Columns["origin"].FillWeight = 5;
            this.dataGridView.Columns["product"].FillWeight = 10;
            this.dataGridView.Columns["name"].FillWeight = 10;
            this.dataGridView.Columns["email"].FillWeight = 10;
            this.dataGridView.Columns["country"].FillWeight = 7;
            this.dataGridView.Columns["telephone"].FillWeight = 7;
            this.dataGridView.Columns["company"].FillWeight = 10;
            this.dataGridView.Columns["address"].FillWeight = 5;
            this.dataGridView.Columns["fax"].FillWeight = 5;
            this.dataGridView.Columns["sendTime"].FillWeight = 10;
            this.dataGridView.Columns["source"].FillWeight = 5;
        }
    }
}
