using System.Collections.Generic;
using System.Threading.Tasks;
using web_text_forum.Models;

namespace web_text_forum.Application.Interfaces
{
    public interface ITagService
    {
        Task<Tag?> GetTagByIdAsync(int id);
        Task<IEnumerable<Tag>> GetAllTagsAsync();
        Task<Tag?> GetTagByDescriptionAsync(string description);
        Task AddTagAsync(Tag tag);
        Task UpdateTagAsync(Tag tag);
        Task DeleteTagAsync(int id);
    }
}