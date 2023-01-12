using System.Linq.Expressions;
using Dal.Models;
using Dal.Repository;

namespace Dal.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            if (product != null)
            {
                return await _productRepository.CreateAsync(product);
            }
            return null;
        }

        public async Task Delete(int id)
        {
            await _productRepository.Delete(id);
        }

        public async Task<IReadOnlyCollection<Product>> FindByConditionAsync(Expression<Func<Product, bool>> conditon)
        {
            return await _productRepository.FindByConditionAsync(conditon);
        }

        public async Task<IReadOnlyCollection<Product>> Filter(BanProductFilter filter)
        {
            List<Expression<Func<Product, bool>>> predicates = new();

            if (filter.Id != null) predicates.Add(p => p.Id == filter.Id);
            if (filter.Name != "") predicates.Add(p => p.Name.StartsWith(filter.Name));
            if (filter.Price != null) predicates.Add(p => p.Price == filter.Price);
            if (filter.SellerEmail != "") predicates.Add(p => p.Seller.User.Email.StartsWith(filter.SellerEmail));
            if (filter.SellerName != "") predicates.Add(p => p.Seller.User.UserName.StartsWith(filter.SellerName));

            return await _productRepository.FindByConditionsAsync(predicates);
        }

        public async Task<Product> FirstOfDefult(Expression<Func<Product, bool>> conditon)
        {
            return await _productRepository.FirstOfDefult(conditon);
        }

        public async Task Edit(Product product)
        {
            await _productRepository.Edit(product);
        }
    }
}