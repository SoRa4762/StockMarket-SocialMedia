using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ICommentRepository _commentRepo;
        public CommentController(ApplicationDBContext context, ICommentRepository commentRepo)
        {
            _context = context;
            _commentRepo = commentRepo;
        }

        [HttpGet]
        [Route("{id}")]
        // [HttpGet("{id}")] this or that? Only one way to figure out
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
        public async Task<IActionResult> Create([FromBody] CreateCommentRequestDto createCommentDto)
        {
            //turn it into comment model and then back to commentDTO
            var commentModel = createCommentDto.CreateCommentDtoToCommentModel();
            await _commentRepo.PostComment(commentModel);
            // return Ok(stockDto); I could just do this
            return CreatedAtAction(nameof(GetByIdAsync), new { id = commentModel.Id }, commentModel.FromCommentModelToCommentDto());
        }
    }
}