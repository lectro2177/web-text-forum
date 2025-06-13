using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using web_text_forum.Models;
using web_text_forum.Application.Interfaces;
using web_text_forum.Attributes;

namespace web_text_forum.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Like>> Get(int id)
        {
            var like = await _likeService.GetLikeByIdAsync(id);
            if (like == null) return NotFound();
            return Ok(like);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Like>>> GetAll()
        {
            var likes = await _likeService.GetAllLikesAsync();
            return Ok(likes);
        }

        [HttpGet("post/{postId}")]
        public async Task<ActionResult<IEnumerable<Like>>> GetByPostId(int postId)
        {
            var likes = await _likeService.GetLikesByPostIdAsync(postId);
            return Ok(likes);
        }

        [HttpGet("post/{postId}/user/{userId}")]
        public async Task<ActionResult<Like>> GetByPostAndUser(int postId, int userId)
        {
            var like = await _likeService.GetLikeByPostAndUserAsync(postId, userId);
            if (like == null) return NotFound();
            return Ok(like);
        }
        
        [BasicAuthorize]
        [HttpPost]
        public async Task<ActionResult> Create(Like like)
        {
            await _likeService.AddLikeAsync(like);
            return CreatedAtAction(nameof(Get), new { id = like.Id }, like);
        }

        //[BasicAuthorize]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    await _likeService.DeleteLikeAsync(id);
        //    return NoContent();
        //}
    }
}