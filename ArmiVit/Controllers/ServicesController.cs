using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using ArmiVit.Models;

namespace ArmiVit.Controllers
{
    [Route("[controller]")]
    public class ServicesController : Controller
    {
        private readonly AppDbContext _context;

        public ServicesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Services/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var services = await _context.Services.ToListAsync();
            return Json(services);
        }

        // POST: /Services/CreateService
        [HttpPost("CreateService")]
        public async Task<IActionResult> CreateService([FromBody] Service model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Некоректні дані" });

            _context.Services.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { success = true, id = model.Id });
        }

        // PUT: /Services/UpdateService/5
        [HttpPut("UpdateService/{id}")]
        public async Task<IActionResult> UpdateService(int id, [FromBody] Service model)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return NotFound(new { success = false, message = "Послугу не знайдено" });

            service.Name = model.Name;
            service.Description = model.Description;
            service.Price = model.Price;

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        // DELETE: /Services/DeleteService/5
        [HttpDelete("DeleteService/{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return NotFound(new { success = false, message = "Послугу не знайдено" });

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}