using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ch3CaseStudies.Models
{
    public class Customer
    {

        [Key]
        public int CustomerId { get; set; }

        [ValidateNever]
        public string Slug => (FirstName +"-"+ LastName)?.Replace(' ', '-').ToLower();

        [Required(ErrorMessage = "First Name is Required!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 25 characters long.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 25 characters long.")]
        public string? LastName { get; set;}

        [Required(ErrorMessage = "Address is Required!")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "City is Required!")]
        public string? City { get; set; }
        [Required(ErrorMessage = "State is Required!")]
        public string? State { get; set; }
        [Required(ErrorMessage = "Zip is Required!")]
        public string? Zip { get; set; }
        [Required(ErrorMessage = "Country is Required!")]
        public string? Country { get; set; }

        [Required(ErrorMessage = "Phone is Required!")]
        [Phone(ErrorMessage = "Valid Phone is Required!")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Email is Required!")]
        [EmailAddress(ErrorMessage = "Valid Email is Required!")]
        public string? Email { get; set; }

    }
}
