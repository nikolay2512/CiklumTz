using System;
using Microsoft.AspNetCore.Mvc;

namespace InternetShopParser.ApiController
{
    public class ProductController : Controller 
    {

        /// <summary>
        /// Products the list.
        /// </summary>
        /// <returns>The list.</returns>
        public IActionResult ProductList()
        => View();

        /// <summary>
        /// Product this instance.
        /// </summary>
        /// <returns>The product.</returns>
        public IActionResult Product()
        => View();
    }
}
