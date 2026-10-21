using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Medrea_Diana_Lab2.Models;

namespace Medrea_Diana_Lab2.Pages.BookPages
{
    public class CreateModel : BookCategoriesPageModel
    {
        private readonly Medrea_Diana_Lab2Context _context;

        public CreateModel(Medrea_Diana_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public IActionResult OnGet()
        {
            ViewData["AuthorID"] = new SelectList(_context.Authors, "ID", "FullName");
            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");
            // New book has no categories yet — pass an empty Book
            PopulateAssignedCategoryData(_context, new Book());
            return Page();
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            if (!ModelState.IsValid)
            {
                PopulateAssignedCategoryData(_context, new Book());
                return Page();
            }

            // Build the BookCategory join entries from the checked checkboxes
            Book.BookCategories = new List<BookCategory>();
            foreach (var catId in selectedCategories ?? Array.Empty<string>())
            {
                if (int.TryParse(catId, out int id))
                {
                    Book.BookCategories.Add(new BookCategory { CategoryID = id });
                }
            }

            _context.Book.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
