using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InternetShopParser.Model.Attributes;

namespace InternetShopParser.Model.Database.Entities
{
    public class ProductUpdetePrice
    {

        [Key]
        public int Id { get; set; }

        public DateTime DateUpdate { get; set; }

        [ColumnMoneyType]
        public decimal PriceUpdate { get; set; }

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}
