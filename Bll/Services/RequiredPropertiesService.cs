using Thing.UnitOfWork;
using Domain.Models;
using System.Linq.Expressions;

namespace Thing.Services
{
    public class RequiredPropertiesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RequiredPropertiesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Create(int categoryId, string propertyName)
        {
            try
            {
                propertyName = propertyName.Trim();
                if (propertyName == "") return false;

                var prop = await _unitOfWork.RequiredPropertyRepository.CreateAndReturnAsync(new RequiredProperty { Name = propertyName, CategoryId = categoryId });
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
                await _unitOfWork.RequiredPropertyRepository.Update(id, name);
                return true;
            }
            catch (Exception) { return false; }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                await _unitOfWork.RequiredPropertyRepository.Delete(id);
                return true;
            }
            catch (Exception) { return false; }
        }

        public async Task<IReadOnlyCollection<RequiredProperty>> FindByConditioAsync(Expression<Func<RequiredProperty, bool>> conditon)
        {
            return await _unitOfWork.RequiredPropertyRepository.FindByConditionAsync(conditon);
        }
    }
}