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

        [BasicAuthorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _likeService.DeleteLikeAsync(id);
            return NoContent();
        }

        [BasicAuthorize]
        [HttpDelete("post/{postId}/remove/{count}")]
        public async Task<ActionResult> RemoveLikesFromPost(int postId, int count)
        {
            if (count <= 0)
                return BadRequest("Count must be greater than zero.");

            var likes = await _likeService.GetLikesByPostIdAsync(postId);
            var likeList = likes is List<Like> l ? l : new List<Like>(likes);

            if (likeList.Count == 0)
                return NotFound("No likes found for this post.");

            if (likeList.Count < count)
                return BadRequest("Not enough likes to remove.");

            for (int i = 0; i < count; i++)
            {
                await _likeService.DeleteLikeAsync(likeList[i].Id);
            }

            // Check if the post still has likes
            var remainingLikes = await _likeService.GetLikesByPostIdAsync(postId);
            if (!remainingLikes.Any())
                return BadRequest("All likes have been removed. Post must have at least one like.");

            return NoContent();
        }

        [BasicAuthorize]
        [HttpPost("post/{postId}/add/{count}")]
        public async Task<ActionResult> AddLikesToPost(int postId, int count, [FromBody] int[] userIds)
        {
            if (count <= 0)
                return BadRequest("Count must be greater than zero.");

            if (userIds == null || userIds.Length != count)
                return BadRequest("The number of userIds must match the count.");

            var createdLikes = new List<Like>();
            
            for (int i = 0; i < count; i++)
            {
                var like = new Like
                {
                    PostId = postId,
                    UserId = userIds[i],
                    CreatedAt = DateTime.UtcNow
                };
                await _likeService.AddLikeAsync(like);

                createdLikes.Add(like);
            }

            return CreatedAtAction(nameof(GetByPostId), new { postId = postId }, createdLikes);
        }
        
    }
}