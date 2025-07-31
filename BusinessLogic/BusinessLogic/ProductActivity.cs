using BusinessLogic.Coordinator;
using BusinessLogic.Models;
using Database.Entities;
using Database.Repository;
using Microsoft.IdentityModel.Protocols;
using System.Collections;

namespace BusinessLogic.ProductCoordinator
{
    public class ProductActivity : IProductCoordinator
    {

        private readonly InventoryRepository<Database.Entities.Product> _inventoryRepository;
        private readonly InventoryRepository<Database.Entities.Supplier> _supplierRepository;
        private readonly InventoryRepository<Database.Entities.Category> _categoryRepository;
        public ProductActivity(InventoryRepository<Database.Entities.Product> inventoryRepository,
            InventoryRepository<Database.Entities.Supplier> supplierRepository,
            InventoryRepository<Database.Entities.Category> categoryRepository)
        {
            _inventoryRepository = inventoryRepository;
            _supplierRepository = supplierRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<ProductModel> AddProduct(Product request)
        {
            request.CreatedDate = DateTime.Now;
            request.ModifiedDate = DateTime.Now;
            var result = await _inventoryRepository.CreateAsync(request);

            return new ProductModel
            {
                Id = result.Id,
                ProductName = request.ProductName,
                ProductPrice = result.Price,
                CategoryId = result.CategoryId,
                SupplierId = result.SupplierId,
                StockQuantity = result.StockQuantity,
            };
        }

        public async Task<ProductModel> UpdateProduct(Product request)
        {
            request.ModifiedDate = DateTime.Now;
            var result = await _inventoryRepository.UpdateAsync(request);

            return new ProductModel
            {
                Id = result.Id,
                ProductName = request.ProductName,
                ProductPrice = result.Price,
                CategoryId = result.CategoryId,
                SupplierId = result.SupplierId,
            };
        }
        public async Task<IEnumerable<ProductModel>> GetProducts()
        {
            var response = await _inventoryRepository.GetAllAsync();

            List<ProductModel> products = new List<ProductModel>();

            foreach (var item in response)
            {
                ProductModel product = new ProductModel();

                product.Id = item.Id;
                product.ProductPrice = item.Price;
                product.ProductName = item.ProductName;
                product.StockQuantity = item.StockQuantity;
                product.CategoryId = item.CategoryId;
                product.SupplierId = item.SupplierId;
                product.CreatedDate = item.CreatedDate;
                product.Supplier = GetSupplier(item.SupplierId).Result;
                product.Category = GetCategory(item.CategoryId).Result;

                products.Add(product);
            }

            return products;
        }

        private async Task<CategoryModel> GetCategory(long categoryId)
        {
            if (categoryId > 0) 
            {
                var res = await _categoryRepository.GetByIdAsync(categoryId);
                return SetCategoryDetails(res);
            }
            return new CategoryModel();
        }

        private CategoryModel SetCategoryDetails(Category? categoryRes)
        {
            return new CategoryModel
            {
                Id = categoryRes.Id,
                CategoryName = categoryRes.CategoryName,
            };
        }

        public async Task<ProductModel?> GetProduct(long productId)
        {
            var result = await _inventoryRepository.GetByIdAsync(productId);
            if (result == null)
            {
                return null;
            }
            return new ProductModel
            {
                Id = result.Id,
                ProductName = result.ProductName,
                ProductPrice = result.Price,
                CategoryId = result.CategoryId,
                SupplierId = result.SupplierId,
                Supplier = GetSupplier(result.SupplierId).Result,
                Category = GetCategory(result.CategoryId).Result
            };
        }
        public async Task<bool> DeleteProduct(long productId)
        {
            var result = await _inventoryRepository.DeleteAsync(productId);
            return result;
        }
        private async Task<SupplierModel> GetSupplier(long supplierId)
        {
            if (supplierId >0)
            {
                var res = await _supplierRepository.GetByIdAsync(supplierId);
                return SetSupplierDetails(res);
            }
            return new SupplierModel();
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
    }
}
