using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SooWebSiteTools
{
    public partial class Form1 : Form
    {
        private ProducctDAO dao;
        private string SelectLang;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dao = new ProducctDAO();
            DataTable dt = dao.GetLanguages();
            LangComboBox.SelectedIndexChanged += new EventHandler(LangComboBoxChanged);
            LangComboBox.DisplayMember = "NAME";
            LangComboBox.ValueMember = "ID";
            LangComboBox.DataSource = dt;
            LangComboBox.SelectedIndex = 0;
            webBrowser.Url = new System.Uri(Application.StartupPath + "\\KindEditor\\Editor.htm", System.UriKind.Absolute);
            webBrowser2.Url = new System.Uri(Application.StartupPath + "\\KindEditor\\Editor.htm", System.UriKind.Absolute);


            DataTable table = new DataTable();
            table.Columns.Add("Image", typeof(Image));
            table.Columns.Add("Model", typeof(string));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Price", typeof(string));
            this.dataGridView.DataSource = table;
            
            DataGridViewColumn column0 = this.dataGridView.Columns[0];
            column0.HeaderText = "Product Image";
            column0.Width = 120;
            DataGridViewColumn column = this.dataGridView.Columns[1];
            column.HeaderText = "Product Model";
            column.Width = 150;
            DataGridViewColumn column2 = this.dataGridView.Columns[2];
            column2.HeaderText = "Product Name";
            column2.Width = 200;
            DataGridViewColumn column3 = this.dataGridView.Columns[3];
            column3.HeaderText = "Product Price";
            column3.Width = 200;
            
            
        }

        void LangComboBoxChanged(object sender, EventArgs e)
        {
            SelectLang =  Convert.ToString(LangComboBox.SelectedValue);
            this.checkedListBox.Items.Clear();
            this.checkedListBox.DataSource = dao.GetCategories(SelectLang);
            this.checkedListBox.DisplayMember = "name";
            this.checkedListBox.ValueMember = "id";
            this.maComboBox.Items.Clear();
            this.maComboBox.DataSource = dao.GetManufacturers();
            this.maComboBox.DisplayMember = "name";
            this.maComboBox.ValueMember = "id";
            this.maComboBox.SelectedValue = "12";

            this.StockComboBox.Items.Clear();
            this.StockComboBox.DataSource = dao.GetStockStatus(SelectLang);
            this.StockComboBox.DisplayMember = "name";
            this.StockComboBox.ValueMember = "id";

            this.WeightComboBox.Items.Clear();
            this.WeightComboBox.DataSource = dao.GetWeightClass(SelectLang);
            this.WeightComboBox.DisplayMember = "name";
            this.WeightComboBox.ValueMember = "id";

            this.LengthComboBox.Items.Clear();
            this.LengthComboBox.DataSource = dao.GetLengthClass(SelectLang);
            this.LengthComboBox.DisplayMember = "name";
            this.LengthComboBox.ValueMember = "id";
            this.availableTextBox.Text = DateTime.Now.ToString("MM/dd/yyyy");
        }

        List<string> pictureList = new List<string>();
        private void OpenBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ofd.Filter = "图片文件(*.jpg)| *.jpg; *.jpeg";
            ofd.Multiselect = true;
            ofd.ShowDialog();
            pictureList.Clear();
            foreach(string fileName in ofd.FileNames)
            {
                pictureList.Add(fileName);
            }
            BackgroundWorker work = new BackgroundWorker();
            work.WorkerSupportsCancellation = true;
            work.DoWork += new DoWorkEventHandler(work_DoWork);
            work.RunWorkerAsync();
            work.Dispose();
        }

        void work_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Image", typeof(Image));
            table.Columns.Add("Model", typeof(string));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Price", typeof(string));

            foreach (string path in pictureList)
            {
                DataRow dr = table.NewRow();
                dr["Image"] = Image.FromFile(path);
                dr["Model"] = Path.GetFileNameWithoutExtension(path);
                dr["Name"] = Path.GetFileName(path);
                dr["Price"] = "0.00";
                table.Rows.Add(dr);
            }
            this.dataGridView.DataBindings.Clear();
            this.dataGridView.DataSource = table;
        }


    }
}
