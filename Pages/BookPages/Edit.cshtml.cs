using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Medrea_Diana_Lab2.Models;

namespace Medrea_Diana_Lab2.Pages.BookPages
{
    public class EditModel : BookCategoriesPageModel
    {
        private readonly Medrea_Diana_Lab2Context _context;

        public EditModel(Medrea_Diana_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.BookCategories)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (book == null)
            {
                return NotFound();
            }

            Book = book;
            PopulateAssignedCategoryData(_context, Book);

            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");
            ViewData["AuthorID"] = new SelectList(_context.Authors, "ID", "FullName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            if (!ModelState.IsValid)
            {
                PopulateAssignedCategoryData(_context, Book);
                return Page();
            }

            var bookToUpdate = await _context.Book
                .Include(b => b.BookCategories)
                .FirstOrDefaultAsync(m => m.ID == Book.ID);

            if (bookToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(bookToUpdate, "Book",
                b => b.Title, b => b.AuthorID, b => b.Price,
                b => b.PublishingDate, b => b.PublisherID))
            {
                UpdateBookCategories(_context, selectedCategories, bookToUpdate);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(Book.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToPage("./Index");
            }

            PopulateAssignedCategoryData(_context, bookToUpdate);
            return Page();
        }

        private bool BookExists(int id)
        {
            return (_context.Book?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}