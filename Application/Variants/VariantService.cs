using AspNetCoreGeneratedDocument;
using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Attributes; 
using Enterprise_E_Commerce_Management_System.Application.AttributeValues;
using Enterprise_E_Commerce_Management_System.Application.CategoryAttributes;
using Enterprise_E_Commerce_Management_System.Application.Currencies;
using Enterprise_E_Commerce_Management_System.Application.Products;
using Enterprise_E_Commerce_Management_System.Application.Variants.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Variants.Results;
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Infrastructures.AttributeValues;
using Enterprise_E_Commerce_Management_System.Models;
using Enterprise_E_Commerce_Management_System.Models.Attributes;
using Enterprise_E_Commerce_Management_System.Models.Variants;
using Enterprise_E_Commerce_Management_System.Models.Variants.Enums;
using Enterprise_E_Commerce_Management_System.ViewModels.Variant;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Enterprise_E_Commerce_Management_System.Application.Variants
{
    public class VariantService:IVariantService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IProductService _productService;
        private readonly ICurrencyService _currencyService;
        private readonly ICategoryAttributeService _categoryAttributeService;
        private readonly IAttributeService _attributeService;
        private readonly IAttributeValueService _attributeValueService;
        public VariantService(IMapper mapper,
            IUnitOfWork uow,
            IProductService productService,
            ICurrencyService currencyService,
            ICategoryAttributeService categoryAttributeService,
            IAttributeService attributeService,
            IAttributeValueService attributeValueService)
        { 
            _uow = uow;
            _productService = productService;
            _currencyService= currencyService;
            _mapper= mapper;
            _attributeValueService= attributeValueService;
            _categoryAttributeService = categoryAttributeService;
            _attributeService= attributeService;
        } 

        public async Task<bool> CheckSkuUniqueAsync(string sku,int? variantId=null)
        {
            if(variantId.HasValue)//UpdateMode
            {
                var variantDB = await _uow.Variants
                    .GetByIdAsync(variantId.Value);
                if(variantDB.SKU == sku)//SKU Not Changed
                return true;
            }
            //Check Uniqueness When Changed Or AddMode
            bool IsUnique = !await _uow.Variants.ExistsBySKUAsync(sku);
            return IsUnique;
        }
        public async Task<bool> CheckUniqueVariantAttributeByProductIdAsync
            (enAttributeName Name, string Value, int ProductId, int? VariantId = null)
        {
            if (Name==default || Value==default)
                return true;//Still not selected before submit

            if (VariantId.HasValue)//UpdateMode
            {
                var attributes = await _uow.Variants.
                    GetAttributesNameValueListByIdAsync(VariantId.Value);
                bool IsNoChange = attributes.Any(
                    att => att.Name == Name && att.Value == Value);

                if (IsNoChange)
                    return true;
            }
            //Check Uniqueness When Changed Or AddMode
            bool IsUnique = !await _productService.HasVariantAttributeAsync(ProductId,Name,Value);
            return IsUnique;
        } 

        public async Task<VariantListViewModel>GetListByProductIdAsync(VariantFilterDTO filter, int currencyId)
        {
            var dto = await _uow.Variants.GetListByProductIdAsync(filter, currencyId);
            var viewModel = _mapper.Map<VariantListViewModel>(dto);
            return viewModel;
        }
        public async Task<enDeleteVariantResult> SoftOrHardDeleteAsync(int Id)
        {
            var variantDB = await _uow.Variants.GetByIdAsync(Id);
            if (variantDB == null)
                return enDeleteVariantResult.VariantNotFound;
            variantDB.MarkAsDeleted();
            //bool HasOrders = await _uow.Variants.HasOrders(Id);
            //if (HasOrders)
            //{ 
            //    variantDB.MarkAsDeleted();
            //}
            //else
            //{
            //   await _attributeValueService.DeleteValuesByVariantIdAsync(Id);
            //}
            await _uow.SaveChangesAsync();
            return enDeleteVariantResult.Success;
        }

        private bool IsValidName(enAttributeName name)
        {
            return Enum.GetValues<enAttributeName>().Contains(name);
        }
        private Dictionary<enAttributeName, enDisplayType> GetDisplayTypesMap()
        {
            return new Dictionary<enAttributeName, enDisplayType>
             {
                 // =========================
                 // Visual
                 // =========================
                 { enAttributeName.Color, enDisplayType.Color },

                 // =========================
                 // General Select
                 // =========================
                 { enAttributeName.Size, enDisplayType.Dropdown },
                 { enAttributeName.Brand, enDisplayType.Dropdown },
                 { enAttributeName.Model, enDisplayType.Dropdown },
                 { enAttributeName.Material, enDisplayType.Dropdown },
                 { enAttributeName.Weight, enDisplayType.Dropdown },
                 { enAttributeName.Length, enDisplayType.Dropdown },
                 { enAttributeName.Width, enDisplayType.Dropdown },
                 { enAttributeName.Height, enDisplayType.Dropdown },
                 { enAttributeName.Volume, enDisplayType.Dropdown },
                 { enAttributeName.Capacity, enDisplayType.Dropdown },

                 // =========================
                 // Fashion
                 // =========================
                 { enAttributeName.Gender, enDisplayType.Dropdown },
                 { enAttributeName.AgeGroup, enDisplayType.Dropdown },
                 { enAttributeName.ClothingFit, enDisplayType.Dropdown },
                 { enAttributeName.FabricType, enDisplayType.Dropdown },
                 { enAttributeName.Pattern, enDisplayType.Dropdown },
                 { enAttributeName.SleeveType, enDisplayType.Dropdown },
                 { enAttributeName.CollarType, enDisplayType.Dropdown },
                 { enAttributeName.ClosureType, enDisplayType.Dropdown },
                 { enAttributeName.HeelType, enDisplayType.Dropdown },
                 { enAttributeName.ShoeSize, enDisplayType.Dropdown },

                 // =========================
                 // Electronics
                 // =========================
                 { enAttributeName.Storage, enDisplayType.Dropdown },
                 { enAttributeName.RAM, enDisplayType.Dropdown },
                 { enAttributeName.OperatingSystem, enDisplayType.Dropdown },
                 { enAttributeName.ScreenSize, enDisplayType.Dropdown },
                 { enAttributeName.Resolution, enDisplayType.Dropdown },
                 { enAttributeName.BatteryCapacity, enDisplayType.Dropdown },
                 { enAttributeName.Connectivity, enDisplayType.Dropdown },
                 { enAttributeName.PowerConsumption, enDisplayType.Dropdown },
                 { enAttributeName.Voltage, enDisplayType.Dropdown },

                 // =========================
                 // Home
                 // =========================
                 { enAttributeName.RoomType, enDisplayType.Dropdown },
                 { enAttributeName.FurnitureType, enDisplayType.Dropdown },
                 { enAttributeName.AssemblyRequired, enDisplayType.Dropdown },
                 { enAttributeName.FinishType, enDisplayType.Dropdown },
                 { enAttributeName.Shape, enDisplayType.Dropdown },

                 // =========================
                 // Beauty
                 // =========================
                 { enAttributeName.SkinType, enDisplayType.Dropdown },
                 { enAttributeName.HairType, enDisplayType.Dropdown },
                 { enAttributeName.Shade, enDisplayType.Dropdown },
                 { enAttributeName.FragranceType, enDisplayType.Dropdown },

                 // =========================
                 // Sports
                 // =========================
                 { enAttributeName.SportType, enDisplayType.Dropdown },
                 { enAttributeName.EquipmentType, enDisplayType.Dropdown },
                 { enAttributeName.ResistanceLevel, enDisplayType.Dropdown },

                 // =========================
                 // Automotive
                 // =========================
                 { enAttributeName.VehicleType, enDisplayType.Dropdown },
                 { enAttributeName.Compatibility, enDisplayType.Dropdown },

                 // =========================
                 // Grocery
                 // =========================
                 { enAttributeName.Flavor, enDisplayType.Dropdown },
                 { enAttributeName.ExpiryPeriod, enDisplayType.Dropdown },
                 { enAttributeName.Organic, enDisplayType.Dropdown },
                 { enAttributeName.DietaryType, enDisplayType.Dropdown },

                 // =========================
                 // Books
                 // =========================
                 { enAttributeName.Language, enDisplayType.Dropdown },
                 { enAttributeName.CoverType, enDisplayType.Dropdown },

                 // =========================
                 // Office
                 // =========================
                 { enAttributeName.PaperSize, enDisplayType.Dropdown },
                 { enAttributeName.InkType, enDisplayType.Dropdown },
                 { enAttributeName.UsageType, enDisplayType.Dropdown }
             };
        }

        private enDisplayType GetDisplayTypeByName(enAttributeName name)
        {
            return GetDisplayTypesMap().GetValueOrDefault(name);
        }

        // ==============================
        // COLORS
        // ==============================
 
        private List<string> GetVariantColorsViewModel() => new()
        {
            "Black","White","Gray","Silver","Gold",
            "Blue","Navy","Sky Blue","Teal",
            "Red","Maroon","Burgundy",
            "Green","Olive","Mint",
            "Brown","Beige","Camel","Tan",
            "Pink","Rose","Purple","Lavender",
            "Yellow","Mustard","Orange",
            "Transparent","Multicolor"
        };

        // ==============================
        // CLOTHING
        // ==============================

        private List<string> GetClothingSizesViewModel() => new()
        {
            "XXXS","XXS","XS","S","M","L","XL","XXL","3XL","4XL","5XL"
        };

        private List<string> GetShoeSizesViewModel() => new()
        {
            "35","36","37","38","39","40","41","42","43","44","45","46","47","48"
        };

        private List<string> GetGenderOptionsViewModel() => new()
        {
            "Men","Women","Unisex","Boys","Girls"
        };

        private List<string> GetAgeGroupOptionsViewModel() => new()
        {
            "Baby","Kids","Teen","Adult","Senior"
        };

        private List<string> GetFabricTypeOptions() => new()
        {
            "Cotton","Organic Cotton","Polyester","Wool","Silk",
            "Denim","Linen","Leather","Faux Leather",
            "Spandex","Rayon","Nylon","Velvet","Fleece","Synthetic"
        };

        private List<string> GetPatternOptions() => new()
        {
            "Solid","Striped","Checked","Printed",
            "Floral","Graphic","Polka Dot","Camouflage","Abstract"
        };

        // ==============================
        // ELECTRONICS
        // ==============================

        private List<string> GetStorageOptionsViewModel() => new()
        {
            "16 GB","32 GB","64 GB","128 GB","256 GB","512 GB",
            "1 TB","2 TB","4 TB"
        };

        private List<string> GetRamOptionsViewModel() => new()
        {
            "1 GB","2 GB","4 GB","6 GB","8 GB","12 GB",
            "16 GB","24 GB","32 GB","64 GB","128 GB"
        };
        
        private List<string> GetScreenSizesViewModel() => new()
        {
            "5 inch","5.5 inch","6.1 inch","6.5 inch","6.7 inch",
            "13 inch","14 inch","15.6 inch","17 inch",
            "19 inch","21 inch","24 inch","27 inch",
            "32 inch","43 inch","50 inch","55 inch","65 inch","75 inch","85 inch"
        };

        private List<string> GetBatteryCapacityViewModel() => new()
        {
            "2000 mAh","3000 mAh","4000 mAh","4500 mAh",
            "5000 mAh","6000 mAh","7000 mAh","8000 mAh",
            "10000 mAh","20000 mAh"
        };

        private List<string> GetOperatingSystemOptions() => new()
        {
            "Android","iOS","Windows","macOS","Linux","Chrome OS","HarmonyOS"
        };

        private List<string> GetConnectivityOptions() => new()
        {
            "WiFi","Bluetooth","NFC",
            "4G","5G",
            "WiFi + Bluetooth",
            "WiFi + 4G",
            "WiFi + 5G",
            "WiFi + Bluetooth + NFC"
        };

        private List<string> GetVoltageOptionsViewModel() => new()
        {
            "110V","220V","240V","110-220V"
        };

        // ==============================
        // HOME & FURNITURE
        // ==============================

        private List<string> GetRoomTypeOptions() => new()
        {
            "Living Room","Bedroom","Kitchen","Bathroom",
            "Office","Dining Room","Outdoor","Kids Room"
        };

        private List<string> GetFurnitureTypeOptions() => new()
        {
            "Chair","Dining Chair","Office Chair",
            "Table","Dining Table","Coffee Table",
            "Sofa","Sectional Sofa",
            "Bed","Bunk Bed",
            "Wardrobe","Desk","Cabinet","TV Unit","Bookshelf"
        };

        private List<string> GetFinishTypeOptions() => new()
        {
            "Matte","Glossy","Wood Finish","Metal Finish",
            "Glass Finish","Marble Finish"
        };

        private List<string> GetShapeOptions() => new()
        {
            "Round","Square","Rectangle","Oval",
            "L-Shape","U-Shape"
        };

        // ==============================
        // BEAUTY
        // ==============================

        private List<string> GetSkinTypeOptions() => new()
        {
            "Normal","Dry","Oily","Combination","Sensitive","Acne Prone"
        };

        private List<string> GetHairTypeOptions() => new()
        {
            "Straight","Curly","Wavy","Coily","Frizzy"
        };

        private List<string> GetShadeOptions() => new()
        {
            "Very Light","Light","Light Medium",
            "Medium","Tan","Deep","Very Deep"
        };

        private List<string> GetFragranceTypeOptions() => new()
        {
            "Floral","Woody","Fresh","Oriental","Citrus",
            "Fruity","Spicy","Aquatic","Musk"
        };

        // ==============================
        // SPORTS
        // ==============================

        private List<string> GetSportTypeOptions() => new()
        {
            "Football","Basketball","Tennis","Running",
            "Swimming","Gym","Cycling","Yoga","Boxing","Cricket"
        };

        private List<string> GetResistanceLevelOptions() => new()
        {
            "Very Light","Light","Medium","Heavy","Extra Heavy"
        };

        // ==============================
        // AUTOMOTIVE
        // ==============================

        private List<string> GetVehicleTypeOptions() => new()
        {
            "Sedan","SUV","Hatchback","Coupe",
            "Convertible","Truck","Van","Motorcycle","Electric Vehicle"
        };

        // ==============================
        // GROCERY
        // ==============================

        private List<string> GetFlavorOptions() => new()
        {
            // Soft Drinks
            "Cola","Diet Cola","Cherry Cola","Vanilla Cola",
            "Orange","Lemon","Lime","Grapefruit",
            "Apple","Mango","Berry","Mixed Fruit",
        
            // Snacks
            "Cheese","BBQ","Salted","Sweet","Spicy","Hot & Spicy",
        
            // Sweets & Desserts
            "Vanilla","Chocolate","Strawberry","Caramel",
            "Hazelnut","Mint","Coffee",
        
            // General
            "Original","Classic","Zero Sugar","Sugar Free"
        };

        private List<string> GetDietaryTypeOptions() => new()
        {
            "Regular",
            "Vegan",
            "Vegetarian",
            "Halal",
            "Kosher",
            "Gluten Free",
            "Lactose Free",
            "Sugar Free",
            "Low Carb",
            "Keto",
            "Organic",
            "Non-GMO"
        };

        // ==============================
        // BOOKS
        // ==============================

        private List<string> GetLanguageOptions() => new()
        {
            "English","Arabic","French","German",
            "Spanish","Italian","Chinese","Japanese"
        };

        private List<string> GetCoverTypeOptions() => new()
        {
            "Hardcover","Paperback","Spiral Bound","Digital"
        };

        // ==============================
        // OFFICE
        // ==============================

        private List<string> GetPaperSizeOptions() => new()
        {
            "A3","A4","A5","A6","Letter","Legal","Executive"
        };

        private List<string> GetInkTypeOptions() => new()
        {
            "Black","Color","Black & Color","Tri-Color","Cyan","Magenta","Yellow"
        };


        // ===========================================
        // MAIN MAPPING FUNCTION (FULL COVERAGE)
        // ===========================================

        private List<VariantAttributeSelectItemViewModel>
        MapAttributeNamesToAttributeSelectItems(List<enAttributeName> names, int productId)
        {
            var list = new List<VariantAttributeSelectItemViewModel>();
            void Add(enAttributeName name, List<string> values)
            {
                list.Add(new VariantAttributeSelectItemViewModel()
                {
                    DisplayType = GetDisplayTypeByName(name),
                    Name = name,
                    ValueOptions = values
                });
            }

            foreach (var name in names)
            {
                switch (name)
                {
                    case enAttributeName.Color: Add(name, GetVariantColorsViewModel()); break;
                    case enAttributeName.Size: Add(name, GetClothingSizesViewModel()); break;
                    case enAttributeName.ShoeSize: Add(name, GetShoeSizesViewModel()); break;
                    case enAttributeName.Gender: Add(name, GetGenderOptionsViewModel()); break;
                    case enAttributeName.AgeGroup: Add(name, GetAgeGroupOptionsViewModel()); break;
                    case enAttributeName.FabricType: Add(name, GetFabricTypeOptions()); break;
                    case enAttributeName.Pattern: Add(name, GetPatternOptions()); break;

                    case enAttributeName.Storage: Add(name, GetStorageOptionsViewModel()); break;
                    case enAttributeName.RAM: Add(name, GetRamOptionsViewModel()); break;
                    case enAttributeName.ScreenSize: Add(name, GetScreenSizesViewModel()); break;
                    case enAttributeName.BatteryCapacity: Add(name, GetBatteryCapacityViewModel()); break;
                    case enAttributeName.OperatingSystem: Add(name, GetOperatingSystemOptions()); break;
                    case enAttributeName.Connectivity: Add(name, GetConnectivityOptions()); break;
                    case enAttributeName.Voltage: Add(name, GetVoltageOptionsViewModel()); break;

                    case enAttributeName.RoomType: Add(name, GetRoomTypeOptions()); break;
                    case enAttributeName.FurnitureType: Add(name, GetFurnitureTypeOptions()); break;
                    case enAttributeName.FinishType: Add(name, GetFinishTypeOptions()); break;
                    case enAttributeName.Shape: Add(name, GetShapeOptions()); break;

                    case enAttributeName.SkinType: Add(name, GetSkinTypeOptions()); break;
                    case enAttributeName.HairType: Add(name, GetHairTypeOptions()); break;
                    case enAttributeName.Shade: Add(name, GetShadeOptions()); break;
                    case enAttributeName.FragranceType: Add(name, GetFragranceTypeOptions()); break;

                    case enAttributeName.SportType: Add(name, GetSportTypeOptions()); break;
                    case enAttributeName.ResistanceLevel: Add(name, GetResistanceLevelOptions()); break;

                    case enAttributeName.VehicleType: Add(name, GetVehicleTypeOptions()); break;

                    case enAttributeName.Flavor: Add(name, GetFlavorOptions()); break;
                    case enAttributeName.DietaryType: Add(name, GetDietaryTypeOptions()); break;

                    case enAttributeName.Language: Add(name, GetLanguageOptions()); break;
                    case enAttributeName.CoverType: Add(name, GetCoverTypeOptions()); break;

                    case enAttributeName.PaperSize: Add(name, GetPaperSizeOptions()); break;
                    case enAttributeName.InkType: Add(name, GetInkTypeOptions()); break;
                }
            }

            return list;
        }


        public async Task<VariantFormViewModel>
            GetVariantFormViewModelAsync(int productId,int currencyId, int? variantId = null)
        {
            //Add Or Edit
            var viewModel = new VariantFormViewModel();
            var currencies = await _currencyService.GetAllNameIdViewModelAsync();
            //int categoryId = await _uow.Products.GetCategoryIdByIdAsync(productId);  
            var attributeNames= await _categoryAttributeService.GetAttributeNamesListByProductIdAsync(productId);
            viewModel.Attributes = MapAttributeNamesToAttributeSelectItems(attributeNames,productId);
            viewModel.Currencies = currencies;
            viewModel.Code = await _currencyService.GetCodeByIdAsync(currencyId);
            decimal dollarRate = await _currencyService.GetDollarRateByIdAsync(currencyId);

            if (variantId.HasValue)//Edit
            {
                var variantDB = await _uow.Variants
                    .GetAsReadOnlyIncludeAttributesByIdAsync(variantId.Value);
                viewModel.SKU=variantDB.SKU;
                viewModel.StockQuantity = variantDB.StockQuantity;
                viewModel.Price = variantDB.Price * dollarRate;
                viewModel.ProductId= variantDB.ProductId;
                viewModel.Id = variantDB.Id;
                viewModel.Cost=variantDB.Cost * dollarRate;
                viewModel.ProductId = variantDB.ProductId;//For Remote Validation
                foreach (var attr in viewModel.Attributes)
                {
                    attr.Value = variantDB.AttributeValues
                        .FirstOrDefault(a => a.Attribute.Name == attr.Name).Value;
                }
            }
            else //Add
            { 
                viewModel.ProductId = productId;  
            } 
            return viewModel;
        }
        public async Task<enUpdateVariantResult> UpdateAsync(VariantFormViewModel viewModel, int currencyId)
        {
            var variantDB = await _uow.Variants
               .GetIncludeAttributesByIdAsync(viewModel.Id.Value);
            if (variantDB==null)
                return enUpdateVariantResult.VariantNotFound; 

            bool IsUnique = await CheckSkuUniqueAsync(
                viewModel.SKU, viewModel.Id.Value);
            if (!IsUnique)
                return enUpdateVariantResult.NotUniqueSKU;

            var attributeNames = viewModel.Attributes
                .Select(att => att.Name) 
                .ToArray();

            var attributeValue= viewModel.Attributes
                .Select(att => att.Value)
                .ToArray();

            foreach(var attr in viewModel.Attributes)
            {
                IsUnique =await CheckUniqueVariantAttributeByProductIdAsync(
                    attr.Name, attr.Value, viewModel.ProductId, viewModel.Id);
                if (!IsUnique)
                    return enUpdateVariantResult.NotUniqueAttributes;
            } 

            foreach(var name in attributeNames)
            {
                if (!IsValidName(name))
                    return enUpdateVariantResult.InvalidAttributeName;
            }
            decimal currencyRate = await _currencyService.GetDollarRateByIdAsync(currencyId);
            variantDB.Price = viewModel.Price / currencyRate;
            variantDB.Cost=viewModel.Cost / currencyRate;
            variantDB.StockQuantity= viewModel.StockQuantity;
            variantDB.SKU = viewModel.SKU;
            foreach (var attr in variantDB.AttributeValues)
            {
                attr.Value = variantDB.AttributeValues
                    .FirstOrDefault(a => a.Attribute.Name == attr.Attribute.Name).Value;
            } 
            await _uow.SaveChangesAsync();
            return enUpdateVariantResult.Success;
        }
        public async Task<enCreateVariantResult> CreateAsync(VariantFormViewModel viewModel, int currencyId)
        {
            bool IsUnique =  await CheckSkuUniqueAsync(viewModel.SKU);
            if (!IsUnique)
                return enCreateVariantResult.NotUniqueSKU;

            var attributeNames = viewModel.Attributes
                .Select(att => att.Name) 
                .ToArray();

            var attributeValue = viewModel.Attributes
                .Select(att => att.Value)
                .ToArray();

            foreach (var attr in viewModel.Attributes)
            {
                IsUnique = await CheckUniqueVariantAttributeByProductIdAsync(
                    attr.Name, attr.Value, viewModel.ProductId);
                if (!IsUnique)
                    return enCreateVariantResult.NotUniqueAttributes;
            }

            foreach (var name in attributeNames)
            {
                if (!IsValidName(name))
                    return enCreateVariantResult.InvalidAttributeName;
            }
            decimal currencyRate = await _currencyService.GetDollarRateByIdAsync(currencyId); 
            var variantDB = new Variant()
            {
                IsActive= true,
                Cost=viewModel.Cost / currencyRate,
                Price=viewModel.Price / currencyRate,
                ProductId=viewModel.ProductId,
                SKU=viewModel.SKU,
                StockQuantity=viewModel.StockQuantity,
                //variantDB.IsDeleted = false;//default value
            }; 
    
            foreach(var att in viewModel.Attributes)
            {
                enDisplayType display = GetDisplayTypeByName(att.Name);
                variantDB.AttributeValues.Add(new ()
                { 
                    AttributeId=await _attributeService.GetIdByNameAsync(att.Name),
                    Value=att.Value 
                });
            }
            await _uow.Variants.AddAsync(variantDB);
            await _uow.SaveChangesAsync();
            return enCreateVariantResult.Success;
        }
 
        public async Task<decimal> GetUnitPriceAsync(int Id)
        {
            return await _uow.Variants.GetUnitPriceAsync(Id);
        }

        public async Task<enRecalculateVariantStatusResult> 
            RecalculateQuantityAndStatusAsync(int variantId,int quantityToAdd)
        {
            var variant = await _uow.Variants
                .GetByIdAsync(variantId);
            if (variant == null)
                return enRecalculateVariantStatusResult.VariantNotFound; 

            bool IsRemove = quantityToAdd < 0;
            bool RemoveExceedingAmount = IsRemove && Math.Abs(quantityToAdd) > variant.StockQuantity ;
            if (RemoveExceedingAmount) throw new Exception("Remove exceeding amount is forbidden.");

            variant.StockQuantity += quantityToAdd;
            variant.IsActive = variant.StockQuantity > 0; 
            return enRecalculateVariantStatusResult.Success;
        }

         
    }
}
