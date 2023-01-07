using System.Linq.Expressions;
using Thing.Models;
using Thing.Models.ViewModels;
using Thing.Repository;

namespace Thing.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task CreateAsync(Product product)
        {
            if (product != null)
            {
                await _productRepository.CreateAsync(product);
            }
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

        public async Task Edit(Product product)
        {
            await _productRepository.Edit(product);
        }
    }
}