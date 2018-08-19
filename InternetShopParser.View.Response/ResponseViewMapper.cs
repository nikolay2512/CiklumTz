using System;
using System.Collections.Generic;
using AutoMapper;
using InternetShopParser.Model;

namespace InternetShopParser.View.Response
{
    public class ResponseViewMapper : IViewMapper
    {
        private readonly IMapper _mapper;

        public ResponseViewMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ServerResponse<object> Map(AOResult<object> aoResult)
        => AoResultToServerResponse<object, object>(aoResult);

        #region  IViewMapper implementation

        #endregion

        #region -- Private helpers --

        private ServerResponse<TOut> AoResultToServerResponse<TIn, TOut>(AOResult<TIn> aoResult)
        {
            var serverResponse = new ServerResponse<TOut>
            {
                Ok = aoResult.IsSuccess,
                Message = aoResult.Message,
                Errors = aoResult.Errors,
                Code = aoResult.Code,
                Result = _mapper.Map<TOut>(aoResult.Result),
            };

            return serverResponse;
        }

        #endregion
    }
}
