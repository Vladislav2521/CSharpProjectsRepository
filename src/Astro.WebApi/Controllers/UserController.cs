using Astro.Database;
using Astro.Domain.Entities;
using Astro.WebApi.Models;
using Astro.WebApi.Params;
using Astro.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Astro.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // Официант
    public class UserController : ControllerBase
    {
        // Повар
        private UserService userService;

        // Бармен


        public UserController(UserService userService)
        {
            this.userService = userService;

        }

        [HttpPost("CreateUser")]
        public void CreateUser(CreateUserParams userToCreate)
        {
            userService.CreateUser(userToCreate);
        }

        [HttpPost("GetUserInfo")]
        public UserModel GetUserInfo(int id)
        {
            var user = userService.GetUserInfo(id);
            return user;
        }

        [HttpPost("UpdateUser")]
        public void UpdateUser(UpdateUserParams userToUpdate)
        {
            userService.UpdateUser(userToUpdate);
        }

        [HttpDelete("DeleteUser")]
        public void DeleteUser(int id)
        {
            userService.DeleteUser(id);
        }
    } 
}