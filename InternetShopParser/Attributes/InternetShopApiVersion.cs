using System;
using Microsoft.AspNetCore.Mvc;

namespace InternetShopParser.Attributes
{
    public class InternetShopApiVersion : ApiVersionAttribute
    {
        public InternetShopApiVersion(int major, int minor)
            : base($"{major}.{minor}")
        {
        }
    }
}
