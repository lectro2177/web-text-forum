using System.Collections.Generic;
using System.Threading.Tasks;
using web_text_forum.Models;
using web_text_forum.Application.Interfaces;

namespace web_text_forum.Application.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public Task<Tag?> GetTagByIdAsync(int id) => _tagRepository.GetByIdAsync(id);

        public Task<IEnumerable<Tag>> GetAllTagsAsync() => _tagRepository.GetAllAsync();

        public Task<Tag?> GetTagByDescriptionAsync(string description) => _tagRepository.GetByDescriptionAsync(description);

        public Task AddTagAsync(Tag tag) => _tagRepository.AddAsync(tag);

        public Task UpdateTagAsync(Tag tag) => _tagRepository.UpdateAsync(tag);

        public Task DeleteTagAsync(int id) => _tagRepository.DeleteAsync(id);
    }
}