
using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.ShippingProviders.DTOs;
using Enterprise_E_Commerce_Management_System.Application.ShippingProviders.Results;
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Infrastructures.Shipments;
using Enterprise_E_Commerce_Management_System.Models.ShippingProviders;
using Enterprise_E_Commerce_Management_System.ViewModels.Shipment;
using Enterprise_E_Commerce_Management_System.ViewModels.ShippingProvider;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Enterprise_E_Commerce_Management_System.Application.ShippingProviders
{
    public class ShippingProviderService : IShippingProviderService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper; 
        public ShippingProviderService
            (IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow; 
            _mapper = mapper;
        }

        public async Task<ShippingProviderPagedListViewModel> 
            GetListViewModelAsync(ShippingProviderFilterViewModel filter)
        {
            var filterDTO = _mapper.Map<ShippingProviderFilterDTO>(filter);
            var listDTO = await _uow.ShippingProviders.GetListDtoAsync(filterDTO);
            var listVM = _mapper.Map<ShippingProviderPagedListViewModel>(listDTO);
            return listVM;
        }

        public async Task<List<ShippingProviderIdNameViewModel>> GetAllViewModelAsync()
        {
            ICollection<ShippingProviderNameIdDTO> dto = await _uow.ShippingProviders.GetAllAsync();
            var viewModel= _mapper.Map<List<ShippingProviderIdNameViewModel>>(dto);
            return viewModel;
        }

        
        public async Task<ShippingProviderFormViewModel> GetFormViewModelAsync(int? ShippingProviderId)
        {
            var viewModel = new ShippingProviderFormViewModel();
            if (ShippingProviderId.HasValue)//Edit Only
            {
                var providerDB = await _uow.ShippingProviders.GetAsReadOnlyByIdAsync(ShippingProviderId.Value);
                viewModel.Name=providerDB.Name;
                viewModel.Id=providerDB.Id;
                viewModel.IsActive=providerDB.IsActive;
            }
            //Add Or Edit
            return viewModel;
        }

        public async Task<bool> CheckUniqueNameAsync(string Name,int Id=0)
        {
            if(Id>0)//Update
            {
                //1.Check if name exists
                var providerDB = await _uow.ShippingProviders.GetByIdAsync(Id);
                if (providerDB == null) 
                    return false;
                //2.If Provider Name not changed ok
                if (Name == providerDB.Name)//Same Name
                    return true;
            }
            //Check Uniqueness if Add Or Update When Name Changed
            return ! await _uow.ShippingProviders.ExistsByNameAsync(Name);
        }

        public async Task<enUpdateShippingProviderResult> UpdateAsync(ShippingProviderFormViewModel viewModel)
        {
            if (!viewModel.Id.HasValue)
                return enUpdateShippingProviderResult.NotUpdateMode; 
            var providerDB = await _uow.ShippingProviders.GetByIdAsync(viewModel.Id.Value);
            if (providerDB == null)
                return enUpdateShippingProviderResult.ProviderNotFound;

            providerDB.Name=viewModel.Name;
            await _uow.SaveChangesAsync();
            return enUpdateShippingProviderResult.Success;
        }

        public async Task<enCreateShippingProviderResult> CreateAsync(ShippingProviderFormViewModel viewModel)
        {
            if (viewModel.Id.HasValue)
                return enCreateShippingProviderResult.NotCreateMode;

            var providerDB = new ShippingProvider()
            {
                IsActive = true,
                Name=viewModel.Name
            };
            await _uow.ShippingProviders.AddAsync(providerDB);
            await _uow.SaveChangesAsync();
            return enCreateShippingProviderResult.Success;
        }

        public async Task<enChangeShippingProviderStatusResult> ChangeActivityStatusById(int ShippingProviderId)
        {
            var providerDB = await _uow.ShippingProviders.GetByIdAsync(ShippingProviderId);
            if (providerDB == null)
                return enChangeShippingProviderStatusResult.ProviderNotFound;

            providerDB.IsActive = !providerDB.IsActive;
            await _uow.SaveChangesAsync();
            return enChangeShippingProviderStatusResult.Success;
        }
    }
}
