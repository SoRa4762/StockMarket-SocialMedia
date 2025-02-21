using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
// using api.Repository; //no need to import

namespace api.Repository
{
    //* CAREFUL: you will get error message if you do not implement interfaces
    public class CommentRepository : ICommentRepository
    {
        //will light up only when used by other methods except constructor
        private readonly ApplicationDBContext _context;
        //why did I do this? I bet I had to use this somewhere... (probably wrong on that one)
        //yeah I use this, in controller, and not DTO but Repository... damn
        // private readonly UpdateCommentRequestDto _updateDto;

        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
            // _updateDto = updateDto;
        }

        public async Task<Comment?> DeleteCommentAsync(int id)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (commentModel == null)
            {
                return null;
            }
            // _context.Remove(id); //you dont't pass id bruh...
            _context.Remove(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            var comments = await _context.Comments.Include(c => c.User).ToListAsync();
            return comments; // I could've directly: return await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            var comment = await _context.Comments.Include(c => c.User).FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null)
            {
                return null;
            }
            return comment;
        }

        public async Task<Comment> PostCommentAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> UpdateCommentAsync(int id, UpdateCommentRequestDto updateDto)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (existingComment == null)
            {
                return null;
            }

            existingComment.Title = updateDto.Title;
            existingComment.Content = updateDto.Content;
            existingComment.UpdatedOn = updateDto.UpdatedOn;

            await _context.SaveChangesAsync();
            return existingComment;
        }
    }
}