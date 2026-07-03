using ArmiVit.Models;
using Data;
using Microsoft.AspNetCore.Mvc;

namespace ArmiVit.Controllers
{
    public class ProgramItemsController : Controller
    {
        private readonly AppDbContext _context;

        public ProgramItemsController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult CreateProgramItem(int programId)
        {
            ViewBag.ProgramId = programId;
            return View();
        }


        [HttpPost]
        public IActionResult CreateProgramItem(ServiceProgramItem model)
        {
            _context.ServiceProgramItems.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Edit", "Programs", new { id = model.TrainingProgramId });
        }


        public IActionResult DeleteProgramItem(int id)
        {
            var item = _context.ServiceProgramItems.Find(id);
            int programId = 0;
            if (item != null)
            {
                programId = item.TrainingProgramId;
                _context.ServiceProgramItems.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction("Edit", "Programs", new { id = programId });
        }
    }
}