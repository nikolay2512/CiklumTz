using System;
using System.Collections.Generic;

namespace InternetShopParser.View.Models
{
    public class ProductSearchView : TableView
    {
        public IEnumerable<ProductView> Products { get; set; }
    }
}
