using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class CategoryAttributeRepository : ICategoryAttributeRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryAttributeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryAttribute>> GetAttributesByCategoryIdAsync(int categoryId)
        {
            return await _context.CategoryAttributes
                                 .Where(ca => ca.CategoryId == categoryId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<CategoryAttribute>> GetAllAttributesAsync()
        {
            return await _context.CategoryAttributes.ToListAsync();
        }
    }
}