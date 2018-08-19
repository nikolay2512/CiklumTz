using System;
namespace InternetShopParser.Model.Database.Options
{
    public class StoreParserOption
    {
        public string StoreUrl { get; set; }
        public string TagCatalog { get; set; }
        public string TagName { get; set; }
        public string TagParentImage { get; set; }
        public string TagPrice { get; set; }
        public string TagCurrency { get; set; }
        public string TagNewPrice { get; set; }
        public string TagNewCurrency { get; set; }
        public string TagDescription { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
}
