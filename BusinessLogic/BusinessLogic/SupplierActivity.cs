using BusinessLogic.Coordinator;
using BusinessLogic.Models;
using Database.Entities;
//using models = Inventory.Models;
using Database.Repository;
using System.Collections;

namespace BusinessLogic.SupplierCoordinator
{
    public class SupplierActivity : ISupplierCoordinator
    {
        private readonly InventoryRepository<Database.Entities.Supplier> _inventoryRepository;
        public SupplierActivity(InventoryRepository<Database.Entities.Supplier> inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        public async Task<SupplierModel> AddSupplier(Supplier request)
        {

            request.CreatedDate = DateTime.Now;
            request.ModifiedDate = DateTime.Now;
            var result = await _inventoryRepository.CreateAsync(request);

            return new SupplierModel
            {
                MobileNo = result.MobileNo,
                Name = request.SupplierName,
            };
        }

        public async Task<SupplierModel> UpdateSupplier(Supplier request)
        {
            request.ModifiedDate = DateTime.Now;
            var result = await _inventoryRepository.UpdateAsync(request);

            return new SupplierModel
            {
                Id = result.Id,
                Name = result.SupplierName,
                MobileNo = result.MobileNo
            };
        }
        public async Task<IEnumerable<SupplierModel>> GetSuppliers()
        {
            var response = await _inventoryRepository.GetAllAsync();

            List<SupplierModel> suppliers = new List<SupplierModel>();

            foreach (var item in response)
            {
                SupplierModel supplier = new SupplierModel();

                supplier.Name = item.SupplierName;
                supplier.MobileNo = item.MobileNo;
                supplier.Id = item.Id;
                supplier.IsDeleted = item.IsDeleted;
                supplier.CreatedDate = item.CreatedDate;


                suppliers.Add(supplier);
            }

            return suppliers;
        }
        public async Task<SupplierModel?> GetSupplier(long supplierId)
        {
            var result = await _inventoryRepository.GetByIdAsync(supplierId);
            if (result == null)
            {
                return null;
            }
            return new SupplierModel
            {
                Id = result.Id,
                Name = result.SupplierName,
                MobileNo = result.MobileNo,
                IsDeleted = result.IsDeleted,
                CreatedDate = result.CreatedDate
            };
        }

        public async Task<bool> DeleteSupplier(long supplierId)
        {
            var result = await _inventoryRepository.DeleteAsync(supplierId);
            return result;
        }

    }
}
