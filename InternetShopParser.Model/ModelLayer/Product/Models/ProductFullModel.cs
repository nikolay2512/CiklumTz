using System;
namespace InternetShopParser.Model.ModelLayer.Product.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageSource { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string Descriptions { get; set; }
    }
}
