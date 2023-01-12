using Microsoft.EntityFrameworkCore;
using Dal.Context;
using Dal.Models;
using Dal.Repository.Interfaces;

namespace Dal.Repository
{
    public class PropertyValueRepository : BaseRepository<RequiredPropertyValue>, IPropertyValueRepository
    {
        public PropertyValueRepository(ThingDbContext context) : base(context)
        {
        }

        public async Task Delete(int id)
        {
            var propertyValue = await Entities.FirstOrDefaultAsync(x => x.Id == id);
            if (propertyValue != null) Entities.Remove(propertyValue);
        }
    }
}