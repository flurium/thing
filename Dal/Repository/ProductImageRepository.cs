using Thing.Context;
using Thing.Repository.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

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