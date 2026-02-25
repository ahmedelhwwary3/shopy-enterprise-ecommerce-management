
using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Countries;
using Enterprise_E_Commerce_Management_System.Application.Payments.DTOs;
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Infrastructures.Payments;
using Enterprise_E_Commerce_Management_System.ViewModels.Payment;

namespace Enterprise_E_Commerce_Management_System.Application.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPaymentQuery _query;
        private readonly ICountryService _countryService;
        public PaymentService(
            IUnitOfWork uow,
            IMapper mapper,
            IPaymentQuery query,
            ICountryService countryService)
        {
            _uow = uow;
            _mapper = mapper;
            _query= query;
            _countryService = countryService;
        }
        public async Task AddAsync(Payment payment)
        {
            await _uow.Payments.AddAsync(payment);
        }

        public async Task<PaymentPagedListViewModel> GetAllAsync(PaymentFilterViewModel filter, int currencyId)
        {
            var filterDTO = _mapper.Map<PaymentFilterDTO>(filter);
            var listDTO = await _query.GetAllListAsync(filterDTO, currencyId);
            var listVM = _mapper.Map<PaymentPagedListViewModel>(listDTO);
            if(listVM.Filter.CountryList ==null)
                listVM.Filter.CountryList = await _countryService.GeAllViewModelAsync();
            return listVM;
        }

        public async Task<bool> ExistsByOrderIdAsync(int orderId)
        {
            return await _uow.Payments.ExistsByOrderIdAsync(orderId);
        }
    }
}
