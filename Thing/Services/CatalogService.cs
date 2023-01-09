using Thing.Models;
using Thing.Repository;

namespace Thing.Services
{
    public class CatalogService
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly CommentRepository _commentRepository;
        private readonly AnswerRepository _answerRepository;
        private readonly FavoriteRepository _favoriteRepository;
        private readonly OrderRepository _orderRepository;

        public CatalogService(ProductRepository productRepository, CategoryRepository categoryRepository, CommentRepository commentRepository,
            AnswerRepository answerRepository, FavoriteRepository favoriteRepository, OrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _commentRepository = commentRepository;
            _answerRepository = answerRepository;
            _favoriteRepository = favoriteRepository;
            _orderRepository = orderRepository;
        }

        public virtual async Task<IReadOnlyCollection<Category>> GetAllCategoriesAsync() => await _categoryRepository.GetAllAsync();

        public virtual async Task AddCommentAsync(Comment comment)
        {
            await _commentRepository.CreateAsync(comment);
        }

        public virtual async Task AddFavoriteAsync(Favorite favorit)
        {
            await _favoriteRepository.CreateAsync(favorit);
        }

        public virtual async Task<bool> IsFavoriteExistsAsync(Favorite favorite) => await _favoriteRepository.IsFavoriteExistsAsync(favorite);

        public async Task<IReadOnlyCollection<Comment>> GetCommentsWithAnswersAsync(int productId)
        {
            return await _commentRepository.FindWithAnswersImageUserAsync(c => c.ProductId == productId);
        }

        public async Task<IReadOnlyCollection<Product>> GetProductsForCategoryAsync(int categoryId)
        {
            return await _productRepository.FindWithImages(p => p.CategoryId == categoryId);
        }

        public async Task<Product?> GetProductDetailsAsync(int productId)
        {
            return await _productRepository.FindWithImagesProps(productId);
        }

        public virtual async Task AddAnswerAsync(Answer answer)
        {
            await _answerRepository.CreateAsync(answer);
        }

        public virtual async Task<bool> IsOrderExistsAsync(Order Order) => await _orderRepository.IsOrderExistsAsync(Order);

        public virtual async Task AddOrderAsync(Order Order)
        {
            await _orderRepository.CreateAsync(Order);
        }
    }
}