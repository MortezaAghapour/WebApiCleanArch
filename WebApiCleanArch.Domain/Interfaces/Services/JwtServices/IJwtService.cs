using System;
using System.Collections.Generic;
using System.Text;
using WebApiCleanArch.Domain.Entities.Users;
using WebApiCleanArch.Domain.Interfaces.GeneralIntefaces;

namespace WebApiCleanArch.Domain.Interfaces.Services.JwtServices
{
    public interface IJwtService
    {

        string Generate(User user);
    }
}
