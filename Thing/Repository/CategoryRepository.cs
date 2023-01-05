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

        public async Task Delete(int id)
        {
            var category = await Entities.FirstOrDefaultAsync(c => c.Id == id).ConfigureAwait(false);
            if (category != null) Entities.Remove(category);
            await _db.SaveChangesAsync();
        }

        public async Task Update(int id, string name)
        {
            var category = await Entities.FirstOrDefaultAsync(c => c.Id == id).ConfigureAwait(false);
            if (category != null) category.Name = name;
            await _db.SaveChangesAsync();
        }
    }
}