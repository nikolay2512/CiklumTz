using System;
namespace InternetShopParser.Model.ModelLayer.ProductUpdetePrice.Models
{
    public class ProductUpdetePriceModel
    {
        public int Id { get; set; }

        public DateTime DateUpdate { get; set; }

        public string DateUpdateStr { get; set; }

        public decimal PriceUpdate { get; set; }
    }
}
