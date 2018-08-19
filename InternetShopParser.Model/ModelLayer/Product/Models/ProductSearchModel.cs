using System;
using System.Collections;
using System.Collections.Generic;

namespace InternetShopParser.Model.ModelLayer.Product.Models
{
    public class ProductSearchModel : TableModel
    {
        public IEnumerable<ProductModel> Products { get; set; }
    }
}
