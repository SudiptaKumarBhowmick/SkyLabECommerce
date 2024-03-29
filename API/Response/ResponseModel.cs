﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Response
{
    public class ResponseModel : IResponseModel
    {
        public int StatusCode { get; set; } = 0;
        public string Message { get; set; } = "";
    }

    public interface IResponseModel
    {
        public int StatusCode { get; set; }
        string Message { get; set; }
    }

    public interface ISingleResponseModel<TModel> : IResponseModel
    {
        TModel? Model { get; set; }
    }

    public interface IListResponseModel<TModel> : IResponseModel
    {
        IEnumerable<TModel>? Model { get; set; }
    }

    public class SingleResponseModel<TModel> : ISingleResponseModel<TModel>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = "";
        public TModel? Model { get; set; }
    }

    public class ListResponseModel<TModel> : IListResponseModel<TModel>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = "";
        public IEnumerable<TModel>? Model { get; set; }
    }
}
