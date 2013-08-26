using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace com.soomes
{
    public class FileUtils
    {
        public static string GetLogFolder()
        {
            string LogFolderPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Log";
            if (!Directory.Exists(LogFolderPath))
            {
                Directory.CreateDirectory(LogFolderPath);
            }
            return LogFolderPath + Path.DirectorySeparatorChar;
        }
    }
}
