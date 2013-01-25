using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soomes;

namespace AliHelper
{
    public partial class ProductView : UserControl
    {
        private ProductDetail _productDetail;
        [DefaultValue(1), Category("自定义属性"), Description("产品详情")]
        public ProductDetail AliProductDetail
        {
            get
            {
                return this._productDetail;
            }
            set
            {
                this._productDetail = value;
            }
        }

        
        public ProductView()
        {
            InitializeComponent();
            this.webBrowser1.Navigate(Application.StartupPath + "\\KindEditor\\Editor.htm");
        }

        private void staticImageWaterMarkId_CheckedChanged(object sender, EventArgs e)
        {
            if (this.staticImageWaterMarkId.Checked)
            {
                staticImageWaterMarkIdGroup.Visible = true;
            }
            else 
            {
                staticImageWaterMarkIdGroup.Visible = false;
            }
        }

        private void ProductView_Load(object sender, EventArgs e)
        {

        }

        public void LoadProductDetailValue()
        {
            if (AliProductDetail.productName != null)
            {
                this.productName.Tag = AliProductDetail.productName;
                this.productName.Text = AliProductDetail.productName.Val;
            }
            if (AliProductDetail.productKeyword != null)
            {
                this.productKeyword.Tag = AliProductDetail.productKeyword;
                this.productKeyword.Text = AliProductDetail.productKeyword.Val;
            }
            if (AliProductDetail.keywords2 != null)
            {
                this.keywords2.Tag = AliProductDetail.keywords2;
                this.keywords2.Text = AliProductDetail.keywords2.Val;
            }
            if (AliProductDetail.keywords3 != null)
            {
                this.keywords3.Tag = AliProductDetail.keywords3;
                this.keywords3.Text = AliProductDetail.keywords3.Val;
            }
            if (AliProductDetail.summary != null)
            {
                this.summary.Tag = AliProductDetail.summary;
                this.summary.Text = AliProductDetail.summary.Val;
            }
            if (AliProductDetail.productTeamInputBox != null)
            {
                this.productTeamInputBox.Tag = AliProductDetail.productTeamInputBox;
                this.productTeamInputBox.Text = AliProductDetail.productTeamInputBox.Val;
            }
            if (AliProductDetail.productDescriptionTemp != null)
            {
                this.webBrowser1.Tag = AliProductDetail.productDescriptionTemp;
                string content = AliProductDetail.productDescriptionTemp.Val;
                this.webBrowser1.Document.InvokeScript("SetData", new object[] { content });
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
            this.staticImageWaterMarkIdGroup.Visible = false;
            if (AliProductDetail.staticImageWaterMarkId != null)
            {
                this.staticImageWaterMarkId.Tag = AliProductDetail.staticImageWaterMarkId;
                this.staticImageWaterMarkId.Checked = AliProductDetail.staticImageWaterMarkId.Checked;
                if (this.staticImageWaterMarkId.Checked)
                {
                    this.staticImageWaterMarkIdGroup.Visible = true;
                }
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
                        attrKey.Text = el.Val;
                    }
                    Control attrVal = Controls.Find("customAttrVal" + i, true)[0];
                    if (attrVal != null)
                    {
                        attrVal.Tag = AliProductDetail.CustomAttr[el];
                        attrVal.Text = AliProductDetail.CustomAttr[el].Val;
                    }
                }
            }

            if (AliProductDetail.minOrderQuantity != null)
            {
                this.minOrderQuantity.Tag = AliProductDetail.minOrderQuantity;
                this.minOrderQuantity.Text = AliProductDetail.minOrderQuantity.Val;
            }
            if (AliProductDetail.minOrderUnit != null)
            {
                this.minOrderUnit.Tag = AliProductDetail.minOrderUnit;
                this.minOrderUnit.Text = AliProductDetail.minOrderUnit.Val;
            }
            if (AliProductDetail.moneyType != null)
            {
                this.moneyType.Tag = AliProductDetail.moneyType;
                this.moneyType.Text = AliProductDetail.moneyType.Val;
            }
            if (AliProductDetail.priceRangeMin != null)
            {
                this.priceRangeMin.Tag = AliProductDetail.priceRangeMin;
                this.priceRangeMin.Text = AliProductDetail.priceRangeMin.Val;
            }
            if (AliProductDetail.priceRangeMax != null)
            {
                this.priceRangeMax.Tag = AliProductDetail.priceRangeMax;
                this.priceRangeMax.Text = AliProductDetail.priceRangeMax.Val;
            }
            if (AliProductDetail.priceUnit != null)
            {
                this.priceUnit.Tag = AliProductDetail.priceUnit;
                this.priceUnit.Text = AliProductDetail.priceUnit.Val;
            }
            if (AliProductDetail.port != null)
            {
                this.port.Tag = AliProductDetail.port;
                this.port.Text = AliProductDetail.port.Val;
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
                this.paymentMethodOtherDesc.Text = AliProductDetail.paymentMethodOtherDesc.Val;
            }
            if (AliProductDetail.supplyQuantity != null)
            {
                this.supplyQuantity.Tag = AliProductDetail.supplyQuantity;
                this.supplyQuantity.Text = AliProductDetail.supplyQuantity.Val;
            }
            if (AliProductDetail.supplyUnit != null)
            {
                this.supplyUnit.Tag = AliProductDetail.supplyUnit;
                this.supplyUnit.Text = AliProductDetail.supplyUnit.Val;
            }
            if (AliProductDetail.supplyPeriod != null)
            {
                this.supplyPeriod.Tag = AliProductDetail.supplyPeriod;
                this.supplyPeriod.Text = AliProductDetail.supplyPeriod.Val;
            }
            if (AliProductDetail.minOrderQuantity != null)
            {
                this.minOrderQuantity.Tag = AliProductDetail.minOrderQuantity;
                this.minOrderQuantity.Text = AliProductDetail.minOrderQuantity.Val;
            }
            if (AliProductDetail.minOrderQuantity != null)
            {
                this.minOrderQuantity.Tag = AliProductDetail.minOrderQuantity;
                this.minOrderQuantity.Text = AliProductDetail.minOrderQuantity.Val;
            }
            if (AliProductDetail.consignmentTerm != null)
            {
                this.consignmentTerm.Tag = AliProductDetail.consignmentTerm;
                this.consignmentTerm.Text = AliProductDetail.consignmentTerm.Val;
            }
            if (AliProductDetail.packagingDesc != null)
            {
                this.packagingDesc.Tag = AliProductDetail.packagingDesc;
                this.packagingDesc.Text = AliProductDetail.packagingDesc.Val;
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

            if (AliProductDetail.minOrderUnit != null)
            {
                this.minOrderUnit.Tag = AliProductDetail.minOrderUnit;
                this.minOrderUnit.DisplayMember = "Label";
                this.minOrderUnit.ValueMember = "Val";
                FormElement selected = AliProductDetail.minOrderUnit.Options[0];
                foreach (FormElement e in AliProductDetail.minOrderUnit.Options)
                {
                    this.minOrderUnit.Items.Add(e);
                    if (e.Checked) selected = e;
                }
                this.minOrderUnit.SelectedItem = selected;
            }
            if (AliProductDetail.moneyType != null)
            {
                this.moneyType.Tag = AliProductDetail.moneyType;
                this.moneyType.DisplayMember = "Label";
                this.moneyType.ValueMember = "Val";
                FormElement selected = AliProductDetail.moneyType.Options[0];
                foreach (FormElement e in AliProductDetail.moneyType.Options)
                {
                    this.moneyType.Items.Add(e);
                    if (e.Checked) selected = e;
                }
                this.moneyType.SelectedItem = selected;
            }
            if (AliProductDetail.priceUnit != null)
            {
                this.priceUnit.Tag = AliProductDetail.priceUnit;
                this.priceUnit.DisplayMember = "Label";
                this.priceUnit.ValueMember = "Val";
                FormElement selected = AliProductDetail.priceUnit.Options[0];
                foreach (FormElement e in AliProductDetail.priceUnit.Options)
                {
                    this.priceUnit.Items.Add(e);
                    if (e.Checked) selected = e;
                }
                this.priceUnit.SelectedItem = selected;
            }
            if (AliProductDetail.supplyUnit != null)
            {
                this.supplyUnit.Tag = AliProductDetail.supplyUnit;
                this.supplyUnit.DisplayMember = "Label";
                this.supplyUnit.ValueMember = "Val";
                FormElement selected = AliProductDetail.supplyUnit.Options[0];
                foreach (FormElement e in AliProductDetail.supplyUnit.Options)
                {
                    this.supplyUnit.Items.Add(e);
                    if (e.Checked) selected = e;
                }
                this.supplyUnit.SelectedItem = selected;
            }
            if (AliProductDetail.supplyPeriod != null)
            {
                this.supplyPeriod.Tag = AliProductDetail.supplyPeriod.Name;
                this.supplyPeriod.DisplayMember = "Label";
                this.supplyPeriod.ValueMember = "Val";
                FormElement selected = AliProductDetail.supplyPeriod.Options[0];
                foreach (FormElement e in AliProductDetail.supplyPeriod.Options)
                {
                    this.supplyPeriod.Items.Add(e);
                    if (e.Checked) selected = e;
                }
                this.supplyPeriod.SelectedItem = selected;
            }
            int height = 20;
            int tabIndex = 1;
            if (this.SysAttrPanel.Controls.Count>0)
            {
                foreach (Control c in this.SysAttrPanel.Controls)
                {
                    this.SysAttrPanel.Controls.Remove(c);
                }
            }
            foreach (AttributeNode attr in AliProductDetail.SysAttr)
            {
                LoadSystemAttributes(attr, ref height, ref tabIndex);
                height = height + 30;
                tabIndex = tabIndex + 1;
            }

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
                comboBox.SelectedItem = selNode;
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

    }
}
