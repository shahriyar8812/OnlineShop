using Domain.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductModel>> GetAllProductsAsync()
        {
            _logger.LogInformation("Getting all products from service...");
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<IEnumerable<ProductModel>> GetProductsByCategoryAsync(ProductCategory category)
        {
            _logger.LogInformation($"Getting products for category: {category} from service...");
            return await _productRepository.GetProductsByCategoryAsync(category);
        }

        public async Task<ProductModel> GetProductByIdAsync(int id)
        {
            _logger.LogInformation($"Getting product with ID {id} from service...");
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task AddProductAsync(ProductModel product)
        {
            _logger.LogInformation($"Adding product with Title: {product.Title} from service...");
            await _productRepository.AddProductAsync(product);
        }

        public async Task UpdateProductAsync(ProductModel product)
        {
            _logger.LogInformation($"Updating product with ID: {product.Id} from service...");
            await _productRepository.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            _logger.LogInformation($"Deleting product with ID: {id} from service...");
            await _productRepository.DeleteProductAsync(id);
        }
    }
}
