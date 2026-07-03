namespace ArmiVit.Models
{
    public class TrainingProgram
    {
        public int Id { get; set; }
        public string Name { get; set; }       
        public string Duration { get; set; }    
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; } = false; 

        public List<ServiceProgramItem> Items { get; set; } = new List<ServiceProgramItem>();
    }
}