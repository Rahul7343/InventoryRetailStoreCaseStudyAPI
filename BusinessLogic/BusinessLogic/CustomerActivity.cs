using BusinessLogic.Coordinator;
using BusinessLogic.Models;
using Database.Entities;
using Database.Repository;
using System.Collections;

namespace BusinessLogic.CustomerCoordinator
{
    public class CustomerActivity : ICustomerCoordinator
    {
        private readonly InventoryRepository<Database.Entities.Customer> _inventoryRepository;
        public CustomerActivity(InventoryRepository<Database.Entities.Customer> inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        public async Task<CustomerModel> AddCustomer(Customer request)
        {
            request.CreatedDate = DateTime.Now;
            request.ModifiedDate = DateTime.Now;
            var result = await _inventoryRepository.CreateAsync(request);

            return new CustomerModel
            {
                MobileNo = result.MobileNo,
                Name = request.CustomerName,
                Id = result.Id,
                Address = result.Address,
            };
        }

        public async Task<CustomerModel> UpdateCustomer(Customer request)
        {
            
            request.ModifiedDate = DateTime.Now;
            var result = await _inventoryRepository.UpdateAsync(request);

            return new CustomerModel
            {
                Id = result.Id,
                Name = result.CustomerName,
                MobileNo = result.MobileNo,
                Address = result.Address
            };
        }
        public async Task<IEnumerable<CustomerModel>> GetCustomers()
        {
            var response = await _inventoryRepository.GetAllAsync();

            List<CustomerModel> customers = new List<CustomerModel>();

            foreach (var item in response)
            {
                CustomerModel customer = new CustomerModel();

                customer.Name = item.CustomerName;
                customer.MobileNo = item.MobileNo;
                customer.CreatedDate = item.CreatedDate;
                customer.Id = item.Id;
                customer.Address = item.Address;
                customers.Add(customer);
            }

            return customers;
        }
        public async Task<CustomerModel?> GetCustomer(long customerId)
        {
            var result = await _inventoryRepository.GetByIdAsync(customerId);
            if (result == null)
            {
                return null;
            }
            return new CustomerModel
            {
                Id = result.Id,
                Name = result.CustomerName,
                MobileNo = result.MobileNo,
                Address = result.Address,
            };
        }

        public async Task<bool> DeleteCustomer(long customerId)
        {
            var result = await _inventoryRepository.DeleteAsync(customerId);
            return result;
        }

    }
}
