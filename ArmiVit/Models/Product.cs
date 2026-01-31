using System.ComponentModel.DataAnnotations.Schema;

namespace ArmiVit.Models;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
 [Column(TypeName = "decimal(18,2)")]   
    public decimal Price { get; set; }
}
