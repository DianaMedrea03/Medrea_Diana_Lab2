using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Medrea_Diana_Lab2.Models;

namespace Medrea_Diana_Lab2.Pages_BookPages
{
    public class DetailsModel : PageModel
    {
        private readonly Medrea_Diana_Lab2Context _context;

        public DetailsModel(Medrea_Diana_Lab2Context context)
        {
            _context = context;
        }

        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
    .Include(b => b.Publisher)
    .Include(b => b.Author)
    .Include(b => b.BookCategories)
        .ThenInclude(bc => bc.Category)
    .FirstOrDefaultAsync(m => m.ID == id);

            if (book is not null)
            {
                Book = book;

                return Page();
            }

            return NotFound();
        }
    }
}
