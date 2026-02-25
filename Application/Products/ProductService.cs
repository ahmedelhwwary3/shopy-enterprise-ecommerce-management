using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Carts;
using Enterprise_E_Commerce_Management_System.Application.Categories;
using Enterprise_E_Commerce_Management_System.Application.Products.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Products.Results;
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Infrastructures.Products;
using Enterprise_E_Commerce_Management_System.Models.Attributes;
using Enterprise_E_Commerce_Management_System.Models.Products;
using Enterprise_E_Commerce_Management_System.ViewModels.UserProducts;
using System.Threading.Tasks;

namespace Enterprise_E_Commerce_Management_System.Application.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;
        private readonly IProductQuery _prdQuery;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webEnv;
        public ProductService(IUnitOfWork uow,IProductQuery prdQuery,
            IMapper mapper, ICategoryService categoryService
            ,IWebHostEnvironment webEnv /*By Default Registered*/)
        {
            _webEnv=webEnv;
            _categoryService=categoryService;
            _uow = uow;
            _prdQuery=prdQuery;
            _mapper=mapper;
        }
        
        public async Task<ProductPageViewModel> GetPageViewModel()
        {
            var categoryList= await _categoryService.GetBaseListAsync();  
            var viewModel=new ProductPageViewModel();
            viewModel.Categories= categoryList; 

            return viewModel;
        }
        public async Task<bool> CheckUniqueCategoryIdNameAsync(string Name, int? CategoryId = null, int? Id=null)
        {
            if (!CategoryId.HasValue)//No Category Selected
                return true;
            if (Id.HasValue)//Update Mode
            {
                var prdRepo = await _uow.Products
                    .GetAsReadOnlyByIdAsync(Id.Value);

                if (prdRepo != null && prdRepo.Name == Name)
                    return true;//No Change
            }
            //UpdateChange Or AddMode
            bool IsUnique= ! await _uow.Products.ExistsByNameAndCategoryId(Name, CategoryId.Value);
            return IsUnique;
        } 

        public async Task RecalculateProductStatusAsync(int Id)
        {
            bool AvalilableInStock = await _uow.Products
                .GetAllVariantsStockQuantityByIdAsync(Id) > 0; 
            var prdDB = await _uow.Products.GetByIdAsync(Id); 
            prdDB.IsActive = AvalilableInStock; 
        }
        public async Task<ProductFormViewModel> GetFormViewModelAsync(int? Id=null)
        {
            var viewModel = new ProductFormViewModel();
            var categoryListVM = await _categoryService.GetSubListAsync();
            if (Id.HasValue)//Edit
            {
                var prdDB = await _uow.Products
                     .GetAsReadOnlyByIdAsync(Id.Value);
               
                viewModel = _mapper.Map<ProductFormViewModel>(prdDB);
                viewModel.Categories = categoryListVM;
                return viewModel;
            }
            //Add
            viewModel.Categories = categoryListVM;
            return viewModel; 
        }

        public async Task<bool> CheckImageUniqueNameAsync(string ImageName,int?Id=null)
        {
            if(Id.HasValue)//Edit
            {
                var productDB = await _uow.Products.GetAsReadOnlyByIdAsync(Id.Value);
                if (productDB == null)
                    return false;
                if (productDB.ImageName == ImageName)
                    return true;
            }
            bool IsUnique = ! await _uow.Products.ExistsByImageNameAsync(ImageName);
            return IsUnique;
        }

        public async Task<enCreateProductResult>CreateProductAsync(ProductFormViewModel viewModel)
        {
            if (viewModel.Id.HasValue)//Edit
                return enCreateProductResult.NotCreateMode; 

            bool IsValidCategory = await _uow.Categories.ExistsByIdAsync(viewModel.CategoryId);
            if (!IsValidCategory)
                return enCreateProductResult.CategoryNotFound;

            bool IsUnique = await CheckUniqueCategoryIdNameAsync(viewModel.Name, viewModel.CategoryId);
            if (!IsUnique)
                return enCreateProductResult.NotUniqueProductName;

            IsUnique = await CheckImageUniqueNameAsync(viewModel.ImageName);
            if (!IsUnique)
                return enCreateProductResult.NotUniqueImageName;

            var prdDB = _mapper.Map<Product>(viewModel);
            prdDB.IsActive = true;
            prdDB.CreateDate = DateOnly.FromDateTime(DateTime.Now);
            prdDB.Description = viewModel.Description ?? string.Empty;
            if (!ValidateImage(viewModel.ImageFile))
                return enCreateProductResult.InvalidImage;

            prdDB.ImageName = await SaveFileAsync(viewModel.ImageFile);
            await _uow.Products.AddAsync(prdDB);
            await _uow.SaveChangesAsync();
            return enCreateProductResult.Success;
        }
        public async Task<enUpdateProductResult> UpdateProductAsync(ProductFormViewModel viewModel)
        {
            if (!viewModel.Id.HasValue)//Add
                return enUpdateProductResult.NotUpdateMode;

            var prdDB = await _uow.Products.GetByIdAsync(viewModel.Id.Value);
            if (prdDB == null)
                return enUpdateProductResult.ProductNotFound; 
            bool IsUnique = await CheckUniqueCategoryIdNameAsync(
                viewModel.Name, viewModel.CategoryId, viewModel.Id.Value);

            IsUnique = await CheckImageUniqueNameAsync(viewModel.ImageName,viewModel.Id);
            if (!IsUnique)
                return enUpdateProductResult.NotUniqueImageName;

            bool IsValidCategory = await _uow.Categories.ExistsByIdAsync(viewModel.CategoryId);
            if (!IsValidCategory)
                return enUpdateProductResult.CategoryNotFound;

            if (!IsUnique)
                return enUpdateProductResult.NotUniqueName;

            var imagefile = viewModel.ImageFile;
            if(imagefile!=null)
            {
                if (!ValidateImage(imagefile))
                    return enUpdateProductResult.InvalidImage;

                prdDB.ImageName = await SaveFileAsync(imagefile);
            }
            prdDB = prdDB.CopyValuesFromDTO(_mapper.Map<UpdateProductDTO>(viewModel));
            await _uow.SaveChangesAsync();
            return enUpdateProductResult.Success;
        }
        public bool ValidateImage(IFormFile image)
        {
            if (image == null)
                return true;

            var ext = new[] { ".jpg", ".jpeg", ".png" };
            var fileExt=Path.GetExtension(image.FileName.Trim());
            return ext.Contains(fileExt);
        }
        private async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null)
                throw new Exception("You should select Image.");

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName.Trim());
            //string appPath = Directory.GetCurrentDirectory();
            //string folderPath = Path.Combine(appPath, "wwwroot/Images");
            string folderPath = Path.Combine(_webEnv.WebRootPath, "Images");//Directly (ASPcore Feature)
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, fileName);
            using (var stream = new FileStream(filePath,FileMode.Create))
            {
               await file.CopyToAsync(stream);
            }
            return fileName;
        }
        public async Task<ProductPagedListViewModel> 
            GetAvailableListAsync(ProductFilterDTO filter)
        {
            var result = await _prdQuery.GetProductListWithCountAsync(filter);
            return _mapper.Map<ProductPagedListViewModel>(result);
        }

        public async Task<enDeleteProductResult> SoftOrHardDeleteProductAsync(int Id)
        {
            var prdDB= await _uow.Products.GetByIdAsync(Id);
            if (prdDB == null)
                return enDeleteProductResult.ProductNotFound;
            bool HasVariants = await _uow.Products.HasVariants(Id);
            if(HasVariants)
            {
                prdDB.MarkAsDeleted();//Soft Delete
            }
            else
                await _uow.Products.DeleteByIdAsync(Id);//Hard Delete

            await _uow.SaveChangesAsync();
            return enDeleteProductResult.Success;
        }

        public async Task<Product> GetAsReadOnlyByIdAsync(int Id)
        {
            var productDB = await _uow.Products.GetAsReadOnlyByIdAsync(Id);
            return productDB;
        }

        public async Task<bool> HasVariantAttributeAsync(int ProductId, enAttributeName Name, string Value)
        {
            return await _uow.Products.HasVariantAttributeAsync(ProductId, Name, Value);
        }
         
    }
}
