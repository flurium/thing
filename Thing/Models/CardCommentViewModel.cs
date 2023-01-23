namespace Thing.Models
{
    public class CardCommentViewModel
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public string Pros { get; set; }
        public string Cons { get; set; }
        public string? CommentatorName { get; set; }
    }
}