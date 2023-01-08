using Thing.Models;
using Thing.Repository;

namespace Thing.Services
{
    public class ProductImageService
    {
        private readonly ProductImageRepository _productImageRepository;

        public ProductImageService(ProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }

        public async Task<IReadOnlyCollection<ProductImage>> List() => await _productImageRepository.GetAllAsync();

        public async Task CreateAsync(ProductImage productImage)
        {
            if (productImage != null)
            {
                await _productImageRepository.CreateAsync(productImage);
            }

        }

        public async Task Delete(int id)
        {
            await _productImageRepository.Delete(id);
        }

    }
}

