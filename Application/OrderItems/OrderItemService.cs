using Enterprise_E_Commerce_Management_System.Application.CartItems.DTOs;
using Enterprise_E_Commerce_Management_System.Application.OrderItems.Results;
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Models.Carts;
using Enterprise_E_Commerce_Management_System.Models.OrderItems;

namespace Enterprise_E_Commerce_Management_System.Application.OrderItems
{
    public class OrderItemService:IOrderItemService
    {
        private readonly IUnitOfWork _uow;
        public OrderItemService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<enAddOrderItemResult> AddAsync(OrderItem item)
        {
            if (item.Price <= 0 || item.Quantity <= 0)//Business Rule
                return enAddOrderItemResult.InvalidData;
            await _uow.OrderItems.AddAsync(item);
            return enAddOrderItemResult.Success;
        }
      
    }
}
