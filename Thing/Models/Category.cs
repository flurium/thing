using System.Net.Http.Headers;

namespace Thing.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public List<Product> Products {get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
