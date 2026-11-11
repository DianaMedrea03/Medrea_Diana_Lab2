using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medrea_Diana_Lab2.Models
{
    public class Book
    {
        public int ID { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 3)]
        [Display(Name = "Book Title")]
        public string Title { get; set; } = null!;
        public int? AuthorID { get; set; }
        public Authors? Author { get; set; }
        [Range(0.01, 500)]

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishingDate { get; set; }
        public int? PublisherID { get; set; }
        public Publisher? Publisher { get; set; }
        public ICollection<Borrowing>? Borrowings { get; set; }
        public ICollection<BookCategory>? BookCategories { get; set; }
    }
}
