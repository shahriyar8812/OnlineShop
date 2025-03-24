using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.ProductCategoryAttributes)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Include(p => p.ProductCategoryAttributes)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductCategoryAttributes)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            if (product.ProductCategoryAttributes != null && product.ProductCategoryAttributes.Any())
            {
                foreach (var attr in product.ProductCategoryAttributes)
                {
                    attr.ProductId = product.Id;
                    _context.ProductCategoryAttributes.Add(attr);
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            var existingProduct = await _context.Products
                .Include(p => p.ProductCategoryAttributes)
                .FirstOrDefaultAsync(p => p.Id == product.Id);

            if (existingProduct == null)
            {
                return;
            }

            existingProduct.Title = product.Title;
            existingProduct.Code = product.Code;
            existingProduct.Description = product.Description;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.ImageUrl = product.ImageUrl;
            existingProduct.CategoryAttributeId = product.CategoryAttributeId;

            if (product.ProductCategoryAttributes != null && product.ProductCategoryAttributes.Any())
            {
                var existingAttributes = existingProduct.ProductCategoryAttributes?.ToList() ?? new List<ProductCategoryAttribute>();
                foreach (var existingAttr in existingAttributes)
                {
                    if (!product.ProductCategoryAttributes.Any(a => a.CategoryAttributeId == existingAttr.CategoryAttributeId))
                    {
                        _context.ProductCategoryAttributes.Remove(existingAttr);
                    }
                }

                foreach (var attr in product.ProductCategoryAttributes)
                {
                    var existingAttr = existingProduct.ProductCategoryAttributes?
                        .FirstOrDefault(a => a.CategoryAttributeId == attr.CategoryAttributeId);
                    if (existingAttr == null)
                    {
                        attr.ProductId = product.Id;
                        _context.ProductCategoryAttributes.Add(attr);
                    }
                    else
                    {
                        existingAttr.Value = attr.Value;
                    }
                }
            }
            else if (existingProduct.ProductCategoryAttributes != null)
            {
                _context.ProductCategoryAttributes.RemoveRange(existingProduct.ProductCategoryAttributes);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductCategoryAttributes)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product != null)
            {
                if (product.ProductCategoryAttributes != null)
                {
                    _context.ProductCategoryAttributes.RemoveRange(product.ProductCategoryAttributes);
                }
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ProductCategoryAttribute> GetProductCategoryAttributeAsync(int productId)
        {
            return await _context.ProductCategoryAttributes
                .FirstOrDefaultAsync(attr => attr.ProductId == productId);
        }

        public async Task UpdateProductCategoryAttributeAsync(ProductCategoryAttribute attribute)
        {
            _context.ProductCategoryAttributes.Update(attribute);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductCategoryAttribute?> GetProductCategoryAttributeAsync(int productId, int categoryAttributeId)
        {
            return await _context.ProductCategoryAttributes
                .FirstOrDefaultAsync(pca => pca.ProductId == productId && pca.CategoryAttributeId == categoryAttributeId);
        }

        public async Task AddProductCategoryAttributeAsync(ProductCategoryAttribute productCategoryAttribute)
        {
            _context.ProductCategoryAttributes.Add(productCategoryAttribute);
            await _context.SaveChangesAsync();
        }
    }
}