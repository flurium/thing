using Dal.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public IUserRepository UserRepository { get; }
    }
}