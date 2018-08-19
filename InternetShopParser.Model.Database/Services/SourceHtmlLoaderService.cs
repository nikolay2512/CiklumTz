using System;
using System.Net;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using InternetShopParser.Model.Services;

namespace InternetShopParser.Model.Database.Services
{
    public class SourceHtmlLoaderService : BaseService, ISourceHtmlLoaderService
    {
        private readonly IStoreParserProvider _storeParserProvider;
        private readonly WebClient _webClient;

        public SourceHtmlLoaderService(IStoreParserProvider storeParserProvider, WebClient webClient)
        {
            _storeParserProvider = storeParserProvider;
            _webClient = webClient;
        }

        public async Task<AOResult<string>> GetSourcePageAsync(int pageNumber)
        => await BaseInvokeAsync<string>(async(aoResult) =>
        {
            Uri pageUri = new Uri($"{_storeParserProvider.GetStoreUrl(pageNumber)}");
            string source = await _webClient.DownloadStringTaskAsync(pageUri);
            aoResult.SetSuccess(source);
        });


    }
}
