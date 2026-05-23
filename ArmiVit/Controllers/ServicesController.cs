using Microsoft.AspNetCore.Mvc;
using ArmiVit.Models;

namespace ArmiVit.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult CreateService()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateService(Service model)
        {
            return View(model);
        }
    }
}
