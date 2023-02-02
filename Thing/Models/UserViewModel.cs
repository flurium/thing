namespace Thing.Models
{
  public class UserViewModel
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsBanned { get; set; }
    public bool IsAdmin { get; set; }
  }
}