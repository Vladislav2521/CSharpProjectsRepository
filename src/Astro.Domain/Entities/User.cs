
using Astro.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Astro.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Range(1900,3000)] // Пример валидации
        public int Year { get; set; }

        [MaxLength(100)] // Пример валидации
        public string Email { get; set; }

        public List<Review> Reviews { get; set; } // навигационное свойство
    }
}
