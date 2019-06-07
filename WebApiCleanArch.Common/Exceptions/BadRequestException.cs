using System;
using WebApiCleanArch.Common.Enums;

namespace WebApiCleanArch.Common.Exceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException()
            : base(ApiStatusCodes.BadRequest)
        {
        }

        public BadRequestException(string message)
            : base(ApiStatusCodes.BadRequest, message)
        {
        }

        public BadRequestException(object additionalData)
            : base(ApiStatusCodes.BadRequest, additionalData)
        {
        }

        public BadRequestException(string message, object additionalData)
            : base(ApiStatusCodes.BadRequest, message, additionalData)
        {
        }

        public BadRequestException(string message, Exception exception)
            : base(ApiStatusCodes.BadRequest, message, exception)
        {
        }

        public BadRequestException(string message, Exception exception, object additionalData)
            : base(ApiStatusCodes.BadRequest, message, exception, additionalData)
        {
        }
    }
}
