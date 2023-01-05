using System.Linq.Expressions;
using Thing.Models;
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

        public async Task Edit(Product product)
        {
            await _productRepository.Edit(product);
        }

    }
}
