using Ch3CaseStudies.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Ch3CaseStudies.Models.Configuration
{
    public class IncidentConfig : IEntityTypeConfiguration<Incident>
    {
        public void Configure(EntityTypeBuilder<Incident> entity)
        {
            entity.HasData(new Incident() { IncidentId = 1, Title = "Incident1", Description = "Incident1", DateOpened = DateTime.Now, CustomerId = 1, ProductId = 1, TechnicianId = 1 });
        }
    }
}
