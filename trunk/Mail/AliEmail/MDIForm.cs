using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;


namespace AliEmail
{
    public partial class MDIForm : Form
    {
        private int childFormNumber = 0;
        FolderSelectorForm fsForm;
        MailForm mailForm;
        OrigForm origForm;
        helpForm helpForm;
        private DataTable origDataTable;
        private DataTable mailDataTable;
        public MDIForm()
        {
            InitializeComponent();
            if (helpForm == null)
            {
                helpForm = new helpForm();
                helpForm.MdiParent = this;
                helpForm.WindowState = FormWindowState.Maximized;
                helpForm.Show();
                helpForm.FormClosed += new FormClosedEventHandler(helpForm_FormClosed);
            }
        }

        void helpForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            helpForm.Dispose();
            helpForm = null;
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "窗口 " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "数据库文件(*.mdb)|*.mdb|所有文件(*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                ReadDataFormMdb(fileName);
            }
        }

        private void ReadDataFormMdb(String fileName)
        {
            DataSet ds = DAO.FetchMailContext(fileName);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                origDataTable = ds.Tables[0];
                this.toolStripStatusLabel.Text = "读取原始邮件成功。";
                this.runToolBtn.Enabled = true;
                this.runToolStripMenuItem.Enabled = true;
                origForm = new OrigForm();
                origForm.MdiParent = this;
                origForm.FormClosed += new FormClosedEventHandler(origForm_FormClosed);
                origForm.setDataGrigViewContext(ds);
                origForm.Show();
            }
            else
            {
                MessageBox.Show("读取数据库出错，请检查数据库是否包含Email表或者表记录不为空。");
            }
        
        }

        void origForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            origForm.Dispose();
            origForm = null;
        }

        private void SaveToXls(String fileName)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(mailDataTable);
            ExcelUtils excel = new ExcelUtils();
            excel.ExportToExcel(ds,fileName);
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Excel文件 (*.xls)|*.xls";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                this.saveToolBtn.Enabled = false;
                this.saveToolStripMenuItem.Enabled = false;
                string fileName = saveFileDialog.FileName;
                this.toolStripStatusLabel.Text = "正在保存到Excel文件，请稍候...";
                SaveToXls(fileName);
                this.toolStripStatusLabel.Text = "成功保存到Excel文件。";
                this.saveToolBtn.Enabled = true;
                this.saveToolStripMenuItem.Enabled = true;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        public IfParseMail getMailParser(string htmlBody, DateTime sendTime)
        {
            long inquireTime = long.Parse(sendTime.ToString("yyyyMMddHHmm"));

            if (Regex.Matches(htmlBody, "http://www.globalsources.com").Count > 1)
            {
                return new ParseGlobelSourceMail();
            }

            if (Regex.Matches(htmlBody, "http://www.made-in-china.com").Count > 1)
            {
                return new ParseMadeInChinaMail();
            }

            if (Regex.Matches(htmlBody, "http://www.alibaba.com").Count > 1)
            {
                if (inquireTime < 201102240000)
                {
                    return new ParseAliBefor20110224Mail();
                }
                if (inquireTime > 201102240000 && inquireTime < 201103010000)
                {
                    return new ParseAliBetween24to30Mail();
                }
                if (inquireTime > 201103010000)
                {
                    return new ParseAliAfter20110301Mail();
                }                
            }

            return null;
        }

        private void runToolBtn_Click(object sender, EventArgs e)
        {

            IfParseMail pMail = null;
            mailDataTable = DefineTable();
            if (origDataTable != null && origDataTable.Rows.Count > 0)
            {
                Int64 i = 1;
                foreach (DataRow dr in origDataTable.Rows)
                {
                    string htmlBody = (string)dr[1];
                    string stringSendTime = (string)dr[2];
                    DateTime sendTime = DateTime.ParseExact(stringSendTime, "yyyy-MM-dd HH:mm", null);
                    DataRow dataRow = mailDataTable.NewRow();
                    pMail = getMailParser(htmlBody, sendTime);
                    if (pMail == null)
                    {
                        continue;
                    }
                    object[] row = pMail.Parse((string)dr[0], htmlBody);
                    if (string.IsNullOrEmpty((string)row[5])) continue;
                    row[0] = i;
                    row[11] = stringSendTime;
                    row[12] = pMail.getType();
                    dataRow.ItemArray = row;
                    mailDataTable.Rows.Add(dataRow);
                    i++;
                }
            }
            this.saveToolBtn.Enabled = true;
            this.saveToolStripMenuItem.Enabled = true;
            if (mailForm == null)
            {
                mailForm = new MailForm();
                mailForm.MdiParent = this;
                mailForm.WindowState = FormWindowState.Maximized;
                mailForm.FormClosed += new FormClosedEventHandler(mailForm_FormClosed);
                mailForm.setDataGrigViewContext(mailDataTable);
                mailForm.Show();
            }
            else
            {
                mailForm.setDataGrigViewContext(mailDataTable);
                mailForm.Activate();
                mailForm.WindowState = FormWindowState.Maximized;
                mailForm.Focus();
            }
        }

        void mailForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mailForm.Dispose();
            mailForm = null;
        }

        public static DataTable DefineTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(Int64));
            table.Columns.Add("ip", typeof(string));
            table.Columns.Add("origin", typeof(string));
            table.Columns.Add("product", typeof(string));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("email", typeof(string));
            table.Columns.Add("country", typeof(string));
            table.Columns.Add("telephone", typeof(string));
            table.Columns.Add("company", typeof(string));
            table.Columns.Add("address", typeof(string));           
            table.Columns.Add("fax", typeof(string));
            table.Columns.Add("sendTime", typeof(string));
            table.Columns.Add("source", typeof(string));
            table.Columns["id"].Caption = "ID";
            table.Columns["ip"].Caption = "Message IP";
            table.Columns["origin"].Caption = "Origin";
            table.Columns["product"].Caption = "Product";
            table.Columns["name"].Caption = "Name";
            table.Columns["email"].Caption = "Email";
            table.Columns["company"].Caption = "Company";
            table.Columns["address"].Caption = "Address/webSite";
            table.Columns["country"].Caption = "Country/Region";
            table.Columns["sendTime"].Caption = "Sent On";
            table.Columns["source"].Caption = "Source";
          //  table.Columns["id"].ColumnMapping = MappingType.Hidden;
            return table;
        }

        private void outlookToolBtn_Click(object sender, EventArgs e)
        {
            DataTable dt = ReadOutlookPstDB();
            if (dt.Rows.Count > 0)
            {
                fsForm = new FolderSelectorForm();
                fsForm.SetCheckedListBox(dt);
                fsForm.SelectFinishedEvent += new SelectFinishedEventHandler(fsForm_SelectFinishedEvent);
                fsForm.ShowDialog();
            }           
        }



        private DataTable ReadOutlookPstDB()
        {
            DataTable table = new DataTable();
            table.Columns.Add("entryID", typeof(string));
            table.Columns.Add("folderName", typeof(string));
            Microsoft.Office.Interop.Outlook.Application app = new Microsoft.Office.Interop.Outlook.ApplicationClass();
            //app.Session.AddStore("D:\\achievo\\outlook_mail.pst");
            Microsoft.Office.Interop.Outlook.NameSpace NS = app.GetNamespace("MAPI");

            try
            {
                foreach (Microsoft.Office.Interop.Outlook.MAPIFolder objFolder in NS.Folders)
                {
                    GetSubFolder(table, objFolder, objFolder.Name);
                }            
            }
            catch (COMException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                NS.Logoff();
                app = null;
            }
            return table;
        }

        private void GetSubFolder(DataTable table, Microsoft.Office.Interop.Outlook.MAPIFolder objFolder, string parentName)
        {
            foreach (Microsoft.Office.Interop.Outlook.MAPIFolder subFolder in objFolder.Folders)
            {
                if (subFolder.DefaultItemType.ToString().Equals("olMailItem"))
                {
                    DataRow dr = table.NewRow();
                    dr["entryID"] = subFolder.EntryID;
                    dr["folderName"] = parentName + " > " + subFolder.Name;
                    table.Rows.Add(dr);
                    if (subFolder.Folders.Count > 0)
                    {
                        GetSubFolder(table, subFolder, (string)dr["folderName"]);
                    }
                }
            }
        }

        void fsForm_SelectFinishedEvent(object sender, MyEventArgs e)
        {
            fsForm.SelectFinishedEvent -= new SelectFinishedEventHandler(fsForm_SelectFinishedEvent);
            List<string> entries = (List<string>)e.Value;
            Microsoft.Office.Interop.Outlook.Application app = new Microsoft.Office.Interop.Outlook.ApplicationClass();
            Microsoft.Office.Interop.Outlook.NameSpace NS = app.GetNamespace("MAPI");
            Microsoft.Office.Interop.Outlook.MAPIFolder objFolder;
            Microsoft.Office.Interop.Outlook.MailItem objMail;

            DataTable table = new DataTable();
            table.Columns.Add("subject", typeof(string));
            table.Columns.Add("body", typeof(string));
            table.Columns.Add("sentOn", typeof(string));
            try
            {
                foreach (string entryID in entries)
                {
                    objFolder = NS.GetFolderFromID(entryID);
                    for (int i = 1; i <= objFolder.Items.Count; i++)
                    {
                        try
                        {
                            objMail = (Microsoft.Office.Interop.Outlook.MailItem)objFolder.Items[i];
                            DataRow dr = table.NewRow();
                            if (!string.IsNullOrEmpty(objMail.Subject) && !string.IsNullOrEmpty(objMail.HTMLBody))
                            {
                                
                                dr["subject"] = objMail.Subject;
                                dr["body"] = objMail.HTMLBody;
                                dr["sentOn"] = objMail.SentOn.ToString("yyyy-MM-dd HH:mm");
                                table.Rows.Add(dr);
                            }
                        }
                        catch (System.InvalidCastException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }            
            }
            catch (COMException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                NS.Logoff();
                objFolder = null;
                objMail = null;
                app = null;
            }

            origDataTable = table;
            this.toolStripStatusLabel.Text = "从Outlook 2007读取原始邮件成功。";
            this.runToolBtn.Enabled = true;
            this.runToolStripMenuItem.Enabled = true;
            this.fsForm.Close();
            this.fsForm.Dispose();
            this.fsForm = null;
            DataSet ds = new DataSet();
            ds.Tables.Add(origDataTable);
            if (origForm == null)
            {
                origForm = new OrigForm();
                origForm.MdiParent = this;
                origForm.WindowState = FormWindowState.Maximized;
                origForm.FormClosed += new FormClosedEventHandler(origForm_FormClosed);
                origForm.setDataGrigViewContext(ds);
                origForm.Show();
            }
            else 
            {
                origForm.setDataGrigViewContext(ds);
                origForm.Activate();
                origForm.WindowState = FormWindowState.Maximized;
                origForm.Focus();
            }
        }

        private void helpToolBtn_Click(object sender, EventArgs e)
        {
            if (helpForm == null)
            {
                helpForm = new helpForm();
                helpForm.MdiParent = this;
                helpForm.WindowState = FormWindowState.Maximized;
                helpForm.FormClosed += new FormClosedEventHandler(helpForm_FormClosed);
                helpForm.Show();
            }
            else {
                helpForm.WindowState = FormWindowState.Maximized;
                helpForm.Activate();
                helpForm.Focus();
            }
        }
    }
}
