using ArmiVit.Models;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Data;
using System.Diagnostics;

namespace ArmiVit.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
  public IActionResult AboutMe()
    {
       var List= _context.AboutMePage.ToList().Last();
        
        return View(List);
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
