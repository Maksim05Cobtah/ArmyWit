using System.Text.Json.Serialization;

namespace ArmiVit.Models
{
    public class Service
    {
        public int Id { get; set; }

        public int AboutPageId { get; set; }

        public List<Result> Results { get; set; } = new();
        public List<GetIn> GetIns { get; set; } = new();

        public AboutPage AboutPage { get; set; }

        public string? Price { get; set; }

        public bool IsDeleted { get; set; }
    }
}