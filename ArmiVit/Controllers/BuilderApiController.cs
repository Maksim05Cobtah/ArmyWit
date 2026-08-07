using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArmiVit.Models;
using Data;
using System.Threading.Tasks;
using System.Linq;

namespace ArmiVit.Controllers
{
    [Route("api/builder")]
    [ApiController]
    public class BuilderApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Впроваджуємо контекст бази даних
        public BuilderApiController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Оновлення конкретного елемента (PageElement)
        [HttpPost("update-element")]
        public async Task<IActionResult> UpdateElement([FromBody] ElementUpdateDto model)
        {
            if (model == null)
            {
                return BadRequest("Дані не передано.");
            }

            var element = await _context.PageElements.FindAsync(model.Id);
            if (element == null)
            {
                return NotFound($"Елемент з ID {model.Id} не знайдено.");
            }

            // Оновлюємо значення
            element.Content = model.Content;
            if (!string.IsNullOrEmpty(model.Type))
            {
                element.Type = model.Type;
            }

            // Зберігаємо зміни у базу
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Елемент успішно оновлено." });
        }

        // 2. Додавання нової секції (CustomSection)
        [HttpPost("add-section")]
        public async Task<IActionResult> AddSection([FromBody] SectionCreateDto model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Name))
            {
                return BadRequest("Назва секції не може бути порожньою.");
            }

            // Вираховуємо порядковий номер (Order) для розміщення в кінці
            int maxOrder = await _context.CustomSections
                .Where(s => !s.IsDeleted)
                .Select(s => (int?)s.Order)
                .MaxAsync() ?? 0;

            var newSection = new CustomSection
            {
                Name = model.Name,
                Order = maxOrder + 1,
                BackgroundColor = "#ffffff",
                TextColor = "#000000",
                IsDeleted = false
            };

            _context.CustomSections.Add(newSection);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, sectionId = newSection.Id });
        }

        // 3. М'яке видалення (Soft Delete) секції
        [HttpDelete("delete-section/{id}")]
        public async Task<IActionResult> DeleteSection(int id)
        {
            var section = await _context.CustomSections.FindAsync(id);
            if (section == null)
            {
                return NotFound($"Секцію з ID {id} не знайдено.");
            }

            // Позначаємо як видалену замість фізичного видалення
            section.IsDeleted = true;

            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Секцію позначено як видалену." });
        }
    }

    #region DTOs

    public class ElementUpdateDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
    }

    public class SectionCreateDto
    {
        public string Name { get; set; }
    }

    #endregion
}