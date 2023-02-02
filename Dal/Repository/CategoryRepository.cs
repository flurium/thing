using Dal.Context;
using Dal.Repository.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dal.Repository
{
  public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
  {
    public CategoryRepository(ThingDbContext context) : base(context)
    {
    }

    public virtual async Task<IReadOnlyCollection<Category>> FindByConditionWithPropertiesAsync(Expression<Func<Category, bool>> conditon)
        => await Entities.Include(c => c.RequiredProperties).Where(conditon).ToListAsync().ConfigureAwait(false);

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