using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aspose.Network.Verify;

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
            else
            {
                splitContainer1.Panel1.Show();
                splitContainer1.SplitterDistance = 94;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Dao = DAOFactory.Instance.GetMailModelDAO();
            LoadDataview();
        }
        #endregion

        #region DataGridView 初始化处理
        void LoadDataview()
        {
            this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            SourceCombo.ValueMember = "Id";
            SourceCombo.DisplayMember = "Name";
            SourceCombo.Items.Insert(0, new ListItem("",""));
            List<ListItem> sourceList = Dao.GetComboBoxData("Source");
            SourceCombo.Items.AddRange(sourceList.ToArray());
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
            this.dataGridView1.DataSource = dt;

            DataGridViewColumn column = this.dataGridView1.Columns[0];
            column.HeaderText = " ";
            column.Width = 20;
            column.Frozen = true;
            DataGridViewColumn column0 = this.dataGridView1.Columns[1];
            column0.HeaderText = "Email";
            column0.Width =200;
            DataGridViewColumn column1 = this.dataGridView1.Columns[2];
            column1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column1.HeaderText = "Product";
            column1.Width = 120;
            DataGridViewColumn column2 = this.dataGridView1.Columns[3];
            column2.HeaderText = "Buyer Name";
            column2.Width = 100;
            DataGridViewColumn column3 = this.dataGridView1.Columns[4];
            column3.HeaderText = "Country";
            column3.Width = 150;
            DataGridViewColumn column4 = this.dataGridView1.Columns[5];
            column4.HeaderText = "Company";
            column4.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column4.Width = 180;
            DataGridViewColumn column5 = this.dataGridView1.Columns[6];
            column5.HeaderText = "Subject";
            column5.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column5.Width = 400;
            DataGridViewColumn column6 = this.dataGridView1.Columns[7];
            column6.HeaderText = "Source";
            column6.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column6.Width = 100;
            DataGridViewColumn column7 = this.dataGridView1.Columns[8];
            column7.HeaderText = "SendDate";
            column7.Width = 120;
            DataGridViewColumn column8 = this.dataGridView1.Columns[9];
            column8.HeaderText = "Verify";
            column8.Width = 100;
            DataGridViewColumn column9 = this.dataGridView1.Columns[10];
            column9.HeaderText = "Id";
            column9.Visible = false;
            List<MailModel> mailModelList = Dao.GetMailModelList(null);
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
                    if (item.Verify1 == 0)
                    {
                        row["Verify"] = "未验证";

                    }
                    else if (item.Verify1 == 1)
                    {

                        row["Verify"] = "验证不存在";

                    }
                    else if (item.Verify1 == 2)
                    {

                        row["Verify"] = "通过";
                    }
                    row["SendDate"] = item.SendDate;
                    row["Id"] = item.Id;
                    dt.Rows.Add(row);
                }
            }


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

        void f_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadDataview();
        }

        private void validationBtn_Click(object sender, EventArgs e)
        {
            validationBtn.Enabled = false;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerAsync();
            worker.Dispose();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<MailModel> mailModelList = Dao.GetMailModelList(null);
            EmailValidator emailValidator = new EmailValidator();
            ValidationResult result;
            foreach (MailModel model in mailModelList)
            {
                emailValidator.Validate(model.Email, out result);
                if (result.ReturnCode ==ValidationResponseCode.ValidationSuccess)
                {
                    model.Verify1 = 1;
                }
                else
                {
                    //lblResult.Text = "邮箱地址不存在，返回代码:\r\n " + result.ReturnCode + ".";
                    model.Verify1 = 2;
                }
                Dao.UpdateMailVerify(model);
            }
            validationBtn.Enabled = true;
            LoadDataview();
        }

 
    }
    
}
