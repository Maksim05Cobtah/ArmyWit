namespace ArmiVit.Models.ViewsModel
{
    public class CategoryViewModel
    {
        public string Name { get; set; }

        public List<Categories> Categories { get; set; } = new List<Categories>();
    }
}
