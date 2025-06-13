using System.Collections.Generic;
using System.Threading.Tasks;
using web_text_forum.Models;

namespace web_text_forum.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByUsernameAsync(string username);
        public Task<bool> IsModeratorAsync(string username);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}