using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApiCleanArch.Common.Enums;
using WebApiCleanArch.Common.Extensions;

namespace WebApiCleanArch.Application.ViewModels.ApiResultViewModels
{
    public class ApiResult
    {
        public ApiResult(bool isSuccess, ApiStatusCodes statusCode, string message = null)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Message = message ?? statusCode.GetDisplayName();
        }
        public bool IsSuccess { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
        public ApiStatusCodes StatusCode { get; set; }

        #region Implicit Operators
        public static implicit operator ApiResult(OkResult result)
        {
            return new ApiResult(true, ApiStatusCodes.Success);
        }

        public static implicit operator ApiResult(BadRequestResult result)
        {
            return new ApiResult(false, ApiStatusCodes.BadRequest);
        }

        public static implicit operator ApiResult(BadRequestObjectResult result)
        {
            var message = result.Value.ToString();
            if (!(result.Value is SerializableError errors))
                return new ApiResult(false, ApiStatusCodes.BadRequest, message);
            var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
            message = string.Join(" | ", errorMessages);
            return new ApiResult(false, ApiStatusCodes.BadRequest, message);
        }

        public static implicit operator ApiResult(ContentResult result)
        {
            return new ApiResult(true, ApiStatusCodes.Success, result.Content);
        }

        public static implicit operator ApiResult(NotFoundResult result)
        {
            return new ApiResult(false, ApiStatusCodes.NotFound);
        }
        #endregion

    }

    public class ApiResult<TData> : ApiResult where TData : class
    {
        public ApiResult(bool isSuccess, ApiStatusCodes statusCode, TData data, string message = null)
            : base(isSuccess, statusCode, message)
        {
            Data = data;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TData Data { get; set; }
        #region Implicit Operators
        public static implicit operator ApiResult<TData>(TData data)
        {
            return new ApiResult<TData>(true, ApiStatusCodes.Success, data);
        }

        public static implicit operator ApiResult<TData>(OkResult result)
        {
            return new ApiResult<TData>(true, ApiStatusCodes.Success, null);
        }

        public static implicit operator ApiResult<TData>(OkObjectResult result)
        {
            return new ApiResult<TData>(true, ApiStatusCodes.Success, (TData)result.Value);
        }

        public static implicit operator ApiResult<TData>(BadRequestResult result)
        {
            return new ApiResult<TData>(false, ApiStatusCodes.BadRequest, null);
        }

        public static implicit operator ApiResult<TData>(BadRequestObjectResult result)
        {
            var message = result.Value.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                message = string.Join(" | ", errorMessages);
            }
            return new ApiResult<TData>(false, ApiStatusCodes.BadRequest, null, message);
        }

        public static implicit operator ApiResult<TData>(ContentResult result)
        {
            return new ApiResult<TData>(true, ApiStatusCodes.Success, null, result.Content);
        }

        public static implicit operator ApiResult<TData>(NotFoundResult result)
        {
            return new ApiResult<TData>(false, ApiStatusCodes.NotFound, null);
        }

        public static implicit operator ApiResult<TData>(NotFoundObjectResult result)
        {
            return new ApiResult<TData>(false, ApiStatusCodes.NotFound, (TData)result.Value);
        }
        #endregion
    }
}
