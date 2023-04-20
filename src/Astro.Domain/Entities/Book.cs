

namespace Astro.Domain.Entities
{
    public class Book
    {
       public int Id { get; set; }
        public string Title { get; set; }
       public string Genre { get; set; }
       public double Price { get; set; }
       
       public int AuthorId { get; set; } // внешний ключ
        public Author Author { get; set; } // навигационное свойство
            
    }
}
