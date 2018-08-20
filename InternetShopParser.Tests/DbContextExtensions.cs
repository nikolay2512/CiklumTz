using System;
using InternetShopParser.Model.Database;

namespace InternetShopParser.Tests
{
    public static class DbContextExtensions
    {
        public static T AttachToContext<T>(this InternetShopDbContext context, T entity)
            where T : class
        {
            var result = context.Add(entity).Entity;
            context.SaveChanges();

            return result;
        }
    }
}
