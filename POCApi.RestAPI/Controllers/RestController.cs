using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using POCApi.Core.DomainServices;
using POCApi.Core.Entities;
using POCApi.Core.Generic;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using POCApi.RestAPI.RESTInterface;

namespace POCApi.RestAPI.Controllers
{
    public abstract class RestController: ControllerBase 
    {
        protected IMapper _mapper { get; set; }

        public RestController(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected ActionResult<ApiResponse<T>> Success<T>(T data) where T: class
        {
            return Ok(new ApiResponse<T>
            {
                Success = true,
                Message = "Success",
                Data = data,
            });
        }

        protected ActionResult<ApiResponse<Collection<TView>>> Collection<TView, TModel>(Collection<TModel> collection) 
            where TView: class 
            where TModel: class
        {
            var mappedList = _mapper.Map<List<TView>>(collection.Data);
            var mappedCollection = new Collection<TView>
            {
                TotalCount = collection.TotalCount,
                Data = mappedList
            };
            return Success(mappedCollection);
        }
    }
}
