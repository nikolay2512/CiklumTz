using System;
using System.Threading.Tasks;
using InternetShopParser.Model.ModelLayer.Product.Models;
using InternetShopParser.Model.ModelLayer.ProductUpdetePrice.Models;

namespace InternetShopParser.Model.Services
{
    public interface IProductService
    {
        AOResult<ProductSearchModel> GetList(int skip, int take);
        AOResult<ProductFullModel> GetInfo(int id);
        AOResult<ProductUpdetePriceSearchModel> GetUpdatePricesList(int productId, int skip, int take);
    }
}
