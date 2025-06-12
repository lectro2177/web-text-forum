using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using web_text_forum.Models;
using web_text_forum.Application.Interfaces;

namespace web_text_forum.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> Get(int id)
        {
            var tag = await _tagService.GetTagByIdAsync(id);
            if (tag == null) return NotFound();
            return Ok(tag);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetAll()
        {
            var tags = await _tagService.GetAllTagsAsync();
            return Ok(tags);
        }

        [HttpGet("description/{description}")]
        public async Task<ActionResult<Tag>> GetByDescription(string description)
        {
            var tag = await _tagService.GetTagByDescriptionAsync(description);
            if (tag == null) return NotFound();
            return Ok(tag);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Tag tag)
        {
            await _tagService.AddTagAsync(tag);
            return CreatedAtAction(nameof(Get), new { id = tag.Id }, tag);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Tag tag)
        {
            if (id != tag.Id) return BadRequest();
            await _tagService.UpdateTagAsync(tag);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _tagService.DeleteTagAsync(id);
            return NoContent();
        }
    }
}