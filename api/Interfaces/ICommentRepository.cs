using api.Dtos.Comment;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAllComments();
        public Task<Comment?> GetCommentByIdAsync(int id);
        public Task<Comment> PostComment(Comment comment);
        public Task<Comment?> UpdateComment(int id, UpdateCommentRequestDto updateDto);
        public Task<Comment?> DeleteComment(int id);
    }
}