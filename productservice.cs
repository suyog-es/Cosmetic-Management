using System.Collections.Generic;
using AdvancedCosmeticManagementSystem.Entities;
using AdvancedCosmeticManagementSystem.Repositories;

namespace AdvancedCosmeticManagementSystem.Services
{
    public class ProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService()
        {
            _productRepository = new InMemoryRepository<Product>();
        }

        public void AddProduct(Product product)
        {
            _productRepository.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.Update(product);
        }

        public void DeleteProduct(int id)
        {
            _productRepository.Delete(id);
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetById(id);
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAll();
        }
    }
}