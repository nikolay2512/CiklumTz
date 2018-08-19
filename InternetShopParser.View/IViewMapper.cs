using System;
using InternetShopParser.Model;
using InternetShopParser.Model.ModelLayer.Product.Models;
using InternetShopParser.Model.ModelLayer.ProductUpdetePrice.Models;
using InternetShopParser.View.Models;

namespace InternetShopParser.View
{
    public interface IViewMapper
    {
        ServerResponse<object> Map(AOResult<object> aoResult);
        ServerResponse<ProductSearchView> Map(AOResult<ProductSearchModel> aoResult);
        ServerResponse<ProductFullView> Map(AOResult<ProductFullModel> aoResult);
        ServerResponse<ProductUpdetePriceSearchView> Map(AOResult<ProductUpdetePriceSearchModel> aoResult);
    }
}
