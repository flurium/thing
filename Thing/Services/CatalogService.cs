using Thing.Models;
using Thing.Models.ViewModels;
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
        private readonly FavoriteRepository _favoriteRepository;
        private readonly CustomPropertyRepository _customPropertyRepository;
        private readonly RequiredPropertyRepository _requiredPropertyRepository;
        private readonly PropertyValueRepository _propertyValueRepository;
        private readonly UserRepository _userRepository;
        private readonly OrderRepository _orderRepository;

        public CatalogService(ProductRepository productRepository, CategoryRepository categoryRepository,
            ProductImageRepository productImageRepository, CommentRepository commentRepository,
            AnswerRepository answerRepository,
            FavoriteRepository favoriteRepository, RequiredPropertyRepository requiredPropertyRepository,
            PropertyValueRepository propertyValueRepository, CustomPropertyRepository customPropertyRepository,
            UserRepository userRepository, OrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _productImageRepository = productImageRepository;
            _commentRepository = commentRepository;
            _answerRepository = answerRepository;

            _favoriteRepository = favoriteRepository;
            _requiredPropertyRepository = requiredPropertyRepository;
            _propertyValueRepository = propertyValueRepository;
            _customPropertyRepository = customPropertyRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }

        public virtual async Task<IReadOnlyCollection<Category>> GetAllCategoriesAsync() => await _categoryRepository.GetAllAsync();

        public virtual async Task<IReadOnlyCollection<Comment>> GetAllCommentsAsync() => await _commentRepository.GetAllAsync();

        public virtual async Task<IReadOnlyCollection<Comment>> GetProductCommentsByIdAsync(int Id) => await _commentRepository.FindByConditionAsync(c => c.ProductId == Id);

        public virtual async Task<IReadOnlyCollection<Answer>> GetAllAnswersAsync() => await _answerRepository.GetAllAsync();

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

        public virtual async Task<IReadOnlyCollection<CustomProperty>> GetCustomPropertiesByProductIdAsync(int Id)
            => await _customPropertyRepository.FindByConditionAsync(cp => cp.ProductId == Id);

        public virtual async Task<IReadOnlyCollection<RequiredProperty>> GetRequiredPropertiesByCategoryIdAsync(int Id)
         => await _requiredPropertyRepository.FindByConditionAsync(rp => rp.CategoryId == Id);

        public virtual async Task<IReadOnlyCollection<RequiredPropertyValue>> GetRequiredPropertyValueProductIdAsync(int ProductId)
         => await _propertyValueRepository.FindByConditionAsync(pv => pv.ProductId == ProductId);

        public async Task<IReadOnlyCollection<RequiredPropertyViewModel>> GetPropertyValuesOfProductByProductIdAndCategoryIdAsync(int ProductId, int CategoryId)
        {
            var RequiredProperties = await GetRequiredPropertiesByCategoryIdAsync(CategoryId);
            var RequiredPropertiesValues = await GetRequiredPropertyValueProductIdAsync(ProductId);
            var ProductValues = new List<RequiredPropertyViewModel>();

            foreach (var RequiredProperty in RequiredProperties)
            {
                foreach (var RequiredPropertyValue in RequiredPropertiesValues)
                {
                    if (RequiredProperty.Id == RequiredPropertyValue.PropertyId)
                    {
                        var value = new RequiredPropertyViewModel()
                        {
                            PropertyName = RequiredProperty.Name,
                            PropertyValue = RequiredPropertyValue.Value
                        };
                        ProductValues.Add(value);
                        break;
                    }
                }
            }
            return ProductValues;
        }

        public async Task<IReadOnlyCollection<CardProductViewModel>> GetProductsCardsAsync(int CategoryId)
        {
            var products = await this.FindProductsByCategoryIdAsync(CategoryId);
            var cardProducts = new List<CardProductViewModel>();
            foreach (var product in products)
            {
                string url;
                if (_productImageRepository.GetImageByProductIdAsync(product.Id).Result == null)
                {
                    url = "data:image/png;base64,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";
                }
                else
                {
                    url = _productImageRepository.GetImageByProductIdAsync(product.Id).Result.Url;
                }
                var cardProduct = new CardProductViewModel()
                {
                    ImageUrl = url,
                    Name = product.Name,
                    Price = product.Price,
                    ProductId = product.Id,
                    Status = product.Status
                };
                cardProducts.Add(cardProduct);
            }

            return cardProducts;
        }

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

        public async Task<IReadOnlyCollection<CardAnswerViewModel>> GetAnswersCardsByCommentIdAsync(int CommentId)
        {
            var answers = await this.GetAnswersByCommentIdAsync(CommentId);
            var cardAnswers = new List<CardAnswerViewModel>();
            foreach (var answer in answers)
            {
                CardAnswerViewModel cardAnswer;
                if (answer.UserId != null)
                {
                    cardAnswer = new CardAnswerViewModel()
                    {
                        CommentatorName = _userRepository.GetUserById(answer.UserId).Result.UserName,
                        CommentId = answer.CommentId,
                        Content = answer.Content
                    };
                }
                else
                {
                    cardAnswer = new CardAnswerViewModel()
                    {
                        CommentatorName = null,
                        CommentId = answer.CommentId,
                        Content = answer.Content
                    };
                }
                cardAnswers.Add(cardAnswer);
            }

            return cardAnswers;
        }

        private async Task<IReadOnlyCollection<Answer>> GetAnswersByCommentIdAsync(int CommentId) => await _answerRepository.FindByConditionAsync(c => c.CommentId == CommentId);

        public async Task<IReadOnlyCollection<CardAnswerViewModel>> GetAnswersCardsByProductIdAsync(int ProductId)
        {
            var comments = await this.GetProductCommentsByIdAsync(ProductId);
            var cardAnswers = new List<CardAnswerViewModel>();
            foreach (var comment in comments)
            {
                cardAnswers.AddRange(await GetAnswersCardsByCommentIdAsync(comment.Id));
            }

            return cardAnswers;
        }

        public async Task<Category> GetCategoryByIdAsync(int Id) => await _categoryRepository.GetByIdAsync(Id);

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