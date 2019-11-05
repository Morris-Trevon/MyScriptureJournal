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
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournal.Data.JournalContext _context;

        public IndexModel(MyScriptureJournal.Data.JournalContext context)
        {
            _context = context;
        }

        public IList<Reference> Reference { get;set; }

        public async Task OnGetAsync()
        {
            Reference = await _context.References
                .Include(r => r.City)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
