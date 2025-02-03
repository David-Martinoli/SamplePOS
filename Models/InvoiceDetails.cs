namespace SamplePOS_ServerSide.Models
{
    public class InvoiceDetails
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal { get { return Quantity * Price; } }
    }
}
