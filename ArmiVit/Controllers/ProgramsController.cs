using ArmiVit.Models;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public IActionResult Index()
        {
            var programs = _context.TrainingPrograms
                .Include(p => p.Items)
                .ToList();
            return View(programs);
        }

      
        [HttpPost]
        public IActionResult Create(TrainingProgram program)
        {
            _context.TrainingPrograms.Add(program);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Edit(int id)
        {
            var program = _context.TrainingPrograms
                .Include(p => p.Items)
                .FirstOrDefault(p => p.Id == id);

            if (program == null) return NotFound();
            return View(program);
        }

        [HttpPost]
        public IActionResult Edit(TrainingProgram program)
        {
            var existing = _context.TrainingPrograms.Find(program.Id);
            if (existing != null)
            {
                existing.Name = program.Name;
                existing.Duration = program.Duration;
                existing.Price = program.Price;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var program = _context.TrainingPrograms.Find(id);
            if (program != null)
            {
                program.IsDeleted = true;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}