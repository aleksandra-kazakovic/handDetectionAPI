using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using POCApi.Core.Exceptions;
using POCApi.Core.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using POCApi.RestAPI.RESTInterface;

namespace POCApi.RestAPI
{
    public static class ErrorHandler
    {
        public static async Task HandleAsync(IConfiguration configuration, HttpContext context, ILogger<Startup> logger)
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var err = exceptionHandlerPathFeature?.Error;

            var statusCode = (int)System.Net.HttpStatusCode.InternalServerError;

            var response = new ApiResponse<object>
            {
                Success = false,
                ErrorCode = ErrorDictionary.ErrInternalServerError.ErrorCode,
                Message = ErrorDictionary.ErrInternalServerError.ErrorMessage,
                Data = null
            };

            if (err != null)
            {
                if (err is ObjectNotFoundException || err is ValidationException)
                {
                    response = new ApiResponse<object>
                    {
                        Success = false,
                        ErrorCode = (err as DemoException).Error?.ErrorCode,
                        Message = err.Message,
                        Data = null
                    };

                    if (err is ObjectNotFoundException)
                    {
                        statusCode = (int)System.Net.HttpStatusCode.NotFound;
                    }
                    else if (err is ValidationException)
                    {
                        statusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    }
                    else
                    {
                        statusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                    }
                }
            }

            logger.LogError($"Exception of type '{err.GetType().Name}' occured.", err);

            context.Response.StatusCode = statusCode;
            var devEx = configuration["ShowDeveloperException"];
            if (devEx?.ToLower() == "true")
            {
                await context.Response.WriteAsJsonAsync(new
                {
                    response.Success,
                    response.ErrorCode,
                    err.Message,
                    StackTraceForDevelopers = err.StackTrace
                });
            }
            else
            {
                await context.Response.WriteAsJsonAsync(new
                {
                    response.Success,
                    response.ErrorCode,
                    response.Message
                });
            }
        }
    }
}
