using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soomes;
using System.Web;
using System.Net;
using System.IO;

namespace AliHelper
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class ProductView : UserControl
    {
        private ProductsManager productsManager;
        private ImpProductDetail impProductDetail;
        private DataTable dataTable;
        private int PrevSelectedId = 0;
        [DefaultValue(1), Category("自定义属性"), Description("产品详情")]
        public ProductDetail AliProductDetail { set; get; }
        
        public ProductView()
        {
            InitializeComponent();
            productsManager = new ProductsManager();
            impProductDetail = new ImpProductDetail();
            this.webBrowser1.ObjectForScripting = this;
            this.webBrowser1.Navigate(Application.StartupPath + "\\KindEditor\\Editor.htm");
            InitDataGridview();
        }

        #region Load DataGridView

        public void InitDataGridview()
        {
            this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(Int32));
            dataTable.Columns.Add("Check", typeof(Boolean));
            dataTable.Columns.Add("Image", typeof(Image));
            dataTable.Columns.Add("Subject", typeof(string));
            dataTable.Columns.Add("RedModel", typeof(string));
            dataTable.Columns.Add("IsWindowProduct", typeof(string));
            dataTable.Columns.Add("Status", typeof(string));
            dataTable.Columns.Add("OwnerMemberName", typeof(string));
            dataTable.Columns.Add("GmtModified", typeof(string));
            
            this.dataGridView1.DataSource = dataTable;
            DataGridViewColumn column7 = this.dataGridView1.Columns[0];
            column7.HeaderText = "Id";
            column7.Width = 10;
            column7.Visible = false;
            DataGridViewColumn column0 = this.dataGridView1.Columns[1];
            column0.HeaderText = "选中";
            column0.Width = 80;
            DataGridViewColumn column = this.dataGridView1.Columns[2];
            column.HeaderText = "产品图片";
            column.Width = 120;

            DataGridViewColumn column1 = this.dataGridView1.Columns[3];
            column1.HeaderText = "产品名称";
            column1.Width = 360;

            DataGridViewColumn column11 = this.dataGridView1.Columns[4];
            column11.HeaderText = "型号";
            column11.Width = 120;

            DataGridViewColumn column2 = this.dataGridView1.Columns[5];
            column2.HeaderText = "橱窗产品";
            column2.Width = 80;

            DataGridViewColumn column3 = this.dataGridView1.Columns[6];
            column3.HeaderText = "产品状态";
            column3.Width = 80;

            DataGridViewColumn column4 = this.dataGridView1.Columns[7];
            column4.HeaderText = "所属成员";
            column4.Width = 100;

            DataGridViewColumn column6 = this.dataGridView1.Columns[8];
            column6.HeaderText = "更新时间";
            column6.Width = 100;
           
        }

        public void LoadDataGridView(int GroupId)
        {
            this.dataTable.Clear();
            List<AliProduct> productList = productsManager.GetProductList(GroupId);
            if (productList.Count > 0)
            {
                foreach (AliProduct item in productList)
                {
                    DataRow row = this.dataTable.NewRow();
                    row["Id"] = item.Id;
                    row["Check"] = false;
                    string imageFile = FileUtils.GetProductImagesFolder() + Path.DirectorySeparatorChar + item.Id + ".jpg";
                    if (File.Exists(imageFile))
                    {
                        row["Image"] = Image.FromFile(imageFile, true);
                    }else{
                        row["Image"] = global::AliHelper.Properties.Resources.no_image;
                    }
                    row["Subject"] = item.Subject;
                    row["RedModel"] = item.RedModel;
                    row["IsWindowProduct"] = item.IsWindowProduct?"是":"";
                    row["Status"] = item.Status;
                    row["OwnerMemberName"] = item.OwnerMemberName;
                    row["GmtModified"] = item.GmtModified;
                    this.dataTable.Rows.Add(row);
                }
            }
        }
        #endregion

        #region RadioBox ChangeEvent
        private void staticImageWaterMarkId_CheckedChanged(object sender, EventArgs e)
        {
            this.staticImageWaterMarkIdGroup.Visible = 
                this.staticImageWaterMarkId.Enabled && this.staticImageWaterMarkId.Checked;
        }

        private void dynamicImageWaterMarkId_CheckedChanged(object sender, EventArgs e)
        {
            this.dynamicImageWaterMarkIdGroup.Visible = 
                this.dynamicImageWaterMarkId.Enabled && this.dynamicImageWaterMarkId.Checked;
        }
        private void static_and_dyn0_CheckedChanged(object sender, EventArgs e)
        {
            this.staticImagePanel.Visible = static_and_dyn0.Checked;
            this.dynamicImagePanel.Visible = !static_and_dyn0.Checked;
        }
        #endregion

        private void ProductView_Load(object sender, EventArgs e)
        {
            if (DataCache.Instance.MinOrderUnitOptions != null)
            {
                this.minOrderUnit.DisplayMember = "Name";
                this.minOrderUnit.ValueMember = "Value";
                FormElement selected = DataCache.Instance.MinOrderUnitOptions[0];
                foreach (FormElement el in DataCache.Instance.MinOrderUnitOptions)
                {
                    this.minOrderUnit.Items.Add(el);
                }
                this.minOrderUnit.SelectedItem = selected;
            }
            if (DataCache.Instance.MoneyTypeOptions != null)
            {
                this.moneyType.DisplayMember = "Name";
                this.moneyType.ValueMember = "Value";
                FormElement selected = DataCache.Instance.MoneyTypeOptions[0];
                foreach (FormElement el in DataCache.Instance.MoneyTypeOptions)
                {
                    this.moneyType.Items.Add(el);
                }
                this.moneyType.SelectedItem = selected;
            }
            if (DataCache.Instance.PriceUnitOptions != null)
            {
                this.priceUnit.DisplayMember = "Name";
                this.priceUnit.ValueMember = "Value";
                FormElement selected = DataCache.Instance.PriceUnitOptions[0];
                foreach (FormElement el in DataCache.Instance.PriceUnitOptions)
                {
                    this.priceUnit.Items.Add(el);
                }
                this.priceUnit.SelectedItem = selected;
            }
            if (DataCache.Instance.SupplyUnitOptions != null)
            {
                this.supplyUnit.DisplayMember = "Name";
                this.supplyUnit.ValueMember = "Value";
                FormElement selected = DataCache.Instance.SupplyUnitOptions[0];
                foreach (FormElement el in DataCache.Instance.SupplyUnitOptions)
                {
                    this.supplyUnit.Items.Add(el);
                }
                this.supplyUnit.SelectedItem = selected;
            }
            if (DataCache.Instance.SupplyPeriodOptions != null)
            {
                this.supplyPeriod.DisplayMember = "Name";
                this.supplyPeriod.ValueMember = "Value";
                FormElement selected = DataCache.Instance.SupplyPeriodOptions[0];
                foreach (FormElement el in DataCache.Instance.SupplyPeriodOptions)
                {
                    this.supplyPeriod.Items.Add(el);
                }
                this.supplyPeriod.SelectedItem = selected;
            }
            if (DataCache.Instance.GroupListOptions != null)
            {
                this.productTeamInputBox.DisplayMember = "Name";
                this.productTeamInputBox.ValueMember = "Value";
                FormElement selected = DataCache.Instance.GroupListOptions[0];
                foreach (FormElement el in DataCache.Instance.GroupListOptions)
                {
                    this.productTeamInputBox.Items.Add(el);
                }
                this.productTeamInputBox.SelectedItem = selected;
            }
            this.staticImagePanel.Visible = true;
            this.dynamicImagePanel.Visible = false;
        }
        
        public void LoadProductDetailValue()
        {
            if (AliProductDetail.productName != null)
            {
                this.productName.Tag = AliProductDetail.productName;
                this.productName.Text = AliProductDetail.productName.Value;
            }
            if (AliProductDetail.productKeyword != null)
            {
                this.productKeyword.Tag = AliProductDetail.productKeyword;
                this.productKeyword.Text = AliProductDetail.productKeyword.Value;
            }
            if (AliProductDetail.keywords2 != null)
            {
                this.keywords2.Tag = AliProductDetail.keywords2;
                this.keywords2.Text = AliProductDetail.keywords2.Value;
            }
            if (AliProductDetail.keywords3 != null)
            {
                this.keywords3.Tag = AliProductDetail.keywords3;
                this.keywords3.Text = AliProductDetail.keywords3.Value;
            }
            if (AliProductDetail.summary != null)
            {
                this.summary.Tag = AliProductDetail.summary;
                this.summary.Text = AliProductDetail.summary.Value;
            }
            if (AliProductDetail.productTeamInputBox != null)
            {
                this.productTeamInputBox.Tag = AliProductDetail.productTeamInputBox;
                string groupIds = AliProductDetail.productGroupId1.Value + "_"
                    + AliProductDetail.productGroupId2.Value + "_" + AliProductDetail.productGroupId3.Value;
                FormElement selected = DataCache.Instance.GroupListOptions[0];
                foreach (FormElement el in DataCache.Instance.GroupListOptions)
                {
                    if (el.Value == groupIds)
                    {
                        selected = el;
                        break;
                    }
                }
                this.productTeamInputBox.SelectedItem = selected;
            }
            if (AliProductDetail.productDescriptionTemp != null)
            {
                this.webBrowser1.Tag = AliProductDetail.productDescriptionTemp;
                string content = AliProductDetail.productDescriptionTemp.Value;
                this.webBrowser1.Document.InvokeScript("SetData", new object[] { HttpUtility.HtmlDecode(content) });
            }

            if (AliProductDetail.static_and_dyn0 != null)
            {
                this.static_and_dyn0.Tag = AliProductDetail.static_and_dyn0;
                this.static_and_dyn0.Checked = AliProductDetail.static_and_dyn0.Checked;
            }
            if (AliProductDetail.static_and_dyn1 != null)
            {
                this.static_and_dyn1.Tag = AliProductDetail.static_and_dyn1;
                this.static_and_dyn1.Checked = AliProductDetail.static_and_dyn1.Checked;
            }
            static_and_dyn0_CheckedChanged(null, null);

            //static image
            if (AliProductDetail.staticImageWaterMarkId != null)
            {
                this.staticImageWaterMarkId.Tag = AliProductDetail.staticImageWaterMarkId;
                this.staticImageWaterMarkId.Checked = AliProductDetail.staticImageWaterMarkId.Checked;
            }
            this.staticImageWaterMarkId.Enabled = true;
            if (AliProductDetail.pageType != null && AliProductDetail.pageType.Value == Constants.PageType_Edit)
            {
                this.staticImageWaterMarkId.Enabled = false;
            }
            this.staticImageWaterMarkId_CheckedChanged(null, null);
            if (AliProductDetail.fmppr0stati_y != null)
            {
                this.fmppr0stati_y.Tag = AliProductDetail.fmppr0stati_y;
                this.fmppr0stati_y.Checked = AliProductDetail.fmppr0stati_y.Checked;
            }
            if (AliProductDetail.fmppr0stati_n != null)
            {
                this.fmppr0stati_n.Tag = AliProductDetail.fmppr0stati_n;
                this.fmppr0stati_n.Checked = AliProductDetail.fmppr0stati_n.Checked;
            }
            if (AliProductDetail.fmppr0static_center != null)
            {
                this.fmppr0static_center.Tag = AliProductDetail.fmppr0static_center;
                this.fmppr0static_center.Checked = AliProductDetail.fmppr0static_center.Checked;
            }
            if (AliProductDetail.fmppr0static_down != null)
            {
                this.fmppr0static_down.Tag = AliProductDetail.fmppr0static_down;
                this.fmppr0static_down.Checked = AliProductDetail.fmppr0static_down.Checked;
            }

            //dynamic images
            if (AliProductDetail.dynamicImageWaterMarkId != null)
            {
                this.dynamicImageWaterMarkId.Tag = AliProductDetail.dynamicImageWaterMarkId;
                this.dynamicImageWaterMarkId.Checked = AliProductDetail.dynamicImageWaterMarkId.Checked;
            }
            this.dynamicImageWaterMarkId.Enabled = true;
            if (AliProductDetail.pageType != null && AliProductDetail.pageType.Value == Constants.PageType_Edit)
            {
                this.dynamicImageWaterMarkId.Enabled = false;
            }
            this.dynamicImageWaterMarkId_CheckedChanged(null, null);
            if (AliProductDetail.fmppr0dyn_y != null)
            {
                this.fmppr0dyn_y.Tag = AliProductDetail.fmppr0dyn_y;
                this.fmppr0dyn_y.Checked = AliProductDetail.fmppr0dyn_y.Checked;
            }
            if (AliProductDetail.fmppr0dyn_n != null)
            {
                this.fmppr0dyn_n.Tag = AliProductDetail.fmppr0dyn_n;
                this.fmppr0dyn_n.Checked = AliProductDetail.fmppr0dyn_n.Checked;
            }
            if (AliProductDetail.fmppr0dyna_center != null)
            {
                this.fmppr0dyna_center.Tag = AliProductDetail.fmppr0dyna_center;
                this.fmppr0dyna_center.Checked = AliProductDetail.fmppr0dyna_center.Checked;
            }
            if (AliProductDetail.fmppr0dyna_down != null)
            {
                this.fmppr0dyna_down.Tag = AliProductDetail.fmppr0dyna_down;
                this.fmppr0dyna_down.Checked = AliProductDetail.fmppr0dyna_down.Checked;
            }

            if (AliProductDetail.CustomAttr != null)
            {
                int i = 0;
                foreach (FormElement el in AliProductDetail.CustomAttr.Keys)
                {
                    i++;
                    Control attrKey = Controls.Find("customAttrKey" + i, true)[0];
                    if (attrKey != null)
                    {
                        attrKey.Tag = el;
                        attrKey.Text = el.Value;
                    }
                    Control attrVal = Controls.Find("customAttrVal" + i, true)[0];
                    if (attrVal != null)
                    {
                        attrVal.Tag = AliProductDetail.CustomAttr[el];
                        attrVal.Text = AliProductDetail.CustomAttr[el].Value;
                    }
                }
            }

            if (AliProductDetail.minOrderQuantity != null)
            {
                this.minOrderQuantity.Tag = AliProductDetail.minOrderQuantity;
                this.minOrderQuantity.Text = AliProductDetail.minOrderQuantity.Value;
            }
            
            if (AliProductDetail.priceRangeMin != null)
            {
                this.priceRangeMin.Tag = AliProductDetail.priceRangeMin;
                this.priceRangeMin.Text = AliProductDetail.priceRangeMin.Value;
            }
            if (AliProductDetail.priceRangeMax != null)
            {
                this.priceRangeMax.Tag = AliProductDetail.priceRangeMax;
                this.priceRangeMax.Text = AliProductDetail.priceRangeMax.Value;
            }
            
            if (AliProductDetail.port != null)
            {
                this.port.Tag = AliProductDetail.port;
                this.port.Text = AliProductDetail.port.Value;
            }
            if (AliProductDetail.paymentMethod1 != null)
            {
                this.paymentMethod1.Tag = AliProductDetail.paymentMethod1;
                this.paymentMethod1.Checked = AliProductDetail.paymentMethod1.Checked;
            }
            if (AliProductDetail.paymentMethod2 != null)
            {
                this.paymentMethod2.Tag = AliProductDetail.paymentMethod2;
                this.paymentMethod2.Checked = AliProductDetail.paymentMethod2.Checked;
            }
            if (AliProductDetail.paymentMethod3 != null)
            {
                this.paymentMethod3.Tag = AliProductDetail.paymentMethod3;
                this.paymentMethod3.Checked = AliProductDetail.paymentMethod3.Checked;
            }
            if (AliProductDetail.paymentMethod4 != null)
            {
                this.paymentMethod4.Tag = AliProductDetail.paymentMethod4;
                this.paymentMethod4.Checked = AliProductDetail.paymentMethod4.Checked;
            }
            if (AliProductDetail.paymentMethod5 != null)
            {
                this.paymentMethod5.Tag = AliProductDetail.paymentMethod5;
                this.paymentMethod5.Checked = AliProductDetail.paymentMethod5.Checked;
            }
            if (AliProductDetail.paymentMethod6 != null)
            {
                this.paymentMethod6.Tag = AliProductDetail.paymentMethod6;
                this.paymentMethod6.Checked = AliProductDetail.paymentMethod6.Checked;
            }
            if (AliProductDetail.paymentMethodOther != null)
            {
                this.paymentMethodOther.Tag = AliProductDetail.paymentMethodOther;
                this.paymentMethodOther.Checked = AliProductDetail.paymentMethodOther.Checked;
            }
            if (AliProductDetail.paymentMethodOtherDesc != null)
            {
                this.paymentMethodOtherDesc.Tag = AliProductDetail.paymentMethodOtherDesc;
                this.paymentMethodOtherDesc.Text = AliProductDetail.paymentMethodOtherDesc.Value;
            }
            if (AliProductDetail.supplyQuantity != null)
            {
                this.supplyQuantity.Tag = AliProductDetail.supplyQuantity;
                this.supplyQuantity.Text = AliProductDetail.supplyQuantity.Value;
            }
            if (AliProductDetail.minOrderQuantity != null)
            {
                this.minOrderQuantity.Tag = AliProductDetail.minOrderQuantity;
                this.minOrderQuantity.Text = AliProductDetail.minOrderQuantity.Value;
            }
            if (AliProductDetail.minOrderQuantity != null)
            {
                this.minOrderQuantity.Tag = AliProductDetail.minOrderQuantity;
                this.minOrderQuantity.Text = AliProductDetail.minOrderQuantity.Value;
            }
            if (AliProductDetail.consignmentTerm != null)
            {
                this.consignmentTerm.Tag = AliProductDetail.consignmentTerm;
                this.consignmentTerm.Text = AliProductDetail.consignmentTerm.Value;
            }
            if (AliProductDetail.packagingDesc != null)
            {
                this.packagingDesc.Tag = AliProductDetail.packagingDesc;
                this.packagingDesc.Text = AliProductDetail.packagingDesc.Value;
            }

            if (AliProductDetail.minOrderUnit != null)
            {
                this.minOrderUnit.Tag = AliProductDetail.minOrderUnit;
                FormElement selected = DataCache.Instance.MinOrderUnitOptions[0];
                foreach (FormElement el in this.minOrderUnit.Items)
                {
                    if (el.Value == AliProductDetail.minOrderUnit.Value)
                    {
                        selected = el;
                        break;
                    }
                }
                this.minOrderUnit.SelectedItem = selected;
            }
            if (AliProductDetail.moneyType != null)
            {
                this.moneyType.Tag = AliProductDetail.moneyType;
                FormElement selected = DataCache.Instance.MoneyTypeOptions[0];
                foreach (FormElement el in DataCache.Instance.MoneyTypeOptions)
                {
                    if (el.Value == AliProductDetail.moneyType.Value)
                    {
                        selected = el;
                        break;
                    }
                }
                this.moneyType.SelectedItem = selected;
            }
            if (AliProductDetail.priceUnit != null)
            {
                this.priceUnit.Tag = AliProductDetail.priceUnit;
                FormElement selected = DataCache.Instance.PriceUnitOptions[0];
                foreach (FormElement el in this.priceUnit.Items)
                {
                    if (el.Value == AliProductDetail.priceUnit.Value)
                    {
                        selected = el;
                        break;
                    }
                }
                this.priceUnit.SelectedItem = selected;
            }
            if (AliProductDetail.supplyUnit != null)
            {
                this.supplyUnit.Tag = AliProductDetail.supplyUnit;
                FormElement selected = DataCache.Instance.SupplyUnitOptions[0];
                foreach (FormElement el in this.supplyUnit.Items)
                {
                    if (el.Value == AliProductDetail.supplyUnit.Value)
                    {
                        selected = el;
                        break;
                    }
                }
                this.supplyUnit.SelectedItem = selected;
            }
            if (AliProductDetail.supplyPeriod != null)
            {
                this.supplyPeriod.Tag = AliProductDetail.supplyPeriod;
                FormElement selected = DataCache.Instance.SupplyPeriodOptions[0];
                foreach (FormElement el in this.supplyPeriod.Items)
                {
                    if (el.Value == AliProductDetail.supplyPeriod.Value)
                    {
                        selected = el;
                        break;
                    }
                }
                this.supplyPeriod.SelectedItem = selected;
            }

            // 
            // SysAttrPanel
            // 
            if (this.SysAttrPanel != null)
            {
                if (this.SysAttrPanel.Controls.Count > 0)
                {
                    foreach (Control c in this.SysAttrPanel.Controls)
                    {
                        this.SysAttrPanel.Controls.Remove(c);
                    }
                }
                this.AttrTab.Controls.Remove(this.SysAttrPanel);
                this.SysAttrPanel.Dispose();
                this.SysAttrPanel = null;
            }
            this.SysAttrPanel = new System.Windows.Forms.Panel();
            this.SysAttrPanel.Location = new System.Drawing.Point(0, 0);
            this.SysAttrPanel.Name = "SysAttrPanel";
            this.SysAttrPanel.Size = new System.Drawing.Size(550, 342);
            this.AttrTab.Controls.Add(this.SysAttrPanel);

            int height = 20;
            int tabIndex = 1;
            foreach (AttributeNode attr in AliProductDetail.SysAttr)
            {
                LoadSystemAttributes(attr, ref height, ref tabIndex);
                height = height + 30;
                tabIndex = tabIndex + 1;
            }

        }

        public void LoadProductIamges()
        {
            List<ImageJson> imageJsons = JsonConvert.FromJson<List<ImageJson>>(AliProductDetail.imageFiles.Value);
            if (imageJsons == null || imageJsons.Count == 0)
            {
                return;
            }
            for (int i = 1; i <= 6; i++ )
            {
                PictureBox picBox = (PictureBox) this.dynamicImagePanel.Controls.Find("dnImage"+i, false)[0];
                picBox.Image = global::AliHelper.Properties.Resources.no_image;
            }
            this.staticImage.Image = global::AliHelper.Properties.Resources.no_image;
            WebClient webClient = new WebClient();
            int j = 1;
            foreach (ImageJson image in imageJsons)
            {
                if (this.AliProductDetail.static_and_dyn0.Checked)
                {
                    image.localImg = FileUtils.DownloadProductImage(webClient, image.fileURL, AliProductDetail.pid);
                    this.staticImage.Image = Image.FromFile(image.localImg, true);
                }
                else
                {
                    image.localImg = FileUtils.DownloadProductImage(webClient, image.fileURL, AliProductDetail.pid, image.fileSrcOrder);
                    PictureBox picBox = (PictureBox)this.dynamicImagePanel.Controls.Find("dnImage" + j, false)[0];
                    picBox.Image = Image.FromFile(image.localImg, true);
                    j++;
                }
            }
            webClient.Dispose();
            webClient = null;
        }

        private void LoadSystemAttributes(AttributeNode attrNode, ref int height, ref int tabIndex)
        {
            Label label = new System.Windows.Forms.Label();
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(20, height);
            label.Name = attrNode.Data.Id+"-label";
            label.Size = new System.Drawing.Size(65, 12);
            label.TabIndex = 22;
            label.Text = attrNode.Data.Value;
            this.SysAttrPanel.Controls.Add(label);
            if (attrNode.Data.ShowType == ShowType.InputString)
            {
                TextBox textBox = new TextBox();
                textBox.Location = new System.Drawing.Point(100, height-5);
                textBox.Name = attrNode.Data.Id;
                textBox.Size = new System.Drawing.Size(200, 20);
                textBox.Tag = attrNode.Data;
                textBox.TabIndex = tabIndex;
                if (attrNode.Nodes != null && attrNode.Nodes.Count > 0)
                {
                    textBox.Text = attrNode.Nodes[0].Data.Value;
                }
                this.SysAttrPanel.Controls.Add(textBox);
            }
            else if (attrNode.Data.ShowType == ShowType.ListBox)
            {
                int loc = 100;
                ComboBox comboBox = new ComboBox();
                comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                comboBox.FormattingEnabled = true;
                comboBox.Location = new System.Drawing.Point(loc, height - 5);
                comboBox.Name = attrNode.Data.Id;
                comboBox.Tag = attrNode.Data;
                comboBox.Size = new System.Drawing.Size(150, 22);
                loc = loc + 150 + 10;
                comboBox.TabIndex = tabIndex;
                this.SysAttrPanel.Controls.Add(comboBox);
                comboBox.DisplayMember = "Value";
                comboBox.ValueMember = "Id";
                Soomes.Attribute selNode = null;
                foreach (AttributeNode attr in attrNode.Nodes)
                {
                    comboBox.Items.Add(attr.Data);
                    if (attr.Data.Selected)
                    {
                        selNode = attr.Data;
                    }
                    if (attr.Nodes != null && attr.Nodes.Count > 0)
                    {
                        TextBox textBox = new TextBox();
                        textBox.Location = new System.Drawing.Point(loc, height - 5);
                        textBox.Name = attr.Nodes[0].Data.Id;
                        textBox.Size = new System.Drawing.Size(100, 20);
                        loc = loc + 100 + 10;
                        textBox.Tag = attr.Nodes[0].Data;
                        textBox.TabIndex = tabIndex;
                        if (attr.Nodes[0].Nodes.Count > 0)
                        {
                            textBox.Text = attr.Nodes[0].Nodes[0].Data.Value;
                        }
                        this.SysAttrPanel.Controls.Add(textBox);
                    }
                }
                comboBox.SelectedItem = selNode;
            }
            else if (attrNode.Data.ShowType == ShowType.CheckBox)
            {
                int loc = 100;
                int controlCount = 0;
                foreach (AttributeNode attr in attrNode.Nodes)
                {
                    if (controlCount > 5) { controlCount = 0; loc = 100; height = height + 30; }
                    CheckBox checkBox = new CheckBox();
                    checkBox.Location = new System.Drawing.Point(loc, height - 5);
                    checkBox.Name = attr.Data.Id;
                    checkBox.Tag = attr.Data;
                    checkBox.Text = attr.Data.Value;
                    checkBox.Checked = attr.Data.Selected;
                    checkBox.Size = new System.Drawing.Size(70, 22);
                    checkBox.TabIndex = tabIndex;
                    this.SysAttrPanel.Controls.Add(checkBox);
                    loc = loc + 70;
                    if (attr.Nodes != null && attr.Nodes.Count > 0)
                    {
                        TextBox textBox = new TextBox();
                        textBox.Location = new System.Drawing.Point(loc, height - 5);
                        textBox.Name = attr.Nodes[0].Data.Id;
                        textBox.Size = new System.Drawing.Size(100, 20);
                        textBox.Tag = attr.Nodes[0].Data;
                        textBox.TabIndex = tabIndex;
                        if (attr.Nodes[0].Nodes.Count > 0)
                        {
                            textBox.Text = attr.Nodes[0].Nodes[0].Data.Value;
                        }
                        this.SysAttrPanel.Controls.Add(textBox);
                    }
                    controlCount++;
                   
                }
            }

            else if (attrNode.Data.ShowType == ShowType.Country)
            {
                int loc = 100;
                ComboBox comboBox = new ComboBox();
                comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                comboBox.FormattingEnabled = true;
                comboBox.Location = new System.Drawing.Point(loc, height - 5);
                comboBox.Name = attrNode.Data.Id;
                comboBox.Tag = attrNode.Data;
                comboBox.Size = new System.Drawing.Size(150, 22);
                loc = loc + 150 + 10;
                comboBox.TabIndex = tabIndex;
                this.SysAttrPanel.Controls.Add(comboBox);
                comboBox.DisplayMember = "Value";
                comboBox.ValueMember = "Id";
                Soomes.Attribute selNode = null;
                if (attrNode.Nodes != null)
                {
                    foreach (AttributeNode attr in attrNode.Nodes)
                    {
                        comboBox.Items.Add(attr.Data);
                        if (attr.Data.Selected)
                        {
                            selNode = attr.Data;
                        }
                        if (attr.Nodes[0].Nodes != null && attr.Nodes[0].Nodes.Count > 0)
                        {
                            ComboBox subComboBox = new ComboBox();
                            subComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                            subComboBox.FormattingEnabled = true;
                            subComboBox.Location = new System.Drawing.Point(loc, height - 5);
                            subComboBox.Name = attr.Nodes[0].Data.Id;
                            subComboBox.Size = new System.Drawing.Size(150, 20);
                            loc = loc + 150 + 10;
                            subComboBox.Tag = attr.Nodes[0].Data;
                            subComboBox.TabIndex = tabIndex;
                            this.SysAttrPanel.Controls.Add(subComboBox);
                            subComboBox.DisplayMember = "Value";
                            subComboBox.ValueMember = "Id";
                            Soomes.Attribute subSelNode = null;
                            foreach (AttributeNode subAttr in attr.Nodes[0].Nodes)
                            {
                                subComboBox.Items.Add(subAttr.Data);
                                if (subAttr.Data.Selected)
                                {
                                    subSelNode = subAttr.Data;
                                }
                            }
                            subComboBox.SelectedItem = subSelNode;
                        }
                    }
                }
                comboBox.SelectedItem = selNode;
            }
        }

        void ComputeLink_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "请选择要上传的图片";
            open.Filter = "图片文件(*.jpg,*.bmp,*.png,*.gif)|*.jpg;*.bmp;*.png;*.gif";
            open.Multiselect = true;
            if (open.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in open.FileNames)
                {
                    //lboxPicPath.Items.Add(file);
                }
            }
        }

        private void ImageBankLink_LinkClicked(object sender, EventArgs e)
        {
            ImageForm f = new ImageForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        private void ImageBankLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                if (id != PrevSelectedId)
                {
                    PrevSelectedId = id;
                    ProductDetail detail = productsManager.GetProductDetail(id);
                    if (detail == null)
                    {
                        AliProduct product = productsManager.GetAliProduct(id);
                        detail = impProductDetail.GetEditFormElements(product);
                        
                        productsManager.InsertOrUpdateProdcutDetail(detail);
                    }
                    this.AliProductDetail = detail;
                    if (detail != null)
                    {
                        this.LoadProductIamges();
                        this.LoadProductDetailValue();
                    }
                }
            }
        }

        public void ShowPhotobank()
        {
            ImageForm f = new ImageForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        public void GetAllControlValue(Control control)
        {
            ControlCollection controls = control.Controls;
            foreach (Control c in controls)
            {
                if (c.Controls.Count > 0)
                {
                    GetAllControlValue(c);
                }
                if (c.Tag == null)
                {
                    continue;
                }
                string controlName = c.Name;
                string controlValue = c.Text;
                bool controlCheck = false;
                string controlType = c.GetType().Name;
                if (controlType == "RadioButton")
                {
                    controlCheck = ((RadioButton)c).Checked;
                }
                else if (controlType == "CheckBox")
                {
                    controlCheck = ((CheckBox)c).Checked;
                }
                else if (controlType == "ComboBox")
                {
                    controlValue = (string)((ComboBox)c).SelectedValue;
                }
                else if (controlType == "WebBrowser")
                {
                    controlValue = (string)((WebBrowser)c).Document.InvokeScript("GetData", null);
                }
                else if (controlType == "TextBox")
                {
                    controlValue = c.Text;
                }
                if (c.Tag.GetType().Name == "FormElement")
                {
                    FormElement el = (FormElement)c.Tag;
                }
                else if (c.Tag.GetType().Name == "AttributeNode")
                {
                    AttributeNode el = (AttributeNode)c.Tag;
                }
                System.Diagnostics.Trace.WriteLine("controlType: " + controlType + "  controlName: " + controlName + "  value: " + controlValue);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetAllControlValue(this.splitContainer1.Panel2);
        }
        
    }
}
                    