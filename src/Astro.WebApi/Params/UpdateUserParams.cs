namespace Astro.WebApi.Params
{
    public class UpdateUserParams
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Year { get; set; }
        public string Email { get; set; }
    }
}
