namespace ArmiVit.Models
{
    public class AboutPage
    {
        public int Id { get; set; }

        public string AboutMeText1 { get; set; }

        public string ExperienceText1 { get; set; }
        public List<Service> Services { get; set; } = new();

        public Images Images { get; set; }

    }
}