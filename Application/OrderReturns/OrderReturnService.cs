using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Countries;
using Enterprise_E_Commerce_Management_System.Application.OrderReturns.DTOs;
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Infrastructures.OrderReturns;
using Enterprise_E_Commerce_Management_System.Models.OrderReturns;
using Enterprise_E_Commerce_Management_System.ViewModels.OrderReturn; 
using System.Threading.Tasks;

namespace Enterprise_E_Commerce_Management_System.Application.OrderReturns
{
  
    public class OrderReturnService : IOrderReturnService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IOrderReturnQuery _query;
        private readonly ICountryService _countryService;
        public OrderReturnService(
            IUnitOfWork Uow,
            IMapper mapper,
            IOrderReturnQuery query,
            ICountryService countryService)
        {
            _uow = Uow; 
            _mapper = mapper;
            _countryService = countryService;
            _query = query;
        }

        public async Task AddAsync(OrderReturn orderReturn)
        {
            await _uow.OrderReturns.AddAsync(orderReturn);
        }

        public async Task<OrderReturn> GetByOrderIdAsync(int orderId)
        {
            return await _uow.OrderReturns.GetByOrderIdAsync(orderId);
        }

        public async Task<OrderReturnPagedListViewModel> GetAllAsync(OrderReturnFilterViewModel filter,int currencyId)
        {
            var filterDTO = _mapper.Map<OrderReturnFilterDTO>(filter);
            var listDTO= await _query.GetAllListAsync(filterDTO,currencyId);
            var listVM = _mapper.Map<OrderReturnPagedListViewModel>(listDTO);
            if(listVM.Filter.Countries==null)
                listVM.Filter.Countries = await _countryService.GeAllViewModelAsync();

            return listVM;
        }
    }
}
