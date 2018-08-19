using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using InternetShopParser.Model.Constants;
using InternetShopParser.Model.ModelLayer.Product.Models;
using InternetShopParser.Model.Services;

namespace InternetShopParser.Model.Database.Services
{
    public class HtmlParserService : BaseService, IHtmlParserService
    {
        private readonly IStoreParserProvider _storeParserProvider;
        private readonly ISourceHtmlLoaderService _sourceHtmlLoaderService;

        public HtmlParserService(IStoreParserProvider storeParserProvider,
                                 ISourceHtmlLoaderService sourceHtmlLoaderService)
        {
            _storeParserProvider = storeParserProvider;
            _sourceHtmlLoaderService = sourceHtmlLoaderService;
        }

        public async Task<AOResult<IEnumerable<ProductFullModel>>> GetProductsFromShopHtmlAsync()
        => await BaseInvokeAsync<IEnumerable<ProductFullModel>>(async (aoResult) =>
        {
            List<ProductFullModel> productList = new List<ProductFullModel>();
            for (int i = _storeParserProvider.GetStartPage(); i <= _storeParserProvider.GetEndPage(); i++)
            {
                AOResult<string> sourceHtml = await _sourceHtmlLoaderService.GetSourcePageAsync(i);
                if (sourceHtml.IsSuccess)
                {
                    IHtmlDocument htmlDocument = await GetHtmlDocumentFromSourceAsync(sourceHtml.Result);
                    productList.AddRange(Parse(htmlDocument));
                }

            }
            aoResult.SetSuccess(productList);
        });

        #region -------- Private helpers ------------

        private async Task<IHtmlDocument> GetHtmlDocumentFromSourceAsync(string source)
        {
            HtmlParser docParser = new HtmlParser();
            return await docParser.ParseAsync(source);
        }

        private List<ProductFullModel> Parse(IHtmlDocument document)
        {
            var productList = new List<ProductFullModel>();

            var products = document.QuerySelectorAll(_storeParserProvider.GetTagCatalog());
            foreach (var itemProd in products)
            {
                var product = new ProductFullModel();

                var nameTag = itemProd.QuerySelectorAll("a").FirstOrDefault(x=>x.ClassName != null && x.ClassName.Contains("product-name"));
                string name = nameTag?.TextContent.Trim();
                if (name == null)
                {
                    continue;
                }
                product.Name = name;

                var imageUrlParent = itemProd.QuerySelector(_storeParserProvider.GetTagParentImage());
                string imageUrl = imageUrlParent?.Children.FirstOrDefault(u => u.TagName.Contains("IMG"))?.Attributes.FirstOrDefault(u => u.LocalName == "data-original")?.Value.Trim();
                if (imageUrl == null)
                {
                    continue;
                }
                product.ImageSource = imageUrl;

                var priceTag = itemProd.QuerySelector(_storeParserProvider.GetTagPrice());
                if (priceTag == null)
                    priceTag = itemProd.QuerySelector(_storeParserProvider.GetTagNewPrice());
                string price = priceTag?.TextContent.Trim().Replace(" ", string.Empty);
                if (price == null)
                {
                    continue;
                }
                decimal number = 0M;
                var priceWithoutSpaceStr = Regex.Replace(price, ReqularErxpressionConstants.SelectInvisibleSpace, "");

                if (decimal.TryParse(priceWithoutSpaceStr, out number))
                    product.Price = number;
                
                var currencyTag = itemProd.QuerySelector(_storeParserProvider.GetTagCurrency());
                if (currencyTag == null)
                    currencyTag = itemProd.QuerySelector(_storeParserProvider.GetTagNewCurrency());
                string currency = currencyTag?.TextContent.Trim();
                if (name == null)
                {
                    continue;
                }
                product.Currency = currency;

                var desctiptionTag = itemProd.QuerySelector(_storeParserProvider.GetTagDescription());
                string description = desctiptionTag?.TextContent.Trim();
                if (description == null)
                {
                    continue;
                }
                product.Descriptions = description;

                productList.Add(product);
            }
            return productList;
        }

        #endregion
    }
}
