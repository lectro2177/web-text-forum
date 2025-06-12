using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_text_forum.Models;
using web_text_forum.Application.Interfaces;
using web_text_forum.Data;

namespace web_text_forum.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ForumContext _context;

        public CommentRepository(ForumContext context)
        {
            _context = context;
        }

        public async Task<Comment?> GetByIdAsync(int id) =>
            await _context.Comments.FindAsync(id);

        public async Task<IEnumerable<Comment>> GetAllAsync() =>
            await _context.Comments.ToListAsync();

        public async Task<IEnumerable<Comment>> GetByPostIdAsync(int postId) =>
            await _context.Comments
                .AsNoTracking()
                .Where(c => c.PostId == postId)
                .ToListAsync();

        public async Task AddAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }
    }
}