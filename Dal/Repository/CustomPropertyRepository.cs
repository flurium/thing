using Dal.Context;
using Dal.Repository.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repository
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