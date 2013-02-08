using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliHelper
{
    public delegate void NewEditItemEvent(object sender, ItemEventArgs e);

    public class ItemEventArgs : EventArgs
    {
        public object Item;

        public ItemEventArgs(object _item)
        {
            this.Item = _item;
        }
    }
}
