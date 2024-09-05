using System.Collections.Generic;
using AdvancedCosmeticManagementSystem.Entities;
using AdvancedCosmeticManagementSystem.Repositories;

namespace AdvancedCosmeticManagementSystem.Services
{
    public class SupplierService
    {
        private readonly IRepository<Supplier> _supplierRepository;

        public SupplierService()
        {
            _supplierRepository = new InMemoryRepository<Supplier>();
        }

        public void AddSupplier(Supplier supplier)
        {
            _supplierRepository.Add(supplier);
        }

        public void UpdateSupplier(Supplier supplier)
        {
            _supplierRepository.Update(supplier);
        }

        public void DeleteSupplier(int id)
        {
            _supplierRepository.Delete(id);
        }

        public Supplier GetSupplierById(int id)
        {
            return _supplierRepository.GetById(id);
        }

        public List<Supplier> GetAllSuppliers()
        {
            return _supplierRepository.GetAll();
        }
    }
}