using ArmiVit.Models;
using Models;

namespace ArmiVit.Models.ViewsModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public IFormFile? ImageFile { get; set; }
        public bool IsPopular { get; set; }

        public string? SearchTerm { get; set; }

        public List<Product> Products { get; set; }
        public List<Categories> Categories { get; set; }
    }
}