using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using InternetShopParser.Model;
using InternetShopParser.Model.Database;
using InternetShopParser.Model.Database.Entities;
using InternetShopParser.Model.Database.Services;
using InternetShopParser.Model.ModelLayer.Product.Models;
using InternetShopParser.Model.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace InternetShopParser.Tests
{
    public abstract class BaseTest
    {
        protected readonly TestServer _testServer;
        protected readonly IDateTimeProvider _dateTimeProvider;
        protected readonly IHtmlParserService _htmlParserService;
        protected readonly IMapper _mapper;

        protected BaseTest()
        {
            IWebHostBuilder webHostBuild =
                    WebHost.CreateDefaultBuilder()
                           .UseStartup<Startup>()
                           .UseEnvironment("Development")
                           .UseWebRoot(Directory.GetCurrentDirectory())
                           .UseContentRoot(Directory.GetCurrentDirectory());
            _testServer = new TestServer(webHostBuild);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EntityToModelProfile());
            });
            _mapper = mockMapper.CreateMapper();


            _dateTimeProvider = new StubDateTimeProvider();
            _htmlParserService = new StubHtmlParserService();
        }


        protected virtual void AddProducts(InternetShopDbContext context, int count)
        {
            for (int i = 1; i <= count; i++)
            {
                context.AttachToContext(new Product()
                {
                    Name = nameof(Product) + i,
                    DateCreate = _dateTimeProvider.UtcNow,
                    ImageSource = i.ToString() + ".jpeg",
                    Descriptions = nameof(Product.Descriptions) + i,
                    Currency = "грн",
                    Price = i,
                    ProductUpdetePrices = new List<ProductUpdetePrice>()
                    {
                        new ProductUpdetePrice()
                        {
                            DateUpdate = _dateTimeProvider.UtcNow,
                            PriceUpdate = i
                        }
                    }
                });
            }
        }

        protected void InMemoryTest(Action<InternetShopDbContext> action)
        {
            var options = new DbContextOptionsBuilder<InternetShopDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new InternetShopDbContext(options))
            {
                action(context);
            }
        }
    }

    public class StubDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => new DateTime(2018, 8, 19);

        public DateTime UtcNow => Now;
    }

    public class StubHtmlParserService : BaseService, IHtmlParserService
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        public StubHtmlParserService()
        {
            _dateTimeProvider = new StubDateTimeProvider();
        }
        public async Task<AOResult<IEnumerable<ProductFullModel>>> GetProductsFromShopHtmlAsync()
        => await BaseInvokeAsync<IEnumerable<ProductFullModel>>(async(aoResult) =>
        {
            List<ProductFullModel> productList = new List<ProductFullModel>()
            {
                new ProductFullModel()
                {
                    Name = nameof(Product) + 1,
                    ImageSource = 1.ToString() + ".jpeg",
                    Descriptions = nameof(Product.Descriptions) + 1,
                    Currency = "грн",
                    Price = 1,
                },

                new ProductFullModel()
                {
                    Name = nameof(Product) + 2,
                    ImageSource = 2.ToString() + ".jpeg",
                    Descriptions = nameof(Product.Descriptions) + 2,
                    Currency = "грн",
                    Price = 2,
                },

                new ProductFullModel()
                {
                    Name = nameof(Product) + 3,
                    ImageSource = 3.ToString() + ".jpeg",
                    Descriptions = nameof(Product.Descriptions) + 3,
                    Currency = "грн",
                    Price = 3,
                }
            };
            aoResult.SetSuccess(productList);
        });
    }
}
