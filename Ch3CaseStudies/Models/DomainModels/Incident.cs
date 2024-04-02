using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Ch3CaseStudies.Models.DomainModels
{
    public class Incident
    {
        [Key]
        public int IncidentId { get; set; }

        [ValidateNever]
        public string Slug => ("Incident" + "-" + IncidentId)?.Replace(' ', '-').ToLower() + DateOpened.ToShortDateString();

        [Required(ErrorMessage = "Title is Required!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 50 characters long.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Description is Required!")]
        [StringLength(250, MinimumLength = 2, ErrorMessage = "Description must be between 2 and 250 characters long.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Date Opened is Required!")]
        public DateTime DateOpened { get; set; } = DateTime.Now;

        public DateTime? DateClosed { get; set; }

        [Required(ErrorMessage = "Please select a Customer")]
        //[ForeignKey("CustomerId")]
        public int? CustomerId { get; set; }

        [ValidateNever]
        public Customer Customer { get; set; } = null!;


        [Required(ErrorMessage = "Please select a Product")]
        //[ForeignKey("ProductId")]
        public int? ProductId { get; set; }

        [ValidateNever]
        public Product Product { get; set; } = null!;

        [ValidateNever]
        public int? TechnicianId { get; set; }

        [ValidateNever]
        public Technician Technician { get; set; } = null!;



    }
}
