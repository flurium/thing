using System.Linq.Expressions;
using Thing.Models;
using Thing.Repository;

namespace Thing.Services
{
    public class RequiredPropertyValueService
    {
        private readonly PropertyValueRepository _propertyValueRepository;

        public RequiredPropertyValueService(PropertyValueRepository propertyRepository)
        {
            _propertyValueRepository= propertyRepository;
        }

        public async Task CreateAsync(RequiredPropertyValue property)
        {
            if (property != null)
            {
                 await _propertyValueRepository.CreateAsync(property);
            }
        }

        public async Task<IReadOnlyCollection<RequiredPropertyValue>> FindByConditionAsync(Expression<Func<RequiredPropertyValue, bool>> conditon)
        {
            return await _propertyValueRepository.FindByConditionAsync(conditon);
        }
    }
}
