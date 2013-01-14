using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace AliHelper
{
    public partial class CheckCodeForm : Form
    {
        public string CheckCode;
        public CheckCodeForm()
        {
            InitializeComponent();
        }
        private void CheckCodeForm_Load(object sender, EventArgs e)
        {
            this.CheckImage.Image = GetWebCheckCodeImage();
        }
        private void CheckImage_Click(object sender, EventArgs e)
        {
            this.CheckImage.Image = GetWebCheckCodeImage();
        }

        private Image GetWebCheckCodeImage()
        {
            string url = ShareCookie.Instance.CheckCodeUrl + "&t=" + DateUtils.DateTimeToInt(DateTime.Now);
            Uri uri = new Uri(url);
            WebRequest myReq = WebRequest.Create(uri);
            WebResponse result = myReq.GetResponse();
            Stream receviceStream = result.GetResponseStream();
            Image CheckCodeImge = Image.FromStream(receviceStream);
            receviceStream.Close();
            result.Close();
            return CheckCodeImge;
        }
       
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            this.CheckCode = this.txtCheckCode.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void txtCheckCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnConfirm_Click(sender, e);
            }
        }

        

        
    }
}
