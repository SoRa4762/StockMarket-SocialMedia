using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        public CommentController(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCommentsAsync()
        {
            var comments = await _commentRepo.GetAllCommentsAsync();
            var commentDto = comments.Select(c => c.FromCommentModelToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet]
        [Route("{id}")]
        // [HttpGet("{id}")]
        // this or that? Only one way to figure out; yeah, i need to 
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var commentDto = await _commentRepo.GetCommentByIdAsync(id);
            if (commentDto == null)
            {
                return NotFound();
            }
            return Ok(commentDto.FromCommentModelToCommentDto());
        }

        [HttpPost]
        public async Task<IActionResult> PostComment([FromBody] CreateCommentRequestDto createCommentDto)
        {
            //turn it into comment model and then back to commentDTO
            var commentModel = createCommentDto.CreateCommentDtoToCommentModel();
            var comment = await _commentRepo.PostCommentAsync(commentModel);
            // return Ok(stockDto); I could just do this
            //* I did this because this is a test: Test Failed!
            //! Error: No route matches the supplied values
            return CreatedAtAction(nameof(GetByIdAsync), new { id = comment.Id }, comment.FromCommentModelToCommentDto());
        }
    }
}