using ArmiVit.Models;
using ArmiVit.Models.ViewsModel;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ArmiVit.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(string? searchTerm, decimal? minPrice, decimal? maxPrice)
        {
            var productsQuery = _context.Products.Where(p => !p.IsDeleted).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));
            }

            if (minPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.Price <= maxPrice.Value);
            }

            var products = productsQuery.ToList();
            var categories = _context.Categories.Where(x => !x.IsDeleted).ToList();

            var model = new ProductViewModel
            {
                Products = products,
                Categories = categories,
                SearchTerm = searchTerm
            };

            return View(model);
        }

        public IActionResult Category(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id && !c.IsDeleted);

            if (category == null)
            {
                return NotFound();
            }

            var products = _context.Products.Where(p => p.CategoryId == id && !p.IsDeleted).ToList();

            var model = new ProductViewModel
            {
                Categories = _context.Categories.Where(x => !x.IsDeleted).ToList(),
                Products = products
            };

            ViewBag.CategoryName = category.Name;

            return View(model);
        }

        public IActionResult AboutMe()
        {
            var programs = _context.TrainingPrograms
                .Include(p => p.Items)
                .Where(p => !p.IsDeleted)
                .ToList();

            ViewBag.Certificates = _context.Certificates.ToList();
            ViewBag.AboutContents = _context.AboutContents
                .ToDictionary(x => x.Key, x => x.Content);

            return View(programs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

        // ==========================================
        // МЕТОДИ ДЛЯ СЕРТИФІКАТІВ ТА ЗБЕРЕЖЕННЯ КОНТЕНТУ
        // ==========================================

        [HttpPost]
        public async Task<IActionResult> UploadCertificate(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { success = false, message = "Файл не вибрано" });

            var uploadsFolder = Path.Combine(_env.WebRootPath, "images", "certificates");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var certificate = new Certificate
            {
                ImageUrl = "/images/certificates/" + fileName
            };

            _context.Certificates.Add(certificate);
            await _context.SaveChangesAsync();

            return Json(new { success = true, id = certificate.Id, imageUrl = certificate.ImageUrl });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCertificate(int id)
        {
            var cert = await _context.Certificates.FindAsync(id);
            if (cert == null)
                return NotFound(new { success = false });

            var filePath = Path.Combine(_env.WebRootPath, cert.ImageUrl.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            _context.Certificates.Remove(cert);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        // AJAX: Збереження тексту/контенту в базу даних
        [HttpPost]
        public async Task<IActionResult> SaveAboutContent(string key, string content)
        {
            if (string.IsNullOrEmpty(key))
                return BadRequest(new { success = false, message = "Ключ не вказано" });

            var item = await _context.AboutContents.FirstOrDefaultAsync(x => x.Key == key);

            if (item == null)
            {
                item = new AboutContent
                {
                    Key = key,
                    Content = content ?? string.Empty
                };
                _context.AboutContents.Add(item);
            }
            else
            {
                item.Content = content ?? string.Empty;
                _context.AboutContents.Update(item);
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }
    }
}