using WebApiCleanArch.Domain.Entities.Base;
using WebApiCleanArch.Domain.Entities.Users;

namespace WebApiCleanArch.Domain.Entities.Posts
{
    public class Post : BaseEntity
    {


        #region Fields

        #endregion

        #region Properties

        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }

        #endregion

        #region Nav
        public User Author { get; set; }
        public Category Category { get; set; }

        #endregion



    }
}
