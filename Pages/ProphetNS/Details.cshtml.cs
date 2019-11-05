using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.ProphetNS
{
    public class DetailsModel : PageModel
    {
        private readonly MyScriptureJournal.Data.JournalContext _context;

        public DetailsModel(MyScriptureJournal.Data.JournalContext context)
        {
            _context = context;
        }

        public Prophets Prophets { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Prophets = await _context.Prophet.FirstOrDefaultAsync(m => m.ProphetID == id);

            if (Prophets == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
