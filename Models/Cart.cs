namespace SamplePOS.Models{
public class Cart
{
    public List<Product> Products { get; set; } = new List<Product>();

    public decimal Total => Products.Sum(p => p.Price);

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public void ClearCart()
    {
        Products.Clear();
    }
}
}