using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiCleanArch.Application.ViewModels.AppSettingViewModels
{
    public class JwtSetting
    {
        public string SecreteKey { get; set; }
        public string EncryptKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int NotBeforMiniutes { get; set; }
        public int ExpirationMiniutes { get; set; }

    }
}
