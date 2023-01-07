using System.Linq.Expressions;
using Thing.Models;
using Thing.Repository;
namespace Thing.Services
{
    public class CatalogService
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;

        private readonly ProductImageRepository _productImageRepository;
        private readonly CommentRepository _commentRepository;
        private readonly AnswerRepository _answerRepository;
        private readonly CommentImageRepository _commentImageRepository;
        private readonly FavoriteRepository _favoriteRepository;

        public CatalogService(ProductRepository productRepository, CategoryRepository categoryRepository,
            ProductImageRepository productImageRepository, CommentRepository commentRepository,
            AnswerRepository answerRepository, CommentImageRepository commentImageRepository, FavoriteRepository favoriteRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _productImageRepository = productImageRepository;
            _commentRepository = commentRepository;
            _answerRepository = answerRepository;
            _commentImageRepository = commentImageRepository;
            _favoriteRepository = favoriteRepository;
        }

        public virtual async Task<IReadOnlyCollection<Category>> GetAllCategoriesAsync() => await _categoryRepository.GetAllAsync();

        public virtual async Task<IReadOnlyCollection<Comment>> GetAllCommentsAsync() => await _commentRepository.GetAllAsync();

        public virtual async Task<IReadOnlyCollection<Comment>> GetProductCommentsByIdAsync(int Id) => await _commentRepository.FindByConditionAsync(c => c.ProductId == Id);

        public virtual async Task<IReadOnlyCollection<Answer>> GetAllAnswersAsync() => await _answerRepository.GetAllAsync();

        public virtual async Task<IReadOnlyCollection<CommentImage>> GetAllCommentsImagesAsync() => await _commentImageRepository.GetAllAsync();

        public async Task<IReadOnlyCollection<Product>> FindProductsByCategoryIdAsync(int CategoryId) => await _productRepository.FindByConditionAsync(pc => pc.CategoryId == CategoryId);

        public virtual async Task AddCommentAsync(Comment comment)
        {
            await _commentRepository.CreateAsync(comment);
        }

        public virtual async Task<IReadOnlyCollection<ProductImage>> GetAllImagesAsync() => await _productImageRepository.GetAllAsync();

        public virtual async Task<IReadOnlyCollection<ProductImage>> GetProductImagesById(int Id) => await _productImageRepository.FindByConditionAsync(pi => pi.ProductId == Id);

        public virtual async Task<Product> GetProductByIdAsync(int Id) => await _productRepository.GetByIdAsync(Id);

        public virtual async Task AddFavoriteAsync(Favorite favorit)
        {
            await _favoriteRepository.CreateAsync(favorit);
        }

        public virtual async Task<bool> IsFavoriteExistsAsync(Favorite favorite) => await _favoriteRepository.IsFavoriteExistsAsync(favorite);
    }
}