namespace Medrea_Diana_Lab2.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string CategoryName { get; set; } = null!;
        public ICollection<BookCategory>? BookCategories { get; set; }
    }
}
