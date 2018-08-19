using System;
using InternetShopParser.Model.Database.Options;
using InternetShopParser.Model.Services;
using Microsoft.Extensions.Options;

namespace InternetShopParser.Model.Database.Services
{
    public class StoreParserProvider : IStoreParserProvider
    {
        private readonly StoreParserOption _storeParserOption;

        public StoreParserProvider(IOptions<StoreParserOption> storeParserOption)
        {
            _storeParserOption = storeParserOption.Value;
        }

        public int GetEndPage()
        => _storeParserOption.EndPage;

        public int GetStartPage()
        => _storeParserOption.StartPage;

        public string GetStoreUrl(int pageNumber)
        => string.Format(_storeParserOption.StoreUrl, pageNumber);

        public string GetTagCatalog()
        => _storeParserOption.TagCatalog;

        public string GetTagDescription()
        => _storeParserOption.TagDescription;

        public string GetTagName()
        => _storeParserOption.TagName;

        public string GetTagParentImage()
        => _storeParserOption.TagParentImage;

        public string GetTagPrice()
        => _storeParserOption.TagPrice;

        public string GetTagCurrency()
        => _storeParserOption.TagCurrency;

        public string GetTagNewPrice()
        => _storeParserOption.TagNewPrice;

        public string GetTagNewCurrency()
        => _storeParserOption.TagNewCurrency;
    }
}
