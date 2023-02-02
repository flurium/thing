using Dal.Context;
using Dal.Repository.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

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