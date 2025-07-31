using BusinessLogic.Coordinator;
using BusinessLogic.Models;
using Database.Entities;
using Database.Repository;
using System.Collections;

namespace BusinessLogic.CategoryCoordinator
{
    public class CategoryActivity : ICategoryCoordinator
    {
        private readonly InventoryRepository<Database.Entities.Category> _inventoryRepository;
        public CategoryActivity(InventoryRepository<Database.Entities.Category> inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        public async Task<CategoryModel> AddCategory(Category request)
        {

            var result = await _inventoryRepository.CreateAsync(request);

            return new CategoryModel
            {
                Id = result.Id,
                CategoryName = request.CategoryName,
            };
        }

        public async Task<CategoryModel> UpdateCategory(Category request)
        {
            var result = await _inventoryRepository.UpdateAsync(request);

            return new CategoryModel
            {
                Id = result.Id,
                CategoryName = result.CategoryName,
            };
        }
        public async Task<IEnumerable<CategoryModel>> GetCategories()
        {
            var response = await _inventoryRepository.GetAllAsync();

            List<CategoryModel> categories = new List<CategoryModel>();

            foreach (var item in response)
            {
                CategoryModel category = new CategoryModel();

                category.CategoryName = item.CategoryName;
                category.Id = item.Id;

                categories.Add(category);
            }

            return categories;
        }
        public async Task<CategoryModel?> GetCategory(long categoryId)
        {
            var result = await _inventoryRepository.GetByIdAsync(categoryId);
            if (result == null)
            {
                return null;
            }
            return new CategoryModel
            {
                Id = result.Id,
                CategoryName = result.CategoryName,
            };
        }

        public async Task<bool> DeleteCategory(long categoryId)
        {
            var result = await _inventoryRepository.DeleteAsync(categoryId);
            return result;
        }

    }
}
