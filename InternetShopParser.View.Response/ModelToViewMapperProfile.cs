using System;
using AutoMapper;
using InternetShopParser.Model.ModelLayer;
using InternetShopParser.Model.ModelLayer.Product.Models;
using InternetShopParser.Model.ModelLayer.ProductUpdetePrice.Models;
using InternetShopParser.View.Models;

namespace InternetShopParser.View.Response
{
    public class ModelToViewMapperProfile : Profile
    {
        public ModelToViewMapperProfile()
        {
            CreateMap<TableModel, TableView>();
            CreateMap<ProductModel, ProductView>();
            CreateMap<ProductUpdetePriceModel, ProductUpdetePriceView>();
            CreateMap<ProductFullModel, ProductFullView>();
            CreateMap<ProductSearchModel, ProductSearchView>();
            CreateMap<ProductUpdetePriceSearchModel, ProductUpdetePriceSearchView>();
        }
    }
}
