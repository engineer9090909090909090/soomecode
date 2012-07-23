using System;
using System.Collections.Generic;
using System.Text;

namespace com.soomes.ali
{
    public class MyEventArgs : EventArgs
    {
        private object _value;
        public MyEventArgs(object val)
        {
            this._value = val;
        }

        public object Value
        {
            get { return this._value; }
        }
    }
}
