using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.ScriptureJournal
{
    public class EditModel : PageModel
    {
        private readonly MyScriptureJournal.Data.JournalContext _context;

        public EditModel(MyScriptureJournal.Data.JournalContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Journal Journal { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Journal = await _context.Journals.FindAsync(id);

            if (Journal == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var journalToUpdate = await _context.Journals.FindAsync(id);

            if (journalToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Journal>(
                journalToUpdate,
                "student",
                s => s.JournalName, s => s.CreationDate))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }

        private bool JournalExists(int id)
        {
            return _context.Journals.Any(e => e.ID == id);
        }
    }
}
