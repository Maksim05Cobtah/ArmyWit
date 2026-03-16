using ArmiVit.Models;
using ArmiVit.Models.ViewsModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using System.Diagnostics;

namespace ArmiVit.Controllers;

public class HomeController : Controller
{

    private readonly AppDbContext _context;
      public HomeController(AppDbContext context)
        {
            _context = context;
        }


    public IActionResult Index()
    {
        
    var products = _context.Products.ToList();
        var Categories = _context.Categories.ToList();
        var Model = new ProductViewModel { Categories = Categories, Products = products };
        return View(Model);

    }
    public IActionResult AboutMe()
    {
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
