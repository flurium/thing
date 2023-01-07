using System.Linq.Expressions;
using Thing.Models;
using Thing.Repository;

namespace Thing.Services
{
    public class CatalogService
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly ProductCategoryRepository _productCategoryRepository;
        private readonly ProductImageRepository _productImageRepository;
        private readonly CommentRepository _commentRepository;
        private readonly AnswerRepository _answerRepository;
        private readonly CommentImageRepository _commentImageRepository;

        public CatalogService(ProductRepository productRepository, CategoryRepository categoryRepository, ProductCategoryRepository productCategoryRepository, ProductImageRepository productImageRepository, CommentRepository commentRepository, AnswerRepository answerRepository, CommentImageRepository commentImageRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _productCategoryRepository = productCategoryRepository;
            _productImageRepository = productImageRepository;
            _commentRepository = commentRepository;
            _answerRepository = answerRepository;
            _commentImageRepository = commentImageRepository;
        }

        public virtual async Task<IReadOnlyCollection<Category>> GetAllCategoriesAsync() => await _categoryRepository.GetAllAsync();

        public virtual async Task<IReadOnlyCollection<Comment>> GetAllCommentsAsync() => await _commentRepository.GetAllAsync();

        public virtual async Task<IReadOnlyCollection<Comment>> GetProductCommentsByIdAsync(int Id) => await _commentRepository.FindByConditionAsync(c => c.ProductId == Id);

        public virtual async Task<IReadOnlyCollection<Answer>> GetAllAnswersAsync() => await _answerRepository.GetAllAsync();

        public virtual async Task<IReadOnlyCollection<CommentImage>> GetAllCommentsImagesAsync() => await _commentImageRepository.GetAllAsync();

        public async Task<ICollection<Product>> FindProductsByCategoryIdAsync(int CategoryId)
        {
            var productsCategories = await _productCategoryRepository.FindByConditionAsync(pc => pc.CategoryId == CategoryId);
            var products = new List<Product>();
            foreach (var productCategory in productsCategories)
            {
                var product = await _productRepository.GetByIdAsync(productCategory.ProductId);
                products.Add(product);
            }
            return products;
        }

        public virtual async Task AddCommentAsync(Comment comment)
        {
            await _commentRepository.CreateAsync(comment);
        }

        public virtual async Task<IReadOnlyCollection<ProductImage>> GetAllImagesAsync() => await _productImageRepository.GetAllAsync();

        public virtual async Task<IReadOnlyCollection<ProductImage>> GetProductImagesById(int Id) => await _productImageRepository.FindByConditionAsync(pi => pi.ProductId == Id);

        public virtual async Task<Product> GetProductByIdAsync(int Id) => await _productRepository.GetByIdAsync(Id);

        public virtual async Task AddFavoriteAsync(Favorite favorit)
        {
            await _commentRepository.CreateAsync(favorit);
        }
    }
}