using System;
using Microsoft.AspNetCore.Mvc;

namespace InternetShopParser.ApiController
{
    [Route("v{version:apiVersion}/[controller]")]
    public abstract class BaseController : Controller
    {
        public BaseController()
        {
        }
    }
}
