using Astro.Database;
using Astro.Domain.Entities;
using Astro.WebApi.Models;
using Astro.WebApi.Params;
using Microsoft.EntityFrameworkCore;

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


        // В методе CreateBook
        // 1. Попытаться получить из БД автора с наименованием BookToCreate.Author
        // 2. Если автор был найден, то newBook.Author = автору которого я вытащил из БД
        // 3. Если книга не была найдена, то вставить туда код с 32 по 35 строчку
        // Include тут не нужен
        // Самое чтобы в таблицах в БД автор не дублировался, а оставался один
        public bool CreateBook(CreateBookParams bookToCreate) // Создать класс CreateBookParams. Когда нужно прокинуть более трёх параметров, то лучше использовать отдельный класс для их хранения.
        {
            Book newBook = new Book();                        // создаётся новый объект newBook, экземпляр класса Book
            newBook.Title = bookToCreate.Title;               // значение свойства Title объекта newBook присваивается из параметра bookToCreate класса CreateBookParams.
            var author = bookDbContext.Set<Author>().FirstOrDefault(author => author.Name == bookToCreate.Author); // в переменную "author" записывается результат запроса к базе данных с помощью метода FirstOrDefault.
            // Мы ищем автора в БД с помощью FirstOrDefault, передавая в качестве аргумента лямбда-выражение, которое проверяет, равно ли свойство Name объекта Author
            // имени, указанному в bookToCreate. 
            if (author != null) // проверяется, найден ли автор в БД
            {
                newBook.Author = author; // если автор найден, присваиваем его объекту newBook.Author
            }
            else // если автор равен null, то это значит что автор не найден в БД
            {
                newBook.Author = new Author 
                {
                    Name = bookToCreate.Author // создаётся новый объект Author, которому присваивается имя, указанное в параметре bookToCreate.Author
                                               // этот объект (new Author) присваивается свойству Author объекта newBook
                };
            }
            // значения свойств Genre и Price объекта newBook также присваиваются из параметра bookToCreate
            newBook.Genre = bookToCreate.Genre;
            newBook.Price = bookToCreate.Price;

            bookDbContext.Set<Book>().Add(newBook); // добавляем объект newBook в контекст БД
            bookDbContext.SaveChanges();
            return true; 
        }

        // Использовать модели надо только в тех методах, которые что-то отдают.
        // Модели используем тогда, когда возвращаем больше одного поля.
        public BookModel GetBookInfo(int id) // в 99% случаев ищут по уникальному полю 'Id'
        {
            var book = bookDbContext.Set<Book>()
                .Include(book => book.Author) // внутри Include указываются ссылки на навиг. свойства
                .FirstOrDefault(book => book.Id == id);
            var bookModel = new BookModel(); // Создание экземпляра класса
            bookModel.Id = id;
            bookModel.Title = book.Title;
            bookModel.Author = book.Author.Name;
            bookModel.Genre = book.Genre;
            bookModel.Price = book.Price;

            return bookModel;
        }

        public BookShortModel GetBookShortInfo(int id)
        {
            var book = bookDbContext.Set<Book>().FirstOrDefault(book => book.Id == id); // сырая книга
            var bookShortInfoModel = new BookShortModel(); // Создание экземпляра класса

            bookShortInfoModel.Title = book.Title;
            bookShortInfoModel.Author = book.Author.Name;
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
                existingBook.Genre = bookToUpdate.Genre;
                existingBook.Price = bookToUpdate.Price;

                var existingAuthor = bookDbContext.Set<Author>().FirstOrDefault(author => author.Id == bookToUpdate.AuthorId);
                if (existingAuthor != null)
                {
                    existingBook.Author = existingAuthor;
                }
                
                bookDbContext.SaveChanges();
                return true;
            }
            else return false;
        }
        // Теперь метод принимает AuthorId вместо Author в параметрах, создаёт объект Author из базы данных на основе AuthorId, и назначает его свойству Author сущности Book.

        public bool DeleteBook(int id)
        {
            Book bookToDelete = bookDbContext.Set<Book>().FirstOrDefault(book => book.Id == id);

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
