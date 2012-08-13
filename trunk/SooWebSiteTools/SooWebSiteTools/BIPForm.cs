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

namespace WebTools
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
            this.dataGridView.DataSource = table;
            
            DataGridViewColumn column0 = this.dataGridView.Columns[0];
            column0.HeaderText = "Product Image";
            column0.Width = 100;
            DataGridViewColumn column = this.dataGridView.Columns[1];
            column.HeaderText = "Product Model";
            column.Width = 200;
            
            
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
            ofd.Dispose();
            SetGVDel del = new SetGVDel(UpdateDataGridView);
            this.BeginInvoke(del);
        }
        private delegate void SetGVDel();
        void UpdateDataGridView()
        {
            MsgToolsLabel.Text = "正在导入产品图片，请稍候...";
            UpdBtn.Enabled = false;
            SubmitBtn.Enabled = false;
            pictureList.Clear();
            this.dataGridView.DataBindings.Clear();
            DataTable table = new DataTable();
            table.Columns.Add("Image", typeof(Image));
            table.Columns.Add("Model", typeof(string));
            this.dataGridView.DataSource = table;
            foreach (string fileName in fileNames)
            {
                pictureList.Add(fileName);
                DataRow dr = table.NewRow();
                dr["Image"] = new Bitmap(Image.FromFile(fileName), 100, 100);
                dr["Model"] = Path.GetFileNameWithoutExtension(fileName);
                table.Rows.Add(dr);
            }
            
            MsgToolsLabel.Text = "";
            UpdBtn.Enabled = true;
            SubmitBtn.Enabled = true;
        }



        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                splitContainer.SplitterDistance = 705;
                splitContainer.Panel1.Show();
            }
        }

        private void UpdBtn_Click(object sender, EventArgs e)
        {
            productDesc = string.Empty;
            productDesc2 = string.Empty;
            if (CategoriesList.CheckedItems.Count ==0)
            {
                MessageBox.Show("没有任何产品种类被选中，请选择产品种类。");
                return;
            }

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
            Keywords = keywordTextBox.Text.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            Tags = TagTextBox.Text.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            ProNames = NamesTextBox.Text.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            CurrentKeywordIndex = 0;
            CurrentProNameIndex = 0;
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork +=new DoWorkEventHandler(UpdBtn_DoWork);
            bgWorker.RunWorkerAsync();
            bgWorker.Dispose();
        }
        #region fetch Next Value for keyword, Product name
        private string[] Keywords = new string[0] { };
        private int CurrentKeywordIndex = 0;
        private string[] Tags = new string[0] { };
        private string[] ProNames = new string[0] { };
        private int CurrentProNameIndex = 0;

        private string GetNextKeywordValue()
        {
            if (Keywords == null || Keywords.Length == 0)
            {
                return "";
            }
            if (CurrentKeywordIndex >= Keywords.Length)
            {
                CurrentKeywordIndex = 0;
            }
            string val = Keywords[CurrentKeywordIndex];
            CurrentKeywordIndex++;
            return val;
        }
        private string GetNextProNameValue()
        {
            if (CurrentProNameIndex >= ProNames.Length)
            {
                CurrentProNameIndex = 0;
            }
            string val = ProNames[CurrentProNameIndex];
            CurrentProNameIndex++;
            return val;
        }
        #endregion

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

            List<TagModel> TagList = new List<TagModel>();
            foreach (string  tag in Tags)
            {
                TagModel tModel = new TagModel();
                tModel.Tag = tag;
                TagList.Add(tModel);
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
            int orderNum = dao.getMaxSortOrder(CategoryList);
            foreach (string path in pictureList)
            {
                if (path.IndexOf("data") == -1) continue;

                string model = Path.GetFileNameWithoutExtension(path);
                string image = path.Substring(path.IndexOf("data")).Replace("\\", "/");
                ImageModel imageModel = new ImageModel();
                imageModel.Image = image;

                bool IsSameModel = false;
                foreach (string key in productDic.Keys)
                {
                    if (key.StartsWith(model))
                    {
                        productModel = productDic[key];
                        productModel.Images.Add(imageModel);
                        productModel.Image = image;
                        productModel.Model = model;
                        productDic.Remove(key);
                        productDic.Add(model, productModel);
                        IsSameModel = true;
                        break;
                    }
                    if (model.StartsWith(key))
                    {
                        productModel = productDic[key];
                        productModel.Images.Add(imageModel);
                        IsSameModel = true;
                        break;
                    }
                }
                if (IsSameModel)
                {
                    continue;
                }
                productModel = new ProductModel();
                productModel.Images.Add(imageModel);
                productModel.Image = image;
                productModel.Model = model;
                productModel.LanguageId = Convert.ToInt32(SelectLang);
                productModel.Statust = 1;
                productModel.SortOrder = ++orderNum;
                productModel.ManufacturerId = ManufacturerId;
                productModel.StockStatusId = StockStatusId;
                productModel.WeightClassId = WeightClassId;
                productModel.LengthClassId = LengthClassId;
                productModel.Quantity = Qty;
                productModel.Minimum = Moq;
                productModel.Sku = Unit;
                productModel.Upc = HsCode;
                productModel.AvailableDate = AvailableDate;
                productModel.Name = GetNextProNameValue();
                productModel.MetaKeyword = GetNextKeywordValue();
                productModel.MetaDescription = productModel.Name + ", " 
                    + productModel.Model + ", " + productModel.MetaKeyword;
                productModel.Description = productDesc;
                productModel.Description2 = productDesc2;
                productModel.Categories.AddRange(CategoryList);
                productModel.Tags.AddRange(TagList);
                productDic.Add(model, productModel);
     
                //dao.InsertProducts(productDic.Values.ToList());
            }
            MsgToolsLabel.Text = "";
            ImpBtn.Enabled = true;
            UpdBtn.Enabled = true;
            SubmitBtn.Enabled = true;
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            MsgToolsLabel.Text = "正在提交数据，请稍候...";
            ImpBtn.Enabled = false;
            UpdBtn.Enabled = false;
            SubmitBtn.Enabled = false;
            try
            {
                dao.InsertProducts(productDic.Values.ToList());
                MsgToolsLabel.Text = "提交成功.";
            }
            catch (Exception ex)
            {
                MsgToolsLabel.Text = "提交失败，原因: "+ex.InnerException.Message;
            }
            
            ImpBtn.Enabled = true;
            UpdBtn.Enabled = true;
            SubmitBtn.Enabled = true;
        }

    }
}
