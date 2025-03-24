using Domain.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<IEnumerable<CategoryAttribute>> GetCategoryAttributesByCategoryIdAsync(int categoryId)  // پیاده‌سازی این متد
        {
            return await _categoryRepository.GetCategoryAttributesByCategoryIdAsync(categoryId);
        }
    }
}
