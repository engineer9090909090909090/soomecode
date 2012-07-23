using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;
using System.Runtime.InteropServices;

namespace ImagesTools1
{
    public partial class Form1 : Form
    {
        private BackgroundWorker worker;
        private BackgroundWorker worker1;
        private BackgroundWorker worker2;
        private string defaultImagesfilePath = "";
        private string defaultExportfilePath = "";
        private string configFile = string.Empty;

        public Form1()
        {
            InitializeComponent();
            this.tabControl1.SelectedIndex = 2;
            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            worker1 = new BackgroundWorker();
            worker1.DoWork += new DoWorkEventHandler(worker_DoWork1);
            worker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted1);

            worker2 = new BackgroundWorker();
            worker2.DoWork += new DoWorkEventHandler(worker_DoWork2);
            worker2.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted2);

            ReadInitInfo();
        }

        public void ReadInitInfo()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + Application.ProductName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            configFile = path + Path.DirectorySeparatorChar + "Data.ini";
            if (!File.Exists(configFile))
            {
                File.CreateText(configFile);
            }
            else 
            {
                defaultImagesfilePath = Ini.IniReadValue("DefaultPath", "defaultImagesfilePath", configFile);
                textBox1.Text = defaultImagesfilePath;
                defaultExportfilePath = Ini.IniReadValue("DefaultPath", "defaultExportfilePath", configFile);
                textBox2.Text = defaultExportfilePath;
            }
        }


        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            //首次defaultfilePath为空，按FolderBrowserDialog默认设置（即桌面）选择   
            if (defaultImagesfilePath != "")
            {
                //设置此次默认目录为上一次选中目录   
                dialog.SelectedPath = defaultImagesfilePath;
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //记录选中的目录   
                defaultImagesfilePath = dialog.SelectedPath;
                textBox1.Text = defaultImagesfilePath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            //首次defaultfilePath为空，按FolderBrowserDialog默认设置（即桌面）选择   
            if (defaultExportfilePath != "")
            {
                //设置此次默认目录为上一次选中目录   
                dialog.SelectedPath = defaultExportfilePath;
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //记录选中的目录   
                defaultExportfilePath = dialog.SelectedPath;
                textBox2.Text = defaultExportfilePath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            worker.RunWorkerAsync();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripStatusLabel1.Text = "处理完成";
            button3.Enabled = true;
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            button3.Enabled = false;
            HandleImages(defaultImagesfilePath, defaultExportfilePath);           
        }


        public void HandleImages(string inputDir, string outDir)
        {
            int    textSize = 32;
            string textStyle = "Arial";
            string[] files = Directory.GetFiles(inputDir);
             if (files.Length > 0)
             {
                 for (int i = 0; i < files.Length; i++)
                 {
                     FileInfo fi = new FileInfo(files[i]);
                     if (fi.Extension.ToUpper().Equals(".JPG"))
                     {
                         toolStripStatusLabel1.Text = "正在处理" + fi.FullName;
                         Bitmap PictFromFile = (Bitmap)Bitmap.FromFile(fi.FullName);
                         string txt = "Model: " + Path.GetFileNameWithoutExtension(fi.Name);
                         Graphics g = this.CreateGraphics();
                         SizeF sizeF = g.MeasureString(txt, new Font(textStyle, textSize));
                         Size size = PictFromFile.Size;
                         int left = (int)size.Width - (int)sizeF.Width;
                         int top = size.Height - (int)sizeF.Height;
                         Bitmap newImage = ImageHandle.ImageSetText(PictFromFile, txt, textStyle, textSize, Color.Black, left, top);
                         string newFile = outDir + Path.DirectorySeparatorChar + fi.Name;
                         ImageHandle.BmpSave(newImage, newFile);
                         newImage.Dispose();
                     }
                 }
             }
        }



        

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            worker.Dispose();
            worker1.Dispose();
            worker2.Dispose();
            Ini.IniWriteValue("DefaultPath", "defaultImagesfilePath", defaultImagesfilePath, configFile);
            Ini.IniWriteValue("DefaultPath", "defaultExportfilePath", defaultExportfilePath, configFile);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(defaultImagesfilePath))
            {
                return;
            }
            System.Diagnostics.Process.Start(defaultImagesfilePath);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(defaultExportfilePath))
            {
                return;
            }
            System.Diagnostics.Process.Start(defaultExportfilePath);
        }


        

        private void button6_Click(object sender, EventArgs e)
        {
            string SelectFolder = textBox1.Text;
            if (string.IsNullOrEmpty(SelectFolder))
            {
                toolStripStatusLabel1.Text = "图片文件夹不能为空！";
                return;
            }
            string ExprotFolder = textBox2.Text;
            if (string.IsNullOrEmpty(ExprotFolder))
            {
                toolStripStatusLabel1.Text = "导出文件夹不能为空！";
                return;
            }
            if (string.IsNullOrEmpty(tb1PicWidth.Text))
            {
                toolStripStatusLabel1.Text = "图片宽不能为空！";
                return;
            }
            if (string.IsNullOrEmpty(tb1PicHeigh.Text))
            {
                toolStripStatusLabel1.Text = "图片高不能为空！";
                return;
            }
            if (string.IsNullOrEmpty(tb1PicOfRow.Text))
            {
                toolStripStatusLabel1.Text = "一行放几张图片不能为空！";
                return;
            }
            tb1BtnFlag = Flag_RunHtml;
            button6.Enabled = false;            
            worker1.RunWorkerAsync();
        }


        private int tb1BtnFlag = 0;
        private int Flag_RunHtml = 1;
        private int Flag_RunImage = 2;
        private void button8_Click(object sender, EventArgs e)
        {
            string SelectFolder = textBox1.Text;
            if (string.IsNullOrEmpty(SelectFolder))
            {
                toolStripStatusLabel1.Text = "图片文件夹不能为空！";
                return;
            }
            string ExprotFolder = textBox2.Text;
            if (string.IsNullOrEmpty(ExprotFolder))
            {
                toolStripStatusLabel1.Text = "导出文件夹不能为空！";
                return;
            }
            if (string.IsNullOrEmpty(tb1PicWidth.Text))
            {
                toolStripStatusLabel1.Text = "图片宽不能为空！";
                return;
            }
            if (string.IsNullOrEmpty(tb1PicHeigh.Text))
            {
                toolStripStatusLabel1.Text = "图片高不能为空！";
                return;
            }
            if (string.IsNullOrEmpty(tb1PicOfRow.Text))
            {
                toolStripStatusLabel1.Text = "一行放几张图片不能为空！";
                return;
            }
            tb1BtnFlag = Flag_RunImage;
            button8.Enabled = false;       
            worker1.RunWorkerAsync();
        }

        void worker_DoWork1(object sender, DoWorkEventArgs e)
        {
            toolStripStatusLabel1.Text = "正在生成网页";
            string SelectFolder = textBox1.Text;
            string ExprotFolder = textBox2.Text;
            int width = Convert.ToInt32(tb1PicWidth.Text);
            int height = Convert.ToInt32(tb1PicHeigh.Text);
            int picOfRow = Convert.ToInt32(tb1PicOfRow.Text);
            string html = HtmlHandle.GenreateHtml(SelectFolder,picOfRow, width, height);
            string ExportFile = ExprotFolder + Path.DirectorySeparatorChar + new FileInfo(SelectFolder).Name + ".html";
            string ExportImage = ExprotFolder + Path.DirectorySeparatorChar + new FileInfo(SelectFolder).Name + ".jpg";
            TextWriter tw = new StreamWriter(ExportFile);
            tw.WriteLine(html);
            tw.Close();
            if (tb1BtnFlag != Flag_RunImage)
            {
                return;
            }
            toolStripStatusLabel1.Text = "正在生成图片";
            int pageWitdh = width * picOfRow + 40;
            int filesCount = Directory.GetFiles(SelectFolder).Length;
            int pageHeight = (filesCount / picOfRow + 1) * (height + 20);
            Bitmap bmp = HtmlToImg.GetHtmlToImg(ExportFile, pageWitdh, pageHeight);
            bmp.Save(ExportImage, ImageFormat.Jpeg);
        }

        void worker_RunWorkerCompleted1(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripStatusLabel1.Text = "处理完成";
            button6.Enabled = true;
            tb1BtnFlag = 0;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string SelectFolder = textBox1.Text;
            if (string.IsNullOrEmpty(SelectFolder))
            {
                toolStripStatusLabel1.Text = "图片文件夹不能为空！";
                return;
            }
            string ExprotFolder = textBox2.Text;
            if (string.IsNullOrEmpty(ExprotFolder))
            {
                toolStripStatusLabel1.Text = "导出文件夹不能为空！";
                return;
            }
            if (string.IsNullOrEmpty(tb3PicWidth.Text))
            {
                toolStripStatusLabel1.Text = "图片宽不能为空！";
                return;
            }
            if (string.IsNullOrEmpty(tb3PicHeigh.Text))
            {
                toolStripStatusLabel1.Text = "图片高不能为空！";
                return;
            }
            if (string.IsNullOrEmpty(tb3PicOfRow.Text))
            {
                toolStripStatusLabel1.Text = "一行放几张图片不能为空！";
                return;
            }
            button7.Enabled = false;
            worker2.RunWorkerAsync();
        }

        void worker_DoWork2(object sender, DoWorkEventArgs e)
        {
            toolStripStatusLabel1.Text = "正在生成网页";
            string SelectFolder = textBox1.Text;
            string ExprotFolder = textBox2.Text;
            int width = Convert.ToInt32(tb3PicWidth.Text);
            int height = Convert.ToInt32(tb3PicHeigh.Text);
            int picOfRow = Convert.ToInt32(tb3PicOfRow.Text);
            Dictionary<string, List<string>> dic = GetImageList(SelectFolder);
            foreach (string styleName in dic.Keys) 
            {
                List<string> styleList = dic[styleName];
                if (styleList == null || styleList.Count <= 0) 
                {
                    continue;
                }

                Bitmap bmp = ImageHandle.GenNewImage(styleList, picOfRow, width, height);
                string ExportImage = ExprotFolder + Path.DirectorySeparatorChar + styleName + ".jpg";
                ImageHandle.BmpSave(bmp, ExportImage);           
                bmp.Dispose();
            }
        }

        static Dictionary<string, List<string>> GetImageList(string SelectFolder)
        {
            string[] jpgFiles = Directory.GetFiles(SelectFolder);
            Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
            foreach (string jpg in jpgFiles)
            {
                FileInfo fi = new FileInfo(jpg);
                if (fi.Extension.ToUpper().Equals(".JPG"))
                {
                    string name = Path.GetFileNameWithoutExtension(fi.Name);
                    string styleName = string.Empty;
                    if (name.Split(' ').Length > 1) 
                    { 
                        styleName =  name.Split(' ')[0];
                    }
                    else
                    {
                        styleName =  name.Split('-')[0];                    
                    }
                    
                    if (!dic.Keys.Contains<string>(styleName))
                    {
                        List<string> styleList = new List<string>();
                        styleList.Add(fi.FullName);
                        dic.Add(styleName, styleList);
                    }
                    else
                    {
                        List<string> styleList = dic[styleName];
                        styleList.Add(fi.FullName);
                    }
                }
            }
            return dic;
        }

        void worker_RunWorkerCompleted2(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripStatusLabel1.Text = "处理完成";
            button7.Enabled = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string SelectFolder = textBox1.Text;
            if (string.IsNullOrEmpty(SelectFolder))
            {
                toolStripStatusLabel1.Text = "图片文件夹不能为空！";
                return;
            }
            string ExprotFolder = textBox2.Text;
            if (string.IsNullOrEmpty(ExprotFolder))
            {
                toolStripStatusLabel1.Text = "导出文件夹不能为空！";
                return;
            }
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork3);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted3);
            worker.RunWorkerAsync();
            worker.Dispose();
        }


        void worker_DoWork3(object sender, DoWorkEventArgs e)
        {
            button9.Enabled = false;
            toolStripStatusLabel1.Text = "开始筛选";
            string SelectFolder = textBox1.Text;
            string ExprotFolder = textBox2.Text;
            Dictionary<string, List<string>> dic = GetImageList(SelectFolder);
            foreach (string styleName in dic.Keys)
            {
                List<string> styleList = dic[styleName];
                if (styleList == null || styleList.Count <= 0)
                {
                    continue;
                }
                string firstName = styleList[0];
                FileInfo fi = new FileInfo(firstName);
                toolStripStatusLabel1.Text = "正在拷贝 " + fi.FullName;
                string ExportImage = ExprotFolder + Path.DirectorySeparatorChar + styleName + ".jpg";
                fi.CopyTo(ExportImage);
            }
        }

        void worker_RunWorkerCompleted3(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripStatusLabel1.Text = "处理完成";
            button9.Enabled = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string SelectFolder = textBox1.Text;
            if (string.IsNullOrEmpty(SelectFolder))
            {
                toolStripStatusLabel1.Text = "图片文件夹不能为空！";
                return;
            }
            string ExprotFolder = textBox2.Text;
            if (string.IsNullOrEmpty(ExprotFolder))
            {
                toolStripStatusLabel1.Text = "导出文件夹不能为空！";
                return;
            }
            if (string.IsNullOrEmpty(tb4PicWidth.Text))
            {
                toolStripStatusLabel1.Text = "图片宽不能为空！";
                return;
            }
            if (string.IsNullOrEmpty(tb4PicHeight.Text))
            {
                toolStripStatusLabel1.Text = "图片高不能为空！";
                return;
            }

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork4);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted4);
            worker.RunWorkerAsync();
            worker.Dispose();
        }

        void worker_DoWork4(object sender, DoWorkEventArgs e)
        {
            string SelectFolder = textBox1.Text;
            string ExprotFolder = textBox2.Text;
            int w = Convert.ToInt32(tb4PicWidth.Text);
            int h = Convert.ToInt32(tb4PicHeight.Text);
            button10.Enabled = false;
            toolStripStatusLabel1.Text = "开始压缩";
            string[] files = Directory.GetFiles(SelectFolder);
            if (files.Length > 0)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    FileInfo fi = new FileInfo(files[i]);
                    if (fi.Extension.ToUpper().Equals(".JPG"))
                    {
                        toolStripStatusLabel1.Text = "正在处理" + fi.FullName;
                        Bitmap PictFromFile = (Bitmap)Bitmap.FromFile(fi.FullName);
                        Bitmap newImage = ImageHandle.ResizeImage(PictFromFile, w, h);
                        string newFile = ExprotFolder + Path.DirectorySeparatorChar + fi.Name;
                        newImage.Save(newFile);
                        newImage.Dispose();
                    }
                }
            }
        }

        void worker_RunWorkerCompleted4(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripStatusLabel1.Text = "处理完成";
            button10.Enabled = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string SelectFolder = textBox1.Text;
            if (string.IsNullOrEmpty(SelectFolder))
            {
                toolStripStatusLabel1.Text = "图片文件夹不能为空！";
                return;
            }
            string ExprotFolder = textBox2.Text;
            if (string.IsNullOrEmpty(ExprotFolder))
            {
                toolStripStatusLabel1.Text = "导出文件夹不能为空！";
                return;
            }
            string webUrl = tb5OfPicTxt.Text;
            if (string.IsNullOrEmpty(webUrl))
            {
                toolStripStatusLabel1.Text = "网站地址不能为空！";
                return;
            }
            string tb5OfWidthValue = tb5OfWidth.Text;
            if (string.IsNullOrEmpty(tb5OfWidthValue))
            {
                toolStripStatusLabel1.Text = "要截取的网页宽度不能为空！";
                return;
            }

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork5);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted5);
            worker.RunWorkerAsync();
            worker.Dispose();
        }

        void worker_DoWork5(object sender, DoWorkEventArgs e)
        {
            toolStripStatusLabel1.Text = "开始快照网页为图片！";
            tb5OfPicTxt.Enabled = false;
            tb5OfWidth.Enabled = false;
            button10.Enabled = false;
            string SelectFolder = textBox1.Text;
            string ExprotFolder = textBox2.Text;
            string webUrl = tb5OfPicTxt.Text.ToLower();
            int tb5OfWidthValue = Convert.ToInt32(tb5OfWidth.Text);
            if (!webUrl.StartsWith("http://"))
            {
                webUrl = "http://" + webUrl;
            }
            string ExportImage = ExprotFolder + Path.DirectorySeparatorChar + "TEMP.jpg";
            Bitmap bmp = HtmlToImg.GetHtmlToImg(webUrl, tb5OfWidthValue, 5000);
            ImageHandle.BmpSave(bmp, ExportImage);
            bmp.Dispose();
        }

        void worker_RunWorkerCompleted5(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripStatusLabel1.Text = "快照网页为图片处理完成!";
            tb5OfPicTxt.Enabled = true;
            tb5OfWidth.Enabled = true;
            button10.Enabled = true;
        }
        
    }
}
