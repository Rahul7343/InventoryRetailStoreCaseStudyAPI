using Database.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace BusinessLogic.Coordinator
{
    public interface IProductCoordinator
    {
        Task<ProductModel> AddProduct(Product product);
        Task<ProductModel> UpdateProduct(Product product);
        Task<bool> DeleteProduct(long productId);
        Task<ProductModel> GetProduct(long productId);
        Task<IEnumerable<ProductModel>> GetProducts();


    }
}
