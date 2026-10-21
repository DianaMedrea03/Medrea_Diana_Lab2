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

        public async Task OnGetAsync(int? id)
        {
            BookD = new BookData();
            BookD.Books = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .OrderBy(b => b.Title)
                .ToListAsync();

            Book = BookD.Books.ToList();

            if (id != null)
            {
                BookID = id.Value;
            }
        }
    }
}
