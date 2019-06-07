using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebApiCleanArch.Domain.Entities.Posts;
using WebApiCleanArch.Domain.Interfaces.GeneralIntefaces;

namespace WebApiCleanArch.Domain.Interfaces.Services.PostsServices
{
    public interface IPostService
    {

        #region Get Methods

        Task<Post> GetPostById(CancellationToken token, int id);
        IQueryable<Post> GetPosts(Expression<Func<Post, bool>> expression = null);
        #endregion

        #region Insert Methods

        Task<bool> InsertPost(CancellationToken token, Post post);
        Task<bool> InsertPosts(CancellationToken token, List<Post> posts);

        #endregion

        #region Update Methods

        Task<bool> UpdatePost(CancellationToken token, Post post);
        Task<bool> UpdatePosts(CancellationToken token, List<Post> posts);
        #endregion

        #region Delete Methods

        Task<bool> DeletePostAsync(CancellationToken token, Post post);
        Task<bool> DeletePosts(CancellationToken token, List<Post> posts);

        #endregion
    }
}
