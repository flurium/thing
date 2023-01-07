using Microsoft.EntityFrameworkCore;
using Thing.Context;
using Thing.Models;
using Thing.Repository.Interfaces;

namespace Thing.Repository
{
    public class RequiredPropertyRepository : BaseRepository<RequiredProperty>, IRequiredPropertyRepository
    {
        public RequiredPropertyRepository(ThingDbContext context) : base(context)
        {
        }

        public async Task<RequiredProperty?> CreateAndReturnAsync(RequiredProperty entity)
        {
            var prop = await Entities.AddAsync(entity).ConfigureAwait(false);
            await _db.SaveChangesAsync();
            return prop.Entity;
        }

        public async Task Update(int id, string name)
        {
            var prop = await Entities.FirstOrDefaultAsync(c => c.Id == id).ConfigureAwait(false);
            if (prop != null) prop.Name = name;
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var property = await Entities.FirstOrDefaultAsync(x => x.Id == id);
            if (property != null) Entities.Remove(property);
            await _db.SaveChangesAsync();
        }
    }
}