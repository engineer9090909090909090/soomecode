using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ImagesTools1
{
    class HtmlToImg
    {
        Bitmap m_Bitmap;

        string m_Url;

        int m_BrowserWidth, m_BrowserHeight;

        public HtmlToImg(string Url, int BrowserWidth, int BrowserHeight)
        {
            m_Url = Url;
            m_BrowserHeight = BrowserHeight;
            m_BrowserWidth = BrowserWidth;
        }

        public static Bitmap GetHtmlToImg(string Url, int BrowserWidth, int BrowserHeight)
        {
            HtmlToImg thumbnailGenerator = new HtmlToImg(Url, BrowserWidth, BrowserHeight);
            return thumbnailGenerator.GenerateHtmlToImgImage();
        }

        public Bitmap GenerateHtmlToImgImage()
        {

            Thread m_thread = new Thread(new ThreadStart(_GenerateHtmlToImgImage));
            m_thread.SetApartmentState(ApartmentState.STA);
            m_thread.Start();
            m_thread.Join();
            return m_Bitmap;
        }

        private void _GenerateHtmlToImgImage()
        {

            WebBrowser m_WebBrowser = new WebBrowser();
            m_WebBrowser.ScrollBarsEnabled = false;
            m_WebBrowser.Navigate(m_Url);
            m_WebBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WebBrowser_DocumentCompleted);
            while (m_WebBrowser.ReadyState != WebBrowserReadyState.Complete)
            Application.DoEvents();
            m_WebBrowser.Dispose();
        }

        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser m_WebBrowser = (WebBrowser)sender;
            m_BrowserHeight = m_WebBrowser.Document.Body.ScrollRectangle.Height;//document完整高度
            m_WebBrowser.ClientSize = new Size(this.m_BrowserWidth, this.m_BrowserHeight);           
            m_WebBrowser.ScrollBarsEnabled = true;
            m_Bitmap = new Bitmap(m_WebBrowser.Bounds.Width, m_WebBrowser.Bounds.Height);
            m_WebBrowser.BringToFront();
            m_WebBrowser.DrawToBitmap(m_Bitmap, m_WebBrowser.Bounds);
            //m_Bitmap = (Bitmap)m_Bitmap.GetThumbnailImage(m_ThumbnailWidth, m_ThumbnailHeight, null, IntPtr.Zero);
        }


    }
}
