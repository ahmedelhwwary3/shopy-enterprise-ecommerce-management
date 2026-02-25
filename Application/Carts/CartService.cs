
using AutoMapper; 
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Infrastructures.Carts; 
using Enterprise_E_Commerce_Management_System.Models.Carts;
using Enterprise_E_Commerce_Management_System.ViewModels.Cart;

namespace Enterprise_E_Commerce_Management_System.Application.Carts
{
    public class CartService:ICartService
    {    
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CartService(ICartQuery query,
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper; 
        }
        public async Task<CartDetailsViewModel> GetDetailsViewModelByIdAsync(int Id, int currencyId)
        {
            var dto = await _uow.CartItems.GetDetailsDtoByCartIdAsync(Id,currencyId);
            var viewModel = _mapper.Map<CartDetailsViewModel>(dto);
            return viewModel;
        }
        public async Task<Cart> GetByIdAsync(int Id)
        {
            return await _uow.Carts
                .GetByIdAsync(Id);
        }
        public async Task<Cart> GetWithItemsByIdAsync(int Id)
        {
            return await _uow.Carts
                .GetByIdAsync(Id,c=>c.CartItems);
        }
        public void Update(Cart cart)
        {
            _uow.Carts.Update(cart);

        }
        public async Task<int> CreateAndGetIdAsync(int? customerId)
        {
            var cart = new Cart();
            cart.IsActive = true;
            cart.CreateDate= DateTime.Now;
            cart.CustomerId= customerId;
            await _uow.Carts.AddAsync(cart);
            await _uow.SaveChangesAsync();
            return cart.Id;
            //return await _query
            //    .CreateAndGetId(cart);
        }
        public async Task<int> GetItemsTotalCountByIdAsync(int Id)
        {
            return await _uow.CartItems
                .GetItemsTotalCountAsync(Id);
        }
        public async Task<decimal> GetItemsTotalPriceByIdAsync(int Id)
        {
            return await _uow.CartItems
                .GetItemsTotalPriceAsync(Id);
        }

        public async Task DeleteItemByItemIdAsync(int CartId, int ItemId)
        {
            await _uow.Carts.DeleteItemByItemIdAsync(CartId, ItemId);
            await _uow.SaveChangesAsync();

            if (await _uow.Carts.IsEmpty(CartId))
            {
                await _uow.Carts.DeleteByIdAsync(CartId); 
                await _uow.SaveChangesAsync();
            }
        }

        public async Task DeleteItemsByIdAsync(int CartId, int ItemId)
        {
            await _uow.Carts.DeleteItemByItemIdAsync(CartId, ItemId);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int cartId)
        {
            var cartIncludesItems = await _uow.Carts.GetByIdAsync(cartId,c=>c.CartItems);
            foreach(var item in cartIncludesItems.CartItems)
            {
                await _uow.Carts.DeleteItemByItemIdAsync(cartId, item.Id);
            }
            await _uow.Carts.DeleteByIdAsync(cartId);
            await _uow.SaveChangesAsync();
        }
    }
}
