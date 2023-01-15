using Dal.UnitOfWork;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;

namespace Dal.Services
{
    public class ProductImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _host;

        public ProductImageService(IUnitOfWork unitOfWork, IWebHostEnvironment appEnv)
        {
            _unitOfWork = unitOfWork;
            _host = appEnv;
        }

        public async Task<IReadOnlyCollection<ProductImage>> List() => await _unitOfWork.ProductImageRepository.GetAllAsync();

        public async Task CreateAsync(ProductImage productImage)
        {
            if (productImage != null)
            {
                await _unitOfWork.ProductImageRepository.CreateAsync(productImage);
            }
        }

        public async Task DeleteFromServer(int ProductId)
        {
            var images = await _unitOfWork.ProductImageRepository.FindByConditionAsync(x => x.ProductId == ProductId);
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