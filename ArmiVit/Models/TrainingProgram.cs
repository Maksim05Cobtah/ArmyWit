namespace ArmiVit.Models
{
    public class TrainingProgram
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<ServiceProgramItem> Items { get; set; } = new List<ServiceProgramItem>();
    }
}