namespace SamplePOS_ServerSide.Models
{
    public class Cart
    {
        public List<Product> Products { get; set; } = [];
        public decimal Total => Products.Sum(p => p.Price);
    }
}