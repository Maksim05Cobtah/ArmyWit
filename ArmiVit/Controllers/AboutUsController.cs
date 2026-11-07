using ArmiVit.Models;
using ArmiVit.Models.ViewsModel;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArmiVit.Controllers
{
    public class AboutUsController : Controller

    {

        private readonly AppDbContext _context;

        public AboutUsController(AppDbContext context)
        {
            _context = context;
        }

        private async Task<string?> SaveImageAsync(
            IFormFile? file,
            string? oldPath,
            string folder)
        {
            if (file == null || file.Length == 0)
                return oldPath;

            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "ABTUS",
                folder
            );

            Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid().ToString()
                + Path.GetExtension(file.FileName);

            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{folder}/{fileName}";
        }

        public async Task<IActionResult> Index()
        {
            if (_context.AboutPages.Any())
            {
                var aboutPage = await _context.AboutPages.OrderBy(x => x.Id).Include(a => a.Images).LastOrDefaultAsync();
           //     var services = _context.Services.Where(x => x.IsDeleted != true).ToList();

                return View(new AboutMeViewModel
                {
                    AboutMe = aboutPage.AboutMeText1,
                    Experience = aboutPage.ExperienceText1,
                    ImagePath = aboutPage.Images.ImgPath,
                   
                });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveInfo(AboutMeViewModel model)
        {

            var img = await SaveImageAsync(model.MainImage, null, "ABTUS");
            
            var conf = new AboutPage
            {
                AboutMeText1 = model.AboutMe,
                ExperienceText1 = model.Experience
                
            };
            _context.AboutPages.Add(conf);
          await  _context.SaveChangesAsync();

            var aboutPage = _context.AboutPages.OrderBy(x=>x.Id).LastOrDefaultAsync();

            conf.Images = new Images { ImgPath = img, AboutPageId = aboutPage.Id };

           await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
