using System.Linq;
using System.Threading.Tasks;
using ArmiVit.Models;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArmiVit.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProgramsController : Controller
    {
        private readonly AppDbContext _context;

        public ProgramsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Programs
        public async Task<IActionResult> Index()
        {
            // Беремо лише активні (не видалені) програми
            var programs = await _context.TrainingPrograms
                .Where(p => !p.IsDeleted)
                .Include(p => p.Items)
                .OrderBy(p => p.Order)
                .ToListAsync();

            return View(programs);
        }

        // GET: Programs/Create (Відображення форми створення)
        public IActionResult Create()
        {
            return View(new TrainingProgram());
        }

        // POST: Programs/Create (Збереження нової програми в БД)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainingProgram program)
        {
            if (ModelState.IsValid)
            {
                // Вираховуємо порядок, щоб нова програма ставала в кінець списку
                int maxOrder = await _context.TrainingPrograms
                    .Where(p => !p.IsDeleted)
                    .Select(p => (int?)p.Order)
                    .MaxAsync() ?? 0;

                program.Order = maxOrder + 1;
                program.IsDeleted = false;

                _context.TrainingPrograms.Add(program);
                await _context.SaveChangesAsync(); // <-- Збереження в БД

                return RedirectToAction(nameof(Index));
            }

            return View(program);
        }

        // GET: Programs/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var program = await _context.TrainingPrograms
                .Include(p => p.Items)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (program == null)
            {
                return NotFound($"Програму з ID {id} не знайдено.");
            }

            return View(program);
        }

        // POST: Programs/Edit/5 (Оновлення програми в БД)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TrainingProgram program)
        {
            if (ModelState.IsValid)
            {
                var existing = await _context.TrainingPrograms.FindAsync(program.Id);

                if (existing == null || existing.IsDeleted)
                {
                    return NotFound();
                }

                // Зберігаємо абсолютно всі поля з моделі TrainingProgram
                existing.Name = program.Name;
                existing.Duration = program.Duration;
                existing.Price = program.Price;
                existing.BackgroundColor = program.BackgroundColor;
                existing.TextColor = program.TextColor;
                existing.Order = program.Order;

                await _context.SaveChangesAsync(); // <-- Збереження змін у БД

                return RedirectToAction(nameof(Index));
            }

            // Якщо форма невалідна, підвантажуємо назад пункти для відображення у View
            program.Items = await _context.ServiceProgramItems
                .Where(i => i.TrainingProgramId == program.Id)
                .ToListAsync();

            return View(program);
        }

        // POST: Programs/Delete/5 (Soft-delete прапорець в БД)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var program = await _context.TrainingPrograms.FindAsync(id);

            if (program != null)
            {
                program.IsDeleted = true; // М'яке видалення
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}