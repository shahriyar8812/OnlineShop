using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<ProductCategoryAttribute> GetProductCategoryAttributeAsync(int productId);
        Task<ProductCategoryAttribute?> GetProductCategoryAttributeAsync(int productId, int categoryAttributeId);
        Task UpdateProductCategoryAttributeAsync(ProductCategoryAttribute attribute);
        Task AddProductCategoryAttributeAsync(ProductCategoryAttribute productCategoryAttribute); // اضافه شده
    }
}
