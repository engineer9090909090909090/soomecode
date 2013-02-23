using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soomes;
using AliHelper.Bussness;

namespace AliHelper
{
    public partial class FinView : UserControl
    {
        FinOrderManager finOrderManager;
        public FinView()
        {
            InitializeComponent();
            FinDetailPager.PageIndex = 1;
            FinDetailPager.PageSize = 100;
            finOrderManager = new FinOrderManager();
            FinOrderManager.OnEditFinDetailEvent += new NewEditItemEvent(OnNewEditEvent);
        }

        void OnNewEditEvent(object sender, ItemEventArgs e)
        {
            BindDataWithPage(FinDetailPager.PageIndex);
        }

        private void FinView_Load(object sender, EventArgs e)
        {
            this.BeginDateTxt.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.EndDateTxt.Value = DateTime.Now;
            this.ItemTypeTxt.DisplayMember = "Label";
            this.ItemTypeTxt.ValueMember = "Key";
            this.EventTypeTxt.DisplayMember = "Label";
            this.EventTypeTxt.ValueMember = "Key";
            this.AssociationTxt.DisplayMember = "Label";
            this.AssociationTxt.ValueMember = "Key";
            this.ItemTypeTxt.DataSource = finOrderManager.GetQueryAppDicOptions(Constants.BussnessType);
            this.EventTypeTxt.DataSource = finOrderManager.GetQueryAppDicOptions(Constants.DebitCredit);
            this.AssociationTxt.DataSource = finOrderManager.GetQueryAppDicOptions(Constants.Employee);
            BindDataWithPage(1);
        }

        private void BindDateTable(List<FinDetails> list)
        {
            FinDetailDataView.Rows.Clear();
            if (list == null) return;
            int i = 0;
            double TotalAmount = 0;
            foreach (FinDetails item in list)
            {
                TotalAmount = TotalAmount + item.TotalAmount;
                object[] item01 = new object[] { 
                    item.DetailId,
                    1 + i,
                    item.FinDate,
                    item.Description,
                    "￥" + item.TotalAmount.ToString("#,##0.00"),
                    item.OrderNo,
                    item.ItemType,
                    item.Association,
                    item.EventType,
                    item.Remark,
                };
                FinDetailDataView.Rows.Add(item01);
                FinDetailDataView.Rows[i].Cells[4].Style.ForeColor = 
                    (item.TotalAmount > 0)? Color.Red : Color.Blue;
                i++;
            }
            object[] summer = new object[] { null,null,null,"合计","￥" + TotalAmount.ToString("#,##0.00"),
                    null,null,null,null, null };
            FinDetailDataView.Rows.Add(summer);
            FinDetailDataView.Rows[i].Cells[4].Style.ForeColor = (TotalAmount > 0) ? Color.Red : Color.Blue;
        }

        private void BindDataWithPage(int Page)
        {
            this.BeginInvoke(new Action(() =>
            {
                QueryObject<FinDetails> query = new QueryObject<FinDetails>();
                query.Page = Page;
                query.PageSize = FinDetailPager.PageSize;
                query.Condition = new FinDetails();
                query.Condition.BeginTime = this.BeginDateTxt.Value.ToString(Constants.DateFormat);
                query.Condition.EndTime = this.EndDateTxt.Value.ToString(Constants.DateFormat);
                query.Condition.Description = this.EventNameTxt.Text.Trim();
                query.Condition.EventType = (string)this.EventTypeTxt.SelectedValue;
                query.Condition.ItemType = (string)this.ItemTypeTxt.SelectedValue;
                query.Condition.OrderNo = this.OrderNoTxt.Text.Trim();
                query.Condition.Association = (string)this.AssociationTxt.SelectedValue;
                QueryObject<FinDetails> result = finOrderManager.GetFinDetails(query);
                if (result.Result != null && result.Result.Count > 0)
                {
                    FinDetailPager.PageIndex = result.Page;
                    FinDetailPager.PageSize = result.PageSize;
                    FinDetailPager.RecordCount = result.RecordCount;
                    BindDateTable(result.Result);
                }
                else
                {
                    FinDetailPager.PageIndex = Page;
                    FinDetailPager.PageSize = 100;
                    FinDetailPager.RecordCount = 0;
                    BindDateTable(null);
                }
            }));
        }
        private void FinDetailPager_PageIndexChanged(object sender, EventArgs e)
        {
            BindDataWithPage(FinDetailPager.PageIndex);
        }

        private void FinDetailQueryBtn_Click(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }

        private void FinDetailExpBtn_Click(object sender, EventArgs e)
        {
            QueryObject<FinDetails> query = new QueryObject<FinDetails>();
            query.IsExport = true;
            query.Condition = new FinDetails();
            query.Condition.BeginTime = this.BeginDateTxt.Value.ToString(Constants.DateFormat);
            query.Condition.EndTime = this.EndDateTxt.Value.ToString(Constants.DateFormat);
            query.Condition.Description = this.EventNameTxt.Text.Trim();
            query.Condition.EventType = (string)this.EventTypeTxt.SelectedValue;
            query.Condition.ItemType = (string)this.ItemTypeTxt.SelectedValue;
            query.Condition.OrderNo = this.OrderNoTxt.Text.Trim();
            query.Condition.Association = (string)this.AssociationTxt.SelectedValue;
            QueryObject<FinDetails> result = finOrderManager.GetFinDetails(query);

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel工作簿(*.xls,*.xlsx)| *.xls; *.xlsx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)  
            {  
                string localFilePath = saveFileDialog1.FileName.ToString();
                try
                {
                    ExportExcel exporter = new ExportExcel();
                    exporter.AddColumn("FinDate","日期");
                    exporter.AddColumn("Description", "描述");
                    exporter.AddColumn("TotalAmount","金额");
                    exporter.AddColumn("OrderNo","所属业务");
                    exporter.AddColumn("ItemType","项目类型");
                    exporter.AddColumn("Association","经手人/相关人");
                    exporter.AddColumn("EventType","收支类型");
                    exporter.AddColumn("Remark","备注");
                    exporter.ExportToExcel<FinDetails>(result.Result, localFilePath);
                    exporter.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存文件出错："　+　ex.Message);
                }
            }
        }

        private void FinDetailDataView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(this.FinDetailDataView.Rows[e.RowIndex].Cells[0].Value);
            if (id == 0) return;
            EditFinDetail f = new EditFinDetail();
            f.UpdateDetail = finOrderManager.GetFinDetail(id);
            f.ShowDialog(this);
        }

        

        
    }
}
