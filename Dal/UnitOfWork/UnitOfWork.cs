using Dal.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IAnswerRepository answerRepository, ICategoryRepository categoryRepository,
            ICommentImageRepository commentImageRepository, ICommentRepository commentRepository,
            ICustomPropertyRepository customPropertyRepository, IFavoriteRepository favoriteRepository,
            IOrderRepositody orderRepositody, IProductImageRepository productImageRepository,
            IProductRepository productRepository, IPropertyValueRepository propertyValueRepository,
            IRequiredPropertyRepository requiredPropertyRepository, ISellerRepository sellerRepository,
            IUserRepository userRepository)
        {
            AnswerRepository = answerRepository;
            CategoryRepository = categoryRepository;
            CommentImageRepository = commentImageRepository;
            CommentRepository = commentRepository;
            CustomPropertyRepository = customPropertyRepository;
            FavoriteRepository = favoriteRepository;
            OrderRepositody = orderRepositody;
            ProductImageRepository = productImageRepository;
            ProductRepository = productRepository;
            RequiredPropertyRepository = requiredPropertyRepository;
            PropertyValueRepository = propertyValueRepository;
            SellerRepository = sellerRepository;
            UserRepository = userRepository;
        }

        public IAnswerRepository AnswerRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public ICommentImageRepository CommentImageRepository { get; }
        public ICommentRepository CommentRepository { get; }
        public ICustomPropertyRepository CustomPropertyRepository { get; }
        public IFavoriteRepository FavoriteRepository { get; }
        public IOrderRepositody OrderRepositody { get; }
        public IProductImageRepository ProductImageRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IPropertyValueRepository PropertyValueRepository { get; }
        public IRequiredPropertyRepository RequiredPropertyRepository { get; }
        public ISellerRepository SellerRepository { get; }
        public IUserRepository UserRepository { get; }
    }
}