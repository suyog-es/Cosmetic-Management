using System;
using System.Linq;
using AdvancedCosmeticManagementSystem.Entities;

namespace AdvancedCosmeticManagementSystem.Services
{
    public class ReportingService
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly SupplierService _supplierService;

        public ReportingService(ProductService productService, CategoryService categoryService, SupplierService supplierService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
        }

        public void GenerateInventoryReport()
        {
            var products = _productService.GetAllProducts();
            var totalValue = products.Sum(p => p.Price * p.StockQuantity);

            Console.WriteLine("Inventory Report");
            Console.WriteLine($"Total Products: {products.Count}");
            Console.WriteLine($"Total Inventory Value: ${totalValue:F2}");
            Console.WriteLine("Top 5 Products by Value:");

            var topProducts = products.OrderByDescending(p => p.Price * p.StockQuantity).Take(5);
            foreach (var product in topProducts)
            {
                Console.WriteLine($"{product.Name}: ${product.Price * product.StockQuantity:F2}");
            }
        }

        public void GenerateCategoryReport()
        {
            var categories = _categoryService.GetAllCategories();
            var products = _productService.GetAllProducts();

            Console.WriteLine("Category Report");
            foreach (var category in categories)
            {
                var categoryProducts = products.Where(p => p.CategoryId == category.Id).ToList();
                var categoryValue = categoryProducts.Sum(p => p.Price * p.StockQuantity);

                Console.WriteLine($"Category: {category.Name}");
                Console.WriteLine($"Number of Products: {categoryProducts.Count}");
                Console.WriteLine($"Total Value: ${categoryValue:F2}");
                Console.WriteLine();
            }
        }

        public void GenerateSupplierReport()
        {
            var suppliers = _supplierService.GetAllSuppliers();
            var products = _productService.GetAllProducts();

            Console.WriteLine("Supplier Report");
            foreach (var supplier in suppliers)
            {
                var supplierProducts = products.Where(p => p.SupplierId == supplier.Id).ToList();
                var supplierValue = supplierProducts.Sum(p => p.Price * p.StockQuantity);

                Console.WriteLine($"Supplier: {supplier.Name}");
                Console.WriteLine($"Number of Products: {supplierProducts.Count}");
                Console.WriteLine($"Total Value: ${supplierValue:F2}");
                Console.WriteLine();
            }
        }
    }
}