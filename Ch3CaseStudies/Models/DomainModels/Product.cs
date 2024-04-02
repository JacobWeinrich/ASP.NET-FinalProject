using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ch3CaseStudies.Models.DomainModels
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [ValidateNever]
        public string Slug => Name?.Replace(' ', '-').ToLower() + '-' + ReleaseDate.Year.ToString();


        [Required(ErrorMessage = "Valid Product Code is Required!")]
        public string ProductCode { get; set; } = null!;

        [Required(ErrorMessage = "Product Name is Required!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Product Price is Required!")]
        [Column(TypeName = "decimal(8,2)")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Product Release Date is Required!")]
        public DateTime ReleaseDate { get; set; }
        [ValidateNever]
        public ICollection<Customer> Customers { get; set; }
    }
}
