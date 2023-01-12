using Dal.Models;
using Dal.Repository;

namespace Dal.Services
{
    public class ImageService
    {
        private readonly ProductImageRepository _productImageRepository;
        private readonly CommentImageRepository _commentImageRepository;

        public ImageService(ProductImageRepository productImageRepository, CommentImageRepository commentImageRepository)
        {
            _productImageRepository = productImageRepository;
            _commentImageRepository = commentImageRepository;
        }

        public virtual async Task AddProductImageAsync(ProductImage productImage)
        {
            await _productImageRepository.CreateAsync(productImage);
        }

        public virtual async Task<IReadOnlyCollection<CommentImage>> GetAllCommentImagesAsync() => await _commentImageRepository.GetAllAsync();

        public virtual async Task AddCommentImageAsync(CommentImage commentImage)
        {
            await _commentImageRepository.CreateAsync(commentImage);
        }
    }
}