using Ch3CaseStudies.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Ch3CaseStudies.Models.Configuration
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> entity)
        {
            entity.HasData(
                new Customer() { CustomerId = 1, FirstName = "Customer1", LastName = "Customer1", Address = "1234 Example St", City = "Example", State = "EX", Zip = "12345", Country = "USA", Phone = "555-555-5555", Email = "Example@example.com" }
                );
        }
    }
}
