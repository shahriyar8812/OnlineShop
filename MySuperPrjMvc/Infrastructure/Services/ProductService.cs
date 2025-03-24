using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<IEnumerable<CategoryAttribute>> GetCategoryAttributesByCategoryIdAsync(int categoryId)
        {
            return await _categoryRepository.GetCategoryAttributesByCategoryIdAsync(categoryId);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _productRepository.GetProductsByCategoryAsync(categoryId);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task AddProductAsync(Product product)
        {
            await _productRepository.AddProductAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteProductAsync(id);
        }

        public async Task AddProductCategoryAttributeAsync(ProductCategoryAttribute attribute)
        {
            await _productRepository.AddProductCategoryAttributeAsync(attribute);
        }

        public async Task<ProductCategoryAttribute> GetProductCategoryAttributeAsync(int productId, int categoryAttributeId)
        {
            return await _productRepository.GetProductCategoryAttributeAsync(productId, categoryAttributeId);
        }

        public async Task UpdateProductCategoryAttributeAsync(ProductCategoryAttribute attribute)
        {
            await _productRepository.UpdateProductCategoryAttributeAsync(attribute);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }
    }
}