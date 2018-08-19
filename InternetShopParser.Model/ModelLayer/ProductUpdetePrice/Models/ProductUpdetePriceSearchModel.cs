using System;
using System.Collections.Generic;

namespace InternetShopParser.Model.ModelLayer.ProductUpdetePrice.Models
{
    public class ProductUpdetePriceSearchModel : TableModel
    {
        public IEnumerable<ProductUpdetePriceModel> ProductUpdetePrices { get; set; }
    }
}