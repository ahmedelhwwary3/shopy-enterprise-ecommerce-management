
using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Orders;
using Enterprise_E_Commerce_Management_System.Application.Payments;
using Enterprise_E_Commerce_Management_System.Application.Products;
using Enterprise_E_Commerce_Management_System.Application.Shipments.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Shipments.Results; 
using Enterprise_E_Commerce_Management_System.Application.Variants;
using Enterprise_E_Commerce_Management_System.Application.Variants.Results;
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Infrastructures.Shipments;
using Enterprise_E_Commerce_Management_System.Models.Shipments;
using Enterprise_E_Commerce_Management_System.ViewModels.Shipment;

namespace Enterprise_E_Commerce_Management_System.Application.Shipments
{
    public class ShipmentOrderService : IShipmentOrderService
    {
        private readonly IUnitOfWork _uow; 
        private readonly IVariantService _variantService;
        private readonly IProductService _productService;
        public ShipmentOrderService
            (IVariantService variantService,
            IProductService productService,
            IUnitOfWork uow)
        { 
            _variantService=variantService;
            _productService=productService;
            _uow=uow;
        }
        public async Task<enRecalculateStockQuantityStatusResult> RecalculateStockQuantityAndStatusAsync(bool IsRemove, int orderId)
        {
            //Filter Unique Variants And Products ,Then Update Thier Quantities
            Dictionary<int, int> variantIdQuantityMap = new Dictionary<int, int>();
            HashSet<int> prdIdMap = new HashSet<int>();

            var orderDB = await _uow.Orders.GetIncludeItemsByOrderIdAsync(orderId);
            if (orderDB?.OrderItems == null)
                return enRecalculateStockQuantityStatusResult.OrderItemsNotFound; 

            foreach (var i in orderDB.OrderItems)
            {
                if (!variantIdQuantityMap.ContainsKey(i.VariantId))
                    variantIdQuantityMap.Add(i.VariantId, i.Quantity);
                else
                    variantIdQuantityMap[i.VariantId] += i.Quantity;

                if (!prdIdMap.Contains(i.Variant.ProductId))
                    prdIdMap.Add(i.Variant.ProductId);
            }

            //Update Quantity
            foreach (var v in variantIdQuantityMap)
            {
                int quantity = IsRemove ? -v.Value : v.Value;
                enRecalculateVariantStatusResult result = await _variantService
                  .RecalculateQuantityAndStatusAsync(v.Key, quantity); 
                if (result == enRecalculateVariantStatusResult.VariantNotFound)
                    return enRecalculateStockQuantityStatusResult.OrderVariantNotFound; 
            }
            //To Refresh Variants Quantity To Get Product And Update Status
            await _uow.SaveChangesAsync();
            foreach (var p in prdIdMap)
            {
                await _productService.RecalculateProductStatusAsync(p);
            }
            return enRecalculateStockQuantityStatusResult.Success;
        }
    }
}
