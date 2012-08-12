using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace SooWebSiteTools
{
    [ComVisible(true)]
    public partial class BIPForm : Form
    {
        private ProducctDAO dao;
        private string SelectLang;
        private string[] fileNames = new string[] { };
        private string productDesc = string.Empty;
        private string productDesc2 = string.Empty;
        private List<string> pictureList = new List<string>();
        Dictionary<string, ProductModel> productDic = new Dictionary<string, ProductModel>();

        public BIPForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dao = new ProducctDAO();
            DataTable dt = dao.GetLanguages();
            LangComboBox.SelectedIndexChanged += new EventHandler(LangComboBoxChanged);
            LangComboBox.DisplayMember = "NAME";
            LangComboBox.ValueMember = "ID";
            LangComboBox.DataSource = dt;
            LangComboBox.SelectedIndex = 0;
            webBrowser1.Navigate(Application.StartupPath + "\\KindEditor\\Editor.htm");
            this.webBrowser1.ObjectForScripting = this;
            webBrowser2.Navigate(Application.StartupPath + "\\KindEditor\\Editor.htm");
            this.webBrowser2.ObjectForScripting = this;

            DataTable table = new DataTable();
            table.Columns.Add("Image", typeof(Image));
            table.Columns.Add("Model", typeof(string));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Price", typeof(string));
            this.dataGridView.DataSource = table;
            
            DataGridViewColumn column0 = this.dataGridView.Columns[0];
            column0.HeaderText = "Product Image";
            column0.Width = 100;
            DataGridViewColumn column = this.dataGridView.Columns[1];
            column.HeaderText = "Product Model";
            column.Width = 150;
            DataGridViewColumn column2 = this.dataGridView.Columns[2];
            column2.HeaderText = "Product Name";
            //column2.Width = 200;
            DataGridViewColumn column3 = this.dataGridView.Columns[3];
            column3.HeaderText = "Product Price";
            column3.Width = 200;
            
            
        }

        void LangComboBoxChanged(object sender, EventArgs e)
        {
            SelectLang =  Convert.ToString(LangComboBox.SelectedValue);
            this.CategoriesList.Items.Clear();
            this.CategoriesList.DataSource = dao.GetCategories(SelectLang);
            this.CategoriesList.DisplayMember = "name";
            this.CategoriesList.ValueMember = "id";
            this.MaComboBox.Items.Clear();
            this.MaComboBox.DataSource = dao.GetManufacturers();
            this.MaComboBox.DisplayMember = "name";
            this.MaComboBox.ValueMember = "id";
            this.MaComboBox.SelectedValue = "12";

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
        }

       
        private void OpenBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ofd.Filter = "图片文件(*.jpg)| *.jpg; *.jpeg";
            ofd.Multiselect = true;
            ofd.ShowDialog();
            if (ofd.FileNames.Length == 0) return;
            fileNames = ofd.FileNames;
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerAsync();
            bgWorker.Dispose();
        }

       
        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            MsgToolsLabel.Text = "正在导入产品图片，请稍候...";
            ImpBtn.Enabled = false;
            UpdBtn.Enabled = false;
            SubmitBtn.Enabled = false;
            pictureList.Clear();
            this.dataGridView.DataBindings.Clear();
            DataTable table = new DataTable();
            table.Columns.Add("Image", typeof(Image));
            table.Columns.Add("Model", typeof(string));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Price", typeof(string));
            foreach (string fileName in fileNames)
            {
                pictureList.Add(fileName);
                DataRow dr = table.NewRow();
                dr["Image"] = new Bitmap(Image.FromFile(fileName), 100, 100);
                dr["Model"] = Path.GetFileNameWithoutExtension(fileName);
                dr["Name"] = Path.GetFileName(fileName);
                dr["Price"] = "0.00";
                table.Rows.Add(dr);
            }
            this.dataGridView.DataSource = table;
            MsgToolsLabel.Text = "";
            ImpBtn.Enabled = true;
            UpdBtn.Enabled = true;
            SubmitBtn.Enabled = true;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            splitContainer.SplitterDistance = 705;
            splitContainer.Panel1.Show();
        }

        private void UpdBtn_Click(object sender, EventArgs e)
        {
            productDesc = string.Empty;
            productDesc2 = string.Empty;
            if (string.IsNullOrEmpty(NamesTextBox.Text.Trim()))
            {
                MessageBox.Show("没有任何产品名称，请输入一组产品名称。");
                return;
            }
            productDesc = this.webBrowser1.Document.InvokeScript("GetData").ToString();
            productDesc2 = this.webBrowser2.Document.InvokeScript("GetData").ToString();
            if (string.IsNullOrEmpty(productDesc))
            {
                MessageBox.Show("没有任何产品描述，请输入相关的产品描述。");
                return;
            }
            if (pictureList.Count == 0)
            {
                MessageBox.Show("没有任何产品图片和型号数据不能执行更新，请从图片导入产品数据。");
                return;
            }
            string[] Keywords = keywordTextBox.Text.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] KeyDescs = keywordDescTextBox.Text.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] ProNames = NamesTextBox.Text.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork +=new DoWorkEventHandler(UpdBtn_DoWork);
            bgWorker.RunWorkerAsync();
            bgWorker.Dispose();
        }

        void UpdBtn_DoWork(object sender, DoWorkEventArgs e)
        {
            MsgToolsLabel.Text = "正在更新批量产品数据，请稍候...";
            ImpBtn.Enabled = false;
            UpdBtn.Enabled = false;
            SubmitBtn.Enabled = false;
            productDic.Clear();
            List<CategoryModel> CategoryList = new List<CategoryModel>();
            foreach (DataRowView dataRow in CategoriesList.CheckedItems)
            {
                Int32 value = Convert.ToInt32(dataRow["id"]);
                CategoryModel cateModel = new CategoryModel();
                cateModel.CategoryId = value;
                CategoryList.Add(cateModel);
            }

            Int32 ManufacturerId = Convert.ToInt32(MaComboBox.SelectedValue);
            Int32 StockStatusId = Convert.ToInt32(StockComboBox.SelectedValue);
            Int32 WeightClassId = Convert.ToInt32(WeightComboBox.SelectedValue);
            Int32 LengthClassId = Convert.ToInt32(LengthComboBox.SelectedValue);
            Int32 Qty = Convert.ToInt32(QtyTextBox.Text.Trim());
            Int32 Moq = Convert.ToInt32(MOQTextBox.Text.Trim());
            string Unit = UnitTextBox.Text.Trim();
            string HsCode = HsCodeTextBox.Text.Trim();
            DateTime AvailableDate = AvailableBox.Value;
            ProductModel productModel = null;
            int orderNum = 0;
            foreach (string path in pictureList)
            {
                if (path.IndexOf("data") == -1) continue;
                orderNum++;
                string imageName = Path.GetFileNameWithoutExtension(path);
                string model = imageName.Split(new char[]{'_','-'})[0];
                string image = path.Substring(path.IndexOf("data")).Replace("\\", "/");
                if (productDic.Keys.Contains(model))
                {
                    productModel = productDic[model];
                }
                else 
                {
                    productModel = new ProductModel();
                    productDic.Add(model, productModel);
                }
                ImageModel imageModel = new ImageModel();
                imageModel.Image = image;
                productModel.Images.Add(imageModel);
                productModel.Image = image;
                productModel.LanguageId = Convert.ToInt32(SelectLang);
                productModel.Statust = 1;
                productModel.SortOrder = orderNum;
                productModel.ManufacturerId = ManufacturerId;
                productModel.StockStatusId = StockStatusId;
                productModel.WeightClassId = WeightClassId;
                productModel.LengthClassId = LengthClassId;
                productModel.Quantity = Qty;
                productModel.Minimum = Moq;
                productModel.Sku = Unit;
                productModel.Upc = HsCode;
                productModel.AvailableDate = AvailableDate;
                productModel.Description = productDesc;
                productModel.Description2 = productDesc2;
            }

            MsgToolsLabel.Text = "";
            ImpBtn.Enabled = true;
            UpdBtn.Enabled = true;
            SubmitBtn.Enabled = true;
        }

    }
}
