using ArmiVit.Models;
using ArmiVit.Models.ViewsModel;
using Microsoft.AspNetCore.Mvc;
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
        var categories = _context.Categories.ToList();

        var model = new ProductViewModel
        {
            Categories = categories,
            Products = products
        };

        return View(model);
    }

    public IActionResult Category(int id)
    {
        var category = _context.Categories.FirstOrDefault(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        var products = _context.Products
            .Where(p => p.CategoryId == id)
            .ToList();

        var model = new ProductViewModel
        {
            Categories = _context.Categories.ToList(),
            Products = products
        };

        ViewBag.CategoryName = category.Name;

        return View(model);
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
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}