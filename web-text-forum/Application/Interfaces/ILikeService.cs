using System.Collections.Generic;
using System.Threading.Tasks;
using web_text_forum.Models;

namespace web_text_forum.Application.Interfaces
{
    public interface ILikeService
    {
        Task<Like?> GetLikeByIdAsync(int id);
        Task<IEnumerable<Like>> GetAllLikesAsync();
        Task<IEnumerable<Like>> GetLikesByPostIdAsync(int postId);
        Task<Like?> GetLikeByPostAndUserAsync(int postId, int userId);
        Task AddLikeAsync(Like like);
        Task DeleteLikeAsync(int id);
    }
}