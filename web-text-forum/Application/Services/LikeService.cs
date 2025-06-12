using System.Collections.Generic;
using System.Threading.Tasks;
using web_text_forum.Models;
using web_text_forum.Application.Interfaces;

namespace web_text_forum.Application.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;

        public LikeService(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public Task<Like?> GetLikeByIdAsync(int id) => _likeRepository.GetByIdAsync(id);

        public Task<IEnumerable<Like>> GetAllLikesAsync() => _likeRepository.GetAllAsync();

        public Task<IEnumerable<Like>> GetLikesByPostIdAsync(int postId) => _likeRepository.GetByPostIdAsync(postId);

        public Task<Like?> GetLikeByPostAndUserAsync(int postId, int userId) => _likeRepository.GetByPostAndUserAsync(postId, userId);

        public Task AddLikeAsync(Like like) => _likeRepository.AddAsync(like);

        public Task DeleteLikeAsync(int id) => _likeRepository.DeleteAsync(id);
    }
}