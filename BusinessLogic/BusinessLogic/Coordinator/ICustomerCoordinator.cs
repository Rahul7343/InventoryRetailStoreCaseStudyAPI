using Database.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace BusinessLogic.Coordinator
{
    public interface ICustomerCoordinator
    {
        Task<CustomerModel> AddCustomer(Customer customer);
        Task<CustomerModel> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(long customerId);
        Task<CustomerModel> GetCustomer(long customerId);
        Task<IEnumerable<CustomerModel>> GetCustomers();


    }
}
