using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.References
{
    public class CreateModel : CityNamePageModelModel
    {
        private readonly MyScriptureJournal.Data.JournalContext _context;

        public CreateModel(MyScriptureJournal.Data.JournalContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateCityDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Reference Reference { get; set; }
             
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyReference = new Reference();

            if (await TryUpdateModelAsync(
                 emptyReference,
                 "reference",   // Prefix for form value.
                 s => s.ReferenceID, s => s.Title, s => s.ChapterAndVerse, s => s.SpiritualNotes))
            {
                _context.References.Add(emptyReference);
                _ = await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select CityID if TryUpdateModelAsync fails.
            PopulateCityDropDownList(_context, emptyReference.CityId);
            return Page();
        }
    }
}
