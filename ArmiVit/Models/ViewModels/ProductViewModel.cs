using System.ComponentModel.DataAnnotations.Schema;

namespace ArmiVit.Models.ViewModels;

public class ProductViewModel
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }

 [Column(TypeName = "decimal(18,2)")]   
    public decimal Price { get; set; }
  public  List<Product> products { get; set; }

}
