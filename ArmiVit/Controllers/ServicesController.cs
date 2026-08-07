using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArmiVit.Models;
using Data;

namespace ArmiVit.Controllers
{
    public class ServicesController : Controller
    {
        private readonly AppDbContext _context;

        public ServicesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            var services = await _context.Services.ToListAsync();
            return View(services);
        }

        // GET: Services/CreateService
        public IActionResult CreateService()
        {
            return View(new Service());
        }

        // POST: Services/CreateService
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateService(Service model)
        {
            if (ModelState.IsValid)
            {
                _context.Services.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Services/EditService?name=НазваПослуги
        public async Task<IActionResult> EditService(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Назву послуги не вказано.");
            }

            var service = await _context.Services.FirstOrDefaultAsync(s => s.Name == name);
            if (service == null)
            {
                return NotFound($"Послугу з назвою \"{name}\" не знайдено.");
            }

            return View(service);
        }

        // POST: Services/EditService
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditService(Service model)
        {
            if (ModelState.IsValid)
            {
                var existingService = await _context.Services.FirstOrDefaultAsync(s => s.Name == model.Name);
                if (existingService == null)
                {
                    return NotFound();
                }

                existingService.Description = model.Description;
                existingService.Time = model.Time;
                existingService.Price = model.Price;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // POST: Services/DeleteService
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteService(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var service = await _context.Services.FirstOrDefaultAsync(s => s.Name == name);
                if (service != null)
                {
                    _context.Services.Remove(service);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}