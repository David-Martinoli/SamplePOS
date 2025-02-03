using Microsoft.EntityFrameworkCore;
using SamplePOS_ServerSide.Data;
using SamplePOS_ServerSide.Models;

namespace SamplePOS_ServerSide.ViewModels
{
    public class ProductsViewModel(DatabaseContext context, CartViewModel cartViewModel)
    {
        public async Task<IEnumerable<Product>> LoadProductsAsync()
        {
            return await context.Products.ToListAsync();
        }

        public void AddProduct(Product product)
        {
            cartViewModel.AddProduct(product);
        }
    }
}