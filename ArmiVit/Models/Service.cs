namespace ArmiVit.Models
{
    public class Service
    {
        public int Id { get; set; }
        public List<Result> Results { get; set; } = new List<Result>();
        public List<GetIn> GetIns { get; set; } = new List<GetIn>();
        public string Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}
