using api.Dtos.Comment;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAllCommentsAsync();
        public Task<Comment?> GetCommentByIdAsync(int id);
        public Task<Comment> PostCommentAsync(Comment comment);
        public Task<Comment?> UpdateCommentAsync(int id, UpdateCommentRequestDto updateDto);
        public Task<Comment?> DeleteCommentAsync(int id);
    }
}