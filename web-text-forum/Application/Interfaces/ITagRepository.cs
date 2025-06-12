using System.Collections.Generic;
using System.Threading.Tasks;
using web_text_forum.Models;

namespace web_text_forum.Application.Interfaces
{
    public interface ITagRepository
    {
        Task<Tag?> GetByIdAsync(int id);
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag?> GetByDescriptionAsync(string description);
        Task AddAsync(Tag tag);
        Task UpdateAsync(Tag tag);
        Task DeleteAsync(int id);
    }
}