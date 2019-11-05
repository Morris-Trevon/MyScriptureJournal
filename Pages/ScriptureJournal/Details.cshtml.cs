using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.ScriptureJournal
{
    public class DetailsModel : PageModel
    {
        private readonly MyScriptureJournal.Data.JournalContext _context;

        public DetailsModel(MyScriptureJournal.Data.JournalContext context)
        {
            _context = context;
        }

        public Journal Journal { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Journal = await _context.Journals
                .Include(s => s.Notes)
                .ThenInclude(e => e.Reference)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Journal == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
