using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Medrea_Diana_Lab2.Models;

namespace Medrea_Diana_Lab2.Pages.BookPages
{
    public class BookData
    {
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
    }

    public class IndexModel : PageModel
    {
        private readonly Medrea_Diana_Lab2Context _context;

        public IndexModel(Medrea_Diana_Lab2Context context)
        {
            _context = context;
        }

        public BookData BookD { get; set; } = new BookData();
        public int BookID { get; set; }

        public IList<Book> Book { get; set; } = default!;

        public string TitleSort { get; set; } = "";
        public string AuthorSort { get; set; } = "";
        public string CurrentFilter { get; set; } = "";

        public async Task OnGetAsync(int? id, string? sortOrder, string? searchString)
        {
            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            AuthorSort = sortOrder == "author" ? "author_desc" : "author";
            CurrentFilter = searchString ?? "";

            BookD = new BookData();
            BookD.Books = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .OrderBy(b => b.Title)
                .ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                BookD.Books = BookD.Books.Where(s =>
                    (s.Author != null && s.Author.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    || (s.Author != null && s.Author.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    || s.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            if (id != null)
            {
                BookID = id.Value;
            }

            switch (sortOrder)
            {
                case "title_desc":
                    BookD.Books = BookD.Books.OrderByDescending(s => s.Title);
                    break;
                case "author_desc":
                    BookD.Books = BookD.Books.OrderByDescending(s => s.Author?.FullName);
                    break;
                case "author":
                    BookD.Books = BookD.Books.OrderBy(s => s.Author?.FullName);
                    break;
                default:
                    BookD.Books = BookD.Books.OrderBy(s => s.Title);
                    break;
            }

            Book = BookD.Books.ToList();
        }
    }
}

