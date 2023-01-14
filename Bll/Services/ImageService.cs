using Dal.Models;
using Dal.Repository;
using Dal.UnitOfWork;
using Domain.Models;

namespace Dal.Services
{
    public class ImageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ImageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual async Task AddProductImageAsync(ProductImage productImage)
        {
            await _unitOfWork.ProductImageRepository.CreateAsync(productImage);
        }

        public virtual async Task<IReadOnlyCollection<CommentImage>> GetAllCommentImagesAsync() => await _unitOfWork.CommentImageRepository.GetAllAsync();

        public virtual async Task AddCommentImageAsync(CommentImage commentImage)
        {
            await _unitOfWork.CommentImageRepository.CreateAsync(commentImage);
        }
    }
}