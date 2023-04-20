using Astro.Database;
using Astro.Domain.Entities;
using Astro.WebApi.Models;
using Astro.WebApi.Params;

namespace Astro.WebApi.Services
{
    // Класс использует пространство имен Astro.Database, вероятно, для доступа к классам, которые предоставляют функциональность для работы с базой данных, в которой хранятся объекты типа Book.
    // В теле класса определяется конструктор, который принимает экземпляр класса LibraryDbContext.Этот класс вероятно является контекстом базы данных,
    // который позволяет выполнять операции с базой данных, такие как добавление, обновление и удаление данных.
    // В конструкторе сервиса BookService происходит инъекция зависимости объекта LibraryDbContext.
    // Это означает, что объект bookDbContext будет создан и передан в конструктор сервиса при создании экземпляра сервиса.
    // После создания экземпляра сервиса BookService можно использовать объект bookDbContext для выполнения операций с базой данных,
    // таких как получение, добавление, обновление и удаление объектов типа Book. Как правило, в методах сервиса определяются логика выполнения этих операций в зависимости от требований приложения.
    public class BookService
    {
        private LibraryDbContext bookDbContext; // Создаётся поле с контекстом БД

        public BookService(LibraryDbContext bookDbContext)
        {
            this.bookDbContext = bookDbContext;
        }

        public bool CreateBook(CreateBookParams bookToCreate) // Создать класс CreateBookParams. Когда нужно прокинуть более трёх параметров, то лучше использовать отдельный класс для их хранения.
        {
            Book newBook = new Book();
            newBook.Title = bookToCreate.Title;
            newBook.Author = new Author { Name = bookToCreate.Author}; // такой вариант инициализации мне нравится гораздо больше
            newBook.Genre = bookToCreate.Genre;
            newBook.Price = bookToCreate.Price;

            bookDbContext.Set<Book>().Add(newBook);
            bookDbContext.SaveChanges();
            return true;
        }
        // Использовать модели надо только в тех методах, которые что-то отдают.
        // Модели используем тогда, когда возвращаем больше одного поля.
        public BookModel GetBookInfo(int id) // в 99% случаев ищут по уникальному полю 'Id'
        {
            var book = bookDbContext.Set<Book>().FirstOrDefault(book => book.Id == id);
            var bookModel = new BookModel(); // Создание экземпляра класса
            bookModel.Id = id;
            bookModel.Title = book.Title;
            bookModel.Author = book.Author; // исправил эту ошибку, изменив тип свойства Author в моей модели BookModel на 'Author' (то есть тип моей сущности Author)
            bookModel.Genre = book.Genre;
            bookModel.Price = book.Price;

            return bookModel;
        }
        public BookShortModel GetBookShortInfo(int id)
        {
            var book = bookDbContext.Set<Book>().FirstOrDefault(book => book.Id == id); // сырая книга
            var bookShortInfoModel = new BookShortModel(); // Создание экземпляра класса

            bookShortInfoModel.Title = book.Title;
            bookShortInfoModel.Author = book.Author;
            return bookShortInfoModel;
        }

        public string GetBookTitle(int id)
        {
            var book = bookDbContext.Set<Book>().FirstOrDefault(book => book.Id == id);
            return book.Title; // Указал тип метода string, потому-что Title это string.
        }

        public bool UpdateBook(UpdateBookParams bookToUpdate)
        {
            var existingBook = bookDbContext.Set<Book>().FirstOrDefault(book => book.Id == bookToUpdate.Id);
            if (existingBook != null)
            {
                existingBook.Title = bookToUpdate.Title;
                existingBook.Author = bookToUpdate.Author;
                existingBook.Genre = bookToUpdate.Genre;
                existingBook.Price = bookToUpdate.Price;
                bookDbContext.SaveChanges();
                return true;
            }
            else return false;
        }
        
        public bool DeleteBook(int id)
        {
            Book bookToDelete = bookDbContext.Set<Book>().First(book => book.Id == id);

            if (bookToDelete != null)
            {
                bookDbContext.Set<Book>().Remove(bookToDelete);
                bookDbContext.SaveChanges();
                return true;

            }
            else return false;
        }

    }
}
