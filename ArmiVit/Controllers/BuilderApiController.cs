using Microsoft.AspNetCore.Mvc;
using ArmiVit.Models;
using System.Linq;

namespace ArmiVit.Controllers
{
    [Route("api/builder")]
    [ApiController]
    public class BuilderApiController : ControllerBase
    {
        // Сюди підключи свій AppDbContext через конструктор, щоб зберігати в БД.

        [HttpPost("update-element")]
        public IActionResult UpdateElement([FromBody] ElementUpdateDto model)
        {
            // Тут логіка оновлення полів бази даних
            // Наприклад: context.TrainingPrograms.Find(model.Id);
            return Ok();
        }

        [HttpPost("add-section")]
        public IActionResult AddSection([FromBody] string name)
        {
            // Тут додавання нової програми в базу
            return Ok();
        }

        [HttpDelete("delete-section/{id}")]
        public IActionResult DeleteSection(int id)
        {
            // Тут soft-delete (IsDeleted = true)
            return Ok();
        }
    }

    public class ElementUpdateDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
    }
}