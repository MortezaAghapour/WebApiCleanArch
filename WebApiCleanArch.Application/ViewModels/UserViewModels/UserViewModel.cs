using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebApiCleanArch.Common.ConstStrings;
using WebApiCleanArch.Common.Helpers;

namespace WebApiCleanArch.Application.ViewModels.UserViewModels
{
    public class UserViewModel:IValidatableObject
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }

        public bool IsActive { get; set; }
        public DateTime LastLoginDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            //business error like username can not contain non-alphabet and numeric
            //do not contain database relationship


            if (CommonHelper.IsAlphaNumeric(UserName))
            {
              yield return new ValidationResult(Resource.UserNameCanNotContainNonAlphabetAndNumeric);
            }

        }
    }
}
