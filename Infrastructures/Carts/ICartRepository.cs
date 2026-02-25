using Enterprise_E_Commerce_Management_System.Models.Carts;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Carts
{
    public interface ICartRepository:IRepository<Cart> 
    {
        Task DeleteItemByItemIdAsync(int CartId,int ItemId);
        Task<bool> IsEmpty(int CartId);
    }
}
