using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(ApplicationDbContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductModel>> GetAllProductsAsync()
        {
            _logger.LogInformation("Fetching all products...");
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<IEnumerable<ProductModel>> GetProductsByCategoryAsync(ProductCategory category)
        {
            _logger.LogInformation($"Fetching products for category: {category}");
            var products = await _context.Products
                                          .Where(p => p.Category == category)
                                          .ToListAsync();
            return products;
        }

        public async Task<ProductModel> GetProductByIdAsync(int id)
        {
            _logger.LogInformation($"Fetching product with ID: {id}");
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProductAsync(ProductModel product)
        {
            _logger.LogInformation($"Adding product with Title: {product.Title}");
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(ProductModel product)
        {
            _logger.LogInformation($"Updating product with ID: {product.Id}");
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            _logger.LogInformation($"Deleting product with ID: {id}");
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
