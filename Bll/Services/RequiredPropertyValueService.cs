using Thing.UnitOfWork;
using Domain.Models;

namespace Thing.Services
{
    public class RequiredPropertyValueService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RequiredPropertyValueService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(RequiredPropertyValue property)
        {
            if (property != null)
            {
                await _unitOfWork.PropertyValueRepository.CreateAsync(property);
            }
        }
    }
}