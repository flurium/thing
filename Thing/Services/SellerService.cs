
using IdentityModel;
using System.Linq.Expressions;
using System.Security.Claims;
using Thing.Models;
using Thing.Repository;


namespace Thing.Services
{
    public class SellerService
    {
        private readonly SellerRepository _sellerRepository;
        private readonly ProductRepository _productRepository;

        public SellerService(SellerRepository sellerRepository, ProductRepository productRepository)
        {
            _sellerRepository= sellerRepository;
            _productRepository= productRepository;
        }

        public async Task CreateAsync(Seller entity) => await _sellerRepository.CreateAsync(entity);

        public async  Task<IReadOnlyCollection<Product>> FindByConditionAsync(Expression<Func<Product, bool>> conditon)
        {
            return await _productRepository.FindByConditionAsync(conditon);
        }
        
        /*
        public async Task<IReadOnlyCollection<Product>> GetAllProducts()
        {
            return await _productRepository.FindByConditionAsync(x => x.Seller == User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
        */
    }
}
