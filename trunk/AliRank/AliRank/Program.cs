using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;

namespace AliRank
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string CpuId = Tools.GetCpuID();
            string MacAdd = Tools.getLocalMAC();
            string ComputeName = Tools.GetComputerName();
            Application.Run(new MainForm());
        }


    }
}
