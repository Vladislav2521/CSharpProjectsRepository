using Astro.Database;
using Astro.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Astro.WebApi.Services
{
    public class UserService
    {
        private LibraryDbContext libraryDbContext; // Создаётся поле с контекстом БД

        public UserService(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        public void CreateUser(
            string firstName,
            string lastName,
            int year,
            string email
            )
        {
            User user = new User(); // Создаём экземпляр класса, чтобы связать параметры и поля
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Year = year;
            user.Email = email;

            libraryDbContext.Set<User>().Add(user);
            libraryDbContext.SaveChanges();
        }

        public bool DeleteUser(string firstName, string lastName)
        {

            User userToDelete = libraryDbContext.Set<User>().First(u => u.FirstName == firstName && u.LastName == lastName); // В этой строке мы используем shopDbContext, чтобы получить сущность User из базы данных. Метод Set<User>() возвращает объект, представляющий коллекцию сущностей User, хранящихся в базе данных.
            // Результат этой операции сохраняется в переменной userToDelete, которая имеет тип User.                               // Затем мы используем метод SingleOrDefault() LINQ, чтобы найти первый элемент в коллекции, удовлетворяющий условию, заданному в лямбда-выражении. В данном случае мы ищем пользователя с именем firstName и фамилией lastName.
            
            if (userToDelete != null) // Затем мы проверяем, что переменная userToDelete не равна null, что означает, что пользователь с указанным именем и фамилией был найден в базе данных.
            {
                libraryDbContext.Set<User>().Remove(userToDelete); // Если пользователь был найден, мы используем shopDbContext для удаления сущности User из базы данных. Для этого мы вызываем метод Remove() для объекта shopDbContext.Set<User>() и передаем ему сущность userToDelete.
                libraryDbContext.SaveChanges(); // После удаления сущности User мы вызываем метод SaveChanges() для shopDbContext, чтобы сохранить изменения в базе данных.
                return true; // Возвращаемое значение true указывает на то, что пользователь был успешно удален из базы данных.
            }
            else return false; // Если переменная userToDelete равна null, то пользователь с указанным именем и фамилией не был найден в базе данных, поэтому мы возвращаем false.

        }

        



        public int GetUserCount()
        {
            return libraryDbContext.Set<User>().Count();
        }

        public string GetFirstNameOfAllUsers()
        {
            var users = libraryDbContext.Set<User>().ToList();
            string names = "";

            foreach (var user in users)
            {
                names = names + ", " + user.FirstName;
            }

            return names;
        }

        public int GetYearSumOfAllUsers()
        {
            var users = libraryDbContext.Set<User>().ToList();
            int sum = 0;

            foreach (var user in users)
            {
                sum = sum + user.Year;
            }

            return sum;
        }

        public int GetUserCountByField()
        {
            return 5;
        }

        public int GetUserCountByField2()
        {
            return 4;
        }
    }
}
