using System.Collections.Generic;
using System.Linq;

namespace AdvancedCosmeticManagementSystem.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : class
    {
        protected List<T> _entities = new List<T>();
        protected int _nextId = 1;

        public void Add(T entity)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null)
            {
                idProperty.SetValue(entity, _nextId++);
            }
            _entities.Add(entity);
        }

        public void Update(T entity)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null)
            {
                int id = (int)idProperty.GetValue(entity);
                var existingEntity = _entities.FirstOrDefault(e => (int)idProperty.GetValue(e) == id);
                if (existingEntity != null)
                {
                    _entities[_entities.IndexOf(existingEntity)] = entity;
                }
            }
        }

        public void Delete(int id)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null)
            {
                var entityToRemove = _entities.FirstOrDefault(e => (int)idProperty.GetValue(e) == id);
                if (entityToRemove != null)
                {
                    _entities.Remove(entityToRemove);
                }
            }
        }

        public T GetById(int id)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null)
            {
                return _entities.FirstOrDefault(e => (int)idProperty.GetValue(e) == id);
            }
            return null;
        }

        public List<T> GetAll()
        {
            return _entities;
        }
    }
}