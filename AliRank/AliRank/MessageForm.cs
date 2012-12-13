using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace AliRank
{
    public partial class MessageForm : Form
    {
        private InquiryDAO inquiryDAO;

        #region 初始化
        public MessageForm()
        {
            InitializeComponent();
            LoadDataview();
        }

        void LoadDataview()
        {
            inquiryDAO = DAOFactory.Instance.GetInquiryDAO();
            this.dataGridView.DataBindings.Clear();
            this.dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            DataTable dt = new DataTable();
            dt.Columns.Add("Check", typeof(Boolean));
            dt.Columns.Add("Content", typeof(string));
            dt.Columns.Add("SendNum", typeof(string));
            dt.Columns.Add("MsgId", typeof(Int32));
            this.dataGridView.DataSource = dt;
            DataGridViewColumn column0 = this.dataGridView.Columns[0];
            column0.HeaderText = "Check";
            column0.Width = 100;
            DataGridViewColumn column = this.dataGridView.Columns[1];
            column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column.HeaderText = "Content";
            column.Width = 550;
            DataGridViewColumn column2 = this.dataGridView.Columns[2];
            column2.HeaderText = "SendNum";
            column2.Width = 100;
            DataGridViewColumn column3 = this.dataGridView.Columns[3];
            column3.HeaderText = "Id";
            column3.Width = 10;
            column3.Visible = false;
            List<InquiryMessages> accountList = inquiryDAO.GetInquiryMessages();
            if (accountList.Count > 0)
            {
                foreach (InquiryMessages item in accountList)
                {
                    DataRow row = dt.NewRow();
                    row["Check"] = false;
                    row["Content"] = item.Content;
                    row["SendNum"] = item.SendNum;
                    row["MsgId"] = item.MsgId;
                    dt.Rows.Add(row);
                }
            }
        }
        
        #endregion

        #region 新增按钮事件
        private void InsertBtn_Click(object sender, EventArgs e)
        {
            InquiryMessages model = new InquiryMessages();
            model.Content = this.ContentBox.Text.Trim();
            if (string.IsNullOrEmpty(model.Content))
            {
                return;
            }
            inquiryDAO.InsertInquiryMessages(model);
            LoadDataview();
        }
        #endregion

        #region 删除按钮事件
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView == null || dataGridView.Rows.Count == 0)
            {
                return;
            }
            List<string> removeList = new List<string>();
            DataTable dt = (DataTable)dataGridView.DataSource;
            for (int j = dt.Rows.Count - 1; j >= 0; j--)
            {
                DataRow dr = dt.Rows[j];
                Boolean chk = System.Boolean.Parse(dr[0].ToString());
                if (chk)
                {
                    removeList.Add(dr[3].ToString());
                    dt.Rows.Remove(dr);
                }
            }
            if (removeList.Count > 0)
            {
                inquiryDAO.DeleteInquiryMessages(removeList);
                MessageBox.Show(removeList.Count + " 行记录被删除。");
            }
        }
        #endregion

  

    }
}
