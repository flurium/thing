using Dal.Repository.Interfaces;

namespace Dal.UnitOfWork
{
    public interface IUnitOfWork
    {
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