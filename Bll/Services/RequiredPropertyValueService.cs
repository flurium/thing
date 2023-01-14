using System.Linq.Expressions;
using Dal.Models;
using Dal.Repository;
using Dal.UnitOfWork;
using Domain.Models;

namespace Dal.Services
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

        public async Task<IReadOnlyCollection<RequiredPropertyValue>> FindByConditionAsync(Expression<Func<RequiredPropertyValue, bool>> conditon)
        {
            return await _unitOfWork.PropertyValueRepository.FindByConditionAsync(conditon);
        }
    }
}