using Ch3CaseStudies.Models.DataLayer;
using Ch3CaseStudies.Models.DomainModels;

namespace Ch3CaseStudies.Models
{
    public static class Check
    {

        public static string CheckEmail(SportsProContext ctx, Customer customer)
        {
            if (customer.CustomerId == 0)
            {
                var duplicateCustomer = ctx.Customers.FirstOrDefault(c => c.Email!.ToLower() == customer.Email!.ToLower());
                if (duplicateCustomer != null)
                {
                    return "Email is already in use.";
                }
                else
                {
                    return "";
                }
            }
            else
            {

                var duplicateCustomer = ctx.Customers.FirstOrDefault(c => c.Email!.ToLower() == customer.Email!.ToLower() && c.CustomerId != customer.CustomerId);
                if (duplicateCustomer != null)
                {
                    return "Email is already in use.";                    
                }
                else
                {
                    return "";
                }
            }


        }
    }
}
