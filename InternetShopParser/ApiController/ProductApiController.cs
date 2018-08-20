using System;
using InternetShopParser.Attributes;
using InternetShopParser.Model.Services;
using InternetShopParser.View;
using InternetShopParser.View.Models;
using Microsoft.AspNetCore.Mvc;

namespace InternetShopParser.ApiController
{
    [InternetShopApiVersion(1,0)]
    public class ProductApiController : BaseController
    {
        private readonly IViewMapper _viewMapper;
        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:InternetShopParser.ApiController.ProductController"/> class.
        /// </summary>
        /// <param name="viewMapper">View mapper.</param>
        /// <param name="productService">Product service.</param>
        public ProductApiController(IViewMapper viewMapper,
                                 IProductService productService)
        {
            _viewMapper = viewMapper;
            _productService = productService;
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name="skip">Skip.</param>
        /// <param name="take">Take.</param>
        [HttpGet(nameof(GetList))]
        public ServerResponse<ProductSearchView> GetList(int skip, int take)
        => _viewMapper.Map(_productService.GetList(skip, take));

        /// <summary>
        /// Gets the info.
        /// </summary>
        /// <returns>The info.</returns>
        /// <param name="id">Identifier.</param>
        [HttpGet(nameof(GetInfo))]
        public ServerResponse<ProductFullView> GetInfo(int id)
        => _viewMapper.Map(_productService.GetInfo(id));

        /// <summary>
        /// Gets the update prices list.
        /// </summary>
        /// <returns>The update prices list.</returns>
        /// <param name="productId">Product identifier.</param>
        /// <param name="skip">Skip.</param>
        /// <param name="take">Take.</param>
        [HttpGet(nameof(GetUpdatePricesList))]
        public ServerResponse<ProductUpdetePriceSearchView> GetUpdatePricesList(int productId, int skip, int take)
        => _viewMapper.Map(_productService.GetUpdatePricesList(productId, skip, take));
    }
}
