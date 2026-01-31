using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ArmiVit.Models;
using ArmiVit.Models.ViewModels;

namespace ArmiVit.Controllers;

public class ProductController : Controller
{
private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }
   
    public IActionResult Index()
    {
        return View();
    }
        public IActionResult AddProduct()
    {
        return View();
    }
     [HttpPost]
    public IActionResult AddProductToTable(ProductViewModel model)
    {
        var newProduct = new Product
        {
            ProductName = model.ProductName,
            ProductDescription = model.ProductDescription,
            Price = model.Price
        };
        _context.Products.Add(newProduct);
        _context.SaveChanges(); 

            return RedirectToAction("AddProduct");   }
   
   
}
