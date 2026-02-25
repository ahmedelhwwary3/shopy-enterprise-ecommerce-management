using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.CartItems.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Countries;
using Enterprise_E_Commerce_Management_System.Application.Couriers.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Couriers.Results;
using Enterprise_E_Commerce_Management_System.Application.ShippingProviders;
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Infrastructures.Countries;
using Enterprise_E_Commerce_Management_System.Models.Carts;
using Enterprise_E_Commerce_Management_System.Models.Couriers;
using Enterprise_E_Commerce_Management_System.Models.Orders;
using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Enterprise_E_Commerce_Management_System.ViewModels.Courier;
using Microsoft.AspNetCore.Identity;

namespace Enterprise_E_Commerce_Management_System.Application.Couriers
{
    public class CourierService : ICourierService
    {
        private readonly IUnitOfWork _uow;
        private readonly ICountryService _countryService;
        private readonly IShippingProviderService _shippingProviderService;
        private readonly IMapper _mapper;
        private readonly ICourierQuery _query;
        public CourierService(
            IUnitOfWork uow,
            ICountryService countryService,
            IShippingProviderService shippingProviderService,
            IMapper mapper,
            ICourierQuery query)
        {
            _uow = uow;
            _countryService = countryService;
            _shippingProviderService = shippingProviderService;
            _mapper=mapper;
            _query=query;
        }

        public async Task<int> GetIdByUserIdAsync(string userId)
        {
            return await _uow.Couriers.GetIdByUserIdAsync(userId);
        }

        public async Task AddAsync(Courier courier)
        {
            await _uow.Couriers.AddAsync(courier);
        }

        public async Task<int> GetIdByUserNameAsync(string userName)
        {
            return await _uow.Couriers.GetIdByUserNameAsync(userName);
        }

        public async Task<EditCouierViewModel> GetEditViewModelAsync(int courierId)
        {
            var tuple = await _uow.Couriers.GetCountryIdAndProviderIdAsync(courierId);
            var viewModel = new EditCouierViewModel()
            {
                CountryId=tuple.Item1,
                ShippingProviderId=tuple.Item2,
                Id=courierId
            };
            viewModel.Countries = await _countryService.GeAllViewModelAsync();
            viewModel.Providers = await _shippingProviderService.GetAllViewModelAsync();
            return viewModel;
        }

        public async Task<enUpdateCourierResult> UpdateAsync(EditCouierViewModel viewModel)
        {
            if (!viewModel.Id.HasValue)
                return enUpdateCourierResult.NotUpdateMode; 

            var courierDB =await _uow.Couriers.GetByIdAsync(viewModel.Id.Value,c=>c.User);
            if (courierDB == null)
                return enUpdateCourierResult.CourierNotFound;

            courierDB.User.CountryId = viewModel.CountryId; 
            courierDB.ShippingProviderId = viewModel.ShippingProviderId;
            //_uow.Couriers.Update(courierDB);
            await _uow.SaveChangesAsync();
            return enUpdateCourierResult.Success;
        }

        public async Task<CourierPagedListViewModel> GetListAsync(CourierFilterViewModel filter)
        {
            var filterDTO = _mapper.Map<CourierFilterDTO>(filter);
            var listDTO = await _query.GetListAsync(filterDTO);
            var listVM = _mapper.Map<CourierPagedListViewModel>(listDTO);
            if (listVM.Filter == null)
                return null;
            if(listVM.Filter.Countries==null)
                listVM.Filter.Countries = await _countryService.GeAllViewModelAsync();
            return listVM;
        }

        public async Task<Courier> GetByUserNameAsync(string userName)
        {
            return await _uow.Couriers.GetByUserNameAsync(userName);
        }

        public async Task<CourierDetailsViewModel> GetDetailsViewModelByIdAsync(int courierId)
        {
            var dto = await _uow.Couriers.GetDetailsDtoByIdAsync(courierId);
            var viewModel = _mapper.Map<CourierDetailsViewModel>(dto);
            return viewModel;
        }
        public async Task<int> GetAssignedOrdersCountByCourierIdAsync(int courierId)
        {
            return await _uow.Couriers.GetAssignedOrdersCountByCourierIdAsync(courierId);
        }
    }
}
