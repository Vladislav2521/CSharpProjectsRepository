using Astro.Database;
using Astro.Domain.Entities;
using Astro.WebApi.Models;
using Astro.WebApi.Params;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        // Не ко всем методам Book написаны методы в BookController

        // В методе CreateBook
        // 1. Попытаться получить из БД автора с наименованием BookToCreate.Author
        // 2. Если автор был найден, то newBook.Author = автору которого я вытащил из БД
        // 3. Если книга не была найдена, то вставить туда код с 32 по 35 строчку
        // Include тут не нужен
        // Самое чтобы в таблицах в БД автор не дублировался, а оставался один

        // В этом методе можно, конечно, вернуть 'void', но 'bool' даёт некую гибкость, например, если возникнут ошибки при сохранении в базу данных, можно вернуть false для указания неудачи операции.
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
            var book = bookDbContext.Set<Book>()
                .Include(book => book.Author)
                .FirstOrDefault(book => book.Id == id); // сырая книга
            var bookShortInfoModel = new BookShortModel(); // Создание экземпляра класса

            bookShortInfoModel.Title = book.Title;
            bookShortInfoModel.Author = book.Author.Name;
            return bookShortInfoModel;
        }

        public List<BookModel> GetBooksByAuthor(Author author)
        {
            var books = bookDbContext.Set<Book>()
                .Where(w => w.Author.Name == author.Name) // отфильтровывает книги по автору  // в лямбда выражениях лучше называть параметр по первой букве метода
                .OrderByDescending(o => o.Author.Name) // упорядочивает в нисходящей секвенции
                .ToList();
            var bookList = books.Select(s => new BookModel
            {
                Id = s.Id,
                Title = s.Title,
                Author = s.Author.Name,
                Genre = s.Genre,
                Price = s.Price
            })
                .ToList();
            return bookList;
        }

        // не написан контроллер
        public List<BookModel> GetBooksMoreThanReviewCount(int reviewCount) // возвращает все книги количество отзывов на которые больше чем указал пользователь
        {
            var books = bookDbContext.Set<Book>()
                .Where(w => w.Reviews.Count > reviewCount)
                .Select(s => new BookModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Author = s.Author.Name,
                    Genre = s.Genre,
                    Price = s.Price
                })
                .ToList();
            return books;
        }

        public List<BooksWithReviewsModel> GetBooksWithFirstReview() // возвращаю книги с текстами первых отзывов
        {
            var booksWithReviews = bookDbContext.Set<Book>() // Set<Book> возвращаю набор объектов из базы данных
                .Include(i => i.Reviews) // этот метод помогает включить связанные объекты Reviews для каждого объекта Book. Это позволяет получить доступ к связанным отзывам книг.
                .Where(w => w.Reviews.Any()) // метод Where фильтрует только те объекты Book, у которых есть хотябы один связанный отзыв. А метод Any() определяет содержит ли коллекция Reviews хотя бы один элемент (да = true, нет = false)
                .Select(s => new BooksWithReviewsModel // преобразует каждый объект Book в новый объект BookWithReviewsModel, выбирая только необходимые поля для возврата (Id, Title, FirstReviewText)
                {
                    Id = s.Id,
                    Title = s.Title,
                    FirstReviewText = s.Reviews.First().Text // присваиваю текст первого отзыва книги с помощью метода First(). Этот метод выбирает первый отзыв из коллекции связанных отзывов для каждой книги
                })
                .ToList(); // преобразую результат запроса в список объектов BooksWithReviewsModel.
            return booksWithReviews; // возвращаю это все дело
        }

        public BooksInfoModel GetBooksInfo() 
        {
            var booksQuery = bookDbContext.Set<Book>(); // создаю первоначальный запрос БД и сохраняю его в переменную booksQuery, которую использую ниже чтобы каждый раз не запрашивать
            // Шаг 1: Сохраняю все данные в переменные
            var booksCount = booksQuery.Count(); // подсчитываю количество книг
            var booksWithTwoOrMoreReviewsCount = booksQuery.Count(c => c.Reviews.Count >= 2); // подсчитываю книги с 2 или больше отзывами
            var totalReviewsCount = bookDbContext.Set<Review>().Count(); // подсчитываю общее количество отзывов на все книги
            // Шаг 2: Создаю пустую переменную, экземляр класса BooksInfoModel, в поля переменной booksInfo прокидываю свои данные из переменных полученных выше
            var booksInfo = new BooksInfoModel
            {
                TotalBooksCount = booksCount,
                BooksWithTwoOrMoreReviewsCount = booksWithTwoOrMoreReviewsCount,
                TotalReviewsCount = totalReviewsCount
            };
            return booksInfo; // возвращаю всю эту песню в свой метод
        }

        public string GetBookTitle(int id)
        {
            var book = bookDbContext.Set<Book>().FirstOrDefault(book => book.Id == id);
            return book.Title; // Указал тип метода string, потому-что Title это string.
        }

        public List<BooksAllModel> GetAllBooksInfo()
        {
            var books = bookDbContext.Set<Book>() // в переменной "books" хранится список всех объектов типа 'Book' из БД
                .Include(book => book.Reviews)
                .Include(book => book.Author)// в результате запроса включил связанные отзывы к каждой книге, а поле ReviewNumber внутри цикла используется для того, чтобы эти все отзывы посчитать.
                .ToList(); // получаю все объекты 'Book' из БД в виде списка (использую метод ToList)
            var bookList = new List<BooksAllModel>(); // создал новый пустой список, куда буду сохранять объекты 'BooksAllModel'

            foreach (var book in books) // при помощи цикла перебираю каждую book в переменной books, хранящей список всех книг
            {
                BooksAllModel bookInfo = new BooksAllModel // для каждой книги я создаю новый объект bookInfo
                {                                          // ниже идёт присвоение свойствам объекта bookInfo значений из объекта book (полученным при переборе элеметов foreach'ем)
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author.Name,
                    ReviewNumber = book.Reviews.Count // при помощи Count считаю количество всех отзывов на книгу и присваиваю эту цифру свойству ReviewNumber
                };

                bookList.Add(bookInfo); // пушу поля с данными из bookInfo в bookList
            }
            return bookList; // в итоге возвращаю список книг (содержит все объекты из bookInfo)
        }

        // Так как этот метод используется для обновления книги и не предлагается использование его в качестве результата или проверки на успешное обновление, то его следует изменить на 'void'.
        // Использование 'void' вместо 'bool' возвращает информацию о том, что метод не возвращает результат и не требует проверки результата. Это может быть полезно, если есть уверенность, 
        // что обновление книги всегда должно происходить без ошибок и проверки на успешное обновление не требуется.
        public void UpdateBook(UpdateBookParams bookToUpdate)
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
                else
                {
                    throw new Exception("Автор для редактирования не найден");
                }
                bookDbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Книга для редактирования не найдена");
            }
        }
        // Теперь метод принимает AuthorId вместо Author в параметрах, создаёт объект Author из базы данных на основе AuthorId, и назначает его свойству Author сущности Book.

        public void DeleteBook(int id)
        {
            Book bookToDelete = bookDbContext.Set<Book>().FirstOrDefault(book => book.Id == id);

            if (bookToDelete != null)
            {
                bookDbContext.Remove(bookToDelete);
                bookDbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Книга не найдена");
            }
        }
    }
}
