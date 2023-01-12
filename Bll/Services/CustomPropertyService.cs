using Dal.Models;
using Dal.Repository;

namespace Dal.Services
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