using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiCleanArch.Application.ViewModels.ApiResultViewModels;
using WebApiCleanArch.Common.Enums;

namespace WebApiCleanArch.Infrastructure.FilterAttributes
{
    public class ApiResultAttribute:ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            switch (context.Result)
            {
                case OkObjectResult okObjectResult:
                {
                    var apiResult=new ApiResult<object>(true,ApiStatusCodes.Success,okObjectResult.Value);
                    context.Result = new JsonResult(apiResult) { StatusCode = okObjectResult.StatusCode };
                    break;
                }
                case OkResult okResult:
                {
                    var apiResult = new ApiResult(true, ApiStatusCodes.Success);
                    context.Result=new JsonResult(apiResult){StatusCode = okResult.StatusCode};
                    break;
                }
                case BadRequestResult badRequestResult:
                {
                    var apiResult = new ApiResult(false, ApiStatusCodes.BadRequest);
                    context.Result = new JsonResult(apiResult) { StatusCode = badRequestResult.StatusCode };
                    break;
                }
                case BadRequestObjectResult badRequestObjectResult:
                {
                    var message = badRequestObjectResult.Value.ToString();
                    if (badRequestObjectResult.Value is SerializableError errors)
                    {
                        var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                        message = string.Join(" | ", errorMessages);
                    }
                    var apiResult = new ApiResult(false, ApiStatusCodes.BadRequest, message);
                    context.Result = new JsonResult(apiResult) { StatusCode = badRequestObjectResult.StatusCode };
                    break;
                }
                case ContentResult contentResult:
                {
                    var apiResult = new ApiResult(true, ApiStatusCodes.Success, contentResult.Content);
                    context.Result = new JsonResult(apiResult) { StatusCode = contentResult.StatusCode };
                    break;
                }
                case NotFoundResult notFoundResult:
                {
                    var apiResult = new ApiResult(false, ApiStatusCodes.NotFound);
                    context.Result = new JsonResult(apiResult) { StatusCode = notFoundResult.StatusCode };
                    break;
                }
                case NotFoundObjectResult notFoundObjectResult:
                {
                    var apiResult = new ApiResult<object>(false, ApiStatusCodes.NotFound, notFoundObjectResult.Value);
                    context.Result = new JsonResult(apiResult) { StatusCode = notFoundObjectResult.StatusCode };
                    break;
                }
                case ObjectResult objectResult when objectResult.StatusCode == null && !(objectResult.Value is ApiResult):
                {
                    var apiResult = new ApiResult<object>(true, ApiStatusCodes.Success, objectResult.Value);
                    context.Result = new JsonResult(apiResult) { StatusCode = objectResult.StatusCode };
                    break;
                }
            }

            base.OnResultExecuting(context);
        }
    }
}
