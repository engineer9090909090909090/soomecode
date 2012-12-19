using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Aspose.Cells;

namespace AliRank
{
    public partial class AccountForm : Form
    {
        private InquiryDAO inquiryDAO;

        #region 初始化
        public AccountForm()
        {
            InitializeComponent();
            inquiryDAO = DAOFactory.Instance.GetInquiryDAO();
            LoadDataview();
        }
        private void AccountForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        void LoadDataview()
        {
            this.dataGridView.DataBindings.Clear();
            this.dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            DataTable dt = new DataTable();
            dt.Columns.Add("Check", typeof(Boolean));
            dt.Columns.Add("Account", typeof(string));
            dt.Columns.Add("Password", typeof(string));
            dt.Columns.Add("Country", typeof(string));
            dt.Columns.Add("Enable", typeof(string));
            dt.Columns.Add("LoginIp", typeof(string));
            dt.Columns.Add("InquiryNum", typeof(string));
            dt.Columns.Add("ID", typeof(Int32));
            this.dataGridView.DataSource = dt;
            DataGridViewColumn column0 = this.dataGridView.Columns[0];
            column0.HeaderText = "Check";
            column0.Width = 80;
            DataGridViewColumn column = this.dataGridView.Columns[1];
            column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column.HeaderText = "Account";
            column.Width = 230;
            DataGridViewColumn column2 = this.dataGridView.Columns[2];
            column2.HeaderText = "Password";
            column2.Width = 80;
            DataGridViewColumn column3 = this.dataGridView.Columns[3];
            column3.HeaderText = "Country";
            column3.Width = 100;
            DataGridViewColumn column4 = this.dataGridView.Columns[4];
            column4.HeaderText = "Enable";
            column4.Width = 80;
            DataGridViewColumn column5 = this.dataGridView.Columns[5];
            column5.HeaderText = "Last LoginIp";
            column5.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column5.Width = 100;
            DataGridViewColumn column6 = this.dataGridView.Columns[6];
            column6.HeaderText = "InquiryNum";
            column6.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column6.Width = 120;
            DataGridViewColumn column7 = this.dataGridView.Columns[7];
            column7.HeaderText = "Id";
            column7.Width = 10;
            column7.Visible = false;
            List<AliAccounts> accountList = inquiryDAO.GetAccounts();
            if (accountList.Count > 0)
            {
                foreach (AliAccounts item in accountList)
                {
                    DataRow row = dt.NewRow();
                    row["Check"] = false;
                    row["Account"] = item.Account;
                    row["Country"] = item.Country;
                    row["Password"] = "******";
                    row["Enable"] = item.Enable == 1 ? "Yes" : "No";
                    row["LoginIp"] = item.LoginIp;
                    row["InquiryNum"] = item.InquiryNum;
                    row["Id"] = item.AccountId;
                    dt.Rows.Add(row);
                }
            }
        }
        
        #endregion

        #region 新增按钮事件
        private void InsertBtn_Click(object sender, EventArgs e)
        {
            AliAccounts model = new AliAccounts();
            model.Account = this.AccountBox.Text.Trim();
            if (string.IsNullOrEmpty(model.Account))
            {
                ErrorMsg.Text = "帐号不能为空.";
                return;
            }
            model.Password = PasswordBox.Text.Trim();
            if (string.IsNullOrEmpty(model.Password))
            {
                ErrorMsg.Text = "密码不能为空.";
                return;
            }
            model.Country = CountryBox.Text.Trim();
            if (string.IsNullOrEmpty(model.Country))
            {
                ErrorMsg.Text = "国家不能为空.";
                return;
            }
            bool existAddress = inquiryDAO.ExistAccount(model.Account);
            if (existAddress)
            {
                ErrorMsg.Text = "帐号已经存在列表中.";
                return;
            }
            inquiryDAO.InsertAccount(model);
            AccountBox.Text = "";
            PasswordBox.Text = "";
            LoadDataview();
        }
        #endregion

        #region 删除按钮事件
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView == null || dataGridView.Rows.Count == 0)
            {
                return;
            }
            List<string> removeList = new List<string>();
            DataTable dt = (DataTable)dataGridView.DataSource;
            for (int j = dt.Rows.Count - 1; j >= 0; j--)
            {
                DataRow dr = dt.Rows[j];
                Boolean chk = System.Boolean.Parse(dr[0].ToString());
                if (chk)
                {
                    removeList.Add(dr[7].ToString());
                    dt.Rows.Remove(dr);
                }
            }
            if (removeList.Count > 0)
            {
                inquiryDAO.DeleteAccount(removeList);
                MessageBox.Show(removeList.Count + " 行记录被删除。");
            }
        }
        #endregion

        #region 导入excel
        private string SelectExcelFile = string.Empty;
        private void ImportBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ofd.Filter = "Excel工作簿(*.xls,*.xlsx)| *.xls; *.xlsx";
            ofd.ShowDialog();
            SelectExcelFile = ofd.FileName;               //获得选择的文件路径
            if (string.IsNullOrEmpty(SelectExcelFile))
            {
                this.ErrorMsg.Text = "要导入的Excel文件不能为空！";
                return;
            }
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerAsync();
            worker.Dispose();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Workbook workBook = new Workbook();
            try
            {
                workBook.Open(SelectExcelFile);
            }
            catch (Exception ex)
            {
                this.ErrorMsg.Text = "打开选定的Excel文件出错: " + ex.Message;
                return;
            }
            List<AliAccounts> accountList = new List<AliAccounts>();
            foreach (Worksheet sheet in workBook.Worksheets)
            {
                int AccountCol = -1;
                int PasswordCol = -1;
                int CountryCol = -1;
                int LoginIpCol = -1;
                string sheetName = sheet.Name;
                Cells cells = sheet.Cells;
                for (int j = 0; j < cells.MaxDataColumn + 1; j++)
                {
                    string value = sheet.Cells[0, j].StringValue.Trim();
                    if ("account".Equals(value.ToLower()))
                    {
                        AccountCol = j;
                    }
                    if ("password".Equals(value.ToLower()))
                    {
                        PasswordCol = j;
                    }
                    if ("country".Equals(value.ToLower()))
                    {
                        CountryCol = j;
                    }
                    if ("loginip".Equals(value.ToLower()))
                    {
                        LoginIpCol = j;
                    }
                    //System.Diagnostics.Trace.WriteLine(value);
                }

                for (int i = 1; i < cells.MaxDataRow + 1; i++)
                {
                    if (AccountCol == -1)
                    {
                        continue;
                    }
                    string Account = sheet.Cells[i, AccountCol].StringValue.Trim();
                    if (!FormatValidation.IsEmail(Account))
                    {
                        continue;
                    }
                    AliAccounts model = new AliAccounts();
                    model.Account = Account;
                    if (PasswordCol != -1)
                    {
                        model.Password = sheet.Cells[i, PasswordCol].StringValue.Trim();
                    }
                    if (CountryCol != -1)
                    {
                        model.Country = sheet.Cells[i, CountryCol].StringValue.Trim();
                    }
                    if (LoginIpCol != -1)
                    {
                        model.LoginIp = sheet.Cells[i, LoginIpCol].StringValue.Trim();
                    }
                    accountList.Add(model);
                }
            }

            if (accountList.Count == 0)
            {
                this.ErrorMsg.Text = "此Excel中未包含任何邮件数据。请重新选择。";
                return;
            }
            inquiryDAO.ImportAccounts(accountList);
            if (dataGridView.InvokeRequired)
            {
                UpdateDataGridView uActive = LoadDataview;
                this.BeginInvoke(uActive, null);
            }
            else
            {
                LoadDataview();
            }
        }
        private delegate void UpdateDataGridView();

        #endregion

        #region 导出Excel
        private void ExportBtn_Click(object sender, EventArgs e)
        {
            string localFilePath;
            List<AliAccounts> accounts = inquiryDAO.GetAccounts();
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
                    ExportToExcel(accounts, localFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存文件出错：" + ex.Message);
                }
            }
            
        }

        private void ExportToExcel(List<AliAccounts> list, string path)
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
            cells[0, 0].PutValue("Account");
            cells[0, 0].SetStyle(style2);
            cells.SetColumnWidth(0, 40);
            cells[0, 1].PutValue("Password");
            cells[0, 1].SetStyle(style2);
            cells.SetColumnWidth(1, 20);
            cells[0, 2].PutValue("Country");
            cells[0, 2].SetStyle(style2);
            cells.SetColumnWidth(2, 20);
            cells[0, 3].PutValue("LoginIP");
            cells[0, 3].SetStyle(style2);
            cells.SetColumnWidth(3, 20);
            cells[0, 4].PutValue("Enable");
            cells[0, 4].SetStyle(style2);
            cells.SetColumnWidth(4, 20);
            cells.SetRowHeight(0, 25);
            //生成数据行 
            for (int i = 1; i < Rownum; i++)
            {
                AliAccounts model = list[i - 1];
                cells[i, 0].PutValue(model.Account);
                cells[i, 0].SetStyle(style3);
                cells[i, 1].PutValue(model.Password);
                cells[i, 1].SetStyle(style3);
                cells[i, 2].PutValue(model.Country);
                cells[i, 2].SetStyle(style3);
                cells[i, 3].PutValue(model.LoginIp);
                cells[i, 3].SetStyle(style3);
                cells[i, 4].PutValue(model.Enable);
                cells[i, 4].SetStyle(style3);
                cells.SetRowHeight(i, 24);
            }
            workbook.Save(path);
        }
        #endregion

        


    }
}
