using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AliEmail
{
    public delegate void SelectFinishedEventHandler(object sender, MyEventArgs e);
    public partial class FolderSelectorForm : Form
    {
        public event SelectFinishedEventHandler SelectFinishedEvent;
        public FolderSelectorForm()
        {
            InitializeComponent();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SetCheckedListBox(DataTable foldersTable)
        {
            this.folderListBox.DataSource = foldersTable;
            this.folderListBox.ValueMember = "entryID";
            this.folderListBox.DisplayMember = "folderName";
        }

        private void confirmBtn_Click(object sender, EventArgs e)
        {
            this.confirmBtn.Enabled = true;
            this.label.Text = "正在从outlook导入邮件数据，请等候...";
            List<string> entries = new List<string>();
            for (int i = 0; i < this.folderListBox.Items.Count; i++)
            {
                if (this.folderListBox.GetItemChecked(i))
                {
                    this.folderListBox.SetSelected(i, true);
                    entries.Add(this.folderListBox.SelectedValue.ToString());
                }
            }
            SelectFinishedEvent(this, new MyEventArgs(entries));
        }

        private void folderListBox_MouseClick(object sender, MouseEventArgs e)
        {
            int index = folderListBox.IndexFromPoint(e.Location);            
            if (this.folderListBox.GetItemChecked(index))
            {
                this.folderListBox.SetItemCheckState(index, CheckState.Unchecked);
            }
            else {
                this.folderListBox.SetItemCheckState(index, CheckState.Checked);
            }
            
        }


    }
}
