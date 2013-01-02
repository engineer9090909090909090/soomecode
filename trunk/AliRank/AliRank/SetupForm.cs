using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AliRank
{
    public partial class SetupForm : Form
    {
        private string IniFile;
        public SetupForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(SetupForm_Load);
            IniFile = FileUtils.CreateAppDataFolderEmptyTextFile(Constants.INI_FILE);
        }

        void SetupForm_Load(object sender, EventArgs e)
        {
            string clickNum = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.AUTO_CLICK_NUM);
            this.textBox1.Text = clickNum;
            string sMaxPauseTime = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.MAX_PAUSE_TIME);
            this.MaxPauseTime.Text = sMaxPauseTime;
            string sMinIntervalTime = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.MIN_INTERVAL_TIME);
            this.MaxIntervalBox.Text = sMinIntervalTime;
            string sMaxQueryPage = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.MAX_QUERY_PAGE);
            this.MaxQueryPage.Text = sMaxQueryPage;

            string network = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.NETWORK_CHOICE);
            if (network.Equals(Constants.NETWORK_VPN))
            {
                VPNRadioBtn.Checked = true;
            }
            else {
                NoneRadioBtn.Checked = true;
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ImpKwForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        private void ImportBtn_Click(object sender, EventArgs e)
        {
            string str = textBox1.Text;
            int a=0;
            if (string.IsNullOrEmpty(str) || int.TryParse(str, out a) == false)
            {
                errorMsg.Text = "点击次数不能为空且只能为数字！";
                return;
            }
            string maxPauseTime = this.MaxPauseTime.Text;
            if (string.IsNullOrEmpty(maxPauseTime) || int.TryParse(maxPauseTime, out a) == false)
            {
                errorMsg.Text = "随机暂停最大时间不能为空且只能为数字！";
                return;
            }
            string minIntervalBox = this.MinIntervalBox.Text;
            if (string.IsNullOrEmpty(minIntervalBox) || int.TryParse(minIntervalBox, out a) == false)
            {
                errorMsg.Text = "最小询盘时间不能为空且只能为数字！";
                return;
            }
            string maxIntervalBox = this.MaxIntervalBox.Text;
            if (string.IsNullOrEmpty(maxIntervalBox) || int.TryParse(maxIntervalBox, out a) == false)
            {
                errorMsg.Text = "最小询盘时间不能为空且只能为数字！";
                return;
            }
            string maxQueryPage = this.MaxQueryPage.Text;
            if (string.IsNullOrEmpty(maxQueryPage) || int.TryParse(maxQueryPage, out a) == false)
            {
                errorMsg.Text = "最大查询页数不能为空或且只能为数字！";
                return;
            }
            DAOFactory.Instance.GetProfileDAO().SetValue(Constants.AUTO_CLICK_NUM, str);
            DAOFactory.Instance.GetProfileDAO().SetValue(Constants.MAX_PAUSE_TIME, maxPauseTime);
            DAOFactory.Instance.GetProfileDAO().SetValue(Constants.MIN_INTERVAL_TIME, minIntervalBox);
            DAOFactory.Instance.GetProfileDAO().SetValue(Constants.MAX_INTERVAL_TIME, maxIntervalBox);
            DAOFactory.Instance.GetProfileDAO().SetValue(Constants.MAX_QUERY_PAGE, maxQueryPage);
            string Network = Constants.NETWORK_VPN;
            if (VPNRadioBtn.Checked)
            {
                Network = Constants.NETWORK_VPN;
            }
            if (NoneRadioBtn.Checked)
            {
                Network = Constants.NETWORK_NONE;
            }
            DAOFactory.Instance.GetProfileDAO().SetValue(Constants.NETWORK_CHOICE, Network);
            this.Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}