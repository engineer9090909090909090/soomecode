using System;
using System.IO;
using System.Threading;

namespace AliHelper
{
    /// <summary>
    /// 日志类(常用的都是用log4net，这里简陋地实现一个写入文本日志类)
    /// </summary>
    public static class Logger
    {
        private static Object objSyncRoot = new Object();
        /// <summary>
        /// 写入异常日志
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteFileLog(string exMsg, string path)
        {
            FileStream fs = null;
            StreamWriter m_streamWriter = null;
            try
            {
                bool isOk = Monitor.TryEnter(objSyncRoot, 2000);
                if (isOk == false)
                {
                    return;
                }
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                path = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                m_streamWriter = new StreamWriter(fs);
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                m_streamWriter.WriteLine(DateTime.Now.ToString() + Environment.NewLine);
                m_streamWriter.WriteLine("-----------------------------------------------------------");
                m_streamWriter.WriteLine("-----------------------------------------------------------");
                m_streamWriter.WriteLine(exMsg);
                m_streamWriter.WriteLine("-----------------------------------------------------------");
                m_streamWriter.WriteLine("-----------------------------------------------------------");
                m_streamWriter.Flush();
            }
            finally
            {
                if (m_streamWriter != null)
                {
                    m_streamWriter.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
                Monitor.Exit(objSyncRoot);
            }
        }

        public static void WriteFileLog(Exception ex, string path)
        {
            string exMsg = CreateExMsg(ex);
            if (string.IsNullOrEmpty(exMsg) == false)
            {
                WriteFileLog(exMsg, path);
            }
        }

        private static string CreateExMsg(Exception ex)
        {
            if (ex == null)
            {
                return string.Empty;
            }
            string error = string.Format("引发异常的方法：{0}{1}错误信息：{2}{3}错误堆栈：{4}{5}",
             ex.TargetSite, Environment.NewLine, ex.Message, Environment.NewLine, ex.StackTrace, Environment.NewLine);
            return error;
        }
    }
}
