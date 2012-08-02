using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aspose.Cells;

namespace SooMailer
{
    public partial class ExcelImpForm : Form
    {
        public ExcelImpForm()
        {
            InitializeComponent();
        }

        private void ExcelImpForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        private void selectBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ofd.Filter = "Excel工作簿(*.xls,*.xlsx)| *.xls; *.xlsx";
            ofd.ShowDialog();
            string fileName = ofd.FileName;               //获得选择的文件路径
            filePath.Text = fileName;
        }

        private void importBtn_Click(object sender, EventArgs e)
        {
            string SelectExcelFile = filePath.Text;
            if (string.IsNullOrEmpty(SelectExcelFile))
            {
                errorMsg.Text = "Excel文件不能为空！";
                return;
            }
            this.importBtn.Enabled = false;
            this.pictureBox1.Visible = true;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerAsync();
            worker.Dispose();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string SelectExcelFile = filePath.Text;
            this.pictureBox1.Visible = false;
            Workbook workBook = new Workbook();
            try
            {
                workBook.Open(SelectExcelFile);
            }
            catch (Exception ex)
            {
                errorMsg.Text = "打开选定的Excel文件出错: " + ex.Message;
                this.importBtn.Enabled = true;
                this.pictureBox1.Visible = false;
                return;
            }
            List<MailModel> mailModelList = new List<MailModel>();
            foreach (Worksheet sheet in workBook.Worksheets)
            {
                int EmailCol = -1;
                int CountryCol = -1;
                int BuyerCol = -1;
                int CompanyCol = -1;
                int SubjectCol = -1;
                int DateCol = -1;
                int ProductTypeCol = -1;
                int SourceCol = -1;
                string sheetName = sheet.Name;
                Cells cells = sheet.Cells;
                for (int j = 0; j < cells.MaxDataColumn + 1; j++)
                {
                    string value = sheet.Cells[0, j].StringValue.Trim();
                    if ("email".Equals(value.ToLower()))
                    {
                        EmailCol = j;
                    }
                    if ("country".Equals(value.ToLower()))
                    {
                        CountryCol = j;
                    }
                    if ("name".Equals(value.ToLower()))
                    {
                        BuyerCol = j;
                    }
                    if ("company".Equals(value.ToLower()))
                    {
                        CompanyCol = j;
                    }
                    if ("date".Equals(value.ToLower()))
                    {
                        DateCol = j;
                    }
                    if ("subject".Equals(value.ToLower()))
                    {
                        SubjectCol = j;
                    }
                    if ("product".Equals(value.ToLower()))
                    {
                        ProductTypeCol = j;
                    }
                    if ("source".Equals(value.ToLower()))
                    {
                        SourceCol = j;
                    }
                    System.Diagnostics.Trace.WriteLine(value);
                }
               
                for (int i = 1; i < cells.MaxDataRow + 1; i++)
                {
                    if (EmailCol == -1)
                    {
                        continue;
                    }
                    string Email = sheet.Cells[i, EmailCol].StringValue.Trim();
                    if (!FormatValidation.IsEmail(Email))
                    {
                        continue;
                    }
                    MailModel model = new MailModel();
                    model.Email = Email;
                    if (CountryCol != -1)
                    {
                        model.Country = sheet.Cells[i, CountryCol].StringValue.Trim();
                    }
                    if (BuyerCol != -1)
                    {
                        model.Username = sheet.Cells[i, BuyerCol].StringValue.Trim();
                    }
                    if (CompanyCol != -1)
                    {
                        model.Company = sheet.Cells[i, CompanyCol].StringValue.Trim();
                    }
                    if (SubjectCol != -1)
                    {
                        model.Subject = sheet.Cells[i, SubjectCol].StringValue.Trim();
                    }
                    if (DateCol != -1)
                    {
                        model.SendDate = sheet.Cells[i, DateCol].StringValue.Trim();
                    }
                    if (ProductTypeCol != -1)
                    {
                        model.ProductType = sheet.Cells[i, ProductTypeCol].StringValue.Trim();
                    }
                    else if (!sheetName.ToLower().StartsWith("sheet"))
                    {
                        model.ProductType = sheetName;
                    }
                    else {
                        model.ProductType = "Other";
                    }
                    if (SourceCol != -1)
                    {
                        model.Source = sheet.Cells[i, SourceCol].StringValue.Trim();
                    }
                    mailModelList.Add(model);
                }
            }

            if (mailModelList.Count == 0)
            {
                errorMsg.Text = "此Excel中未包含任何邮件数据。请重新选择。";
                this.importBtn.Enabled = true;
                this.pictureBox1.Visible = false;
                return;
            }
            MailModelDAO mailModelDAO = DAOFactory.Instance.GetMailModelDAO();
            mailModelDAO.Insert(mailModelList);
            this.importBtn.Enabled = true;
            this.pictureBox1.Visible = false;
            this.Close();
        }
    }
}
