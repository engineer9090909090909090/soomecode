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
                this.productName.Tag = AliProductDetail.productName.Name;
                this.productName.Text = AliProductDetail.productName.Val;
            }
            if (AliProductDetail.productKeyword != null)
            {
                this.productKeyword.Tag = AliProductDetail.productKeyword.Name;
                this.productKeyword.Text = AliProductDetail.productKeyword.Val;
            }
            if (AliProductDetail.keywords2 != null)
            {
                this.keywords2.Tag = AliProductDetail.keywords2.Name;
                this.keywords2.Text = AliProductDetail.keywords2.Val;
            }
            if (AliProductDetail.keywords3 != null)
            {
                this.keywords3.Tag = AliProductDetail.keywords3.Name;
                this.keywords3.Text = AliProductDetail.keywords3.Val;
            }
            if (AliProductDetail.summary != null)
            {
                this.summary.Tag = AliProductDetail.summary.Name;
                this.summary.Text = AliProductDetail.summary.Val;
            }
            if (AliProductDetail.productTeamInputBox != null)
            {
                this.productTeamInputBox.Tag = AliProductDetail.productTeamInputBox.Name;
                this.productTeamInputBox.Text = AliProductDetail.productTeamInputBox.Val;
            }
            if (AliProductDetail.productDescription != null)
            {
                this.webBrowser1.Tag = AliProductDetail.productDescription.Name;
                string val = AliProductDetail.productDescription.Val;
                this.webBrowser1.Document.InvokeScript("SetData", new object[] { val });
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
                        attrKey.Tag = el.Name;
                        attrKey.Text = el.Val;
                    }
                    Control attrVal = Controls.Find("customAttrVal" + i, true)[0];
                    if (attrVal != null)
                    {
                        attrVal.Tag = AliProductDetail.CustomAttr[el].Name;
                        attrVal.Text = AliProductDetail.CustomAttr[el].Val;
                    }
                }
            }

            if (AliProductDetail.minOrderQuantity != null)
            {
                this.minOrderQuantity.Tag = AliProductDetail.minOrderQuantity.Name;
                this.minOrderQuantity.Text = AliProductDetail.minOrderQuantity.Val;
            }
            if (AliProductDetail.minOrderUnit != null)
            {
                this.minOrderUnit.Tag = AliProductDetail.minOrderUnit.Name;
                this.minOrderUnit.Text = AliProductDetail.minOrderUnit.Val;
            }
            if (AliProductDetail.moneyType != null)
            {
                this.moneyType.Tag = AliProductDetail.moneyType.Name;
                this.moneyType.Text = AliProductDetail.moneyType.Val;
            }
            if (AliProductDetail.priceRangeMin != null)
            {
                this.priceRangeMin.Tag = AliProductDetail.priceRangeMin.Name;
                this.priceRangeMin.Text = AliProductDetail.priceRangeMin.Val;
            }
            if (AliProductDetail.priceRangeMax != null)
            {
                this.priceRangeMax.Tag = AliProductDetail.priceRangeMax.Name;
                this.priceRangeMax.Text = AliProductDetail.priceRangeMax.Val;
            }
            if (AliProductDetail.priceUnit != null)
            {
                this.priceUnit.Tag = AliProductDetail.priceUnit.Name;
                this.priceUnit.Text = AliProductDetail.priceUnit.Val;
            }
            if (AliProductDetail.port != null)
            {
                this.port.Tag = AliProductDetail.port.Name;
                this.port.Text = AliProductDetail.port.Val;
            }
            if (AliProductDetail.paymentMethod != null)
            {
                foreach (FormElement el in AliProductDetail.paymentMethod)
                {
                    Control CheckBoxControl = Controls.Find(el.Id, true)[0];
                    if (CheckBoxControl != null)
                    {
                        ((CheckBox)CheckBoxControl).Tag = el.Name;
                        ((CheckBox)CheckBoxControl).Checked = el.Checked;
                    }
                }
            }
            if (AliProductDetail.paymentMethodOtherDesc != null)
            {
                this.paymentMethodOtherDesc.Tag = AliProductDetail.paymentMethodOtherDesc.Name;
                this.paymentMethodOtherDesc.Text = AliProductDetail.paymentMethodOtherDesc.Val;
            }
            if (AliProductDetail.supplyQuantity != null)
            {
                this.supplyQuantity.Tag = AliProductDetail.supplyQuantity.Name;
                this.supplyQuantity.Text = AliProductDetail.supplyQuantity.Val;
            }
            if (AliProductDetail.supplyUnit != null)
            {
                this.supplyUnit.Tag = AliProductDetail.supplyUnit.Name;
                this.supplyUnit.Text = AliProductDetail.supplyUnit.Val;
            }
            if (AliProductDetail.supplyPeriod != null)
            {
                this.supplyPeriod.Tag = AliProductDetail.supplyPeriod.Name;
                this.supplyPeriod.Text = AliProductDetail.supplyPeriod.Val;
            }
            if (AliProductDetail.minOrderQuantity != null)
            {
                this.minOrderQuantity.Tag = AliProductDetail.minOrderQuantity.Name;
                this.minOrderQuantity.Text = AliProductDetail.minOrderQuantity.Val;
            }
            if (AliProductDetail.minOrderQuantity != null)
            {
                this.minOrderQuantity.Tag = AliProductDetail.minOrderQuantity.Name;
                this.minOrderQuantity.Text = AliProductDetail.minOrderQuantity.Val;
            }
            if (AliProductDetail.consignmentTerm != null)
            {
                this.consignmentTerm.Tag = AliProductDetail.consignmentTerm.Name;
                this.consignmentTerm.Text = AliProductDetail.consignmentTerm.Val;
            }
            if (AliProductDetail.packagingDesc != null)
            {
                this.packagingDesc.Tag = AliProductDetail.packagingDesc.Name;
                this.packagingDesc.Text = AliProductDetail.packagingDesc.Val;
            }

        }

        private void ImageBankLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ImageForm f = new ImageForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

    }
}
