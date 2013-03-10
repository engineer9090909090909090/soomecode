using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using Soomes;

namespace Database
{
    public interface IAppDicDAO
    {
        string GetValue(string type, string key);

        List<AppDic> GetOptions(string type);

        void SetValue(string type, string key, string value);

        void DeleteAppDic(string type, string key);
    }


}
