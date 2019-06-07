using System;
using WebApiCleanArch.Common.Enums;

namespace WebApiCleanArch.Common.Exceptions
{
    public class LogicException : AppException
    {
        public LogicException() 
            : base(ApiStatusCodes.LogicError)
        {
        }

        public LogicException(string message) 
            : base(ApiStatusCodes.LogicError, message)
        {
        }

        public LogicException(object additionalData) 
            : base(ApiStatusCodes.LogicError, additionalData)
        {
        }

        public LogicException(string message, object additionalData) 
            : base(ApiStatusCodes.LogicError, message, additionalData)
        {
        }

        public LogicException(string message, Exception exception)
            : base(ApiStatusCodes.LogicError, message, exception)
        {
        }

        public LogicException(string message, Exception exception, object additionalData)
            : base(ApiStatusCodes.LogicError, message, exception, additionalData)
        {
        }
    }
}
