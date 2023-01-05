
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
   

        public SellerService(SellerRepository sellerRepository)
        {
            _sellerRepository= sellerRepository;
        }

        public async Task CreateAsync(Seller entity) => await _sellerRepository.CreateAsync(entity);

        public async Task Delete(string id)
        {
            await _sellerRepository.Delete(id);
        }

        public async Task Edit(Seller seller)
        {
           await _sellerRepository.Edit(seller);
        }

      
       
    }
}
