using System.Collections.Generic;
using WebApiCleanArch.Domain.Entities.Base;

namespace WebApiCleanArch.Domain.Entities.Posts
{
   public class Category:BaseEntity
    {
        #region Fields

        private ICollection<Category> _categories;
        private ICollection<Post> _posts;

        #endregion

        #region Properties

        public string Name { get; set; }
        public int? ParentId { get; set; }
        public string Description { get; set; }

        #endregion

        #region Nav

        public Category Parent { get; set; }

        public ICollection<Category> Categories
        {
            get => _categories ?? new HashSet<Category>();
            set => _categories = value;
        }
        public ICollection<Post> Posts
        {
            get => _posts ?? new HashSet<Post>();
            set => _posts = value;
        }

        #endregion
       

    }
}
