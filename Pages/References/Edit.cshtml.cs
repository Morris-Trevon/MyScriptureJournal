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

namespace MyScriptureJournal.Pages.References
{
    public class EditModel : CityNamePageModelModel
    {
        private readonly MyScriptureJournal.Data.JournalContext _context;

        public EditModel(MyScriptureJournal.Data.JournalContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reference Reference { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Reference = await _context.References
                .Include(r => r.City).FirstOrDefaultAsync(m => m.ReferenceID == id);

            if (Reference == null)
            {
                return NotFound();
            }

            // Select current CityID.
            PopulateCityDropDownList(_context, Reference.CityId);
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referenceToUpdate = await _context.References.FindAsync(id);

            if (referenceToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Reference>(
                 referenceToUpdate,
                 "reference",   // Prefix for form value.
                   c => c.ReferenceID, c => c.Title, c => c.ChapterAndVerse, c => c.SpiritualNotes))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select DepartmentID if TryUpdateModelAsync fails.
            PopulateCityDropDownList(_context, referenceToUpdate.CityId);
            return Page();
        }
    }
}

