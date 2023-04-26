using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astro.Domain.Entities
{
    public class Review
    {
        public int Id {get; set;} // уникальный идентификатор отзыва
        public string Text { get; set; } // собственно, текст отзыва
        public int Rating { get; set; } // оценка книги, которую пользователь поставил в отзыве (к примеру, от 1 до 5)
        public int BookId { get; set; } // внешний ключ книги, на которую написан отзыв
        public Book Book { get; set; } // навигационное свойство
        public int UserId { get; set; } // внешний ключ пользователя, написавшего отзыв
        public User User { get; set; } // навигационное свойство
        public DateTime PublishedDateTime { get; set; } // дата и время написания отзыва

    }
}
