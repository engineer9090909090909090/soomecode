using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AliEmail
{
    public partial class OrigForm : Form
    {
        public OrigForm()
        {
            InitializeComponent();
        }

        public void setDataGrigViewContext(DataSet ds)
        { 
            this.dataGridView.DataSource = ds.Tables[0];
            this.dataGridView.Columns[0].HeaderText = "主题";
            this.dataGridView.Columns[1].HeaderText = "正文";
            this.dataGridView.Columns[2].HeaderText = "发送时间";
            this.dataGridView.Columns[0].FillWeight = 30;
            this.dataGridView.Columns[1].FillWeight = 40;
            this.dataGridView.Columns[2].FillWeight = 30;
        }
    }
}
