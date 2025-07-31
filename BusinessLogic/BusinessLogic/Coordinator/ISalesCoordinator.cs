using Database.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace BusinessLogic.Coordinator
{
    public interface ISalesCoordinator
    {
        Task<SalesModel> AddSale(Sales sale);
        Task<SalesModel> UpdateSale(Sales sale);
        Task<bool> DeleteSale(long saleId);
        Task<SalesModel> GetSale(long saleId);
        Task<IEnumerable<SalesModel>> GetSales();


    }
}
