using Microsoft.EntityFrameworkCore;
using Thing.Context;
using Thing.Models;
using Thing.Repository.Interfaces;

namespace Thing.Repository
{
    public class ProductImageRepository : BaseRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(ThingDbContext context) : base(context)
        {
        }

        public async Task Delete(int id)
        {
            var img = await Entities.FirstOrDefaultAsync(x => x.Id == id);
            if (img != null) Entities.Remove(img);
        }
    }
}