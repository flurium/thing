using Dal.UnitOfWork;
using Domain.Models;

namespace Dal.Services
{
    public class CatalogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CatalogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<IReadOnlyCollection<Category>> GetAllCategoriesAsync() => await _unitOfWork.CategoryRepository.GetAllAsync();

        public virtual async Task AddCommentAsync(Comment comment)
        {
            await _unitOfWork.CommentRepository.CreateAsync(comment);
        }

        public virtual async Task AddFavoriteAsync(Favorite favorit)
        {
            await _unitOfWork.FavoriteRepository.CreateAsync(favorit);
        }

        public virtual async Task<bool> IsFavoriteExistsAsync(Favorite favorite) => await _unitOfWork.FavoriteRepository.IsFavoriteExistsAsync(favorite);

        public async Task<IReadOnlyCollection<Comment>> GetCommentsWithAnswersAsync(int productId)
        {
            return await _unitOfWork.CommentRepository.FindWithAnswersImageUserAsync(c => c.ProductId == productId);
        }

        public async Task<IReadOnlyCollection<Product>> GetProductsForCategoryAsync(int categoryId)
        {
            return await _unitOfWork.ProductRepository.FindWithImages(p => p.CategoryId == categoryId);
        }

        public async Task<Product?> GetProductDetailsAsync(int productId)
        {
            return await _unitOfWork.ProductRepository.FindWithImagesProps(productId);
        }

        public virtual async Task AddAnswerAsync(Answer answer)
        {
            await _unitOfWork.AnswerRepository.CreateAsync(answer);
        }
    }
}