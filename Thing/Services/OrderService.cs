using System.Linq.Expressions;
using Thing.Models;
using Thing.Repository;

namespace Thing.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task CreateAsync(Order order)
        {
            if (order != null)
            {
                await _orderRepository.CreateAsync(order);
            }
        }

        public async Task DeleteAsync(string uId, int pId)
        {
            await _orderRepository.DeleteAsync(uId, pId);
        }

        public async Task<IReadOnlyCollection<Order>> FindIncludeProductsAsync(Expression<Func<Order, bool>> conditon)
        {
            return await _orderRepository.FindIncludeProductsAsync(conditon);
        }

        public async Task<IReadOnlyCollection<Order>> FindByConditionAsync(Expression<Func<Order, bool>> conditon)
        {
            return await _orderRepository.FindByConditionAsync(conditon);
        }

        public async Task Edit(Order order)
        {
            await _orderRepository.Edit(order);
        }

        public async Task<Order?> Get(string uId, int pId)
        {
            return await _orderRepository.GetByIdAsync(uId, pId);
        }

        public async Task Increase(string uId, int pId)
        {
            await _orderRepository.Increase(uId, pId);
        }
    }
}