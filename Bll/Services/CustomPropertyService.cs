using Dal.Models;
using Dal.Repository;
using Dal.UnitOfWork;
using Domain.Models;

namespace Dal.Services
{
    public class CustomPropertyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomPropertyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CustomProperty property)
        {
            if (property != null)
            {
                await _unitOfWork.CustomPropertyRepository.CreateAsync(property);
            }
        }
    }
}