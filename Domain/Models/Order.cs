namespace Domain.Models
{
    public class Order
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        // "INCART", "PAID"
        public string State { get; set; }

        public int Count { get; set; }
    }
}