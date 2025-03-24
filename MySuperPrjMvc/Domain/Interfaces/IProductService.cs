using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<CategoryAttribute>> GetCategoryAttributesByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task AddProductCategoryAttributeAsync(ProductCategoryAttribute productCategoryAttribute);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<ProductCategoryAttribute?> GetProductCategoryAttributeAsync(int productId, int categoryAttributeId);
        Task UpdateProductCategoryAttributeAsync(ProductCategoryAttribute attribute);


    }
}
