using Thing.Models;
using Thing.Repository;

namespace Thing.Services
{
    public class CustomPropertyService
    {
        private readonly CustomPropertyRepository _customPropertyRepository;

        public CustomPropertyService(CustomPropertyRepository customPropertyRepository)
        {
            _customPropertyRepository = customPropertyRepository;
        }

        public async Task CreateAsync(CustomProperty property)
        {
            if (property != null)
            {
                await _customPropertyRepository.CreateAsync(property);
            }
        }
    }
}
