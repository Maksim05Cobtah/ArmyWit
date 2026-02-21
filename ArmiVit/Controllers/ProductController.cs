using ArmiVit.Models.ViewsModel;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Data;
using ProductApi.Models;

namespace Controllers
{
    
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductsController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
            string? uniqueFileName = null;

            if (model.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(fileStream);
                }
            }

            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Quantity = model.Quantity,
                CategoryId = model.CategoryId,
                ImagePath = uniqueFileName,
                Description = model.Description
            };  
        
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("AddProducts");
        }
    }
}
