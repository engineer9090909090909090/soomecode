using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
            //Application.Run(new MainForm());
            
            LoginForm login = new LoginForm();
            if (login.ShowDialog() == DialogResult.OK)
            {
                login.Close();
                Application.Run(new MainForm());
            }
             
        }

        





    }
}
