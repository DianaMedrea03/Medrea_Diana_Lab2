using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Medrea_Diana_Lab2.Models;

namespace Medrea_Diana_Lab2.Pages_Authors
{
    public class IndexModel : PageModel
    {
        private readonly Medrea_Diana_Lab2Context _context;

        public IndexModel(Medrea_Diana_Lab2Context context)
        {
            _context = context;
        }

        public IList<Authors> Authors { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Authors = await _context.Authors.ToListAsync();
        }
    }
}
