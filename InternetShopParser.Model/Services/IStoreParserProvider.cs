using System;
namespace InternetShopParser.Model.Services
{
    public interface IStoreParserProvider
    {
        string GetStoreUrl(int pageNumber);
        string GetTagCatalog();
        string GetTagName();
        string GetTagParentImage();
        string GetTagPrice();
        string GetTagCurrency();
        string GetTagNewPrice();
        string GetTagNewCurrency();
        string GetTagDescription();
        int GetStartPage();
        int GetEndPage();
    }
}
