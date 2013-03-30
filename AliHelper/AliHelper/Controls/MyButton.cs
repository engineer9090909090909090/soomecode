using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AliHelper.Controls
{
    class ListButton : Button
    {
        public ListButton()
        {

        }
        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }
    }
}
