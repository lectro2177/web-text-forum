using System.Collections.Generic;
using System.Threading.Tasks;
using web_text_forum.Models;
using web_text_forum.Application.Interfaces;

namespace web_text_forum.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<User?> GetUserByIdAsync(int id) => _userRepository.GetByIdAsync(id);

        public Task<User?> GetUserByUsernameAsync(string username) => _userRepository.GetByUsernameAsync(username);

        public Task<IEnumerable<User>> GetAllUsersAsync() => _userRepository.GetAllAsync();

        public Task AddUserAsync(User user) => _userRepository.AddAsync(user);

        public Task UpdateUserAsync(User user) => _userRepository.UpdateAsync(user);

        public Task DeleteUserAsync(int id) => _userRepository.DeleteAsync(id);
    }
}