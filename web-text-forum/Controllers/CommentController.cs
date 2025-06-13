using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using web_text_forum.Application.Interfaces;
using web_text_forum.Attributes;
using web_text_forum.Models;

namespace web_text_forum.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> Get(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null) return NotFound();
            return Ok(comment);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAll()
        {
            var comments = await _commentService.GetAllCommentsAsync();
            return Ok(comments);
        }

        
        [HttpGet("post/{postId}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetByPostId(int postId)
        {
            var comments = await _commentService.GetCommentsByPostIdAsync(postId);
            return Ok(comments);
        }

        [BasicAuthorize]
        [HttpPost]
        public async Task<ActionResult> Create(Comment comment)
        {
            await _commentService.AddCommentAsync(comment);
            return CreatedAtAction(nameof(Get), new { id = comment.Id }, comment);
        }

        [BasicAuthorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Comment comment)
        {
            if (id != comment.Id) return BadRequest();
            await _commentService.UpdateCommentAsync(comment);
            return NoContent();
        }

        //[BasicAuthorize]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    await _commentService.DeleteCommentAsync(id);
        //    return NoContent();
        //}
    }
}