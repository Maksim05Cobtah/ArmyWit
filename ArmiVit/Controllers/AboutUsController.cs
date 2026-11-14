using ArmiVit.Models;
using ArmiVit.Models.ViewsModel;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

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
                var aboutPage = await _context.AboutPages.Include(a => a.Images).Include(a => a.Services).ThenInclude(r=>r.Results).Include(a => a.Services).ThenInclude(r=>r.GetIns).OrderBy(x => x.Id).LastOrDefaultAsync();
                //     var services = _context.Services.Where(x => x.IsDeleted != true).ToList();
                await _context.SaveChangesAsync();
                return View(new AboutMeViewModel
                {
                    AboutMe = aboutPage.AboutMeText1,
                    Experience = aboutPage.ExperienceText1,
                    ImagePath = aboutPage.Images.ImgPath,
                    Services = aboutPage.Services
            });

        }
            var conf = new AboutPage
            {
                AboutMeText1 = String.Empty,
                ExperienceText1 = String.Empty,
                Services = new List<Service>(),
            };
            _context.AboutPages.Add(conf);
            await _context.SaveChangesAsync();
            return View();
        }

        [HttpPost]
        public async Task<int> GetLastSaveId()
        {
            var lastsave = await _context.AboutPages.OrderBy(x => x.Id).LastAsync();
            return lastsave.Id;
        }


        [HttpPost]
        public async Task<IActionResult> SaveServices(List<Service> services)
        {
            foreach (var service in services)
            {
                service.AboutPageId = await GetLastSaveId();
                if (service.Id == 0)
                {
                    _context.Services.Add(service);
                }
                else
                {
                    _context.Services.Update(service);
                }
            }

            

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SaveInfo(AboutMeViewModel model)
        {

            var img = await SaveImageAsync(model.MainImage, null, "ABTUS");

            await SaveServices(model.Services);
            
            var conf = new AboutPage
            {
                AboutMeText1 = model.AboutMe,
                ExperienceText1 = model.Experience,
                Services = model.Services,
            };
            _context.AboutPages.Add(conf);
       

            var aboutPage = _context.AboutPages.OrderBy(x=>x.Id).LastOrDefaultAsync();

            conf.Images = new Images { ImgPath = img, AboutPageId = aboutPage.Id };

 

            return RedirectToAction("Index");
        }
    }
}
