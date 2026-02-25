using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Carts.DTOs;
using Enterprise_E_Commerce_Management_System.ViewModels.Cart;

namespace Enterprise_E_Commerce_Management_System.Application.Carts.Mapping
{
    public class CartProfile:Profile
    {
        public CartProfile()
        {
            CreateMap<CartItemDTO, CartItemViewModel>();
            CreateMap<CartDetailsDTO, CartDetailsViewModel>();
        }


    }
}
