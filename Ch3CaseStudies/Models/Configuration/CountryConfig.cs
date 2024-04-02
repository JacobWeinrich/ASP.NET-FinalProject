using Ch3CaseStudies.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Ch3CaseStudies.Models.Configuration
{
    public class CountryConfig : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> entity)
        {
            entity.HasData(new Country() { CountryId = 1, Name = "USA" }, new Country() { CountryId = 2, Name = "Canada" }, new Country() { CountryId = 3, Name = "Mexico" });
        }
    }
}
