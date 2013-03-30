using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliHelper
{
    public delegate void TopFiveSearchingEvent(object sender, TopFiveEventArgs e);
    public delegate void TopFiveSearchEndEvent(object sender, TopFiveEventArgs e);

    public class TopFiveEventArgs : EventArgs
    {
        public TopFiveInfo Item;
        public string Msg;

        public TopFiveEventArgs(TopFiveInfo _obj, string _msg)
        {
            this.Item = _obj;
            this.Msg = _msg;
        }
    }

}
