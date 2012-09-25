using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using System.Reflection;
using System.IO;
using Soomes;
using Microsoft.CSharp;
using System.Text;

namespace AliHelper
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

            StreamReader sr = new StreamReader("c://aa.txt");
            string code = sr.ReadToEnd();
            Category root = (Category)LoadCode.StringEvalToCategoryObject(code);
            System.Diagnostics.Trace.WriteLine(root.childCategorys[2].childCategorys.Count);
            if (root.childCategorys.Count > 0)
            {
                foreach (Category c in root.childCategorys[2].childCategorys)
                {
                    System.Diagnostics.Trace.WriteLine(c.longTitle);
                }
            }
            System.Diagnostics.Trace.WriteLine("========================");
            //Application.Run(new MainForm());
            /*
            LoginForm login = new LoginForm();
            if (login.ShowDialog() == DialogResult.OK)
            {
                login.Close();
                Application.Run(new MainForm());
            }
             */
        }


       


    }
}
