using SamplePOS_ServerSide.Models;

namespace SamplePOS_ServerSide.Services
{
    public class BillingService
    {
        public Invoice GenerateInvoice(List<Product> products)
        {
            return new Invoice
            {
                CustomerName = "Consumidor Final",
                Date = DateTime.Now,
                Details = products,
                Total = products.Sum(p => p.Price)
            };

        }
    }
}
