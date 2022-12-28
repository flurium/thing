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

        public Task Delete(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
