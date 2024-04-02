using Ch3CaseStudies.Models.DomainModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Ch3CaseStudies.Models
{
    public class RegistrationViewModel
    {
        [ValidateNever]
        public List<Customer> Customers { get; set; } = new List<Customer>();

        [ValidateNever]
        public List<Product> Products { get; set; } = new List<Product>();

        [ValidateNever]
        public Customer SelectedCustomer { get; set; } = new Customer();

        [Required(ErrorMessage = "Please select a customer.")]
        public int? SelectedCustomerId { get; set; }

        [ValidateNever]
        public int? SelectedProductId { get; set; }


    }
}
