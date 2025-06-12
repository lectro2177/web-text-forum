using System.Collections.Generic;
using System.Threading.Tasks;
using web_text_forum.Models;
using web_text_forum.Application.Interfaces;

namespace web_text_forum.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public Task<Comment?> GetCommentByIdAsync(int id) => _commentRepository.GetByIdAsync(id);

        public Task<IEnumerable<Comment>> GetAllCommentsAsync() => _commentRepository.GetAllAsync();

        public Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId) => _commentRepository.GetByPostIdAsync(postId);

        public Task AddCommentAsync(Comment comment) => _commentRepository.AddAsync(comment);

        public Task UpdateCommentAsync(Comment comment) => _commentRepository.UpdateAsync(comment);

        public Task DeleteCommentAsync(int id) => _commentRepository.DeleteAsync(id);
    }
}