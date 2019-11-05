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

namespace MyScriptureJournal.Pages.ProphetNS
{
    public class EditModel : ProphetReferencePageModel
    {
        private readonly MyScriptureJournal.Data.JournalContext _context;

        public EditModel(MyScriptureJournal.Data.JournalContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Prophets prophet { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            prophet = await _context.Prophet
                .Include(i => i.priestHoodOffice)
                .Include(i => i.dispensationLinks).ThenInclude(i => i.References)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProphetID == id);


            if (prophet == null)
            {
                return NotFound();
            }
            PopulateAssignedReferenceData(_context, prophet);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedReference)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prophetToUpdate = await _context.Prophet
                .Include(i => i.priestHoodOffice)
                .Include(i => i.dispensationLinks)
                    .ThenInclude(i => i.References)
                .FirstOrDefaultAsync(s => s.ProphetID == id);

            if (prophetToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Prophets>(
                prophetToUpdate,
                "Instructor",
                i => i.FirstMidName, i => i.LastName,
                i => i.DispDate, i => i.dispensationLinks))
            {
                
                UpdateProphetReferences(_context, selectedReference, prophetToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            UpdateProphetReferences(_context, selectedReference, prophetToUpdate);
            PopulateAssignedReferenceData(_context, prophetToUpdate);
            return Page();
        }
    }
}
