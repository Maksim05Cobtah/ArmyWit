using ArmiVit.Models.ViewsModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Data;
using Models;

namespace Controllers
{
    [Authorize(Roles = "Admin")]
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
            // Для адмінки завантажуємо ВСІ товари, щоб бачити кошик видалених
            var products2 = _context.Products
            .Where(x => !x.IsDeleted)
            .ToList();
            var categories = _context.Categories
            .Where(x => !x.IsDeleted)
            .ToList();

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
            if (model.Price == null || model.Name == null || model.CategoryId == null || model.ImageFile == null)
            {
            }
            else
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
                    Description = model.Description,
                    IsDeleted = false, // Новий товар створюється активним
                    IsPopular = model.IsPopular,

                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
            } 
            return RedirectToAction("AddProducts");
        }

        // М'яке видалення
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);

            if (product != null)
            {
                product.IsDeleted = true; // Замість видалення ставимо прапорець true
                _context.Products.Update(product);
                _context.SaveChanges();
            }

            return RedirectToAction("AddProducts");
        }

        // Новий функціонал: Відновлення видаленого товару
        [HttpPost]
        public IActionResult Restore(int id)
        {
            var product = _context.Products.Find(id);

            if (product != null)
            {
                product.IsDeleted = false; // Повертаємо товар назад
                _context.Products.Update(product);
                _context.SaveChanges();
            }

            return RedirectToAction("AddProducts");
        }

        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
                return NotFound();

            var model = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
                IsPopular = product.IsPopular,
                Description = product.Description,
                Categories = _context.Categories
                .Where(x => !x.IsDeleted)
                .ToList()
            };

            return View("EditProduct", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (model.Price == null || model.Name == null || model.CategoryId == null || model.ImageFile == null)
            {
            }
            else
            {
                var product = _context.Products.Find(model.Id);

                if (product == null)
                    return NotFound();

                product.Name = model.Name;
                product.Price = model.Price;
                product.Quantity = model.Quantity;
                product.CategoryId = model.CategoryId;
                product.Description = model.Description;

                if (model.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(fileStream);
                    }

                    product.ImagePath = uniqueFileName;
                }

                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Edit",model.Id);
        }
    }
}