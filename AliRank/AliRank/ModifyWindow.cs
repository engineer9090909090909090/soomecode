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
    public partial class ModifyWindow : Form
    {
        public int iModifyProductId = 0;
        public static bool updatedSuccess = false;
        private KeywordDAO keywordDAO;
        private ShowcaseRankInfo rankInfo;
        public ModifyWindow()
        {
            InitializeComponent();
            ProductLabel.AutoSize = false;    //设置AutoSize
            ProductLabel.Width = 280;          //设置显示宽度
            ProductLabel.Height = 50;
            updatedSuccess = false;
        }

        private void ModifyWindow_Load(object sender, EventArgs e)
        {

            keywordDAO = DAOFactory.Instance.GetKeywordDAO();
            rankInfo = keywordDAO.GetShowcaseRankInfo(iModifyProductId);
            this.ProductLabel.Text = rankInfo.ProductName;
            this.oldKeyword.Text = rankInfo.RankKeyword;
            this.NewKeyword.Focus();

        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void confirmBtn_Click(object sender, EventArgs e)
        {
            string sNewKeyword = this.NewKeyword.Text;
            if (string.IsNullOrEmpty(sNewKeyword))
            {
                this.ErrorMsg.Text = "新排名关键词不能为空！";
                return;
            }
            keywordDAO.UpdateRankKeyword(iModifyProductId, sNewKeyword);
            updatedSuccess = true;
            this.Close();
        }
    }
}
