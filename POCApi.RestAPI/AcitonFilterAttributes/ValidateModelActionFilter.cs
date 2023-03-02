using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using POCApi.Core.Exceptions.Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using POCApi.RestAPI.RESTInterface;
using ActionExecutingContext = Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;
using ActionFilterAttribute = Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute;

namespace POCApi.RestAPI.AcitonFilter
{
    public class ValidateModelActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }
            var errorList = context.ModelState.Where(ms => ms.Value.Errors.Any()).ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => string.IsNullOrEmpty(e.ErrorMessage) ? e.Exception.Message : e.ErrorMessage).ToArray()
            );

            var validationErrors = new List<string>();
            foreach (var item in errorList)
            {
                foreach (var message in item.Value)
                {
                    validationErrors.Add(message);
                }
            }

            var response = new ApiResponse<object>
            {
                Success = false,
                // TODO: proper error type
                ErrorCode = ErrorDictionary.ErrInternalServerError.ErrorCode,
                Message = string.Join(" ", validationErrors),
                Data = null
            };
            context.Result = new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
    }
}
