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
       public List<Review> Reviews { get; set; } // навигационное свойство

       // навигационное свойство Reviews позволяет связать объекты классов Book и Review, т.е. позволяет получить доступ к коллекции связанных
       // объектов Review для каждого объекта Book. Кроме того, оно объявлено с модификатором 'public', что позволяет получить доступ
       // к этому свойству из других классов и содержит модификатор 'set', что позволяет изменять эту коллекцию.
       

        
    }
}
