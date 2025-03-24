using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CategoryAttributeService : ICategoryAttributeService
    {
        private readonly ICategoryAttributeRepository _categoryAttributeRepository;
        private readonly ILogger<CategoryAttributeService> _logger;

        public CategoryAttributeService(ICategoryAttributeRepository categoryAttributeRepository, ILogger<CategoryAttributeService> logger)
        {
            _categoryAttributeRepository = categoryAttributeRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<CategoryAttribute>> GetAttributesByCategoryIdAsync(int categoryId)
        {
            _logger.LogInformation($"Fetching attributes for categoryId: {categoryId} from service...");
            return await _categoryAttributeRepository.GetAttributesByCategoryIdAsync(categoryId);
        }

        public async Task<IEnumerable<CategoryAttribute>> GetAllAttributesAsync()
        {
            _logger.LogInformation("Fetching all category attributes from service...");
            return await _categoryAttributeRepository.GetAllAttributesAsync();
        }
    }
}