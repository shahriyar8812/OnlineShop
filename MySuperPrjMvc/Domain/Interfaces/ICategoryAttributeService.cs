using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICategoryAttributeService
    {
        Task<IEnumerable<CategoryAttribute>> GetAttributesByCategoryIdAsync(int categoryId);
        Task<IEnumerable<CategoryAttribute>> GetAllAttributesAsync();
    }
}