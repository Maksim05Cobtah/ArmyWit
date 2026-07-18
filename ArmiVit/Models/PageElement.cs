namespace ArmiVit.Models
{
    public class PageElement
    {
        public int Id { get; set; }
        public int CustomSectionId { get; set; }
        public CustomSection CustomSection { get; set; }

        public string Type { get; set; } // "heading", "paragraph", "image", "button", "list-item-bullet", "list-item-check"
        public string Content { get; set; } // Текст або шлях до картинки
        public int Order { get; set; } // Порядок елемента

        public string FontSize { get; set; } = "16px";
        public string FontWeight { get; set; } = "normal";
        public string Color { get; set; }
        public string Alignment { get; set; } = "left";
        public string CustomStyles { get; set; }
    }
}