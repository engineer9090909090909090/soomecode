using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ImagesTools1
{
    class ImageHandle
    {
        public static Bitmap ImageSetText(Bitmap b, string txt, string TextFont, int TextSize, Color c, int x, int y)
        {
            if (b == null)
            {
                return null;
            }

            Graphics g = Graphics.FromImage(b);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            FontFamily fm = new FontFamily(TextFont);
            Font font = new Font(fm, TextSize, FontStyle.Regular, GraphicsUnit.Pixel);
            SolidBrush sb = new SolidBrush(c);

            g.DrawString(txt, font, sb, new PointF(x, y));
            g.Dispose();

            return b;
        }

        public static Bitmap GenNewImage(List<string> files, int colNumber, int width, int height)
        {
            string TextFont = "Arial";
            int TextSize = 18;
            Color c = Color.Black;
            int fileCount = files.Count;
            int rowNumber = fileCount % colNumber == 0 ? fileCount / colNumber : (fileCount / colNumber + 1);
            int newImageWidth = (width + 20) * colNumber + 20;
            int newImageHeight = (height + 20) * rowNumber + 20;
            Bitmap newImg = new Bitmap(newImageWidth, newImageHeight);
            Graphics g = Graphics.FromImage(newImg);
            g.Clear(Color.White);
            for (int i = 0; i < files.Count; i++)
            {
                int currentRow = (int)i / colNumber;
                int currentCol = i % colNumber;
                FileInfo fi = new FileInfo(files[i]);
                string txt = "Model: "+Path.GetFileNameWithoutExtension(fi.Name);
                Image img1 = Image.FromFile(fi.FullName);
                
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //g.DrawImageUnscaled(img1, (width + 20) * currentCol + 20, (height + 20) * currentRow + 20, width, height);
                g.DrawImage(img1, (width + 20) * currentCol + 20, (height + 20) * currentRow + 20, width, height);
                SizeF sizeF = g.MeasureString(txt, new Font(TextFont, TextSize));
                int x = (width + 20) * currentCol + (int)(width + 20 - sizeF.Width);
                int y = (height + 20) * currentRow + height - 40;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                FontFamily fm = new FontFamily(TextFont);
                Font font = new Font(fm, TextSize, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush sb = new SolidBrush(c);
                g.DrawString(txt, font, sb, new PointF(x, y));
            
            } 
            g.Dispose();
            return newImg;
        }

        public static void BmpSave(Bitmap newBmp, string outFile)
        {
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;

            //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象。
            ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo jpegICI = null;
            for (int x = 0; x < arrayICI.Length; x++)
            {
                if (arrayICI[x].FormatDescription.Equals("JPEG"))
                {
                    jpegICI = arrayICI[x];//设置JPEG编码
                    break;
                }
            }

            if (jpegICI != null)
            {
                newBmp.Save(outFile, jpegICI, encoderParams);
            }
            else
            {
                newBmp.Save(outFile, ImageFormat.Jpeg);
            }
            newBmp.Dispose();
        
        }

        public static Bitmap ResizeImage(Bitmap bmp, int newW, int newH)
        {
            try
            {
                Bitmap b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }
    }
}
