using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApiCleanArch.Domain.Entities.Users;
using WebApiCleanArch.Domain.Interfaces.GeneralIntefaces;

namespace WebApiCleanArch.Domain.Interfaces.Services.UserServices
{
    public interface IUserService
    {
        Task<User> GetByIdAsync(CancellationToken httpContextRequestAborted, int userId);
    }
}
