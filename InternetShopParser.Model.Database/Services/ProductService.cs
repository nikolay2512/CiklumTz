using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using InternetShopParser.Model.ModelLayer.Product.Models;
using InternetShopParser.Model.Services;
using InternetShopParser.Model.Database.Entities;
using Microsoft.EntityFrameworkCore;
using InternetShopParser.Model.ModelLayer.ProductUpdetePrice.Models;
using System.Threading.Tasks;

namespace InternetShopParser.Model.Database.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly InternetShopDbContext _dbContext;
        private readonly IHtmlParserService _htmlParserService;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMapper _mapper;

        private readonly string _product;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:InternetShopParser.Model.Database.Services.ProductService"/> class.
        /// </summary>
        /// <param name="dbContext">Db context.</param>
        /// <param name="mapper">Mapper.</param>
        public ProductService(InternetShopDbContext dbContext,
                              IHtmlParserService htmlParserService,
                              IDateTimeProvider dateTimeProvider,
                              IMapper mapper)
        {
            _dbContext = dbContext;
            _htmlParserService = htmlParserService;
            _dateTimeProvider = dateTimeProvider;
            _mapper = mapper;

            _product = RemoveServicePostfix(nameof(ProductService));
        }

        /// <summary>
        /// Get the specified skip and take.
        /// </summary>
        /// <returns>The get.</returns>
        /// <param name="skip">Skip.</param>
        /// <param name="take">Take.</param>
        public AOResult<ProductSearchModel> GetList(int skip, int take)
        => BaseInvoke<ProductSearchModel>((aoResult) =>
        {
            IEnumerable<ProductModel> productModels =
                (from p in _dbContext.Products
                 select _mapper.Map<ProductModel>(p));

            aoResult.SetSuccess(new ProductSearchModel()
            {
                TotalCount = productModels.Count(),
                Products = productModels.Skip(skip).Take(take).ToList()
            });
        });

        /// <summary>
        /// Gets the info.
        /// </summary>
        /// <returns>The info.</returns>
        /// <param name="id">Identifier.</param>
        public AOResult<ProductFullModel> GetInfo(int id)
        => BaseInvoke<ProductFullModel>((aoResult) =>
        {
            List<Error> errorList = new List<Error>();
            Product product = _dbContext.Products
                                        .FirstOrDefault(x => x.Id == id);

            if (product == null)
                errorList.Add(new Error(nameof(id), EntityNotExists(_product)));

            if (errorList.Any())
                aoResult.SetError(ModelIsNotValidErrorMessage, errorList);
            else
            {
                aoResult.SetSuccess(new ProductFullModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    ImageSource = product.ImageSource,
                    Price = product.Price,
                    Currency = product.Currency,
                    Descriptions = product.Descriptions
                });
            }
        });

        public AOResult<ProductUpdetePriceSearchModel> GetUpdatePricesList(int productId, int skip, int take)
        => BaseInvoke<ProductUpdetePriceSearchModel>((aoResult) =>
        {
            IEnumerable<ProductUpdetePriceModel> productUpdetePriceModels =
                (from pu in _dbContext.ProductUpdetePrices
                 where pu.ProductId == productId
                 orderby pu.DateUpdate descending
                 select new ProductUpdetePriceModel()
                 {
                     Id = pu.Id,
                     PriceUpdate = pu.PriceUpdate,
                     DateUpdate = pu.DateUpdate,
                     DateUpdateStr = pu.DateUpdate.ToString("dd.MM.yy HH:mm")
                 });

            aoResult.SetSuccess(new ProductUpdetePriceSearchModel()
            {
                TotalCount = productUpdetePriceModels.Count(),
                ProductUpdetePrices = productUpdetePriceModels.Skip(skip).Take(take).ToList()
            });
        });


    }
}
