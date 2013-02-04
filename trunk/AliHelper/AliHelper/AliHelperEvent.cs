using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliHelper
{
    public delegate void NewProductgEvent(object sender, ProductEventArgs e);
    public delegate void EditProductgEvent(object sender, ProductEventArgs e);

    public class ProductEventArgs : EventArgs
    {
        public int CategoryId;
        public int ProductId;

        public ProductEventArgs(int _CategoryId, int _Id)
        {
            this.CategoryId = _CategoryId;
            this.ProductId = _Id;
        }
    }
}
