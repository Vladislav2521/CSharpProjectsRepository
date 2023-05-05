using Astro.Database;
using Astro.Domain.Entities;
using Astro.WebApi.Models;
using Astro.WebApi.Params;
using Microsoft.AspNetCore.Mvc;

namespace Astro.WebApi.Services
{
    public class UserService
    {
        private LibraryDbContext userDbContext; // Создаётя поле типа LibraryDbContext при помощи которого мои методы будут взаимодействовать с БД

        public UserService(LibraryDbContext userDbContext) // Конструктор принимает параметр "userDbContext" типа LibraryDbContext, это значит, что при создании экземпляра UserService,
                                                           // необходимо передать объект LibraryDbContext.
        {                                                  // Параметр "userDbContext" необходим для взаимодействия с БД, параметр "userDbContext" передаётся в конструктор при создании экземпляра класса UserService
            this.userDbContext = userDbContext;            
        }
        // Внутри конструктора происходит присвоение значения параметра userDbContext полю this.userDbContect.
        // Таким образом, поле userDbContext класса UserService, будет содержать переданный объект LibraryDbContext, который будет использоваться для выполнения операций с БД.
        // Это позволяет классу UserService в котором размещены мои методы, получить доступ к контексту базы данных и использовать его для выполнения операций с данными.

        public void CreateUser(CreateUserParams userToCreate)
        {
            User user = new User(); // Создаём экземпляр класса, чтобы связать параметры и поля
            user.FirstName = userToCreate.FirstName;
            user.LastName = userToCreate.LastName;
            user.Year = userToCreate.Year;
            user.Email = userToCreate.Email;

            userDbContext.Set<User>().Add(user);
            userDbContext.SaveChanges();
        }

        public UserModel GetUserInfo(int id)
        {
            var user = userDbContext.Set<User>().FirstOrDefault(user => user.Id == id); // поиск необходимого пользователя по параметру id
            var userModel = new UserModel(); // cоздание экземпляра класса, где буду хранить своего пользователя, выдернутого из БД. Создавать надо для того, чтобы отдавать модель.
            userModel.Id = id;
            userModel.FirstName = user.FirstName;
            userModel.LastName = user.LastName;
            userModel.Year = user.Year;
            userModel.Email = user.Email;
                                             // все поля из выдернутого объекта user из БД присваиватся полям пустой модели, таким образом заполняя её
            userDbContext.SaveChanges();                              
            return userModel; // данные возвращаются и метод отдаёт модель с заполненными полями

        }

        // Implement a method GetAllUsers()
        
        public void UpdateUser(UpdateUserParams userToUpdate)
        {
            var existingUser = userDbContext.Set<User>().FirstOrDefault(user => user.Id == userToUpdate.Id);
            if (existingUser != null)
            {
                existingUser.FirstName = userToUpdate.FirstName;
                existingUser.LastName = userToUpdate.LastName;
                existingUser.Year = userToUpdate.Year;
                existingUser.Email = userToUpdate.Email;

                userDbContext.SaveChanges();
                
            }
        }

        public void DeleteUser(int id)
        {

            User userToDelete = userDbContext.Set<User>().FirstOrDefault(user => user.Id == id); 
            
            if (userToDelete != null) 
            {
                userDbContext.Set<User>().Remove(userToDelete);
                userDbContext.SaveChanges(); 
            }
              
        }
    }
}
