using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_text_forum.Models;
using web_text_forum.Application.Interfaces;
using web_text_forum.Data;
using System.Linq;

namespace web_text_forum.Infrastructure.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ForumContext _context;

        public LikeRepository(ForumContext context)
        {
            _context = context;
        }

        public async Task<Like?> GetByIdAsync(int id) =>
            await _context.Likes.FindAsync(id);

        public async Task<IEnumerable<Like>> GetAllAsync() =>
            await _context.Likes.ToListAsync();

        public async Task<IEnumerable<Like>> GetByPostIdAsync(int postId) =>
            await _context.Likes.Where(l => l.PostId == postId).ToListAsync();

        public async Task<Like?> GetByPostAndUserAsync(int postId, int userId) =>
            await _context.Likes.FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);

        public async Task AddAsync(Like like)
        {
            _context.Likes.Add(like);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var like = await _context.Likes.FindAsync(id);
            if (like != null)
            {
                _context.Likes.Remove(like);
                await _context.SaveChangesAsync();
            }
        }
    }
}