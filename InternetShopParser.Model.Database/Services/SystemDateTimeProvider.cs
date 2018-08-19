using System;
using InternetShopParser.Model.Services;

namespace InternetShopParser.Model.Database.Services
{
    public class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
