using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WebApiCleanArch.Common.Enums
{
    public enum ApiStatusCodes
    {
        [DisplayName("عملیات با موفقیت انجام شد")]
        Success,
        [DisplayName("خطایی در سرور رخ داده است")]
        ServerError,
        [DisplayName("یافت نشد")]
        NotFound,
        [DisplayName("پارامترهای ارسالی نامعتبر می باشد")]
        BadRequest,
        [DisplayName("لیست خالی می باشد")]
        ListEmpty,

        [DisplayName( "خطایی در پردازش رخ داد")]
        LogicError = 5,

        [DisplayName( "خطای احراز هویت")]
        UnAuthorized = 6
    }
}
