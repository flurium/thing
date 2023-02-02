namespace Domain.Models
{
  public class Comment
  {
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int Grade { get; set; }
    public string Content { get; set; }

    /// <summary> Good things about product </summary>
    public string? Pros { get; set; }

    /// <summary> Bad things about product </summary>
    public string? Cons { get; set; }

    public string? UserId { get; set; }
    public User? User { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public IReadOnlyCollection<Answer> Answers { get; set; }
    public IReadOnlyCollection<CommentImage> CommentImages { get; set; }
  }
}