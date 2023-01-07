namespace Thing.Models.ViewModels
{
    public class BanProductFilter
    {
        public int? Id { get; set; } = null;
        public string Name { get; set; } = "";
        public double? Price { get; set; } = null;
        public string SellerEmail { get; set; } = "";
        public string SellerName { get; set; } = "";
    }
}