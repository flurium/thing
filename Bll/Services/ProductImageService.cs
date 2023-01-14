using System.Linq.Expressions;
using Microsoft.AspNetCore.Hosting;
using Dal.Models;
using Dal.Repository;
using Domain.Models;
using Dal.UnitOfWork;

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

        public async Task<IReadOnlyCollection<ProductImage>> FindByConditionAsync(Expression<Func<ProductImage, bool>> conditon)
          =>  await _unitOfWork.ProductImageRepository.FindByConditionAsync(conditon);

        public async Task<IReadOnlyCollection<ProductImage>> List() => await _unitOfWork.ProductImageRepository.GetAllAsync();

        public async Task CreateAsync(ProductImage productImage)
        {
            if (productImage != null)
            {
                await _unitOfWork.ProductImageRepository.CreateAsync(productImage);
            }
        }

        public async Task Delete(int id)
        {
            await _unitOfWork.ProductImageRepository.Delete(id);
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