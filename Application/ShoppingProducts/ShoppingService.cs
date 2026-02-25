using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Carts;
using Enterprise_E_Commerce_Management_System.Application.Categories;
using Enterprise_E_Commerce_Management_System.Application.Currencies;
using Enterprise_E_Commerce_Management_System.Application.ShoppingProducts.DTOs;
using Enterprise_E_Commerce_Management_System.Infrastructures.Products;
using Enterprise_E_Commerce_Management_System.ViewModels.ShoppingProducts;
using System.Threading.Tasks;
namespace Enterprise_E_Commerce_Management_System.Application.ShoppingProducts
{
    public class ShoppingService : IShoppingService
    {
        private readonly IProductQuery _prdQuery; 
        private readonly IMapper _mapper;
        private readonly ICurrencyService _currencyService;
        private readonly ICategoryService _categoryService;
        public ShoppingService
            (IProductQuery prdQuery
            , IMapper mapper, ICategoryService categoryService
            ,ICartService cartService,
            ICurrencyService currencyService)
        {
            _categoryService = categoryService;
            _mapper = mapper; 
            _currencyService = currencyService;
            _prdQuery = prdQuery;
        }
        public async Task<ShoppingPageViewModel> GetPageViewModelAsync(int CurrencyId)
        {
            var categoryList = await _categoryService.GetBaseListAsync();
            var priceList = await GetProductPriceRangeList(CurrencyId);
            var currencyList = await _currencyService.GetAllNameIdViewModelAsync();

            var pageViewModel = new ShoppingPageViewModel();
            pageViewModel.ProductPrices = priceList;
            pageViewModel.Categories = categoryList;
            pageViewModel.Currencies= currencyList;
            pageViewModel.Code = await _currencyService.GetCodeByIdAsync(CurrencyId);
            return pageViewModel;
        }
        public async Task<List<ProductPriceItemViewModel>> GetProductPriceRangeList(int currecnyId)
        {
            decimal dollarRate = await _currencyService.GetDollarRateByIdAsync(currecnyId);
            string currencyCode = await _currencyService.GetCodeByIdAsync(currecnyId);
            int initial = 6 * (int)dollarRate; 
            string SeperateNumber(int number)
            {
                string digits = number.ToString();
                int count = 0;
                string numberText="";
                for(int i=digits.Length-1;i>=0;i--)
                { 
                    if (count%3==0 && count!=0 && digits.Length-1>i)
                    {
                        numberText += ",";
                    }
                    numberText += digits[i];
                    count++;
                }
                return new string(numberText.Reverse().ToArray());
            }
            if(initial<=10)
                initial = 11;
            var list = new List<ProductPriceItemViewModel>();
            //Head
            list.Add(new()
            { 
                MinPrice = null,
                MaxPrice = --initial,
                Label = $"Less than {SeperateNumber(initial)} {currencyCode}" 
            });
            //Body
            int fromValue = initial;
            int? toValue=null;

            for (int i = 0; i < 10; i++)
            {
                fromValue=(toValue.HasValue?
                    (toValue.Value+1):(fromValue+1)); 
                toValue=(int)((fromValue) * 2.75);
                list.Add(new() 
                { 
                    MinPrice= fromValue,
                    MaxPrice= toValue,
                    Label= $" {SeperateNumber(fromValue)} - {SeperateNumber(toValue.Value)} {currencyCode}"

                });
            }
            //Foot
            list.Add(new()
            { 
                MinPrice = toValue,
                MaxPrice = null,
                Label = $"More than {SeperateNumber(toValue.Value + 1)} {currencyCode}"
            }); 

            return list;
        }

        public async Task<ShoppingPagedListViewModel>
            GetProductListAsync(ShoppingFilterDTO filter)
        {
            decimal dollarRate = await _currencyService.GetDollarRateByIdAsync(filter.CurrencyId);
            filter.MinPrice = filter.MinPrice.HasValue? filter.MinPrice / dollarRate:null;
            filter.MaxPrice = filter.MaxPrice.HasValue ? filter.MaxPrice / dollarRate : null;
            var dto = await _prdQuery.GetShoppingListAsync(filter);
            var viewModel = new ShoppingPagedListViewModel()
            {
                Count = dto.Count,
                Code=dto.Code,
                Products = _mapper.Map<List<ShoppingItemViewModel>>(dto.Products)
            };
            return viewModel;
        }
        public async Task<ShoppingtDetailsViewModel>
            GetShoppingtDetailsByProductIdAsync(int ProductId, int currencyId)
        {
            var viewModel = _mapper.Map<ShoppingtDetailsViewModel>(
                await _prdQuery.GetShoppingtDetailsViewModelByIdAsync(ProductId, currencyId));
            return viewModel;
        }
       
    }
}
