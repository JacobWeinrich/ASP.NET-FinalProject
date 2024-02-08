using System.ComponentModel.DataAnnotations;

namespace Ch3CaseStudies.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "Country Name is Required")]
        public string? Name { get; set; }
    }
}
