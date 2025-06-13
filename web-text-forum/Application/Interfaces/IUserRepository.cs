using System.Collections.Generic;
using System.Threading.Tasks;
using web_text_forum.Models;

namespace web_text_forum.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task<bool> IsModeratorAsync(string username);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
    }
}