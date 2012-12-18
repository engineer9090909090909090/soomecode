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
    public partial class VpnForm : Form
    {
        private VpnDAO vpnDao;

        #region 初始化
        public VpnForm()
        {
            InitializeComponent();
            L2tpKeyLabel.Hide();
            L2tpKeyTxtBox.Hide();
            vpnDao = DAOFactory.Instance.GetVpnDAO();
            LoadDataview();
        }

        private void VpnForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        void LoadDataview()
        {
            this.dataGridView.DataBindings.Clear();
            this.dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            DataTable dt = new DataTable();
            dt.Columns.Add("Check", typeof(Boolean));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("Username", typeof(string));
            dt.Columns.Add("Password", typeof(string));
            dt.Columns.Add("Country", typeof(string));
            dt.Columns.Add("VpnType", typeof(string));
            dt.Columns.Add("L2tpSec", typeof(string));
            dt.Columns.Add("VpnName", typeof(string));
            dt.Columns.Add("UpdateTime", typeof(DateTime));
            dt.Columns.Add("ID", typeof(Int32));
            this.dataGridView.DataSource = dt;
            DataGridViewColumn column0 = this.dataGridView.Columns[0];
            column0.HeaderText = "Check";
            column0.Width = 50;
            DataGridViewColumn column = this.dataGridView.Columns[1];
            column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column.HeaderText = "Address";
            column.Width = 180;
            DataGridViewColumn column2 = this.dataGridView.Columns[2];
            column2.HeaderText = "User Name";
            column2.Width = 100;
            DataGridViewColumn column3 = this.dataGridView.Columns[3];
            column3.HeaderText = "Password";
            column3.Width = 100;
            DataGridViewColumn column4 = this.dataGridView.Columns[4];
            column4.HeaderText = "Country";
            column4.Width = 100;
            DataGridViewColumn column5 = this.dataGridView.Columns[5];
            column5.HeaderText = "VPN Type";
            column5.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column5.Width = 100;
            DataGridViewColumn column6 = this.dataGridView.Columns[6];
            column6.HeaderText = "L2TP Sec";
            column6.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column6.Width = 100;
            DataGridViewColumn column7 = this.dataGridView.Columns[7];
            column7.HeaderText = "VPN Name";
            column7.Width = 100;
            DataGridViewColumn column8 = this.dataGridView.Columns[8];
            column8.HeaderText = "Update Time";
            column8.Width = 120;
            DataGridViewColumn column9 = this.dataGridView.Columns[9];
            column9.HeaderText = "Id";
            column9.Width = 10;
            column9.Visible = false;
            List<VpnModel> vpnModelList = vpnDao.GetVpnModelList();
            if (vpnModelList.Count > 0)
            {
                foreach (VpnModel item in vpnModelList)
                {
                    DataRow row = dt.NewRow();
                    row["Check"] = false;
                    row["Address"] = item.Address;
                    row["Username"] = item.Username;
                    row["Password"] = "******";
                    row["Country"] = item.Country;
                    row["VpnType"] = item.VpnType;
                    row["L2tpSec"] = item.L2tpSec;
                    row["VpnName"] = item.Name;
                    row["updateTime"] = item.UpdateTime;
                    row["Id"] = item.Id;
                    dt.Rows.Add(row);
                }
            }
        }
        
        private void L2tpBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (L2tpBtn.Checked)
            {
                L2tpKeyLabel.Show();
                L2tpKeyTxtBox.Show();
            }
            else
            {
                L2tpKeyLabel.Hide();
                L2tpKeyTxtBox.Hide();
            }
        }
        
        #endregion

        #region 新增按钮事件
        private void InsertBtn_Click(object sender, EventArgs e)
        {
            VpnModel model = new VpnModel();
            model.Address = AddressBox.Text.Trim().Replace(" ", "");
            try
            {
                IPAddress.Parse(model.Address);
            }catch{
                ErrorMsg.Text = "输入的VPN IP地址不合法.";
                return;
            }
            model.Username = UsernameBox.Text.Trim();
            if (string.IsNullOrEmpty(model.Username))
            {
                ErrorMsg.Text = "VPN 用户名不能为空.";
                return;
            }
            model.Password = PasswordBox.Text.Trim();
            if (string.IsNullOrEmpty(model.Password))
            {
                ErrorMsg.Text = "VPN 用户名密码不能为空.";
                return;
            }
            model.Country = countryTxt.Text.Trim();
            if (string.IsNullOrEmpty(model.Country))
            {
                ErrorMsg.Text = "国家不能为空.";
                return;
            }
            if (PptpBtn.Checked)
            {
                model.VpnType = Constants.PPTP;
                model.L2tpSec = string.Empty;
            }
            else
            {
                model.VpnType = Constants.L2TP;
                model.L2tpSec = L2tpKeyTxtBox.Text.Trim();
            }
            model.Name = AgentTxt.Text.Trim();
            bool existAddress = vpnDao.ExistAddress(model.Address, model.VpnType);
            if (existAddress)
            {
                ErrorMsg.Text = "VPN 地址已经存在列表中.";
                return;
            }
            vpnDao.Insert(model);
            AddressBox.Text = "";
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
                    removeList.Add(dr[9].ToString());
                    dt.Rows.Remove(dr);
                }
            }
            if (removeList.Count > 0)
            {
                vpnDao.DeleteVpn(removeList);
                MessageBox.Show(removeList.Count + " 行记录被删除。");
            }
        }
        #endregion

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
            List<VpnModel> vpnList = new List<VpnModel>();
            foreach (Worksheet sheet in workBook.Worksheets)
            {
                int AddressCol = -1;
                int UsernameCol = -1;
                int PasswordCol = -1;
                int CountryCol = -1;
                int AgentNameCol = -1;
                int VpnTypeCol = -1;
                int L2tpSecCol = -1;
                string sheetName = sheet.Name;
                Cells cells = sheet.Cells;
                for (int j = 0; j < cells.MaxDataColumn + 1; j++)
                {
                    string value = sheet.Cells[0, j].StringValue.Trim();
                    if ("address".Equals(value.ToLower()))
                    {
                        AddressCol = j;
                    }
                    if ("username".Equals(value.ToLower()))
                    {
                        UsernameCol = j;
                    }
                    if ("password".Equals(value.ToLower()))
                    {
                        PasswordCol = j;
                    }
                    if ("country".Equals(value.ToLower()))
                    {
                        CountryCol = j;
                    }
                    if ("agentname".Equals(value.ToLower()))
                    {
                        AgentNameCol = j;
                    }
                    if ("vpntype".Equals(value.ToLower()))
                    {
                        VpnTypeCol = j;
                    }
                    if ("l2tpsec".Equals(value.ToLower()))
                    {
                        L2tpSecCol = j;
                    }
                    //System.Diagnostics.Trace.WriteLine(value);
                }

                for (int i = 1; i < cells.MaxDataRow + 1; i++)
                {
                    if (AddressCol == -1)
                    {
                        continue;
                    }
                    VpnModel model = new VpnModel();
                    model.Address = sheet.Cells[i, AddressCol].StringValue.Trim();
                    if (UsernameCol != -1)
                    {
                        model.Username = sheet.Cells[i, UsernameCol].StringValue.Trim();
                    }
                    if (PasswordCol != -1)
                    {
                        model.Password = sheet.Cells[i, PasswordCol].StringValue.Trim();
                    }
                    if (CountryCol != -1)
                    {
                        model.Country = sheet.Cells[i, CountryCol].StringValue.Trim();
                    }
                    if (AgentNameCol != -1)
                    {
                        model.Name = sheet.Cells[i, AgentNameCol].StringValue.Trim();
                    }
                    if (VpnTypeCol != -1)
                    {
                        model.VpnType = sheet.Cells[i, VpnTypeCol].StringValue.Trim();
                    }
                    if (L2tpSecCol != -1)
                    {
                        model.L2tpSec = sheet.Cells[i, L2tpSecCol].StringValue.Trim();
                    }
                    vpnList.Add(model);
                }
            }

            if (vpnList.Count == 0)
            {
                this.ErrorMsg.Text = "此Excel中未包含任何邮件数据。请重新选择。";
                return;
            }
            vpnDao.ImportVpns(vpnList);
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


        #region Excel导出
        private void ExportBtn_Click(object sender, EventArgs e)
        {
            string localFilePath;
            List<VpnModel> accounts = vpnDao.GetVpnModelList();
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

        private void ExportToExcel(List<VpnModel> list, string path)
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
            cells[0, 0].PutValue("Address");
            cells[0, 0].SetStyle(style2);
            cells.SetColumnWidth(0, 40);
            cells[0, 1].PutValue("Username");
            cells[0, 1].SetStyle(style2);
            cells.SetColumnWidth(1, 20);
            cells[0, 2].PutValue("Password");
            cells[0, 2].SetStyle(style2);
            cells.SetColumnWidth(2, 20);
            cells[0, 3].PutValue("Country");
            cells[0, 3].SetStyle(style2);
            cells.SetColumnWidth(3, 20);
            cells[0, 4].PutValue("AgentName");
            cells[0, 4].SetStyle(style2);
            cells.SetColumnWidth(4, 20);
            cells[0, 5].PutValue("VpnType");
            cells[0, 5].SetStyle(style2);
            cells.SetColumnWidth(5, 20);
            cells[0, 6].PutValue("L2tpSec");
            cells[0, 6].SetStyle(style2);
            cells.SetColumnWidth(6, 20);
            cells.SetRowHeight(0, 25);
            //生成数据行
            for (int i = 1; i < Rownum; i++)
            {
                VpnModel model = list[i - 1];
                cells[i, 0].PutValue(model.Address);
                cells[i, 0].SetStyle(style3);
                cells[i, 1].PutValue(model.Username);
                cells[i, 1].SetStyle(style3);
                cells[i, 2].PutValue(model.Password);
                cells[i, 2].SetStyle(style3);
                cells[i, 3].PutValue(model.Country);
                cells[i, 3].SetStyle(style3);
                cells[i, 4].PutValue(model.Name);
                cells[i, 4].SetStyle(style3);
                cells[i, 5].PutValue(model.VpnType);
                cells[i, 5].SetStyle(style3);
                cells[i, 6].PutValue(model.L2tpSec);
                cells[i, 6].SetStyle(style3);
                cells.SetRowHeight(i, 24);
            }
            workbook.Save(path);
        }
        #endregion

        
    }
}
