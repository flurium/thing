using Bll.Models;
using Thing.UnitOfWork;
using Domain.Models;
using System.Linq.Expressions;

namespace Thing.Services
{
    public class ProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Product?> CreateAsync(Product product)
        {
            if (product != null)
            {
                return await _unitOfWork.ProductRepository.CreateAndReturnAsync(product);
            }
            return null;
        }

        public async Task Delete(int id)
        {
            await _unitOfWork.ProductRepository.Delete(id);
        }

        public async Task<IReadOnlyCollection<Product>> FindByConditionAsync(Expression<Func<Product, bool>> conditon)
        {
            return await _unitOfWork.ProductRepository.FindByConditionAsync(conditon);
        }

        public async Task<IReadOnlyCollection<Product>> Filter(BanProductFilter filter)
        {
            List<Expression<Func<Product, bool>>> predicates = new();

            if (filter.Id != null) predicates.Add(p => p.Id == filter.Id);
            if (filter.Name != "") predicates.Add(p => p.Name.StartsWith(filter.Name));
            if (filter.Price != null) predicates.Add(p => p.Price == filter.Price);
            if (filter.SellerEmail != "") predicates.Add(p => p.Seller.User.Email.StartsWith(filter.SellerEmail));
            if (filter.SellerName != "") predicates.Add(p => p.Seller.User.UserName.StartsWith(filter.SellerName));

            return await _unitOfWork.ProductRepository.FindByConditionsAsync(predicates);
        }
    }
}