using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

            this.pictureBox1.Visible = true;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerAsync();
            worker.Dispose();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {

            this.pictureBox1.Visible = false;

        }
    }
}
