using System;
namespace InternetShopParser.Model.CustomExceptions
{
    public class AOResultException : Exception
    {
        public AOResultException()
            : base("Something wrong in AOResult")
        {
        }

        public AOResultException(string message)
            : base(message)
        {
        }

        public AOResultException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
