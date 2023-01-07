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
            var product = await Entities.FirstOrDefaultAsync(x => x.Id == id);
            if (product != null) Entities.Remove(product);
        }

        public async Task<Product?> DeleteAndReturn(int id)
        {
            var product = await Entities.FirstOrDefaultAsync(x => x.Id == id);
            if (product != null) Entities.Remove(product);
            return product;
        }

        public async Task Edit(Product product)
        {
            Entities.Update(product);
            await _db.SaveChangesAsync();
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            return Entities.FirstOrDefaultAsync(p => p.Id == id).Result;
        }

    }
}