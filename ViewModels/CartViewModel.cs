using SamplePOS.Models;

namespace SamplePOS.ViewModels
{

    public class CartViewModel
    {
        public Cart Cart { get; set; } = new Cart();

        public void AddProduct(Product product)
        {
            Cart.AddProduct(product);
        }

        public void ClearCart()
        {
            Cart.ClearCart();
        }

        public void Bill()
        {
            // Lógica de facturación
        }
    }
}