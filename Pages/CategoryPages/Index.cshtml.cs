using Medrea_Diana_Lab2.Models;
using Medrea_Diana_Lab2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Medrea_Diana_Lab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly Medrea_Diana_Lab2Context _context;

    public IndexModel(Medrea_Diana_Lab2Context context)
    {
        _context = context;
    }

    public CategoryIndexData CategoryData { get; set; } = new CategoryIndexData();
    public int CategoryID { get; set; }

    public async Task OnGetAsync(int? id)
    {
        CategoryData.Categories = await _context.Category
            .Include(c => c.BookCategories)
                .ThenInclude(bc => bc.Book)
                    .ThenInclude(b => b.Author)
            .OrderBy(c => c.CategoryName)
            .ToListAsync();

        if (id != null)
        {
            CategoryID = id.Value;
            Category category = CategoryData.Categories
                .Where(c => c.ID == id.Value).Single();
            CategoryData.Books = category.BookCategories.Select(bc => bc.Book);
        }
    }
}
