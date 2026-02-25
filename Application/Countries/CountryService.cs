using AutoMapper; 
using Enterprise_E_Commerce_Management_System.Infrastructures.Countries;
using Enterprise_E_Commerce_Management_System.ViewModels.Country;
using System.Threading.Tasks;

namespace Enterprise_E_Commerce_Management_System.Application.Countries
{
    public class CountryService:ICountryService
    {
        private readonly IMapper _mapper;
        private readonly ICountryQuery _query;
        public CountryService(IMapper mapper,ICountryQuery query)
        {
            _mapper = mapper;
            _query=query;
        }
        public async Task<List<CountryNameIdViewModel>> GeAllViewModelAsync()
        {
            var list = await _query.GetAllDtoAsync();
            return _mapper.Map<List<CountryNameIdViewModel>>(list);
        }
    }
}
