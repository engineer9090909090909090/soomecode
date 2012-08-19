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
    public partial class QueryKwForm : Form
    {
        public static bool IsQuery = false;
        private bool IsCancel = false;
        RankInfoDAO rankInfoDao;
        public QueryKwForm()
        {
            InitializeComponent();
            rankInfoDao = DAOFactory.Instance.GetRankInfoDAO();
        }

        private void QueryKwForm_Load(object sender, EventArgs e)
        {
            IsQuery = false;
            IsCancel = false;
            List<RankInfo> list = rankInfoDao.GetRankInfoList();
            if (list.Count > 0)
            {
                string keywords = string.Empty;
                foreach (RankInfo info in list)
                {
                    keywords += info.RankKeyword + "\r\n";
                }
                KwTextBox.Text = keywords;
            }
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            IsQuery = true;
            IsCancel = false;
            this.Close();
        }

        private void QueryKwForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!IsCancel)
            {
                string kwString = KwTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(kwString))
                {
                    List<RankInfo> list = new List<RankInfo>();
                    string[] keywords = kwString.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string s in keywords)
                    {
                        RankInfo info = new RankInfo();
                        info.RankKeyword = s;
                        list.Add(info);
                    }
                    rankInfoDao.Insert(list);
                }
            }
        }

        private void CancenBtn_Click(object sender, EventArgs e)
        {
            IsCancel = true;
            IsQuery = false;
            this.Close();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            IsCancel = false;
            IsQuery = false;
            this.Close();
        }
    }
}
