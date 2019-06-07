using Microsoft.AspNetCore.Identity;
using WebApiCleanArch.Domain.Entities.Base;

namespace WebApiCleanArch.Domain.Entities.Users
{
   public class Role: IdentityRole<int>,IEntity 
    {
        #region Fields



        #endregion

        #region Properties



        public string Description { get; set; }


        #endregion

        #region Nav

        

        #endregion
    
    }
}
