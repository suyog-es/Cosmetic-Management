using System;
using System.Linq;
using AdvancedCosmeticManagementSystem.Entities;

namespace AdvancedCosmeticManagementSystem.Services
{
    public class InventoryService
    {
        private readonly ProductService _productService;

        public InventoryService(ProductService productService)
        {
            _productService = productService;
        }

        public void UpdateStock(int productId, int quantity)
        {
            var product = _productService.GetProductById(productId);
            if (product != null)
            {
                product.StockQuantity += quantity;
                _productService.UpdateProduct(product);
            }
        }

        public void CheckLowStock(int threshold)
        {
            var lowStockProducts = _productService.GetAllProducts()
                .Where(p => p.StockQuantity < threshold)
                .ToList();

            Console.WriteLine($"Products with stock below {threshold}:");
            foreach (var product in lowStockProducts)
            {
                Console.WriteLine($"{product.Name}: {product.StockQuantity}");
            }
        }

        public void CheckExpiringProducts(int daysThreshold)
        {
            var expiringProducts = _productService.GetAllProducts()
                .Where(p => (p.ExpirationDate - DateTime.Now).TotalDays < daysThreshold)
                .ToList();

            Console.WriteLine($"Products expiring within {daysThreshold} days:");
            foreach (var product in expiringProducts)
            {
                Console.WriteLine($"{product.Name}: {product.ExpirationDate:yyyy-MM-dd}");
            }
        }
    }
}