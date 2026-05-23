using Microsoft.AspNetCore.Mvc;
using ArmiVit.Models;

namespace ArmiVit.Controllers
{
    public class ProgramItemsController : Controller
    {
        public IActionResult CreateProgramItem(int programId)
        {
            ViewBag.ProgramId = programId;
            return View();
        }

        [HttpPost]
        public IActionResult CreateProgramItem(ServiceProgramItem model)
        {
            return View(model);
        }
    }
}
