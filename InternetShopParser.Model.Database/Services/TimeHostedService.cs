using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InternetShopParser.Model.Database.Entities;
using InternetShopParser.Model.ModelLayer.Product.Models;
using InternetShopParser.Model.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InternetShopParser.Model.Database.Services
{
    public class TimeHostedService : BaseService, IHostedService, IDisposable
    {
        private Timer _timer;
        private const int Interval = 30000;
        private int i = 0;

        private IHtmlParserService _htmlParserService;
        private IDateTimeProvider _dateTimeProvider;
        private string ConnectionString;

        public TimeHostedService(string connectionString,
                                 IHtmlParserService htmlParserService,
                                 IDateTimeProvider dateTimeProvider)
        {
            _htmlParserService = htmlParserService;
            _dateTimeProvider = dateTimeProvider;
            ConnectionString = connectionString;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(UpdateProductPrice, null, 0, Interval);

            return Task.CompletedTask;
        }

        public void UpdateProductPrice(object state)
        => BaseInvoke<object>((aoResult) =>
        {
            AOResult<IEnumerable<ProductFullModel>> resultParse =  _htmlParserService.GetProductsFromShopHtmlAsync().Result;
            if (resultParse.IsSuccess)
            {
                var optionsBuilder = new DbContextOptionsBuilder<InternetShopDbContext>();
                optionsBuilder.UseNpgsql(ConnectionString);

                using (var _dbContext = new InternetShopDbContext(optionsBuilder.Options))
                {
                    var productList = _dbContext.Products.ToList();
                    foreach (var productModel in resultParse.Result)
                    {
                        
                        var product = productList.FirstOrDefault(x => x.Name.Contains(productModel.Name));
                        if (product != null && product.Price != productModel.Price)
                        {
                            product.Price = product.Price+i;
                            _dbContext.ProductUpdetePrices.Add(new ProductUpdetePrice()
                            {
                                DateUpdate = _dateTimeProvider.UtcNow.AddDays(i),
                                PriceUpdate = product.Price,
                                Product = product
                            });
                        }
                    }
                    _dbContext.SaveChanges();
                    i += 1;
                }
            }
        });

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }


        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
