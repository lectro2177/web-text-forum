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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("by-username/{username}")]
        public async Task<ActionResult<User>> GetByUsername(string username)
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{username}/is-moderator")]
        public async Task<ActionResult<bool>> IsModerator(string username)
        {
            var isModerator = await _userService.IsModeratorAsync(username);
            return Ok(isModerator);
        }


        //[BasicAuthorize]
        //[HttpPost]
        //public async Task<ActionResult> Create(User user)
        //{
        //    await _userService.AddUserAsync(user);
        //    return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        //}

        //[BasicAuthorize]
        //[HttpPut("{id}")]
        //public async Task<ActionResult> Update(int id, User user)
        //{
        //    if (id != user.Id) return BadRequest();
        //    await _userService.UpdateUserAsync(user);
        //    return NoContent();
        //}

        //[BasicAuthorize]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    await _userService.DeleteUserAsync(id);
        //    return NoContent();
        //}
    }
}