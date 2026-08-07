using System.Collections.Generic;

namespace ArmiVit.Models
{
    public class TrainingProgram
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // ДОДАЙТЕ ОЦЕЙ РЯДОК:
        public string Description { get; set; } = string.Empty;

        public string Duration { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; } = false;

        public int Order { get; set; } = 0;
        public string BackgroundColor { get; set; } = "#ffffff";
        public string TextColor { get; set; } = "#1C2E24";

        public List<ServiceProgramItem> Items { get; set; } = new List<ServiceProgramItem>();
    }
}