using Dal.Repository.Interfaces;

namespace Dal.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IAnswerRepository answerRepository, ICategoryRepository categoryRepository,
            ICommentImageRepository commentImageRepository, ICommentRepository commentRepository,
            ICustomPropertyRepository customPropertyRepository, IFavoriteRepository favoriteRepository,
            IOrderRepository orderRepository, IProductImageRepository productImageRepository,
            IProductRepository productRepository, IPropertyValueRepository propertyValueRepository,
            IRequiredPropertyRepository requiredPropertyRepository, ISellerRepository sellerRepository)
        {
            AnswerRepository = answerRepository;
            CategoryRepository = categoryRepository;
            CommentImageRepository = commentImageRepository;
            CommentRepository = commentRepository;
            CustomPropertyRepository = customPropertyRepository;
            FavoriteRepository = favoriteRepository;
            OrderRepository = orderRepository;
            ProductImageRepository = productImageRepository;
            ProductRepository = productRepository;
            RequiredPropertyRepository = requiredPropertyRepository;
            PropertyValueRepository = propertyValueRepository;
            SellerRepository = sellerRepository;
        }

        public IAnswerRepository AnswerRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public ICommentImageRepository CommentImageRepository { get; }
        public ICommentRepository CommentRepository { get; }
        public ICustomPropertyRepository CustomPropertyRepository { get; }
        public IFavoriteRepository FavoriteRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IProductImageRepository ProductImageRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IPropertyValueRepository PropertyValueRepository { get; }
        public IRequiredPropertyRepository RequiredPropertyRepository { get; }
        public ISellerRepository SellerRepository { get; }
    }
}