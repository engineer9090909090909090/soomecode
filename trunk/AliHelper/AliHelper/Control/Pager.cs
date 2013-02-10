using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace AliHelper
{
    public partial class Pager : UserControl
    {

        private int _PageCount = 1;
        private int _PageIndex = 1;
        private int _PageSize = 20;
        private int _RecordCount;
        private string PagerText = "当前{1}/{2}页,每页{3}条,总共{0}条记录";

        public event EventHandler PageIndexChanged;

        public Pager()
        {
            InitializeComponent();
            ShowPageSizeList();
            this.cmbPageSize.SelectedIndex = 0;
        }


        [Category("自定义属性"), Description("是否显示每页显示记录数")]
        public bool ShowPageSizeDropdown { set; get; }

        

        private int PageCount
        {
            get
            {
                return this._PageCount;
            }
        }

        [DefaultValue(1), Category("自定义属性"), Description("当前显示的页数")]
        public int PageIndex
        {
            get
            {
                return this._PageIndex;
            }
            set
            {
                this._PageIndex = value;
            }
        }

        [DefaultValue(20), Description("每页显示的记录数"), Category("自定义属性")]
        public int PageSize
        {
            get
            {
                return this._PageSize;
            }
            set
            {
                if (value <= 1)
                {
                    value = 20;
                }
                this._PageSize = value;
            }
        }

        [Description("要分页的总记录数"), Category("自定义属性")]
        public int RecordCount
        {
            get
            {
                return this._RecordCount;
            }
            set
            {
                if (value != this._RecordCount)
                {
                    this._RecordCount = value;
                    UpdateUI();
                }
            }
        }
        
        protected int GetPageCount(int RecordCounts, int PageSizes)
        {
            int num = 0;
            string str = (Convert.ToDouble(RecordCounts) / Convert.ToDouble(PageSizes)).ToString();
            if (str.IndexOf(".") < 0)
            {
                return Convert.ToInt32(str);
            }
            string[] strArray = Regex.Split(str, @"\.", RegexOptions.IgnoreCase);
            if (!string.IsNullOrEmpty(strArray[1].ToString()))
            {
                num = Convert.ToInt32(strArray[0]) + 1;
            }
            return num;
        }

        protected void SetBtnEnabled()
        {
            if (this._PageIndex == 1)
            {
                this.btnFirst.Enabled = false;
                this.btnPrev.Enabled = false;
                this.btnNext.Enabled =  true;
                this.btnLast.Enabled = true;
            }
            else if ((this._PageIndex > 1) && (this._PageIndex < this._PageCount))
            {
                this.btnFirst.Enabled = true;
                this.btnPrev.Enabled = true;
                this.btnNext.Enabled = true;
                this.btnLast.Enabled = true;
            }
            else if (this._PageIndex == this._PageCount)
            {
                this.btnFirst.Enabled = true;
                this.btnPrev.Enabled = true;
                this.btnNext.Enabled = false;
                this.btnLast.Enabled = false;
            }
            if (this._PageCount == 1)
            {
                this.btnNext.Enabled = false;
                this.btnLast.Enabled = false;
            }
        }


        private void SetPagerText()
        {
            this.txtCurrentPage.Text = this.PageIndex.ToString();
            string[] strArray = new string[] { this.RecordCount.ToString(), this.PageIndex.ToString(), this.PageCount.ToString(), this.PageSize.ToString() };
            this.lblPager.Text = string.Format(this.PagerText, (object[])strArray);
            this.cmbCurrentPage.Items.Clear();
            for(int i = 1; i <= this.PageCount; i ++ )
            {
                this.cmbCurrentPage.Items.Add(i.ToString());
            }
            if (this.PageIndex >= 1)
            {
                this.cmbCurrentPage.SelectedIndex = this.PageIndex - 1;
            }
        }


        private void CustomEvent(object sender, EventArgs e)
        {
            try
            {
                this.PageIndexChanged(sender, e);
            }
            catch (Exception)
            {
                MessageBox.Show("未找到PageIndexChanged事件！");
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            this._PageIndex = 1;
            this.SetPagerText();
            this.SetBtnEnabled();
            this.CustomEvent(sender, e);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            this._PageIndex = this._PageCount;
            this.SetPagerText();
            this.SetBtnEnabled();
            this.CustomEvent(sender, e);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int num = this._PageIndex;
            try
            {
                int num2 = Convert.ToInt32(num) + 1;
                if (num2 >= this._RecordCount)
                {
                    num2 = this._RecordCount;
                }
                this._PageIndex = num2;
                this.SetPagerText();
                this.SetBtnEnabled();
                this.CustomEvent(sender, e);
            }
            catch (Exception)
            {
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            int num = this._PageIndex;
            try
            {
                int num2 = Convert.ToInt32(num) - 1;
                if (num2 <= 0)
                {
                    num2 = 1;
                }
                this._PageIndex = num2;
                this.SetPagerText();
                this.SetBtnEnabled();
                this.CustomEvent(sender, e);
            }
            catch (Exception)
            {
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            string text = this.cmbCurrentPage.SelectedItem.ToString();
            this._PageIndex = Convert.ToInt32(text);
            this.SetPagerText();
            this.SetBtnEnabled();
            this.CustomEvent(sender, e);
        }

        private void WinFormPager_Load(object sender, EventArgs e)
        {
            this.ShowPageSizeList();
            this.SetPagerText();
            this.SetBtnEnabled();
        }

        private void UpdateUI()
        {
            int pageCount = this.GetPageCount(this._RecordCount, this._PageSize);
            this._PageCount = pageCount;
            this.SetPagerText();
            this.SetBtnEnabled();
        }

        private void ShowPageSizeList()
        {
            if (!this.ShowPageSizeDropdown)
            {
                this.lab1PageSize.Visible = false;
                this.lab2PageSize.Visible = false;
                this.lab3PageSize.Visible = false;
                this.cmbPageSize.Visible = false;
            }
            else
            {
                this.lab1PageSize.Visible = true;
                this.lab2PageSize.Visible = true;
                this.lab3PageSize.Visible = true;
                this.cmbPageSize.Visible = true;
            }
        }

        private void cmbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ShowPageSizeDropdown)
            { 
                string text = this.cmbPageSize.SelectedItem.ToString();
                this._PageSize = Convert.ToInt32(text);
                btnGo_Click(sender, e);
            }
        }
    }
}
