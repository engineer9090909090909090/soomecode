using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Cells;
using System.Drawing;
using System.Reflection;

namespace AliHelper
{
    public class ExportExcel :IDisposable
    {
        private Dictionary<string, string> dic = new Dictionary<string, string>();
        public ExportExcel()
        { 

        }
        public void AddColumn(string ColumnName, string ColumnText)
        {
            dic.Add(ColumnName, ColumnText);
        }

        public void ExportToExcel<T>(List<T> list, string path)
        {
            Workbook workbook = new Workbook(); //工作簿 
            Worksheet sheet = workbook.Worksheets[0]; //工作表 
            Cells cells = sheet.Cells;//单元格 

            //为标题设置样式     
            Style styleTitle = workbook.Styles[workbook.Styles.Add()];//新增样式 
            styleTitle.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            styleTitle.Font.Name = "宋体";//文字字体 
            styleTitle.Font.Size = 18;//文字大小 
            styleTitle.Font.IsBold = true;//粗体 

            //样式2 
            Style style2 = workbook.Styles[workbook.Styles.Add()];//新增样式 
            style2.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            style2.Font.Name = "宋体";//文字字体 
            style2.Font.Size = 12;//文字大小 
            style2.Font.IsBold = true;//粗体 
            style2.BackgroundColor = Color.DeepPink;
            style2.IsTextWrapped = true;//单元格内容自动换行 
            style2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            //样式3 
            Style style3 = workbook.Styles[workbook.Styles.Add()];//新增样式 
            style3.HorizontalAlignment = TextAlignmentType.Left;//文字居中 
            style3.Font.Name = "宋体";//文字字体 
            style3.Font.Size = 10;//文字大小 
            style3.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Dotted;
            style3.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Dotted;
            style3.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Dotted;
            style3.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Dotted;

            int Rownum = list.Count;//表格行数
            //生成行1 列名行 
            int index = 0;
            foreach(string key in dic.Keys)
            {
                string columnText = dic[key];
                cells[0, index].PutValue(columnText);
                cells[0, index].SetStyle(style2);
                cells.SetColumnWidth(index, 20);
                index = index + 1;
            }
            cells.SetRowHeight(0, 25);

            //生成数据行 
            for (int i = 1; i <= Rownum; i++)
            {
                T model = list[i - 1];
                Type type = model.GetType();
                index = 0;
                foreach (string key in dic.Keys)
                {
                    PropertyInfo prop = type.GetProperty(key);
                    object val = string.Empty;
                    if (prop != null)
                    {
                        val = prop.GetValue(model, null);
                    }
                    if (prop.Name.IndexOf("Amount") > -1)
                    {
                        string v = ((double)val).ToString("#,##0.00");
                        cells[i, index].PutValue(v);
                        style3.HorizontalAlignment = TextAlignmentType.Right;
                        cells[i, index].SetStyle(style3);
                    }
                    else 
                    {
                        style3.HorizontalAlignment = TextAlignmentType.Left; 
                        cells[i, index].PutValue(val);
                        cells[i, index].SetStyle(style3);
                    }
                    
                    index = index + 1;
                }
                cells.SetRowHeight(i, 24);
            }
            workbook.Save(path);
        }

        public void Dispose()
        {
            this.dic.Clear();
            this.dic = null;
        }
    }
}
