using System;
using WebApiCleanArch.Common.Enums;

namespace WebApiCleanArch.Common.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException()
            : base(ApiStatusCodes.NotFound)
        {
        }

        public NotFoundException(string message)
            : base(ApiStatusCodes.NotFound, message)
        {
        }

        public NotFoundException(object additionalData)
            : base(ApiStatusCodes.NotFound, additionalData)
        {
        }

        public NotFoundException(string message, object additionalData)
            : base(ApiStatusCodes.NotFound, message, additionalData)
        {
        }

        public NotFoundException(string message, Exception exception)
            : base(ApiStatusCodes.NotFound, message, exception)
        {
        }

        public NotFoundException(string message, Exception exception, object additionalData)
            : base(ApiStatusCodes.NotFound, message, exception, additionalData)
        {
        }
    }
}
