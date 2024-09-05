using Blogedium_api.Data;
using Blogedium_api.Exceptions;
using Blogedium_api.Interfaces.Services;
using Blogedium_api.Modals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Blogedium_api.Controllers
{
    [Route("blog")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        // [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpPost("")]
        public async Task<ActionResult<BlogModal>> CreateBlog(BlogModal blogModal)
        {
            try
            {
                var blog = await _blogService.CreateBlogAsync(blogModal);
                return CreatedAtAction(nameof(GetBlogById), new {id = blogModal.Id}, blog);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogModal>> GetBlogById (int id)
        {
            try 
            {
                var user = await _blogService.GetBlogAsync(id);
                return Ok(user);
            }
            catch ( NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<BlogModal>>> GetAllBlogs ()
        {
            try
            {       
                var users = await _blogService.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<BlogModal>> DeleteBlog (int id)
        {
            try
            {
                var user = await _blogService.DeleteBlogAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BlogModal>> UpdateBlog (int id, BlogModal blogModal)
        {
            try
            {
                var user = await _blogService.UpdateBlogAsync(id, blogModal);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("{id}/read")]
        public async Task<ActionResult<BlogModal>> CountReact (int id)
        {
            await _blogService.IncrementReadCountAsync(id);
            return Ok();
        }

    }
}