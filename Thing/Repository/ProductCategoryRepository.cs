using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Thing.Context;
using Thing.Models;
using Thing.Repository.Interfaces;

namespace Thing.Repository
{
    public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(ThingDbContext context) : base(context)
        {
        }


        public async Task Delete(int productId, int categoryId)
        {
            var productCategory = await _db.ProductCategories.FirstOrDefaultAsync(x => (x.ProductId == productId) && (x.CategoryId == categoryId));
            if (productCategory != null)
            {
                _db.Remove(productCategory);
            }

        }


    }
}
