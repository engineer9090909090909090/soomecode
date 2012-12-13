using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AliRank
{
    public partial class MaxInWindow : Form
    {
        public int iModifyProductId = 0;
        public static bool updatedSuccess = false;
        private KeywordDAO keywordDAO;
        private ShowcaseRankInfo rankInfo;
        public MaxInWindow()
        {
            InitializeComponent();
            ProductLabel.AutoSize = false;    //设置AutoSize
            ProductLabel.Width = 280;          //设置显示宽度
            ProductLabel.Height = 50;
            updatedSuccess = false;
        }

        private void MaxInWindow_Load(object sender, EventArgs e)
        {

            keywordDAO = DAOFactory.Instance.GetKeywordDAO();
            rankInfo = keywordDAO.GetShowcaseRankInfo(iModifyProductId);
            this.ProductLabel.Text = rankInfo.ProductName;
            this.oldMaxInquiryQty.Text = rankInfo.MaxInquiryQty.ToString();
            this.NewMaxInquiryQty.Focus();

        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void confirmBtn_Click(object sender, EventArgs e)
        {
            string sNewMaxInquiryQty = this.NewMaxInquiryQty.Text;
            if (string.IsNullOrEmpty(sNewMaxInquiryQty))
            {
                this.ErrorMsg.Text = "新排名询盘数不能为空且必需为1-10的数字！";
                return;
            }
            int iNewMaxInquiryQty = Convert.ToInt32(sNewMaxInquiryQty);
            keywordDAO.UpdateMaxInquiryQty(iModifyProductId, iNewMaxInquiryQty);
            updatedSuccess = true;
            this.Close();
        }
    }
}
