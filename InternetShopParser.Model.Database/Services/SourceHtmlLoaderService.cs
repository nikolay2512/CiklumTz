using System;
using System.Net;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using InternetShopParser.Model.Services;

namespace InternetShopParser.Model.Database.Services
{
    public class HtmlLoaderService : BaseService
    {
        private readonly IStoreParserProvider _storeParserProvider;
        private readonly WebClient _webClient;

        public HtmlLoaderService(IStoreParserProvider storeParserProvider, WebClient webClient)
        {
            _storeParserProvider = storeParserProvider;
            _webClient = webClient;
        }

        public async Task<AOResult<IHtmlDocument>> GetSourcePage(int pageNumber)
        => BaseInvoke<string>((aoResult) =>
        { 
            string source = _webClient.DownloadString($"{_storeParserProvider.GetStoreUrl()}{_storeParserProvider.GetPagePrefix(pageNumber)}/");
            aoResult.SetSuccess(source);
        });

    }
}
