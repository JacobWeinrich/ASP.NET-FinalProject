using Ch3CaseStudies.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Ch3CaseStudies.Models.Configuration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.HasMany(p => p.Customers).WithMany(c => c.Products).UsingEntity(sc => sc.HasData(
                new { ProductsProductId = 1, CustomersCustomerId = 1 }
                ));


            entity.HasData(
                new Product() { ProductId = 1, ProductCode = "P001", Name = "Product1", Price = 29.99m, ReleaseDate = DateTime.Now },
                new Product() { ProductId = 2, ProductCode = "P002", Name = "Product2", Price = 29.99m, ReleaseDate = DateTime.Now }
                );

        }
    }
}
