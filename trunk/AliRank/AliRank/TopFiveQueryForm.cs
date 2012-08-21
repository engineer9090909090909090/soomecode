using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AliRank
{
    public partial class TopFiveQueryForm : Form
    {
        public TopFiveQueryForm()
        {
            InitializeComponent();
        }

        private void TopFiveQueryForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            InitDataview();
        }
        void InitDataview()
        {
            this.dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            DataTable dt = new DataTable();
            dt.Columns.Add("Image", typeof(Image));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Key1", typeof(string));
            dt.Columns.Add("Key2", typeof(string));
            dt.Columns.Add("Key3", typeof(string));
            dt.Columns.Add("Desc", typeof(string));
            this.dataGridView.DataSource = dt;
            DataGridViewColumn column0 = this.dataGridView.Columns[0];
            column0.HeaderText = "产品图片";
            column0.Width = 120;
            DataGridViewColumn column = this.dataGridView.Columns[1];
            column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column.HeaderText = "产品名称";
            column.Width = 200;
            DataGridViewColumn column2 = this.dataGridView.Columns[2];
            column2.HeaderText = "关键词1";
            column2.Width = 180;
            DataGridViewColumn column3 = this.dataGridView.Columns[3];
            column3.HeaderText = "关键词2";
            column3.Width = 180;
            DataGridViewColumn column4 = this.dataGridView.Columns[4];
            column4.HeaderText = "关键词3";
            column4.Width = 180;
            DataGridViewColumn column5 = this.dataGridView.Columns[5];
            column5.HeaderText = "简要描述";
            column5.Width = 250;
            column5.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        void LoadItemToDataView(TopFiveInfo item)
        {
            DataTable dt = (DataTable)this.dataGridView.DataSource;
            DataRow row = dt.NewRow();
            if (string.IsNullOrEmpty(item.Image) || !File.Exists(item.Image))
            {
                row["Image"] = global::AliRank.Properties.Resources.no_image;
            } else {
                row["Image"] = new Bitmap(Image.FromFile(item.Image));
            }
            row["Name"] = item.Name;
            row["Key1"] = item.Key1;
            row["Key2"] = item.Key2;
            row["Key3"] = item.Key3;
            row["Desc"] = item.Desc;
            dt.Rows.Add(row);
        }

        private void keyBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                queryBtn_Click(sender, e);
            }
        }
        private void queryBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(keyBox.Text.Trim()))
            {
                MessageBox.Show("查询关键字不能为空。");
                return;
            }
            queryBtn.Enabled = false;
            DataTable dt = (DataTable)this.dataGridView.DataSource;
            dt.Clear();
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerAsync();
            bgWorker.Dispose();
        }

        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            TopFiveQueryer query = new TopFiveQueryer();
            query.OnTopFiveSearchEndEvent += new TopFiveSearchEndEvent(query_OnTopFiveSearchEndEvent);
            query.Seacher(keyBox.Text.Trim());
            queryBtn.Enabled = true;
        }

        void query_OnTopFiveSearchEndEvent(object sender, TopFiveEventArgs e)
        {
            TopFiveInfo item = e.Item;
            LoadItemToDataView(item);
        }
    }
}
