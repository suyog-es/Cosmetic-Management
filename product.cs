using System;

namespace AdvancedCosmeticManagementSystem.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Price: ${Price:F2}, Stock: {StockQuantity}, Expires: {ExpirationDate:yyyy-MM-dd}, Category ID: {CategoryId}, Supplier ID: {SupplierId}";
        }
    }
}