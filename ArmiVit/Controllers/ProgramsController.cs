using Microsoft.AspNetCore.Mvc;
using ArmiVit.Models;

namespace ArmiVit.Controllers
{
    public class ProgramsController : Controller
    {
        public IActionResult CreateProgram()
        {
            return View();
        }
    }
}
