using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using SamplePOS_ServerSide.Core;
using SamplePOS_ServerSide.Models;
using SamplePOS_ServerSide.Services;

namespace SamplePOS_ServerSide.ViewModels
{

    public class CartViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<Product> ProductsProperties => new(cart.Products);

        public IEnumerable<Product> Products => cart.Products;
        public decimal Total => cart.Total;
        public ICommand CompletePurchaseCommand { get; }


        private readonly BillingService billingService;
        private readonly Cart cart;


        public CartViewModel(Cart cart, BillingService billingService)
        {
            this.cart = cart;
            this.billingService = billingService;
            CompletePurchaseCommand = new RelayCommand(i => CompletePurchase());
        }

        public void AddProduct(Product product)
        {
            cart.Products.Add(product);
            OnPropertyChanged(nameof(Products), nameof(Total));
        }

        public void RemoveProduct(Product product)
        {
            cart.Products.Remove(product);
            OnPropertyChanged(nameof(Products), nameof(Total));
        }

        public void ClearCart()
        {
            cart.Products.Clear();
            OnPropertyChanged(nameof(Products), nameof(Total));
        }

        public void Bill()
        {
            // Lógica de facturación
            Invoice invoice = billingService.GenerateInvoice(cart.Products);

            MessageBus.PublishGeneratedInvoice(new InvoiceGeneratedMessage(invoice));
        }

        private void CompletePurchase()
        {
            Invoice invoice = billingService.GenerateInvoice(cart.Products.ToList());
            MessageBus.PublishGeneratedInvoice(new InvoiceGeneratedMessage(invoice));
        }

        protected void OnPropertyChanged(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}