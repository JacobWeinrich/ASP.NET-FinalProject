using Ch3CaseStudies.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Ch3CaseStudies.Models.Configuration
{
    public class TechnicianConfig : IEntityTypeConfiguration<Technician>
    {
        public void Configure(EntityTypeBuilder<Technician> entity)
        {
            entity.HasData(new Technician() { TechnicianId = 1, Name = "Technician1", Email = "Example@exam.com", Phone = "555-555-5555" });
        }
    }
}
