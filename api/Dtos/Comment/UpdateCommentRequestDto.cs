namespace api.Dtos.Comment
{
    public class UpdateCommentRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        // public int? StockId { get; set; } //probably won't need this here
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}