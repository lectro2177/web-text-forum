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
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public PostController(IPostService postService, IUserService userService)
        {
            _postService = postService;
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> Get(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAll()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("paged/{pageNumber:int}/{pageSize:int}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPaged(
            int pageNumber = 1,
            int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var posts = await _postService.GetAllPostsAsync();

            var pagedPosts = posts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return Ok(pagedPosts);
        }

        [BasicAuthorize]
        [HttpPost]
        public async Task<ActionResult> Create(Post post)
        {
            await _postService.AddPostAsync(post);
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
        }
        
        [BasicAuthorize]
        [HttpPost("{id}/tag/{tagId}")]
        public async Task<ActionResult> TagPost(int id, int tagId)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null) return NotFound();

            var user = HttpContext.User;

            var userName = user.Identity.Name;
            if (userName == null) return Unauthorized();

            var dbUser = await _userService.GetUserByUsernameAsync(userName);
            if (dbUser == null) return NotFound();

            if (dbUser.Role == UserRole.Moderator)
            {
                post.TagId = tagId;                
                await _postService.UpdatePostAsync(post);
                return NoContent();
            }

            return Forbid();
        }
    }
}