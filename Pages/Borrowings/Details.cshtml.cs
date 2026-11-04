using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Medrea_Diana_Lab2.Models;

namespace Medrea_Diana_Lab2.Pages_Borrowings
{
    public class DetailsModel : PageModel
    {
        private readonly Medrea_Diana_Lab2Context _context;

        public DetailsModel(Medrea_Diana_Lab2Context context)
        {
            _context = context;
        }

        public Borrowing Borrowing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowing
                .Include(b => b.Book)
                    .ThenInclude(b => b.Author)
                .Include(b => b.Member)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (borrowing is not null)
            {
                Borrowing = borrowing;

                return Page();
            }

            return NotFound();
        }
    }
}
