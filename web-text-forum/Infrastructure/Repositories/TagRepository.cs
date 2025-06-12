using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_text_forum.Models;
using web_text_forum.Application.Interfaces;
using web_text_forum.Data;

namespace web_text_forum.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ForumContext _context;

        public TagRepository(ForumContext context)
        {
            _context = context;
        }

        public async Task<Tag?> GetByIdAsync(int id) =>
            await _context.Tags.FindAsync(id);

        public async Task<IEnumerable<Tag>> GetAllAsync() =>
            await _context.Tags.ToListAsync();

        public async Task<Tag?> GetByDescriptionAsync(string description) =>
            await _context.Tags.FirstOrDefaultAsync(t => t.TagDescription == description);

        public async Task AddAsync(Tag tag)
        {
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tag tag)
        {
            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
            }
        }
    }
}