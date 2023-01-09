using System.Linq.Expressions;
using Thing.Models;
using Thing.Repository;

namespace Thing.Services
{
    public class RequiredPropertiesService
    {
        private readonly RequiredPropertyRepository _propertyRepository;

        public RequiredPropertiesService(RequiredPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<bool> Create(int categoryId, string propertyName)
        {
            try
            {
                propertyName = propertyName.Trim();
                if (propertyName == "") return false;

                var prop = await _propertyRepository.CreateAndReturnAsync(new RequiredProperty { Name = propertyName, CategoryId = categoryId });
                if (prop == null) return false;

                return true;
            }
            catch (Exception) { return false; }
        }

        /// <returns>Is operation success</returns>
        public async Task<bool> Edit(int id, string name)
        {
            try
            {
                name = name.Trim();
                if (name == "") return false;
                await _propertyRepository.Update(id, name);
                return true;
            }
            catch (Exception) { return false; }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                await _propertyRepository.Delete(id);
                return true;
            }
            catch (Exception) { return false; }
        }

        public async Task<IReadOnlyCollection<RequiredProperty>> FindByConditioAsync(Expression<Func<RequiredProperty, bool>> conditon)
        {
            return await _propertyRepository.FindByConditionAsync(conditon);
        }
    }
}