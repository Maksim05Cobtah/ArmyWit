using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ArmiVit.Models.ViewsModel
{
    public class AboutMeViewModel
    {
        public IFormFile MainImage { get; set; }
        public string ImagePath { get; set; }
        public int ImageId { get; set; }

        public string AboutMe { get; set; }
        public string Experience { get; set; }

        public Service Services { get; set; }
    }
}
