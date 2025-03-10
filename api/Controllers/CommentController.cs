using api.Dtos.Comment;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<User> _userManager;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo, UserManager<User> userManager)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllCommentsAsync()
        {
            //Inherited from ControllerBase
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _commentRepo.GetAllCommentsAsync();
            var commentDto = comments.Select(c => c.FromCommentModelToCommentDto());
            return Ok(commentDto);
        }

        // [HttpGet]
        // [Route("{id}")]
        [HttpGet("{id:int}")]
        // this or that? Only one way to figure out; yeah, works both ways
        public async Task<IActionResult> GetCommentByIdAsync([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _stockRepo.StockExistsAsync(stockId))
            {
                return BadRequest("Stock doesnot exist!");
            }

            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            var commentModel = createCommentDto.CreateCommentDtoToCommentModel(stockId);
            commentModel.UserId = user.Id;
            var comment = await _commentRepo.PostCommentAsync(commentModel);
            //the id there is the id for the route
            //new is used to create a new instance of the anonymous object to be passed to the GetByIdAsync method
            return CreatedAtAction(nameof(GetCommentByIdAsync), new { id = comment }, comment.FromCommentModelToCommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateCommentAsync([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var commentModel = await _commentRepo.UpdateCommentAsync(id, updateDto);
            if (commentModel == null)
            {
                return NotFound();
            }
            return Ok(commentModel.FromCommentModelToCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCommentAsync([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var commentModel = await _commentRepo.DeleteCommentAsync(id);
            if (commentModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}