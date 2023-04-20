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
        public int BookId { get; set; } // идентификатор книги, на которую написан отзыв
        public int UserId { get; set; } // идентификатор пользователя, написавшего отзыв
        public string Text { get; set; } // собственно, текст отзыва
        public int Rating { get; set; } // оценка книги, которую пользователь поставил в отзыве (к примеру, от 1 до 5)
        public DateTime PublishedDateTime { get; set; } // дата и время написания отзыва
        
    }
}
