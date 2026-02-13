using ArmiVit.Models.ViewsModel;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Data;
using ProductApi.Models;

namespace Controllers
{
    
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }
    public IActionResult AddProducts()
    {
        var products2 = _context.Products.ToList();
        var categories = _context.Categories.ToList();
        var model = new ProductViewModel
        {
            Products = products2,
            Categories = categories
        };


        return View(model);
    }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
        {
          
            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Quantity = model.Quantity,
                CategoryId = model.CategoryId
            };  
        
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return View("AddProducts");
        }
    }
}
