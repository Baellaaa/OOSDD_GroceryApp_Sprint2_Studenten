using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;
using System.Timers;

namespace Grocery.App.ViewModels
{
    public class ProductViewModel : BaseViewModel, IDisposable
    {
        private readonly IProductService _productService;
        private System.Timers.Timer _refreshTimer;

        public ObservableCollection<Product> Products { get; set; }

        public ProductViewModel(IProductService productService)
        {
            _productService = productService;
            Products = new(productService.GetAll());
            StartAutoRefresh();
        }

        /// <summary>
        /// Below a method for an auto refresh system
        /// This is to show the stock update in realtime,
        /// as it would have been outdated otherwise
        /// </summary>
        public void Refresh()
        {
            Products.Clear();
            foreach (var product in _productService.GetAll())
            {
                Products.Add(product);
            }
        }

        private void StartAutoRefresh()
        {
            _refreshTimer = new System.Timers.Timer(1000); // 1000ms = 1 second
            _refreshTimer.Elapsed += OnTimerElapsed;
            _refreshTimer.AutoReset = true;
            _refreshTimer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Refresh();
            });
        }

        public void Dispose()
        {
            _refreshTimer?.Stop();
            _refreshTimer?.Dispose();
        }
    }
}