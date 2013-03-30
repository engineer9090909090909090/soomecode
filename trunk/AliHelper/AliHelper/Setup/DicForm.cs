using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soomes;

namespace AliHelper
{
    public partial class DicForm : Form
    {
        public BaseManager baseManager;
        public AppDic SelectedAppDic;
        public DicForm()
        {
            InitializeComponent();
            baseManager = new BaseManager();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            KeyValuePair<string, string> item = (KeyValuePair<string, string>)this.NewDicType.SelectedItem;
            string Key = this.NewKeyTxt.Text.Trim();
            string Val = this.NewValTxt.Text.Trim();
            if (string.IsNullOrEmpty(Key) || string.IsNullOrEmpty(Val))
            {
                return;
            }
            baseManager.SetAppDicValue(item.Key, Key, Val);
            this.BeginInvoke(new Action(() =>
            {
                LoadAppDicData();
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SelectedAppDic == null) return;
            string Val = this.EditValTxt.Text.Trim();
            baseManager.SetAppDicValue(SelectedAppDic.Type, SelectedAppDic.Key, Val);
            this.BeginInvoke(new Action(() =>
            {
                LoadAppDicData();
            }));
        }

        private void LoadAppDicData()
        {
            this.DicListView.Groups.Clear();
            this.DicListView.Items.Clear();
            KeyValuePair<string, string> item = (KeyValuePair<string, string>)this.QueryDicType.SelectedItem;
            if (string.IsNullOrEmpty(item.Key))
            {
                foreach (string key in DataCache.Instance.DicTypeOptions.Keys)
                {
                    LoadAppDicList4Type(key);
                }
            }
            else {
                LoadAppDicList4Type(item.Key);
            }
        }

        private void LoadAppDicList4Type(string Type)
        {
            string val = (string)DataCache.Instance.DicTypeOptions[Type];
            List<AppDic> options = baseManager.GetAppDicOptions(Type);
            ListViewGroup group = new ListViewGroup(val);
            this.DicListView.Groups.Add(group);
            int i = 0;
            foreach (AppDic dic in options)
            {
                ListViewItem item0 = new ListViewItem(
                    new string[] { dic.Key, dic.Label },
                    i++, group);
                item0.Tag = dic;
                this.DicListView.Items.Add(item0);
            }
        }

        private void DicListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                AppDic dic = (AppDic)e.Item.Tag;
                this.EditKeyTxt.Text = dic.Key;
                this.EditValTxt.Text = dic.Label;
                this.EditBtn.Enabled = true;
                this.DeleteBtn.Enabled = true;
                SelectedAppDic = dic;
                foreach (string key in DataCache.Instance.DicTypeOptions.Keys)
                {
                    string val = (string)DataCache.Instance.DicTypeOptions[key];
                    KeyValuePair<string, string> item = new KeyValuePair<string, string>(key, val);
                    if (dic.Type == key)
                    {
                        this.EditDicType.SelectedItem = item;
                    }
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            string Val = this.EditValTxt.Text.Trim();
            baseManager.DeleteAppDic(SelectedAppDic.Type, SelectedAppDic.Key);
            this.EditKeyTxt.Text = string.Empty;
            this.EditValTxt.Text = string.Empty;
            this.EditDicType.SelectedIndex = 0;
            this.EditBtn.Enabled = false;
            this.DeleteBtn.Enabled = false;
            this.BeginInvoke(new Action(() =>
            {
                LoadAppDicData();
            }));
        }

        private void QueryDicType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                LoadAppDicData();
            }));
        }

        private void DicForm_Load(object sender, EventArgs e)
        {
            this.NewDicType.DisplayMember = "Value";
            this.NewDicType.ValueMember = "Key";
            this.EditDicType.DisplayMember = "Value";
            this.EditDicType.ValueMember = "Key";
            this.QueryDicType.DisplayMember = "Value";
            this.QueryDicType.ValueMember = "Key";
            KeyValuePair<string, string> item0 = new KeyValuePair<string, string>("", "所有");
            this.QueryDicType.Items.Add(item0);
            foreach (string key in DataCache.Instance.DicTypeOptions.Keys)
            {
                string val = (string)DataCache.Instance.DicTypeOptions[key];
                KeyValuePair<string, string> item = new KeyValuePair<string, string>(key, val);
                this.NewDicType.Items.Add(item);
                this.EditDicType.Items.Add(item);
                this.QueryDicType.Items.Add(item);
            }
            this.NewDicType.SelectedIndex = 0;
            this.EditDicType.SelectedIndex = 0;
            this.QueryDicType.SelectedIndex = 0;
            this.EditDicType.Enabled = false;
            this.EditKeyTxt.Enabled = false;
        }
    }
}
