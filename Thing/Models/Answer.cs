namespace Thing.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int CommentId { get; set; }
        public Comment Comment { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}