using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Windows.Forms;

namespace AliHelper
{
    public class AliHelperUtils
    {
        public static void LoadAppDicComboBoxValue(ComboBox combo, string val)
        {
            foreach (AppDic dic in combo.Items)
            {
                if (dic.Key == val)
                {
                    combo.SelectedItem = dic;
                    return;
                }
            }
        }

        
    }
}
