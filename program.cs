using System;
using AdvancedCosmeticManagementSystem.Services;

namespace AdvancedCosmeticManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var productService = new ProductService();
            var categoryService = new CategoryService();
            var supplierService = new SupplierService();
            var inventoryService = new InventoryService(productService);
            var reportingService = new ReportingService(productService, categoryService, supplierService);

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Advanced Cosmetic Management System");
                Console.WriteLine("1. Manage Products");
                Console.WriteLine("2. Manage Categories");
                Console.WriteLine("3. Manage Suppliers");
                Console.WriteLine("4. Inventory Management");
                Console.WriteLine("5. Generate Reports");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ManageProducts(productService, categoryService, supplierService);
                        break;
                    case "2":
                        ManageCategories(categoryService);
                        break;
                    case "3":
                        ManageSuppliers(supplierService);
                        break;
                    case "4":
                        ManageInventory(inventoryService);
                        break;
                    case "5":
                        GenerateReports(reportingService);
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to continue.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ManageProducts(ProductService productService, CategoryService categoryService, SupplierService supplierService)
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("Product Management");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. View All Products");
                Console.WriteLine("3. Update Product");
                Console.WriteLine("4. Delete Product");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProduct(productService, categoryService, supplierService);
                        break;
                    case "2":
                        ViewAllProducts(productService);
                        break;
                    case "3":
                        UpdateProduct(productService, categoryService, supplierService);
                        break;
                    case "4":
                        DeleteProduct(productService);
                        break;
                    case "5":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to continue.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void AddProduct(ProductService productService, CategoryService categoryService, SupplierService supplierService)
        {
            Console.Clear();
            Console.WriteLine("Add New Product");

            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid price. Product not added.");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter Stock Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int stockQuantity))
            {
                Console.WriteLine("Invalid stock quantity. Product not added.");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter Expiration Date (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime expirationDate))
            {
                Console.WriteLine("Invalid date format. Product not added.");
                Console.ReadKey();
                return;
            }

            var categories = categoryService.GetAllCategories();
            Console.WriteLine("Available Categories:");
            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Id}: {category.Name}");
            }
            Console.Write("Enter Category ID: ");
            if (!int.TryParse(Console.ReadLine(), out int categoryId))
            {
                Console.WriteLine("Invalid Category ID. Product not added.");
                Console.ReadKey();
                return;
            }

            var suppliers = supplierService.GetAllSuppliers();
            Console.WriteLine("Available Suppliers:");
            foreach (var supplier in suppliers)
            {
                Console.WriteLine($"{supplier.Id}: {supplier.Name}");
            }
            Console.Write("Enter Supplier ID: ");
            if (!int.TryParse(Console.ReadLine(), out int supplierId))
            {
                Console.WriteLine("Invalid Supplier ID. Product not added.");
                Console.ReadKey();
                return;
            }

            var newProduct = new Product
            {
                Name = name,
                Price = price,
                StockQuantity = stockQuantity,
                ExpirationDate = expirationDate,
                CategoryId = categoryId,
                SupplierId = supplierId
            };

            productService.AddProduct(newProduct);

            Console.WriteLine("Product added successfully. Press any key to continue.");
            Console.ReadKey();
        }

        static void ViewAllProducts(ProductService productService)
        {
            Console.Clear();
            Console.WriteLine("All Products");

            var products = productService.GetAllProducts();
            if (products.Count == 0)
            {
                Console.WriteLine("No products found.");
            }
            else
            {
                foreach (var product in products)
                {
                    Console.WriteLine(product);
                }
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        static void UpdateProduct(ProductService productService, CategoryService categoryService, SupplierService supplierService)
        {
            Console.Clear();
            Console.WriteLine("Update Product");

            Console.Write("Enter Product ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("Invalid Product ID.");
                Console.ReadKey();
                return;
            }

            var product = productService.GetProductById(productId);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Updating product: {product}");

            Console.Write("Enter new Name (press Enter to skip): ");
            string nameInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nameInput))
            {
                product.Name = nameInput;
            }

            Console.Write("Enter new Price (press Enter to skip): ");
            string priceInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out decimal newPrice))
            {
                product.Price = newPrice;
            }

            Console.Write("Enter new Stock Quantity (press Enter to skip): ");
            string quantityInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(quantityInput) && int.TryParse(quantityInput, out int newQuantity))
            {
                product.StockQuantity = newQuantity;
            }

            Console.Write("Enter new Expiration Date (yyyy-MM-dd) (press Enter to skip): ");
            string dateInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(dateInput) && DateTime.TryParse(dateInput, out DateTime newDate))
            {
                product.ExpirationDate = newDate;
            }

            var categories = categoryService.GetAllCategories();
            Console.WriteLine("Available Categories:");
            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Id}: {category.Name}");
            }
            Console.Write("Enter new Category ID (press Enter to skip): ");
            string categoryInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(categoryInput) && int.TryParse(categoryInput, out int newCategoryId))
            {
                product.CategoryId = newCategoryId;
            }

            var suppliers = supplierService.GetAllSuppliers();
            Console.WriteLine("Available Suppliers:");
            foreach (var supplier in suppliers)
            {
                Console.WriteLine($"{supplier.Id}: {supplier.Name}");
            }
            Console.Write("Enter new Supplier ID (press Enter to skip): ");
            string supplierInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(supplierInput) && int.TryParse(supplierInput, out int newSupplierId))
            {
                product.SupplierId = newSupplierId;
            }

            productService.UpdateProduct(product);

            Console.WriteLine("Product updated successfully. Press any key to continue.");
            Console.ReadKey();
        }

        static void DeleteProduct(ProductService productService)
        {
            Console.Clear();
            Console.WriteLine("Delete Product");

            Console.Write("Enter Product ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("Invalid Product ID.");
                Console.ReadKey();
                return;
            }

            var product = productService.GetProductById(productId);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Are you sure you want to delete this product: {product}? (Y/N)");
            string confirmation = Console.ReadLine().Trim().ToUpper();

            if (confirmation == "Y")
            {
                productService.DeleteProduct(productId);
                Console.WriteLine("Product deleted successfully.");
            }
            else
            {
                Console.WriteLine("Deletion cancelled.");
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        static void ManageCategories(CategoryService categoryService)
        {
            // Implement category management logic here
        }

        static void ManageSuppliers(SupplierService supplierService)
        {
            // Implement supplier management logic here
        }

        static void ManageInventory(InventoryService inventoryService)
        {
            // Implement inventory management logic here
        }

        static void GenerateReports(ReportingService reportingService)
        {
            // Implement reporting logic here
        }
    }
}