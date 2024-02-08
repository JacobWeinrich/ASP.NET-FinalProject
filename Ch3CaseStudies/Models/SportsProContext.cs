using Microsoft.EntityFrameworkCore;
namespace Ch3CaseStudies.Models
{
    public class SportsProContext : DbContext
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
            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 1, ProductCode = "P001", Name = "Product1", Price = 29.99m, ReleaseDate = DateTime.Now });
            modelBuilder.Entity<Technician>().HasData(new Technician() { TechnicianId = 1, Name = "Technician1", Email = "Example@exam.com", Phone = "555-555-5555" });
            modelBuilder.Entity<Customer>().HasData(new Customer() { CustomerId = 1, FirstName = "Customer1", LastName = "Customer1", Address = "1234 Example St", City = "Example", State = "EX", Zip = "12345", Country = "USA", Phone = "555-555-5555", Email = "Example@example.com" });
            modelBuilder.Entity<Country>().HasData(new Country() { CountryId = 1, Name = "USA" }, new Country() { CountryId = 2, Name = "Canada" }, new Country() { CountryId = 3, Name = "Mexico" });
            modelBuilder.Entity<Incident>().HasData(new Incident() { IncidentId = 1, Title = "Incident1", Description = "Incident1", DateOpened = DateTime.Now, CustomerId = 1, ProductId = 1, TechnicianId = 1 });
        }
    }
}
