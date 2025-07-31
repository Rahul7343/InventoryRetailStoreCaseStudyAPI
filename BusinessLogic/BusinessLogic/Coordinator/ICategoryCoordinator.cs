using Database.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace BusinessLogic.Coordinator
{
    public interface ICategoryCoordinator
    {
        Task<CategoryModel> AddCategory(Category category);
        Task<CategoryModel> UpdateCategory(Category category);
        Task<bool> DeleteCategory(long categoryId);
        Task<CategoryModel> GetCategory(long categoryId);
        Task<IEnumerable<CategoryModel>> GetCategories();


    }
}
