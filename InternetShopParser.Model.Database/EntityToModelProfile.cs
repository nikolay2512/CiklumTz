using System;
using AutoMapper;
using InternetShopParser.Model.Database.Entities;
using InternetShopParser.Model.ModelLayer.Product.Models;

namespace InternetShopParser.Model.Database
{
    public class EntityToModelProfile : Profile
    {
        public EntityToModelProfile()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<Product, ProductFullModel>();
        }
    }
}
