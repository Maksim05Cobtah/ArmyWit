namespace ArmiVit.Models
{
    public class Images
    {
        public int id { get; set; }
        public string ImgPath { get; set; }
        public int AboutPageId { get; set; }

        public AboutPage Page = new AboutPage();
    }
}
