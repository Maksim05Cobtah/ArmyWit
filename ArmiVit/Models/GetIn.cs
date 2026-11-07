namespace ArmiVit.Models
{
    public class GetIn
    {
        public int Id { get; set; }
        public string Texts { get; set; }
        public Service Service { get; set; }
        public int ServiceId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
