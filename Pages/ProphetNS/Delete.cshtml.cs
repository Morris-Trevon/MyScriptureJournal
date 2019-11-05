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
    public class DeleteModel : PageModel
    {
        private readonly MyScriptureJournal.Data.JournalContext _context;

        public DeleteModel(MyScriptureJournal.Data.JournalContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Prophets prophet = await _context.Prophet
                 .Include(i => i.dispensationLinks)
                 .SingleAsync(i => i.ProphetID == id);

            if (prophet == null)
            {
                return RedirectToPage("./Index");
            }

            var city = await _context.City
                .Where(d => d.ProphetID == id)
                .ToListAsync();
            city.ForEach(d => d.ProphetID = null);

            _context.Prophet.Remove(prophet);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
