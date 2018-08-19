using System;
using System.Collections.Generic;

namespace InternetShopParser.View.Models
{
    public class ProductUpdetePriceSearchView : TableView
    {
        public IEnumerable<ProductUpdetePriceView> ProductUpdetePrices { get; set; }
    }
}
