namespace ArmiVit.Models
{
    public class ServiceProgramItem
    {
        public int Id { get; set; }
        public string Text { get; set; } 

        public string Type { get; set; }

        public int TrainingProgramId { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
    }
}