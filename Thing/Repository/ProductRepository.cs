using Microsoft.EntityFrameworkCore;
using Thing.Context;
using Thing.Models;
using Thing.Repository.Interfaces;

namespace Thing.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ThingDbContext context) : base(context)
        {
        }

        public async Task Delete(int id)
        {
            if (id != null && _db.Products != null)
            {
               var product= await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
                if(product != null)
                {
                    _db.Remove(product);
                }
            }
        }

        public async Task Edit(Product product)
        {
            _db.Update(product);
            await _db.SaveChangesAsync();
        }
    }
}
