using System.Threading.Tasks;
using ArmiVit.Models;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArmiVit.Controllers
{
    public class ProgramItemsController : Controller
    {
        private readonly AppDbContext _context;

        public ProgramItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProgramItems/CreateProgramItem?programId=5
        public async Task<IActionResult> CreateProgramItem(int programId)
        {
            // Перевіряємо, чи існує така програма перед відображенням форми
            var programExists = await _context.TrainingPrograms.AnyAsync(p => p.Id == programId && !p.IsDeleted);
            if (!programExists)
            {
                return NotFound($"Програму з ID {programId} не знайдено.");
            }

            ViewBag.ProgramId = programId;

            // Передаємо модель із заповненим TrainingProgramId у View
            return View(new ServiceProgramItem { TrainingProgramId = programId });
        }

        // POST: ProgramItems/CreateProgramItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProgramItem([Bind("Text,Type,TrainingProgramId")] ServiceProgramItem model)
        {
            // Виключаємо навігаційну властивість з перевірки ModelState, 
            // щоб EF Core не просив обов'язкового заповнення об'єкта TrainingProgram
            ModelState.Remove(nameof(ServiceProgramItem.TrainingProgram));

            if (ModelState.IsValid)
            {
                _context.ServiceProgramItems.Add(model);
                await _context.SaveChangesAsync(); // Асинхронне збереження в БД

                return RedirectToAction("Edit", "Programs", new { id = model.TrainingProgramId });
            }

            ViewBag.ProgramId = model.TrainingProgramId;
            return View(model);
        }

        // POST: ProgramItems/DeleteProgramItem/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProgramItem(int id)
        {
            var item = await _context.ServiceProgramItems.FindAsync(id);
            int programId = 0;

            if (item != null)
            {
                programId = item.TrainingProgramId;
                _context.ServiceProgramItems.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Edit", "Programs", new { id = programId });
        }
    }
}