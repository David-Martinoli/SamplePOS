namespace SamplePOS_ServerSide.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public string? CustomerName { get; set; }
        public DateTime Date { get; set; }
        public List<Product>? Details { get; set; }
        public decimal Total { get; set; }
    }

}
