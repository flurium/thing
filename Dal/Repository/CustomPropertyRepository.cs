using Thing.Context;
using Thing.Repository.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Thing.Repository
{
    public class CustomPropertyRepository : BaseRepository<CustomProperty>, ICustomPropertyRepository
    {
        public CustomPropertyRepository(ThingDbContext context) : base(context)
        {
        }

        public async Task Delete(int Id)
        {
            var customProperty = await Entities.FirstOrDefaultAsync(o => o.Id == Id).ConfigureAwait(false);
            if (customProperty != null) Entities.Remove(customProperty);
            _db.SaveChanges();
        }
    }
}