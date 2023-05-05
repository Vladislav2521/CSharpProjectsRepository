List<int> list = new List<int> { 5, 2, 8, 7, 8, 3, 7, 2, 5, 6, 6 };

var a = list
    .Where(a => a > 6); // where фильтрует
    //.ToList();
foreach (var item in a)
{
    Console.WriteLine(item);
}





//List<Review> reviews = new List<Review>();
//var review1 = new Review();
//review1.Id = 1;
//review1.Text = "Nice";
//review1.BookId = 3;
//reviews.Add(review1);

//var review2 = new Review();
//review2.Id = 2;
//review2.Text = "Great";
//review2.BookId = 5;
//reviews.Add(review2);

//var review3 = new Review();
//review3.Id = 3;
//review3.Text = "Excellent";
//review3.BookId = 5;
//reviews.Add(review3);

//var review4 = new Review();
//review4.Id = 4;
//review4.Text = "Excellent";
//review4.BookId = 4;
//reviews.Add(review4);

//var review5 = new Review();
//review5.Id = 5;
//review5.Text = "Excellent";
//review5.BookId = 6;
//reviews.Add(review5);
//// кол.во отзывов на книгу под номером 5
//int number = 0;

//foreach (var review in reviews)
//{
//    if (review.BookId == 3)
//    {
//        number++;
//    }
//}
//Console.WriteLine(number);

//int number2 = reviews.Count(r => r.BookId == 3);
//Console.WriteLine(number2);

//public class Review
//{
//    public int Id { get; set; }
//    public string Text { get; set; }
//    public int BookId { get; set; }

//}










