using System;
using System.Threading;
using System.Threading.Tasks;
using InternetShopParser.Model.Services;
using Microsoft.Extensions.Hosting;

namespace InternetShopParser.Model.Database.Services
{
    public abstract class TimeHostedService
    {
        private Timer _timer;
        private const int Interval = 300;

        private readonly IProductService _productService;

        public TimeHostedService(IProductService productService)
        {
            _productService = productService;
        }

        void Init()
        {
            _timer = new Timer(HandleTimerCallback, null, 0, Interval);
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        void HandleTimerCallback(object state)
        {
            var i = 1;
        }

    }
}
