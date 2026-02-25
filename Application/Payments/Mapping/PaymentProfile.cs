using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Payments.DTOs;
using Enterprise_E_Commerce_Management_System.ViewModels.Payment;

namespace Enterprise_E_Commerce_Management_System.Application.Payments.Mapping
{
    public class PaymentProfile:Profile
    {
        public PaymentProfile()
        {
            CreateMap<PaymentFilterDTO,PaymentFilterViewModel>().ReverseMap();
            CreateMap<PaymentPagedListDTO,PaymentPagedListViewModel>();
            CreateMap<PaymentItemDTO, PaymentItemViewModel>();
        }
    }
}
