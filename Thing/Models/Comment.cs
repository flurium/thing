
namespace Thing.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public string Summary { get; set; }
        public string UserId { get; set; }
        public DateTime CommentDate { get; set; }
        public int ProductId { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
