using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment
{
    public class CreateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be atleast 5 characters")]
        [MaxLength(280, ErrorMessage = "Title must be less than 280 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be atleast 5 characters")]
        [MaxLength(1111, ErrorMessage = "Content must be less than 1111 characters")]
        public string Content { get; set; } = string.Empty;
    }
}