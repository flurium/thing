using System.Linq.Expressions;
using Microsoft.AspNetCore.Hosting;
using Dal.Models;
using Dal.Repository;

namespace Dal.Services
{
    public class ProductImageService
    {
        private readonly ProductImageRepository _productImageRepository;
        private readonly IWebHostEnvironment _host;


        public ProductImageService(ProductImageRepository productImageRepository, IWebHostEnvironment appEnv)
        {
            _productImageRepository = productImageRepository;
            _host = appEnv;
        }

        public async Task<IReadOnlyCollection<ProductImage>> FindByConditionAsync(Expression<Func<ProductImage, bool>> conditon)
          =>  await _productImageRepository.FindByConditionAsync(conditon);

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

        public async Task DeleteFromServer(int ProductId)
        {
            var images = await _productImageRepository.FindByConditionAsync(x => x.ProductId == ProductId);
            foreach (var image in images)
            {
                string fullPath = _host.WebRootPath + image.Url;
                if (System.IO.File.Exists(fullPath))
                {
                    try
                    {
                        System.IO.File.Delete(fullPath);

                    }
                    catch (Exception e)
                    {

                    }
                }

            }
        }
    }
}