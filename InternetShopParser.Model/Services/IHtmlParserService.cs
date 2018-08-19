using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using InternetShopParser.Model.ModelLayer.Product.Models;

namespace InternetShopParser.Model.Services
{
    public interface IHtmlParserService
    {
        Task<AOResult<IEnumerable<ProductFullModel>>> GetProductsFromShopHtmlAsync();
    }
}
