using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Thing.Context;
using Thing.Models;
using Thing.Repository.Interfaces;

namespace Thing.Repository
{
    public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(ThingDbContext context) : base(context)
        {
        }

        public async Task Delete(int id)
        {
            var property = await Entities.FirstOrDefaultAsync(x => x.Id == id);
            if (property != null) Entities.Remove(property);
        }

       
    }
}
