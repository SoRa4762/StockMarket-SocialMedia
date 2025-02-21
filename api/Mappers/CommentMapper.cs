using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    // because of mapper you can know what to do with the data based on if it is coming from model or if it is DTO
    public static class CommentMapper
    {
        public static CommentDto FromCommentModelToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                UpdatedOn = commentModel.UpdatedOn,
                CreatedBy = commentModel.User.UserName,
                StockId = commentModel.StockId
            };
        }

        //takes necessary data for DTO and returns model
        public static Comment CreateCommentDtoToCommentModel(this CreateCommentRequestDto commentDto, int stockId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId
            };
        }
    }
}