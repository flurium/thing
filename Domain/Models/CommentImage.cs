namespace Domain.Models
{
  public class CommentImage
  {
    public int Id { get; set; }
    public string ImagePath { get; set; }

    public int CommentId { get; set; }
    public Comment Comment { get; set; }
  }
}