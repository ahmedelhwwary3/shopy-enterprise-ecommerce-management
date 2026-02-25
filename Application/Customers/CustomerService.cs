using AutoMapper; 
using Enterprise_E_Commerce_Management_System.Application.Countries;
using Enterprise_E_Commerce_Management_System.Application.Customers.DTOs; 
using Enterprise_E_Commerce_Management_System.Application.Customers.Results;
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Infrastructures.Customers; 
using Enterprise_E_Commerce_Management_System.Models.Customers;
using Enterprise_E_Commerce_Management_System.ViewModels.Customer; 

namespace Enterprise_E_Commerce_Management_System.Application.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _uow;
        private readonly ICustomerQuery _customerQuery;
        private readonly IMapper _mapper;
        private readonly ICountryService _countryService;
        public CustomerService(IUnitOfWork Uow,ICustomerQuery customerQuery
            ,IMapper mapper,ICountryService countryService)
        {
            _mapper=mapper;
            _countryService = countryService;
            _uow = Uow;
            _customerQuery = customerQuery;
        }
        public async Task<enCreateCustomerResult> 
            CreateIfNotExistsAsync(CustomerFormViewModel viewModel)
        {
            var custDB = await _uow.Customers
                .GetByEmailOrPhone(viewModel.Email, viewModel.PhoneNumber);
            if (custDB != null)
                return enCreateCustomerResult.CustomerExists;

            custDB = _mapper.Map<Customer>(viewModel);//map also Country(Address) + FullName
            custDB.IsActive = true;
            custDB.CreateDate = DateOnly.FromDateTime(DateTime.Now);

            await _uow.Customers.AddAsync(custDB);
            await _uow.SaveChangesAsync();
            return enCreateCustomerResult.Success;
        }

        public async Task<enUpdateCustomerResult> UpdateAsync(CustomerFormViewModel viewModel)
        {
            var custDB = await _uow.Customers
                .GetByIdAsync(viewModel.Id.Value);
            if (custDB == null)
                return enUpdateCustomerResult.CustomerNotFound;

            //custDB.Status = (enCustomerStatus)viewModel.Status;//ReadOnly
            custDB.PhoneNumber = viewModel.PhoneNumber;
            custDB.FullName = _mapper.Map<FullName>(viewModel.FullName);
            custDB.Address = _mapper.Map<Address>(viewModel.Address);
            custDB.Email = viewModel.Email;
            //_uow.Customers.Update(custDB);

            await _uow.SaveChangesAsync();
            return enUpdateCustomerResult.Success;
        }
        public async Task<int?> GetIdByEmailOrPhoneAsync(string Email, string PhoneNumber)
        {
            return await _uow.Customers.GetIdByEmailOrPhone(Email, PhoneNumber);
        }
        public async Task<enChangeCustomerStatusResult> ChangeCustomerStatusByIdAsync(
            int Id)
        {
            var custDB = await _uow.Customers
                .GetCustomerById(Id);
            if (custDB == null)
                return enChangeCustomerStatusResult.CustomerNotFound;

            custDB.IsActive = !custDB.IsActive;
            await _uow.SaveChangesAsync();
            return enChangeCustomerStatusResult.Success;

        }

        public async Task<CustomerPagedListViewModel>
            GetListAsync(CustomerFilterViewModel filter)
        {
            var filterDTO= _mapper.Map<CustomerFilterDTO>(filter);
            var result = await _customerQuery
                .GetListAsync(filterDTO);
            var viewModel= _mapper.Map<CustomerPagedListViewModel>(result);
            viewModel.Filter = filter;
            return viewModel;
        }

        public async Task<CustomerDetailsWithCountryListViewModel>
            GetDetailsByIdAsync(int Id)
        {
            var custDTO = await _customerQuery.GetDetailsDtoByIdAsync(Id);
            if (custDTO == null)
                return null;

            var custVM =_mapper.Map<CustomerDetailsWithCountryListViewModel>(custDTO);
            return custVM;
        }

        public async Task<CustomerFormViewModel> GetFormViewModelAsync(int? Id=null)
        {
            var viewModel = new CustomerFormViewModel();
            if (Id.HasValue)//Edit Only
            {
                var custDB = await _uow.Customers
                .GetAsReadOnlyByIdAsync(Id.Value);
                if (custDB == null)
                    return null;
                viewModel = _mapper.Map<CustomerFormViewModel>(custDB);
                
            }
            else//Add Only
            {
                viewModel.Address = new AddressViewModel(); 
                viewModel.FullName = new FullNameViewModel();
            }
            viewModel.Address.Countries = await _countryService.GeAllViewModelAsync();
            if (!Id.HasValue)//Add
                viewModel.Address.CountryId = valid.EgyptId;
            return viewModel;
        }

        public async Task<Customer> GetAsReadOnlyByIdAsync(int Id)
        {
            return await _uow.Customers.GetAsReadOnlyByIdAsync(Id);
        }
    }
}