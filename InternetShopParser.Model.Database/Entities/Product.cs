using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InternetShopParser.Model.Attributes;

namespace InternetShopParser.Model.Database.Entities
{
    public class Product
    {
        public Product()
        {
            ProductUpdetePrices = new List<ProductUpdetePrice>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public string ImageSource { get; set; }

        [ColumnMoneyType]
        public decimal Price { get; set; }

        [StringLength(20)]
        public string Currency { get; set; }

        public string Descriptions { get; set; }

        public DateTime DateCreate { get; set; }

        public ICollection<ProductUpdetePrice> ProductUpdetePrices { get; set; }
    }
}
