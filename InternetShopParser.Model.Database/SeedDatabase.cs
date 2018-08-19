using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetShopParser.Model.Database.Entities;
using InternetShopParser.Model.Services;

namespace InternetShopParser.Model.Database
{
    public class SeedDatabase
    {
        private readonly InternetShopDbContext _internetShopDbContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IHtmlParserService _htmlParserService;

        public SeedDatabase(InternetShopDbContext internetShopDbContext,
                            IDateTimeProvider dateTimeProvider,
                            IHtmlParserService htmlParserService)
        {
            _internetShopDbContext = internetShopDbContext;
            _dateTimeProvider = dateTimeProvider;
            _htmlParserService = htmlParserService;
        }

        public void Seed()
        {
            using (var transaction = _internetShopDbContext.Database.BeginTransaction())
            {
                try
                {
                    if(!_internetShopDbContext.Products.Any())
                    {
                        var parseResult = _htmlParserService.GetProductsFromShopHtmlAsync().Result;
                        if(parseResult.IsSuccess)
                        {
                            var products = parseResult.Result.Select(x => new Product()
                            {
                                Name = x.Name,
                                ImageSource = x.ImageSource,
                                Price = x.Price,
                                Currency = x.Currency,
                                Descriptions = x.Descriptions,
                                DateCreate = _dateTimeProvider.UtcNow,
                                ProductUpdetePrices = new List<ProductUpdetePrice>()
                                {
                                    new ProductUpdetePrice()
                                    {
                                        DateUpdate = _dateTimeProvider.UtcNow,
                                        PriceUpdate = x.Price
                                    }
                                }
                            });
                            _internetShopDbContext.Products.AddRange(products);
                        }
                    }
                    _internetShopDbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
