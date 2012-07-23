using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ImagesTools1
{
    class HtmlHandle
    {
        static public string GenreateHtml(string SelectFolder, int picOfRow, int width, int height)
        {
            string[] files = Directory.GetFiles(SelectFolder);
            return GenreateHtml(files.ToList<string>(), picOfRow, width, height);
        }

        static public string GenreateHtml(List<string> files, int picOfRow, int width, int height)
        {
            string html = string.Empty;
            int pageWitdh = width * picOfRow + 20 * picOfRow;
            html = html + "<!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Transitional//EN\" \"http:\\/\\/www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n";
            html = html + "<html xmlns=\"http:\\/\\/www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /></head>\r\n<body>";
            html = html + "<table width=\"" + pageWitdh + "\" border=\"0\">";
            if (files != null  && files.Count > 0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    FileInfo fi = new FileInfo(files[i]);
                    if (fi.Extension.ToUpper().Equals(".JPG"))
                    {
                        if (i % picOfRow == 0)
                        {
                            html = html + "\r\n<tr>";
                        }

                        html = html + "\r\n<td><img src=\"" + fi.FullName.Replace("\\", "/")
                            + "\" width=\"" + width + "\" height=\"" + height + "\" /><div align=\"center\"><b>"
                            + Path.GetFileNameWithoutExtension(fi.Name) + "</div></b><br/><br/></td>";

                        if (i % picOfRow == (picOfRow - 1))
                        {
                            html = html + "\r\n</tr>";
                        }
                    }

                }

            }
            html = html + "</table>";
            html = html + "</body></html>";

            return html;
        }


    }
}
