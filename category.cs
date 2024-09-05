namespace AdvancedCosmeticManagementSystem.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Description: {Description}";
        }
    }
}