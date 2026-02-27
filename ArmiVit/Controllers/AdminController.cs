using ArmiVit.Models;
using ArmiVit.Models.ViewsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Data;

namespace ArmiVit.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }
        public ActionResult AboutAdmin()
        {
            var LastUpdate = _context.AboutMePage.ToList().Last();
            var editor = new AboutEditorViewModel
            {
                AboutMeText1 = LastUpdate.AboutMeText1
               ,
                AboutMeText2 = LastUpdate.AboutMeText2
            };
            return View(editor);
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TextUpdate(AboutEditorViewModel aboutEditorViewModel)
        {
            var saveEx=new AboutEditor { AboutMeText1 = aboutEditorViewModel.AboutMeText1 ,
            AboutMeText2 = aboutEditorViewModel.AboutMeText2 ,
            };
            _context.AboutMePage.Add(saveEx);
            _context.SaveChanges();

            return RedirectToAction("AboutAdmin", "Admin");
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
