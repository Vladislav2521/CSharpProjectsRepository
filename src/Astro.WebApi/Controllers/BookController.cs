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
        public void CreateBook(CreateBookParams bookToCreate)
        {
            bookService.CreateBook(bookToCreate);
        }
         
        // В методах GET в контроллере обычно происходит возврат переменной в метод с передачей модели
        [HttpGet("GetBookInfo")]
        public BookModel GetBookInfo(int id)
        {
            var book = bookService.GetBookInfo(id);
            return book;
        }

        [HttpGet("GetBookShortInfo")]
        public BookShortModel GetBookShortInfo(int id)
        {
           var book = bookService.GetBookShortInfo(id);
           return book;
        }
        // IActionResult не используется в методах GET.

        [HttpGet("GetBooksByAuthor")]
        public List<BookModel> GetBooksByAuthor(Author author)
        {
            var books = bookService.GetBooksByAuthor(author);
            return books;
        }

        [HttpGet("GetBooksMoreThanReviewCount")]   // дописать контроллер-метод
        public List<BookModel> GetBooksMoreThanReviewCount(int reviewCount)
        {
            var books = bookService.GetBooksMoreThanReviewCount(reviewCount);
            return books;
        }

        [HttpGet("GetBooksWithFirstReview")]
        public List<BooksWithReviewsModel> GetBooksWithFirstReview()
        {
            var books = bookService.GetBooksWithFirstReview();
            return books;
        }

        [HttpGet("GetBooksInfo")]
        public BooksInfoModel GetBooksInfo()
        {
            var books = bookService.GetBooksInfo();
            return books;
        }

        [HttpGet("GetBookTitle")]
        public string GetBookTitle(int id)
        {
            var title = bookService.GetBookTitle(id);
            return title;
        }

        [HttpGet("GetAllBooksInfo")]
        public List<BooksAllModel> GetAllBooksInfo()
        {
            var books = bookService.GetAllBooksInfo();
            return books;
        }

        // Никаких статусных ответов в методах UpdateBook и DeleteBook возвращать не нужно.
        [HttpPut("UpdateBook")]
        public void UpdateBook(UpdateBookParams bookToUpdate)
        {
            bookService.UpdateBook(bookToUpdate);
        }
        
        [HttpDelete("DeleteBook")]
        public void DeleteBook(int id)
        {
            bookService.DeleteBook(id);
        }
    }
}
