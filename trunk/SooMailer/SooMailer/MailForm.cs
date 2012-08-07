using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aspose.Network.Verify;
using Aspose.Cells;
using System.Threading;

namespace SooMailer
{
    public partial class MailForm : Form
    {

        private MailModelDAO Dao;

        #region form Initialize

        public MailForm()
        {
            InitializeComponent();
        }
       
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                dataGridView1.Rows[e.RowIndex].HeaderCell.Value = Convert.ToString(e.RowIndex + 1);
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            groupBox1.Width = this.Width - 20;
            if (!findBtn.Checked)
            {
                splitContainer1.Panel1.Hide();
                splitContainer1.SplitterDistance = 0;
            }
            else if (this.WindowState != FormWindowState.Minimized)
            {
                splitContainer1.Panel1.Show();
                splitContainer1.SplitterDistance = 94;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Dao = DAOFactory.Instance.GetMailModelDAO();
            InitSearchCondition();
            List<MailModel> mailModelList = Dao.GetMailModelList(null);
            LoadDataview(mailModelList);
        }
        #endregion

        #region DataGridView 初始化处理

        void InitSearchCondition()
        {
            SourceCombo.SelectedIndexChanged -= new EventHandler(button1_Click);
            TypeComboBox.SelectedIndexChanged -= new EventHandler(button1_Click);
            CountryComboBox.SelectedIndexChanged -= new EventHandler(button1_Click);
            VerifyComboBox.SelectedIndexChanged -= new EventHandler(button1_Click);

            SourceCombo.ValueMember = "Id";
            SourceCombo.DisplayMember = "Name";
            SourceCombo.Items.Clear();
            SourceCombo.Items.Insert(0, new ListItem("", ""));
            List<ListItem> sourceList = Dao.GetComboBoxData("Source");
            SourceCombo.Items.AddRange(sourceList.ToArray());
            SourceCombo.SelectedIndex = 0;
            SourceCombo.SelectedIndexChanged += new EventHandler(button1_Click);

            
            TypeComboBox.ValueMember = "Id";
            TypeComboBox.DisplayMember = "Name";
            TypeComboBox.Items.Clear();
            TypeComboBox.Items.Insert(0, new ListItem("", ""));
            List<ListItem> typeList = Dao.GetComboBoxData("ProductType");
            TypeComboBox.Items.AddRange(typeList.ToArray());
            TypeComboBox.SelectedIndex = 0;
            TypeComboBox.SelectedIndexChanged += new EventHandler(button1_Click);

           
            CountryComboBox.ValueMember = "Id";
            CountryComboBox.DisplayMember = "Name";
            CountryComboBox.Items.Clear();
            CountryComboBox.Items.Insert(0, new ListItem("", ""));
            List<ListItem> CountryList = Dao.GetComboBoxData("Country");
            CountryComboBox.Items.AddRange(CountryList.ToArray());
            CountryComboBox.SelectedIndex = 0;
            CountryComboBox.SelectedIndexChanged += new EventHandler(button1_Click);

           
            VerifyComboBox.ValueMember = "Id";
            VerifyComboBox.DisplayMember = "Name";
            VerifyComboBox.Items.Clear();
            VerifyComboBox.Items.Insert(0, new ListItem("-1", ""));
            VerifyComboBox.Items.Insert(1, new ListItem("0", "未验证"));
            VerifyComboBox.Items.Insert(2, new ListItem("1", "验证通过"));
            VerifyComboBox.Items.Insert(3, new ListItem("2", "验证未通过"));
            VerifyComboBox.SelectedIndex = 0;
            VerifyComboBox.SelectedIndexChanged += new EventHandler(button1_Click);
        }

        void LoadDataview(List<MailModel> mailModelList)
        {
            this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            this.dataGridView1.DataBindings.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("Check", typeof(System.Boolean));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("ProductType", typeof(string));
            dt.Columns.Add("Username", typeof(string));
            dt.Columns.Add("Country", typeof(string));
            dt.Columns.Add("Company", typeof(string));
            dt.Columns.Add("Subject", typeof(string));
            dt.Columns.Add("Source", typeof(string));
            dt.Columns.Add("SendDate", typeof(string));
            dt.Columns.Add("Verify", typeof(string));
            dt.Columns.Add("Id", typeof(string));
            if (mailModelList.Count > 0)
            {
                for (int i = 0; i < mailModelList.Count; i++ )
                {
                    MailModel item = mailModelList[i];
                    DataRow row = dt.NewRow();
                    row["Check"] = false;
                    row["Email"] = item.Email;
                    row["ProductType"] = item.ProductType;
                    row["Username"] = item.Username;
                    row["Country"] = item.Country;
                    row["Company"] = item.Company;
                    row["Subject"] = item.Subject;
                    row["Source"] = item.Source;
                    row["Verify"] = item.GetVerifyString();
                    row["SendDate"] = item.SendDate;
                    row["Id"] = item.Id;
                    dt.Rows.Add(row);
                }
            }
            this.dataGridView1.DataSource = dt;
            DataGridViewColumn column = this.dataGridView1.Columns[0];
            column.HeaderText = " ";
            column.Width = 20;
            column.Frozen = true;
            DataGridViewColumn column0 = this.dataGridView1.Columns[1];
            column0.HeaderText = "Email";
            column0.Width = 200;
            DataGridViewColumn column1 = this.dataGridView1.Columns[2];
            column1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column1.HeaderText = "Product";
            column1.Width = 80;
            DataGridViewColumn column2 = this.dataGridView1.Columns[3];
            column2.HeaderText = "Customer Name";
            column2.Width = 180;
            DataGridViewColumn column3 = this.dataGridView1.Columns[4];
            column3.HeaderText = "Country";
            column3.Width = 120;
            DataGridViewColumn column4 = this.dataGridView1.Columns[5];
            column4.HeaderText = "Company";
            column4.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column4.Width = 120;
            DataGridViewColumn column5 = this.dataGridView1.Columns[6];
            column5.HeaderText = "Subject";
            column5.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column5.Width = 180;
            DataGridViewColumn column6 = this.dataGridView1.Columns[7];
            column6.HeaderText = "Source";
            column6.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column6.Width = 100;
            DataGridViewColumn column7 = this.dataGridView1.Columns[8];
            column7.HeaderText = "SendDate";
            column7.Width = 100;
            DataGridViewColumn column8 = this.dataGridView1.Columns[9];
            column8.HeaderText = "Verify";
            column8.Width = 100;
            DataGridViewColumn column9 = this.dataGridView1.Columns[10];
            column9.HeaderText = "Id";
            column9.Visible = false;
        }
        #endregion

        private void findBtn_Click(object sender, EventArgs e)
        {
            if (findBtn.Checked)
            {
                findBtn.Checked = false;
                splitContainer1.Panel1.Hide();
                splitContainer1.SplitterDistance = 0;
            }
            else
            {
                findBtn.Checked = true;
                splitContainer1.Panel1.Show();
                splitContainer1.SplitterDistance = 94;
            }
        }
        private void excelImpBtn_Click(object sender, EventArgs e)
        {
            ExcelImpForm f = new ExcelImpForm();
            f.FormClosed += new FormClosedEventHandler(f_FormClosed);
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }
        private void f_FormClosed(object sender, FormClosedEventArgs e)
        {
            ExcelImpForm f = (ExcelImpForm)sender;
            if (f.SuccessLoadData)
            {
                InitSearchCondition();
                List<MailModel> mailModelList = Dao.GetMailModelList(null);
                LoadDataview(mailModelList);
            }
           
        }
        private void validationBtn_Click(object sender, EventArgs e)
        {
            BackgroundWorker validWorker = new BackgroundWorker();
            validWorker.DoWork += new DoWorkEventHandler(worker_DoWork);
            validWorker.RunWorkerAsync();
        }


        public static int iCount = 0;
        public static int MaxCount = 10;
        static ManualResetEvent eventX = new ManualResetEvent(false);
        private bool doWorkFlag = false;
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<MailModel> mailModelList = Dao.GetMailModelList(null);
            MaxCount = mailModelList.Count;
            if (MaxCount == 0)
            {
                return;
            }
            validationBtn.Enabled = false;
            stopValidBtn.Enabled = true;
            toolStripLabel.Text = "正在进行邮箱验证，请稍候...";
            doWorkFlag = true;
            ThreadPool.SetMinThreads(5, 40);
            ThreadPool.SetMaxThreads(10, 200);
            for (int i = 0; i < MaxCount; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(DoWork), (object)mailModelList[i]);
            }
            eventX.WaitOne(Timeout.Infinite, true); 
            System.Diagnostics.Trace.WriteLine("线程池结束！");
            validationBtn.Enabled = true;
            stopValidBtn.Enabled = false;
            toolStripLabel.Text = "";
            toolStripStatusLabel.Text = "邮箱验证已经停止.";
        }

        private void DoWork(object obj)
        {
            MailModel model = (MailModel)obj;
            if (!doWorkFlag || model.Verify1 > 0) 
            {
                Interlocked.Increment(ref iCount);
                if (iCount == MaxCount)
                {
                    eventX.Set();
                }
                return;
            };
            toolStripStatusLabel.Text = "验证邮箱地址: " + model.Email;
            bool result = ActiveUp.Net.Mail.SmtpValidator.Validate(model.Email);
            if (result)
            {
                model.Verify1 = 1;
                toolStripStatusLabel.Text = "邮箱地址[ " + model.Email + "]存在.";
            }
            else
            {
                model.Verify1 = 2;
                toolStripStatusLabel.Text = "邮箱地址[" + model.Email + "]不存在";
            }
            Dao.UpdateMailVerify(model);
            Interlocked.Increment(ref iCount);
            if (iCount == MaxCount)
            {
                eventX.Set();
            }
        }

        private void stopValidBtn_Click(object sender, EventArgs e)
        {
            doWorkFlag = false;
            stopValidBtn.Enabled = false;
            validationBtn.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<MailModel> mailModelList = SearchEmailData();
            LoadDataview(mailModelList);
        }

        private List<MailModel> SearchEmailData()
        {
            MailModel model = new MailModel();
            model.Source = ((ListItem)SourceCombo.SelectedItem).Id;
            model.ProductType = ((ListItem)TypeComboBox.SelectedItem).Id;
            model.Country = ((ListItem)CountryComboBox.SelectedItem).Id;
            model.Verify1 = Convert.ToInt32(((ListItem)VerifyComboBox.SelectedItem).Id);
            model.Email = EmailTxtBox.Text.Trim();
            model.Username = NameTxtBox.Text.Trim();
            return Dao.GetMailModelList(model);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string localFilePath;
            List<MailModel> mailModelList = SearchEmailData();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //设置文件类型  
            saveFileDialog1.Filter = "Excel工作簿(*.xls,*.xlsx)| *.xls; *.xlsx";
            //设置默认文件类型显示顺序  
            saveFileDialog1.FilterIndex = 2;
            //保存对话框是否记忆上次打开的目录  
            saveFileDialog1.RestoreDirectory = true;
            //点了保存按钮进入  
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)  
            {  
                //获得文件路径  
                localFilePath = saveFileDialog1.FileName.ToString();
                try
                {
                    ExportToExcel(mailModelList, localFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存文件出错："　+　ex.Message);
                }
            }
        }

        private void ExportToExcel(List<MailModel> list, string path)
        {
            Workbook workbook = new Workbook(); //工作簿 
            Worksheet sheet = workbook.Worksheets[0]; //工作表 
            Cells cells = sheet.Cells;//单元格 

            //为标题设置样式     
            Style styleTitle = workbook.Styles[workbook.Styles.Add()];//新增样式 
            styleTitle.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            styleTitle.Font.Name = "宋体";//文字字体 
            styleTitle.Font.Size = 18;//文字大小 
            styleTitle.Font.IsBold = true;//粗体 

            //样式2 
            Style style2 = workbook.Styles[workbook.Styles.Add()];//新增样式 
            style2.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            style2.Font.Name = "宋体";//文字字体 
            style2.Font.Size = 12;//文字大小 
            style2.Font.IsBold = true;//粗体 
            style2.BackgroundColor = Color.DeepPink;
            style2.IsTextWrapped = true;//单元格内容自动换行 
            style2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            //样式3 
            Style style3 = workbook.Styles[workbook.Styles.Add()];//新增样式 
            style3.HorizontalAlignment = TextAlignmentType.Left;//文字居中 
            style3.Font.Name = "宋体";//文字字体 
            style3.Font.Size = 10;//文字大小 
            style3.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Dotted;
            style3.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Dotted;
            style3.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Dotted;
            style3.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Dotted;

            int Rownum = list.Count;//表格行数 
            //生成行1 列名行 
            cells[0, 0].PutValue("Email");
            cells[0, 0].SetStyle(style2);
            cells.SetColumnWidth(0, 40);
            cells[0, 1].PutValue("Buyer Name");
            cells[0, 1].SetStyle(style2);
            cells.SetColumnWidth(1, 20);
            cells[0, 2].PutValue("Country");
            cells[0, 2].SetStyle(style2);
            cells.SetColumnWidth(2, 20);
            cells[0, 3].PutValue("ProductType");
            cells[0, 3].SetStyle(style2);
            cells.SetColumnWidth(3, 20);
            cells[0, 4].PutValue("Source");
            cells[0, 4].SetStyle(style2);
            cells.SetColumnWidth(4, 20);
            cells[0, 5].PutValue("Date");
            cells[0, 5].SetStyle(style2);
            cells.SetColumnWidth(5, 20);
            cells[0, 6].PutValue("Verify");
            cells[0, 6].SetStyle(style2);
            cells.SetColumnWidth(6, 20);
            cells.SetRowHeight(0, 25);
            //生成数据行 
            for (int i = 1; i < Rownum; i++)
            {
                MailModel model = list[i -1];
                cells[i, 0].PutValue(model.Email);
                cells[i, 0].SetStyle(style3);
                cells[i, 1].PutValue(model.Username);
                cells[i, 1].SetStyle(style3);
                cells[i, 2].PutValue(model.Country);
                cells[i, 2].SetStyle(style3);
                cells[i, 3].PutValue(model.ProductType);
                cells[i, 3].SetStyle(style3);
                cells[i, 4].PutValue(model.Source);
                cells[i, 4].SetStyle(style3);
                cells[i, 5].PutValue(model.SendDate);
                cells[i, 5].SetStyle(style3);
                cells[i, 6].PutValue(model.GetVerifyString());
                cells[i, 6].SetStyle(style3);
                cells.SetRowHeight(i, 24);
            }
            workbook.Save(path);
        }

        private void RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dao.UpdateAllMailVerifyToZreo();
            button1_Click(sender,e);
        }

        

    }
    
}
