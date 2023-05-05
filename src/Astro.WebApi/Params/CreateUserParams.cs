using System.Security.Policy;

namespace Astro.WebApi.Params
{
    public class CreateUserParams
    {
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public int Year { get; set;}
        public string Email { get; set;}
    }
}
