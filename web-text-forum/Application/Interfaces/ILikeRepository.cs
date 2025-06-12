using System.Collections.Generic;
using System.Threading.Tasks;
using web_text_forum.Models;

namespace web_text_forum.Application.Interfaces
{
    public interface ILikeRepository
    {
        Task<Like?> GetByIdAsync(int id);
        Task<IEnumerable<Like>> GetAllAsync();
        Task<IEnumerable<Like>> GetByPostIdAsync(int postId);
        Task<Like?> GetByPostAndUserAsync(int postId, int userId);
        Task AddAsync(Like like);
        Task DeleteAsync(int id);
    }
}