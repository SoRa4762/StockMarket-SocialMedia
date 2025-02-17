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
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCommentsAsync()
        {
            var comments = await _commentRepo.GetAllCommentsAsync();
            var commentDto = comments.Select(c => c.FromCommentModelToCommentDto());
            return Ok(commentDto);
        }

        // [HttpGet]
        // [Route("{id}")]
        [HttpGet("{id}")]
        // this or that? Only one way to figure out; yeah, works both ways
        public async Task<IActionResult> GetCommentByIdAsync([FromRoute] int id)
        {
            var commentDto = await _commentRepo.GetCommentByIdAsync(id);
            if (commentDto == null)
            {
                return NotFound();
            }
            return Ok(commentDto.FromCommentModelToCommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> PostComment([FromRoute] int stockId, [FromBody] CreateCommentRequestDto createCommentDto)
        {
            //turn it into comment model and then back to commentDTO
            // var commentModel = createCommentDto.CreateCommentDtoToCommentModel();
            // var comment = await _commentRepo.PostCommentAsync(commentModel);
            // // return Ok(stockDto); I could just do this
            // //* I did this because this is a test: Test Failed!
            // //! Error: No route matches the supplied values
            // return CreatedAtAction(nameof(GetByIdAsync), new { id = comment.Id }, comment.FromCommentModelToCommentDto());

            if (!await _stockRepo.StockExistsAsync(stockId))
            {
                return BadRequest("Stock doesnot exist!");
            }

            var commentModel = createCommentDto.CreateCommentDtoToCommentModel(stockId);
            var comment = await _commentRepo.PostCommentAsync(commentModel);
            //the id there is the id for the route
            //new is used to create a new instance of the anonymous object to be passed to the GetByIdAsync method
            return CreatedAtAction(nameof(GetCommentByIdAsync), new { id = comment }, comment.FromCommentModelToCommentDto());
        }
    }
}