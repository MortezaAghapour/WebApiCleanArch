using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApiCleanArch.Common.Helpers;
using WebApiCleanArch.Domain.Entities.Posts;
using WebApiCleanArch.Domain.Interfaces.GeneralIntefaces;
using WebApiCleanArch.Domain.Interfaces.Repositories;
using WebApiCleanArch.Domain.Interfaces.Services.PostsServices;

namespace WebApiCleanArch.Infrastructure.Services.PostServices
{
    public class PostService : IPostService, ITransientDependency
    {

        #region Fields

        private readonly IRepository<Post> _repository;


        #endregion

        #region Constrcutors

        public PostService(IRepository<Post> repository)
        {
            _repository = repository;
        }


        #endregion

        #region Methods

        public async Task<Post> GetPostById(CancellationToken token, int id)
        {
            return await _repository.GetByIdAsync(token, id);
        }

        public IQueryable<Post> GetPosts( Expression<Func<Post, bool>> expression = null)
        {
            return expression!=null ? _repository.TableAsNoTracking.Where(expression) : _repository.TableAsNoTracking;
        }

        public async Task<bool> InsertPost(CancellationToken token, Post post)
        {
            if (post==null)
            {
                return false;
            }

            return await _repository.AddAsync(post, token);
        }

        public async Task<bool> InsertPosts(CancellationToken token, List<Post> posts)
        {
            if (!posts.Any())
            {
                return false;
            }

            return await _repository.AddRangeAsync(posts, token);
        }

        public async Task<bool> UpdatePost(CancellationToken token, Post post)
        {
            if (post==null)
            {
                return false;
            }

            return await _repository.UpdateAsync(post, token);
        }

        public async Task<bool> UpdatePosts(CancellationToken token, List<Post> posts)
        {
            if (!posts.Any())
            {
                return false;
            }

            return await _repository.UpdateRangeAsync(posts, token);
        }


        public async Task<bool> DeletePostAsync(CancellationToken token, Post post)
        {
            return await _repository.RemoveAsync(post, token);
        }

        public async Task<bool> DeletePosts(CancellationToken token, List<Post> posts)
        {
            return await _repository.RemoveRangeAsync(posts, token);
        }

        #endregion

    }
}
