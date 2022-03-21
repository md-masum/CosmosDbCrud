using CosmosDbCrud.Dto;
using CosmosDbCrud.Service;
using Microsoft.AspNetCore.Mvc;

namespace CosmosDbCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        #region Tag API

        [HttpGet("Tag")]
        public async Task<ActionResult<List<TagDto>>> TagList()
        {
            return Ok(await _blogService.GetAllTag());
        }

        [HttpGet("Tag/{id}")]
        public async Task<ActionResult<TagDto>> TagGet(string id)
        {
            return Ok(await _blogService.GetTagById(id));
        }

        [HttpPost("Tag")]
        public async Task<ActionResult<TagDto>> TagCreate([FromBody] TagDto item)
        {
            var data = await _blogService.CreateOrUpdateTag(item);
            return CreatedAtAction(nameof(TagGet), new { id = data.Id }, data);
        }

        [HttpPut("Tag/{id}")]
        public async Task<ActionResult<TagDto>> TagEdit([FromBody] TagDto item)
        {
            var data = await _blogService.CreateOrUpdateTag(item);
            return Ok(data);
        }

        [HttpDelete("Tag/{id}")]
        public async Task<IActionResult> TagDelete(string id)
        {
            await _blogService.DeleteTag(id);
            return NoContent();
        }

        #endregion

        #region Category API

        [HttpGet("Category")]
        public async Task<ActionResult<List<CategoryDto>>> CategoryList()
        {
            return Ok(await _blogService.GetAllCategory());
        }

        [HttpGet("Category/{id}")]
        public async Task<ActionResult<CategoryDto>> CategoryGet(string id)
        {
            return Ok(await _blogService.GetCategoryById(id));
        }

        [HttpPost("Category")]
        public async Task<ActionResult<CategoryDto>> CategoryCreate([FromBody] CategoryDto item)
        {
            var data = await _blogService.CreateOrUpdateCategory(item);
            return CreatedAtAction(nameof(CategoryGet), new { id = data.Id }, data);
        }

        [HttpPut("Category/{id}")]
        public async Task<ActionResult<CategoryDto>> CategoryEdit([FromBody] CategoryDto item)
        {
            var data = await _blogService.CreateOrUpdateCategory(item);
            return Ok(data);
        }

        [HttpDelete("Category/{id}")]
        public async Task<IActionResult> CategoryDelete(string id)
        {
            await _blogService.DeleteCategory(id);
            return Ok();
        }

        #endregion

        #region Comment API

        [HttpGet("Comment")]
        public async Task<ActionResult<List<CommentDto>>> CommentList([FromQuery]string postId)
        {
            return Ok(await _blogService.GetAllCommentByPostId(postId));
        }

        [HttpGet("Comment/{id}")]
        public async Task<ActionResult<CommentDto>> CommentGet(string id)
        {
            return Ok(await _blogService.GetCommentById(id));
        }

        [HttpPost("Comment")]
        public async Task<ActionResult<CommentDto>> CommentCreate([FromBody] CommentDto item)
        {
            var data = await _blogService.CreateOrUpdateComment(item);
            return CreatedAtAction(nameof(CommentGet), new { id = data.Id }, data);
        }

        [HttpPut("Comment/{id}")]
        public async Task<ActionResult<CommentDto>> CommentEdit([FromBody] CommentDto item)
        {
            var data = await _blogService.CreateOrUpdateComment(item);
            return Ok(data);
        }

        [HttpDelete("Comment/{id}")]
        public async Task<IActionResult> CommentDelete(string id)
        {
            await _blogService.DeleteComment(id);
            return Ok();
        }

        #endregion

        #region Post API

        [HttpGet("Post")]
        public async Task<ActionResult<List<PostDto>>> PostList()
        {
            return Ok(await _blogService.GetAllPost());
        }

        [HttpGet("Post/{id}")]
        public async Task<ActionResult<PostDto>> PostGet(string id)
        {
            return Ok(await _blogService.GetPostById(id));
        }

        [HttpPost("Post")]
        public async Task<ActionResult<PostDto>> PostCreate([FromBody] PostDto item)
        {
            var data = await _blogService.CreateOrUpdatePost(item);
            return CreatedAtAction(nameof(PostGet), new { id = data.Id }, data);
        }

        [HttpPut("Post/{id}")]
        public async Task<ActionResult<PostDto>> PostEdit([FromBody] PostDto item)
        {
            var data = await _blogService.CreateOrUpdatePost(item);
            return Ok(data);
        }

        [HttpDelete("Post/{id}")]
        public async Task<IActionResult> PostDelete(string id)
        {
            await _blogService.DeletePost(id);
            return Ok();
        }

        #endregion
    }
}
