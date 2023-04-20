using Astro.Database;
using Astro.Domain.Entities;
using Astro.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Astro.WebApi.Services;
using Astro.WebApi.Params;
using Astro.WebApi.Models;

// В теле класса определяется конструктор, который принимает в качестве параметра экземпляр класса BookService.
// Класс BookService вероятно является сервисом, который предоставляет функциональность для работы с объектами типа Book в приложении.
// В конструкторе контроллера BookController происходит инъекция зависимости объекта BookService.
// Это означает, что объект bookService будет создан и передан в конструктор контроллера при создании экземпляра контроллера.
// Далее в контроллере BookController можно будет использовать объект bookService для вызова методов сервиса и обработки запросов, связанных с сущностями типа Book.

namespace Astro.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private BookService bookService;

        public BookController(BookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpPost("CreateBook")]
        public IActionResult CreateBook(CreateBookParams bookToCreate)
        {
            bool bookCreated = bookService.CreateBook(bookToCreate);
            if (bookCreated == true)
            {
                return Ok("Книга успешно создана");
            }
            else return Content("Не удалось создать книгу");
        }

        [HttpGet("GetBook")]
        public BookShortModel GetBookShortInfo(int id)
        {
           var book = bookService.GetBookShortInfo(id);
           return book;
          
            
        }
        // IActionResult не используется в методах GET. 
        [HttpPut("Update")]
        public IActionResult UpdateBook(UpdateBookParams bookToUpdate)
        {
            bool bookUpdated = bookService.UpdateBook(bookToUpdate);
            if (bookUpdated == true)
            {
                return Ok("Книга успешно отредактирована");
            }
            else return Content("Не удалось отредактировать книгу");
        }

        [HttpDelete("DeleteBook")]
        public IActionResult DeleteBook(int id)
        {
            bool bookDeleted = bookService.DeleteBook(id);
            // Проверяем была ли удалена книга из БД

            if (bookDeleted == true)
            {
                return Ok("Книга успешно удалена");
            }
            else return Content("Невозможно удалить несуществующую книгу");
        }
    }
}
