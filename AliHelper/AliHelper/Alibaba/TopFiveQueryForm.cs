using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AliHelper
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
            dt.Columns.Add("Key", typeof(string));
            dt.Columns.Add("Desc", typeof(string));
            dt.Columns.Add("Cate", typeof(string));
            dt.Columns.Add("Href", typeof(string));
            this.dataGridView.DataSource = dt;
            DataGridViewColumn column0 = this.dataGridView.Columns[0];
            column0.HeaderText = "产品图片";
            column0.Width = 120;
            DataGridViewColumn column = this.dataGridView.Columns[1];
            column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column.HeaderText = "产品名称";
            column.Width = 200;
            DataGridViewColumn column2 = this.dataGridView.Columns[2];
            column2.HeaderText = "关键词";
            column2.Width = 200;
            column2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DataGridViewColumn column3 = this.dataGridView.Columns[3];
            column3.HeaderText = "简要描述";
            column3.Width = 250;
            column3.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DataGridViewColumn column4 = this.dataGridView.Columns[4];
            column4.HeaderText = "产品类目";
            column4.Width = 400;
            column4.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DataGridViewColumn column5 = this.dataGridView.Columns[5];
            column5.HeaderText = "Href";
            column5.Width = 10;
            column5.Visible = false;
        }

        void LoadItemToDataView(TopFiveInfo item)
        {
            DataTable dt = (DataTable)this.dataGridView.DataSource;
            DataRow row = dt.NewRow();
            if (string.IsNullOrEmpty(item.Image) || !File.Exists(item.Image))
            {
                row["Image"] = global::AliHelper.Properties.Resources.no_image;
            } else {
                row["Image"] = new Bitmap(Image.FromFile(item.Image));
            }
            row["Name"] = item.Name;
            row["Key"] = item.Key;
            row["Desc"] = item.Desc;
            row["Cate"] = item.Category;
            row["Href"] = item.Href;
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
            keyBox.Enabled = false;
            DataTable dt = (DataTable)this.dataGridView.DataSource;
            dt.Clear();
            MsgText.Text = "正在搜索阿里出口通网站，请稍候...";
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerAsync();
            bgWorker.Dispose();
        }
        private TopFiveInfo lastTopFiveInfo;
        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            TopFiveQueryer query = new TopFiveQueryer();
            query.OnTopFiveSearchEndEvent += new TopFiveSearchEndEvent(query_OnTopFiveSearchEndEvent);
            query.Seacher(keyBox.Text.Trim());
            MsgText.Text = "搜索完成，此关键词有" + lastTopFiveInfo.KeyAdNum + "个固定排名产品，" + lastTopFiveInfo.KeyP4Num + "个P4P排名产品。";
            queryBtn.Enabled = true;
            keyBox.Enabled = true;
        }


        private delegate void UpdateDataGridView(TopFiveInfo item);
        void query_OnTopFiveSearchEndEvent(object sender, TopFiveEventArgs e)
        {
            TopFiveInfo item = e.Item;
            lastTopFiveInfo = e.Item;
            if (dataGridView.InvokeRequired)
            {
                UpdateDataGridView uActive = LoadItemToDataView;
                this.BeginInvoke(uActive, new object[] { item });
            }
            else
            {
                LoadItemToDataView(item);
            }

        }

        #region 右键菜单梆定　右键菜单操作
        int SelectedRowIndex = 0;
        private void dataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        row.Selected = false;
                    }
                    dataGridView.Rows[e.RowIndex].Selected = true;
                    SelectedRowIndex = e.RowIndex;
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string href = Convert.ToString(dataGridView.Rows[SelectedRowIndex].Cells[5].Value);
            System.Diagnostics.Process.Start("iexplore.exe", href);
        }

        #endregion
    }
}
