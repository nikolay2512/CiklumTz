using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShopParser.Model.Attributes
{
    public class ColumnMoneyTypeAttribute : ColumnAttribute
    {
        public ColumnMoneyTypeAttribute()
        {
            TypeName = "Money";
        }
    }
}
