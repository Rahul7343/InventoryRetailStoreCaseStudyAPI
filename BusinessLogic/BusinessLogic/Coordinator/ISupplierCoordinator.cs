using Database.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace BusinessLogic.Coordinator
{
    public interface ISupplierCoordinator
    {
        Task<SupplierModel> AddSupplier(Supplier supplier);
        Task<SupplierModel> UpdateSupplier(Supplier supplier);
        Task<bool> DeleteSupplier(long supplierId);
        Task<SupplierModel> GetSupplier(long supplierId);
        Task<IEnumerable<SupplierModel>> GetSuppliers();


    }
}
