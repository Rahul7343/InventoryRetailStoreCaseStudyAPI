using Azure.Core;
using BusinessLogic.Coordinator;
using BusinessLogic.Models;
using Database.Entities;
using Database.Repository;
using System.Collections;

namespace BusinessLogic.SaleCoordinator
{
    public class SalesActivity : ISalesCoordinator
    {

        private readonly InventoryRepository<Database.Entities.Sales> _inventoryRepository;
        private readonly InventoryRepository<Database.Entities.Supplier> _supplierRepository;
        private readonly InventoryRepository<Database.Entities.Product> _productRepository;
        private readonly InventoryRepository<Database.Entities.Customer> _customerRepository;


        public SalesActivity(InventoryRepository<Database.Entities.Sales> inventoryRepository,
            InventoryRepository<Database.Entities.Supplier> supplierRepository,
            InventoryRepository<Product> productRepository,
            InventoryRepository<Customer> customerRepository)
        {
            _inventoryRepository = inventoryRepository;
            _supplierRepository = supplierRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }
        public async Task<SalesModel> AddSale(Sales request)
        {
            request.CreatedDate = DateTime.Now;
            request.ModifiedDate = DateTime.Now;
            var result = await _inventoryRepository.CreateAsync(request);

            return new SalesModel
            {
                Id = result.Id,
                CustomerId = result.CustomerId,
                Quantity= result.Quantity,
                PurchaseDate = result.PurchaseDate,
                Price = result.Price,
                ProductId = result.ProductId,
            };
        }

        public async Task<SalesModel> UpdateSale(Sales request)
        {
            request.ModifiedDate = DateTime.Now;
            var result = await _inventoryRepository.UpdateAsync(request);

            return new SalesModel
            {
                Id = result.Id,
                Quantity = result.Quantity,
                CustomerId = result.CustomerId,
                PurchaseDate = result.PurchaseDate,
                Price = result.Price,
                ProductId = result.ProductId,
            };
        }
        public async Task<IEnumerable<SalesModel>> GetSales()
        {
            var response = await _inventoryRepository.GetAllAsync();

            List<SalesModel> sales = new List<SalesModel>();

            foreach (var item in response)
            {
                SalesModel sale = new SalesModel();

                sale.Id = item.Id;
                sale.CustomerId = item.CustomerId;
                sale.Quantity = item.Quantity;
                sale.Price = item.Price;
                sale.PurchaseDate = item.PurchaseDate;
                sale.ProductId = item.ProductId;
                sale.Customer = GetCustomer(item.CustomerId).Result;
                sale.Product = GetProduct(item.ProductId).Result;
                sale.IsDeleted = item.IsDeleted;
                sale.CreatedDate = item.CreatedDate;

                sales.Add(sale);
            }

            return sales;
        }

        private async Task<CustomerModel> GetCustomer(long customerId)
        {
            var res = await _customerRepository.GetByIdAsync(customerId);
            return SetCustomerDetails(res);
        }
        private async Task<ProductModel> GetProduct(long productId)
        {
            var res = await _productRepository.GetByIdAsync(productId);
            return SetProductDetails(res);
        }

        private async Task<SupplierModel> GetSupplier(int supplierId)
        {
            var res = await _supplierRepository.GetByIdAsync(supplierId);
            return SetSupplierDetails(res);
        }

        public async Task<SalesModel?> GetSale(long saleId)
        {
            var result = await _inventoryRepository.GetByIdAsync(saleId);
            if (result == null)
            {
                return null;
            }
            var productRes = await _productRepository.GetByIdAsync(result.ProductId);
            var customerRes = await _customerRepository.GetByIdAsync(result.CustomerId);
            return ReturnSalesModel(result, productRes, customerRes);
        }

        private static SalesModel ReturnSalesModel(Sales result, Product? productRes,
            Customer? customerRes)
        {
            return new SalesModel
            {
                Id = result.Id,
                CustomerId = result.CustomerId,
                Quantity = result.Quantity,
                PurchaseDate = result.PurchaseDate,
                Price = result.Price,
                ProductId = result.ProductId,
                IsDeleted = result.IsDeleted,
                CreatedDate = result.CreatedDate,
                //Supplier = SetSupplierDetails(supplierRes),
                Product = SetProductDetails(productRes),
                Customer = SetCustomerDetails(customerRes),
            };
        }

        private static CustomerModel SetCustomerDetails(Customer? customerRes)
        {
            return new CustomerModel
            {
                Id = customerRes.Id,
                Address = customerRes.Address,
                MobileNo = customerRes.MobileNo,
                Name = customerRes.CustomerName,
            };
        }

        private static ProductModel SetProductDetails(Product? productRes)
        {
            return new ProductModel
            {
                Id = productRes.Id,
                ProductName = productRes.ProductName,
                ProductPrice = productRes.Price,
                SupplierId = productRes.SupplierId,
            };
        }

        private static SupplierModel SetSupplierDetails(Supplier? supplierRes)
        {
            return new SupplierModel
            {
                Id = supplierRes.Id,
                Name = supplierRes.SupplierName,
                MobileNo = supplierRes.MobileNo
            };
        }

        public async Task<bool> DeleteSale(long saleId)
        {
            var result = await _inventoryRepository.DeleteAsync(saleId);
            return result;
        }

    }
}
