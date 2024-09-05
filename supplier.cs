namespace AdvancedCosmeticManagementSystem.Entities
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Contact: {ContactPerson}, Email: {Email}, Phone: {Phone}";
        }
    }
}