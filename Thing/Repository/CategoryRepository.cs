using Microsoft.EntityFrameworkCore;
using Thing.Context;
using Thing.Models;
using Thing.Repository.Interfaces;

namespace Thing.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ThingDbContext context) : base(context)
        {

        }

        public async Task Delete(int Id)
        {
            var category = await Entities.FirstOrDefaultAsync(c => c.Id == Id).ConfigureAwait(false);
            if (category != null) Entities.Remove(category);
            _db.SaveChanges();
        }
    }
}
