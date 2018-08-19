using System;
using System.Collections.Generic;
using InternetShopParser.Model;

namespace InternetShopParser.View
{
    public class ServerResponse<T>
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public IEnumerable<Error> Errors { get; set; }
        public T Result { get; set; }
        public Code Code { get; set; }
    }
}
