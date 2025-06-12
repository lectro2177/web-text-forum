using System.Collections.Generic;
using System.Threading.Tasks;
using web_text_forum.Models;
using web_text_forum.Application.Interfaces;

namespace web_text_forum.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public Task<Post?> GetPostByIdAsync(int id) => _postRepository.GetByIdAsync(id);

        public Task<IEnumerable<Post>> GetAllPostsAsync() => _postRepository.GetAllAsync();

        public Task AddPostAsync(Post post) => _postRepository.AddAsync(post);

        public Task UpdatePostAsync(Post post) => _postRepository.UpdateAsync(post);

        public Task DeletePostAsync(int id) => _postRepository.DeleteAsync(id);
    }
}