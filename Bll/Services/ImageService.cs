using Thing.UnitOfWork;
using Domain.Models;

namespace Thing.Services
{
    public class ImageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ImageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual async Task AddCommentImageAsync(CommentImage commentImage)
        {
            await _unitOfWork.CommentImageRepository.CreateAsync(commentImage);
        }
    }
}