using System;
namespace InternetShopParser.View.Models
{
    public class ProductUpdetePriceView
    {
        public int Id { get; set; }

        public DateTime DateUpdate { get; set; }

        public string DateUpdateStr { get; set; }

        public decimal PriceUpdate { get; set; }
    }
}
