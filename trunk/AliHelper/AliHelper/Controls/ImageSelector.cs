using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AliHelper
{
    public partial class ImageSelector : UserControl
    {
        private string _ImageFile;
        public ImageSelector()
        {
            InitializeComponent();
        }

        public bool ShowMenu { set; get; }

        public Image Image
        {
            get
            {
                if (string.IsNullOrEmpty(this._ImageFile))
                {
                    return null;
                }
                else
                {
                    return Image.FromFile(this._ImageFile);
                }
            }
            set 
            {
                this.BackgroundImage = value;
            }
        }

        public string ImageFile
        {
            get
            {
                return this._ImageFile;
            }
            set
            {
                try
                {
                    this.BackgroundImage = ImageUtils.ResizeImage(value, Width, Height);
                    this._ImageFile = value;
                }
                catch
                {
                    this.BackgroundImage = AliHelper.Properties.Resources.sms_image;
                    this._ImageFile = string.Empty;
                }
            }
        }

        public string OpenFileDirectory { set; get; }
        
        private void FileSelectMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (string.IsNullOrEmpty(OpenFileDirectory))
            {
                openFileDialog.InitialDirectory = "c:\\";
            }
            else {
                openFileDialog.InitialDirectory = OpenFileDirectory;
            }
            openFileDialog.Filter = "图片文件|*.*|JPG文件|*.jpg|PNG文件|*.png";
            openFileDialog.Multiselect = false;
            openFileDialog.RestoreDirectory = false;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                
                this.ImageFile = openFileDialog.FileName;
                OpenFileDirectory = new FileInfo(this.ImageFile).DirectoryName;
            }
        }

        private void CutBroadMenuItem_Click(object sender, EventArgs e)
        {
            IDataObject data = System.Windows.Forms.Clipboard.GetDataObject();
            if (data.GetDataPresent(typeof(Bitmap)))
            {
                Image photo = (Image)data.GetData(typeof(Bitmap));
                string tmpImage = FileUtils.GetNewTempImagePath();
                photo.Save(tmpImage);
                photo.Dispose();
                this.ImageFile = tmpImage;
            }
        }

        private void ScreenshotMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void CleanMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = AliHelper.Properties.Resources.sms_image;
            this.ImageFile = string.Empty;
        }

        void f_Click(object sender, EventArgs e)
        {
            Form f = (Form)sender;
            f.Close();
        }

        private void ImageSelector_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(this._ImageFile))
                return;
            Image image = Image.FromFile(this._ImageFile);
            Form f = new Form();
            f.Width = image.Width;
            f.Height = image.Height;
            f.ShowInTaskbar = false;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Click += new EventHandler(f_Click);
            f.BackgroundImage = image;
            f.ShowDialog(this);
        }

        private void ImageSelector_MouseDown(object sender, MouseEventArgs e)
        {
            if (ShowMenu && e.Button == MouseButtons.Right)
            {
                this.CutBroadMenuItem.Enabled = false;
                this.CleanMenuItem.Enabled = false;
                IDataObject data = System.Windows.Forms.Clipboard.GetDataObject();
                if (data.GetDataPresent(typeof(Bitmap)))
                {
                    this.CutBroadMenuItem.Enabled = true;
                }
                if (!string.IsNullOrEmpty(this.ImageFile))
                {
                    this.CleanMenuItem.Enabled = true;
                }

                this.contextMenuStrip1.Show();
            }
        }
    }
}
