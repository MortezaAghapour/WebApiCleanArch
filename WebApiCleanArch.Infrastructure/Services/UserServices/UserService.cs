using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApiCleanArch.Domain.Entities.Users;
using WebApiCleanArch.Domain.Interfaces.GeneralIntefaces;
using WebApiCleanArch.Domain.Interfaces.Repositories;
using WebApiCleanArch.Domain.Interfaces.Services.UserServices;

namespace WebApiCleanArch.Infrastructure.Services.UserServices
{
    public class UserService:IUserService, ITransientDependency
    {

        #region Fields
        private readonly IRepository<User> _repository;

      

        #endregion

        #region Constructors

  public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        #endregion

        #region Methods



        #endregion
        public async Task<User> GetByIdAsync(CancellationToken cancellationToken, int userId)
        {

            return await _repository.GetByIdAsync(cancellationToken, userId);

        }
    }
}
