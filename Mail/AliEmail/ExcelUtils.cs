using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;

namespace AliEmail
{
    public class ExcelUtils
    {
        private string strConn;

        private System.Windows.Forms.OpenFileDialog openFileDlg = new System.Windows.Forms.OpenFileDialog();
        private System.Windows.Forms.SaveFileDialog saveFileDlg = new System.Windows.Forms.SaveFileDialog();

        public ExcelUtils()
        {
            //                                                                                                                                                                   
            // TODO: 在此处添加构造函数逻辑                                                                                                                                      
            //                                                                                                                                                                   
            this.openFileDlg.DefaultExt = "xls";
            this.openFileDlg.Filter = "Excel文件 (*.xls)|*.xls";

            this.saveFileDlg.DefaultExt = "xls";
            this.saveFileDlg.Filter = "Excel文件 (*.xls)|*.xls";

        }

        /// <summary>                                                                                                                                                        
        /// 从选择的Excel文件导入                                                                                                                                                
        /// </summary>                                                                                                                                                           
        /// <returns>DataSet</returns>                                                                                                                                           
        public DataSet ImportFromExcel()
        {
            DataSet ds = new DataSet();
            if (openFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                ds = doImport(openFileDlg.FileName);
            return ds;
        }
        /**/
        /// <summary>                                                                                                                                                        
        /// 从指定的Excel文件导入                                                                                                                                                
        /// </summary>                                                                                                                                                           
        /// <param name="strFileName">Excel文件名</param>                                                                                                                        
        /// <returns></returns>                                                                                                                                                  
        public DataSet ImportFromExcel(string strFileName)
        {
            DataSet ds = new DataSet();
            ds = doImport(strFileName);
            return ds;
        }

        /// <summary>                                                                                                                                                        
        /// 执行导入                                                                                                                                                             
        /// </summary>                                                                                                                                                           
        /// <param name="strFileName">文件名</param>                                                                                                                             
        /// <returns>DataSet</returns>                                                                                                                                           
        private DataSet doImport(string strFileName)
        {
            if (strFileName == "") return null;

            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                "Data Source=" + strFileName + ";" +
                "Extended Properties=Excel 8.0;";
            OleDbDataAdapter ExcelDA = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", strConn);

            DataSet ExcelDs = new DataSet();
            try
            {
                ExcelDA.Fill(ExcelDs, "ExcelInfo");

            }
            catch (Exception err)
            {
                System.Console.WriteLine(err.ToString());
            }
            return ExcelDs;
        }
                                                                                                                      
        /// <summary>                                                                                                                                                        
        /// 导出指定的Excel文件                                                                                                                                                  
        /// </summary>                                                                                                                                                           
        /// <param name="ds">要导出的DataSet</param>                                                                                                                             
        /// <param name="strExcelFileName">要导出的Excel文件名</param>                                                                                                           
        public void ExportToExcel(DataSet ds, string strExcelFileName)
        {
            if (ds.Tables.Count == 0 || strExcelFileName == "") return;
            doExport(ds, strExcelFileName);


        }

        /// <summary>                                                                                                                                                        
        /// 导出用户选择的Excel文件                                                                                                                                              
        /// </summary>                                                                                                                                                           
        /// <param name="ds">DataSet</param>                                                                                                                                     
        public void ExportToExcel(DataSet ds)
        {
            if (saveFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                doExport(ds, saveFileDlg.FileName);

        }

        /// <summary>                                                                                                                                                        
        /// 执行导出                                                                                                                                                             
        /// </summary>                                                                                                                                                           
        /// <param name="ds">要导出的DataSet</param>                                                                                                                             
        /// <param name="strExcelFileName">要导出的文件名</param>                                                                                                                
        private void doExport(DataSet ds, string strExcelFileName)
        {

            Application excel = new Application();
            int rowIndex = 1;
            int colIndex = 0;
            excel.Application.Workbooks.Add(true);
            System.Data.DataTable table = ds.Tables[0];
            foreach (DataColumn col in table.Columns)
            {
                colIndex++;
                excel.Cells[1, colIndex] = col.ColumnName;
            }

            foreach (DataRow row in table.Rows)
            {
                rowIndex++;
                colIndex = 0;
                foreach (DataColumn col in table.Columns)
                {
                    colIndex++;
                    excel.Cells[rowIndex, colIndex] = row[col.ColumnName].ToString();
                }
            }
            excel.Visible = false;
            excel.ActiveWorkbook.SaveAs(strExcelFileName, XlFileFormat.xlExcel7, null, null, false, false, XlSaveAsAccessMode.xlNoChange, null, null, null, null);                                                                                                                                   
            excel.Quit();
            excel = null;
            GC.Collect();//垃圾回收                                                                                                                                              
        }

        /// <summary>                                                                                                                                                        
        /// 从选择的XML文件导入                                                                                                                                                  
        /// </summary>                                                                                                                                                           
        /// <returns>DataSet</returns>                                                                                                                                           
        public DataSet ImportFromXML()
        {
            DataSet ds = new DataSet();
            System.Windows.Forms.OpenFileDialog openFileDlg = new System.Windows.Forms.OpenFileDialog();
            openFileDlg.DefaultExt = "xml";
            openFileDlg.Filter = "xml文件 (*.xml)|*.xml";
            if (openFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                try { ds.ReadXml(openFileDlg.FileName, System.Data.XmlReadMode.ReadSchema); }
                catch { }
            return ds;
        }

        /// <summary>                                                                                                                                                        
        /// 从指定的XML文件导入                                                                                                                                                  
        /// </summary>                                                                                                                                                           
        /// <param name="strFileName">XML文件名</param>                                                                                                                          
        /// <returns></returns>                                                                                                                                                  
        public DataSet ImportFromXML(string strFileName)
        {
            if (strFileName == "")
                return null;
            DataSet ds = new DataSet();
            try { ds.ReadXml(strFileName, System.Data.XmlReadMode.ReadSchema); }
            catch { }
            return ds;
        }



        /// <summary>                                                                                                                                                        
        /// 导出指定的XML文件                                                                                                                                                    
        /// </summary>                                                                                                                                                           
        /// <param name="ds">要导出的DataSet</param>                                                                                                                             
        /// <param name="strXMLFileName">要导出的XML文件名</param>                                                                                                               
        public void ExportToXML(DataSet ds, string strXMLFileName)
        {
            if (ds.Tables.Count == 0 || strXMLFileName == "") return;
            doExportXML(ds, strXMLFileName);
        }

        /// <summary>                                                                                                                                                        
        /// 导出用户选择的XML文件                                                                                                                                                
        /// </summary>                                                                                                                                                           
        /// <param name="ds">DataSet</param>                                                                                                                                     
        public void ExportToXML(DataSet ds)
        {
            System.Windows.Forms.SaveFileDialog saveFileDlg = new System.Windows.Forms.SaveFileDialog();
            saveFileDlg.DefaultExt = "xml";
            saveFileDlg.Filter = "xml文件 (*.xml)|*.xml";
            if (saveFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                doExportXML(ds, saveFileDlg.FileName);
        }


        /// <summary>                                                                                                                                                        
        /// 执行导出                                                                                                                                                             
        /// </summary>                                                                                                                                                           
        /// <param name="ds">要导出的DataSet</param>                                                                                                                             
        /// <param name="strExcelFileName">要导出的XML文件名</param>                                                                                                             
        private void doExportXML(DataSet ds, string strXMLFileName)
        {
            try
            { ds.WriteXml(strXMLFileName, System.Data.XmlWriteMode.WriteSchema); }
            catch (Exception ex)
            { System.Windows.Forms.MessageBox.Show(ex.Message, "Errol"); }
        }

    }


}
