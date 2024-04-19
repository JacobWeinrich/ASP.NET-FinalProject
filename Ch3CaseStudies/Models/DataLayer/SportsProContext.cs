using Ch3CaseStudies.Models.Configuration;
using Ch3CaseStudies.Models.DomainModels;
using Ch3CaseStudies.Models.DomainModels.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
namespace Ch3CaseStudies.Models.DataLayer
{
    public class SportsProContext : IdentityDbContext<User>
    {
        public DbSet<Incident> Incidents { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Technician> Technicians { get; set; } = null!;

        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;


        public SportsProContext(DbContextOptions<SportsProContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CountryConfig());

            modelBuilder.ApplyConfiguration(new CustomerConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new IncidentConfig());
            modelBuilder.ApplyConfiguration(new TechnicianConfig());

        }


    }
}
