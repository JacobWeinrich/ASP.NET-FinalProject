using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ch3CaseStudies.Models.DomainModels
{
    public class Customer
    {

        [Key]
        public int CustomerId { get; set; }

        [ValidateNever]
        public string Slug => (FirstName + "-" + LastName)?.Replace(' ', '-').ToLower()!;

        [Required(ErrorMessage = "First Name is Required!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "First Name must be between 1 and 51 characters long.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Last Name must be between 1 and 51 characters long.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Address is Required!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Address must be between 1 and 51 characters long.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "City is Required!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "City must be between 1 and 51 characters long.")]
        public string? City { get; set; }

        [Required(ErrorMessage = "State is Required!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "State must be between 1 and 51 characters long.")]
        public string? State { get; set; }

        [Required(ErrorMessage = "Zip is Required!")]
        [StringLength(21, MinimumLength = 1, ErrorMessage = "Zip must be between 1 and 21 characters long.")]
        public string? Zip { get; set; }
        [Required(ErrorMessage = "Country is Required!")]
        public string? Country { get; set; }

        [Phone(ErrorMessage = "Valid Phone is Required!")]
        [StringLength(21, MinimumLength = 1, ErrorMessage = "Phone must be between 1 and 21 characters long.")]
        [RegularExpression(@"(?<nr>\([0-9]{3}\)\s+[0-9]{3}\-[0-9]{4})", ErrorMessage = "Invalid Phone# format: (555) 555-5555")]
        public string? Phone { get; set; }

        [EmailAddress(ErrorMessage = "Valid Email is Required!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Email must be between 1 and 51 characters long.")]
        public string? Email { get; set; }

        [ValidateNever]
        public ICollection<Product> Products { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
