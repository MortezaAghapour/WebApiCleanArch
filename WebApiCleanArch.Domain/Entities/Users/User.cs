using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using WebApiCleanArch.Common.Enums;
using WebApiCleanArch.Domain.Entities.Base;
using WebApiCleanArch.Domain.Entities.Posts;

namespace WebApiCleanArch.Domain.Entities.Users
{
    public class User: IdentityUser<int>,IEntity
    {

        #region Fields

        private ICollection<Post> _posts;


        #endregion

        #region Properties

        public string Name { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }
        public int Gender { get; set; }

        public bool IsActive { get; set; }
        public DateTime LastLoginDate { get; set; }
        #endregion

        #region Nav

        public GenderType GenderType
        {
            get => (GenderType)Gender;
            set => Gender = (int) value;
        }

        public ICollection<Post> Posts
        {
            get => _posts ?? new HashSet<Post>();
            set => _posts = value;
        }

        #endregion



    }
}
