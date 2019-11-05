using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.ScriptureJournal
{
    public class CreateModel : PageModel
    {
        private readonly MyScriptureJournal.Data.JournalContext _context;

        public CreateModel(MyScriptureJournal.Data.JournalContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Journal Journal { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyJournal = new Journal();

            if (await TryUpdateModelAsync<Journal>(
                emptyJournal,
                "journal",   // Prefix for form value.
                s => s.JournalName, s => s.CreationDate))
            {
                _context.Journals.Add(emptyJournal);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
