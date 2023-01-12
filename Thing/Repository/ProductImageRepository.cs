using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Dal.Context;
using Dal.Models;
using Dal.Repository.Interfaces;

namespace Dal.Repository
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

        public async Task<ProductImage> GetImageByProductIdAsync(int Id) => await Entities.FirstOrDefaultAsync(x => x.ProductId == Id);

        public async Task<IReadOnlyCollection<ProductImage>> FindByConditionAsync(Expression<Func<ProductImage, bool>> conditon)
           => await Entities.Where(conditon).ToListAsync().ConfigureAwait(false);

      

    }
}