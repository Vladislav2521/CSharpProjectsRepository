using Astro.Database;
using Astro.Domain.Entities;
using Astro.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Astro.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // ��������
    public class UserController : ControllerBase 
    {
        // �����
        private UserService userService;

        // ������
        

        public UserController(UserService userService)
        {
            this.userService = userService;
            
        }

        [HttpPost("CreateUser")]
        public void CreateUser(
            string firstName,
            string lastName,
            int year,
            string email
            )
        {
            userService.CreateUser( firstName, lastName, year, email );
        }

        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(string firstName, string lastName)
        {
            
            bool userDeleted = userService.DeleteUser(firstName, lastName);
            // ���������, ��� �� ����� ������������ �� ��

            if (userDeleted == true)
            {
                return Ok("������������ ������� �����");
            }
            else return NotFound("���������� ������� ��������������� ������������");
        }

        [HttpGet("GetFirstNameOfAllUsers")]
        public string GetFirstNameOfAllUsers()
        {
            return userService.GetFirstNameOfAllUsers();
        }

        [HttpGet("GetYearSumOfAllUsers")]
        public int GetYearSumOfAllUsers()
        {
            return userService.GetYearSumOfAllUsers();
        }

        //[HttpGet("GetUserAndProductCount")]
        //public int GetUserAndProductCount()
        //{
        //    var userCount = userService.GetUserCount();

        //    var productCount = shopService.GetProductCount();

        //    return userCount + productCount;
    


        [HttpGet("GetUserCount")]
        public int GetUserCount()
        {
            int result = userService.GetUserCount();

            return result;
        }

        [HttpGet("GetUserCountByField")]
        public int GetUserCountByField()
        {
            var result = userService.GetUserCountByField();

            return result;
        }

        [HttpGet("GetUserCountByField2")]
        public int GetUserCountByField2()
        {
            var result = userService.GetUserCountByField2();

            return result;
        }
    }
}