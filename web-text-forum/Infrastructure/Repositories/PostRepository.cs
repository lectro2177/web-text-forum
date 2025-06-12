using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_text_forum.Models;
using web_text_forum.Application.Interfaces;
using web_text_forum.Data;

namespace web_text_forum.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ForumContext _context;

        public PostRepository(ForumContext context)
        {
            _context = context;
        }

        public async Task<Post?> GetByIdAsync(int id) =>
            await _context.Posts.FindAsync(id);

        public async Task<IEnumerable<Post>> GetAllAsync() =>
            await _context.Posts.ToListAsync();

        public async Task AddAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }
    }
}