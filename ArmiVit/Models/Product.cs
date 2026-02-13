using ArmiVit.Models;

namespace ProductApi.Models
{
    public class Product
    {
        public int Id { get; set; }        // PK
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }   
        public int Quantity { get; set; }

        
        public Categories Category { get; set; } 
    }
}


