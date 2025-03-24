using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategoryAttribute> ProductCategoryAttributes { get; set; }
        public DbSet<CategoryAttribute> CategoryAttributes { get; set; }
        public DbSet<CategoryAttribute> AttributeType { get; set; }
        public DbSet<CategoryAttribute> ProductCategory { get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CategoryAttribute>()
                .HasOne(ca => ca.Category)
                .WithMany(c => c.CategoryAttributes)
                .HasForeignKey(ca => ca.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ProductCategoryAttribute>()
                .HasOne(pca => pca.Product)
                .WithMany(p => p.ProductCategoryAttributes)
                .HasForeignKey(pca => pca.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProductCategoryAttribute>()
                .HasOne(pca => pca.CategoryAttribute)
                .WithMany()
                .HasForeignKey(pca => pca.CategoryAttributeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
