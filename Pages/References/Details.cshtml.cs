using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.References
{
    public class DetailsModel : PageModel
    {
        private readonly MyScriptureJournal.Data.JournalContext _context;

        public DetailsModel(MyScriptureJournal.Data.JournalContext context)
        {
            _context = context;
        }

        public Reference Reference { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Reference = await _context.References
                .AsNoTracking()
                .Include(r => r.City)
                .FirstOrDefaultAsync(m => m.ReferenceID == id);

            if (Reference == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
