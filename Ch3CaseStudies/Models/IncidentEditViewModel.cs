using Ch3CaseStudies.Models.DomainModels;

namespace Ch3CaseStudies.Models
{
    public class IncidentEditViewModel
    {

        public string Action { get; set; } = "";
        public string ActiveCustomer { get; set; } = "";
        public string ActiveProduct { get; set; } = "";
        public string ActiveTechnician { get; set; } = "";

        public Incident Incident { get; set; } = new Incident();
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Technician> Technicians { get; set; } = new List<Technician>();

        public string CheckActiveCustomer(string activeCustomer) => activeCustomer.ToLower() == ActiveCustomer.ToLower() ? "active" : "";
        public string CheckActiveProduct(string activeProduct) => activeProduct.ToLower() == ActiveProduct.ToLower() ? "active" : "";
        public string CheckActiveTechnician(string activeTechnician) => activeTechnician.ToLower() == ActiveTechnician.ToLower() ? "active" : "";



    }
}
