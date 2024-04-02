using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Ch3CaseStudies.Models.DomainModels
{
    public class Technician
    {
        [Key]
        public int TechnicianId { get; set; }

        [ValidateNever]
        public string Slug => Name?.Replace(' ', '-').ToLower();

        [Required(ErrorMessage = "Valid Technician Name is Required!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Technician Name must be between 2 and 50 characters long.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Valid Technician Email is Required!")]
        [EmailAddress(ErrorMessage = "Valid Technician Email is Required!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Valid Technician Phone is Required!")]
        [Phone(ErrorMessage = "Valid Technician Phone is Required!")]
        public string? Phone { get; set; }


    }
}
