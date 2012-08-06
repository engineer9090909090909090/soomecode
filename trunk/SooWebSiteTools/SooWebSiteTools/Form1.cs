using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            dao = new ProducctDAO();
            DataTable dt = dao.GetLanguages();
            LangComboBox.SelectedIndexChanged += new EventHandler(LangComboBoxChanged);
            LangComboBox.DisplayMember = "NAME";
            LangComboBox.ValueMember = "ID";
            LangComboBox.DataSource = dt;
            LangComboBox.SelectedIndex = 0;
            
           
        }

        void LangComboBoxChanged(object sender, EventArgs e)
        {
            SelectLang =  Convert.ToString(LangComboBox.SelectedValue);
            DataTable dt = dao.GetProducts(SelectLang);
            dataGridView.DataSource = dt;
            this.checkedListBox.Items.Clear();
            this.checkedListBox.DataSource = dao.GetCategories(SelectLang);
            this.checkedListBox.DisplayMember = "name";
            this.checkedListBox.ValueMember = "id";
            this.maComboBox.Items.Clear();
            this.maComboBox.DataSource = dao.GetManufacturers();
            this.maComboBox.DisplayMember = "name";
            this.maComboBox.ValueMember = "id";
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


    }
}
