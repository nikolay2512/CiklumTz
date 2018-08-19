using System;
using System.Threading.Tasks;

namespace InternetShopParser.Model.Services
{
    public interface ISourceHtmlLoaderService
    {
        Task<AOResult<string>> GetSourcePageAsync(int pageNumber);
    }
}
