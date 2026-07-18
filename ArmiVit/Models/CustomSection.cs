using System.Collections.Generic;

namespace ArmiVit.Models
{
    public class CustomSection
    {
        public int Id { get; set; }
        public string Name { get; set; } // Назва для панелі керування (напр., "Головна", "Про мене", "Пакет Старт")
        public int Order { get; set; } // Порядок відображення на сторінці (для Drag & Drop)
        public string BackgroundColor { get; set; } = "#ffffff"; // Колір фону секції
        public string TextColor { get; set; } = "#000000"; // Колір тексту за замовчуванням
        public string PaddingTop { get; set; } = "50px"; // Відступи
        public string PaddingBottom { get; set; } = "50px";
        public bool IsDeleted { get; set; } = false;

        // Елементи всередині цієї секції
        public List<PageElement> Elements { get; set; } = new List<PageElement>();
    }
}