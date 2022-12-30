using Microsoft.EntityFrameworkCore;
using Thing.Context;
using Thing.Models;
using Thing.Repository.Interfaces;

namespace Thing.Repository
{
    public class PropertyValueRepository : BaseRepository<PropertyValue>, IPropertyValueRepository
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
