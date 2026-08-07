namespace ArmiVit.Models
{
    public class Service
    {
        public int Id { get; set; } // <--- Додати це поле
        public string Name { get; set; }
        public string Description { get; set; }
        public int Time { get; set; }
        public decimal Price { get; set; }
    }
}