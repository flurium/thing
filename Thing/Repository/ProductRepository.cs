using Microsoft.EntityFrameworkCore;
using Thing.Context;
using Thing.Models;
using Thing.Repository.Interfaces;

namespace Thing.Repository
{
    // MADE BY MARIA
    public class ProductRepository : BaseRepository<Product>, IPtoductRepository
    {
        public ProductRepository(ThingDbContext context) : base(context)
        {
        }

        public async Task Delete(int id)
        {
            var product= await Entities.FirstOrDefaultAsync(x => x.Id == id);
            if(product != null) Entities.Remove(product);
        }

        public async Task Edit(Product product)
        {
            _db.Update(product);
            await _db.SaveChangesAsync();
        }
    }
}
