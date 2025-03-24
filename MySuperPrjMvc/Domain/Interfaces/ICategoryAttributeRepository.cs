using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICategoryAttributeRepository
    {
        Task<IEnumerable<CategoryAttribute>> GetAttributesByCategoryIdAsync(int categoryId);
        Task<IEnumerable<CategoryAttribute>> GetAllAttributesAsync();
    }
}