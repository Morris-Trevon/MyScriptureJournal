using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.ProphetNS
{
    public class CreateModel : ProphetReferencePageModel
    {
        private readonly MyScriptureJournal.Data.JournalContext _context;

        public CreateModel(MyScriptureJournal.Data.JournalContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var prophet = new Prophets();
            prophet.dispensationLinks = new List<DispensationLinks>();

            // Provides an empty collection for the foreach loop
            // foreach (var course in Model.AssignedCourseDataList)
            // in the Create Razor page.
            PopulateAssignedReferenceData(_context, prophet);
            return Page();
        }

        [BindProperty]
        public Prophets Prophets { get; set; }

        public async Task<IActionResult> OnPostAsync(string[] selectedReferences)
        {
            var newProphet = new Prophets();
            if (selectedReferences != null)
            {
                newProphet.dispensationLinks = new List<DispensationLinks>();
                foreach (var reference in selectedReferences)
                {
                    var referenceToAdd = new DispensationLinks
                    {
                        ReferenceID = int.Parse(reference)
                    };
                    newProphet.dispensationLinks.Add(referenceToAdd);
                }
            }

            if (await TryUpdateModelAsync(
                newProphet,
                "Prophet",
                i => i.LastName, i => i.FirstMidName, 
                i => i.DispDate))
            {
                _context.Prophet.Add(newProphet);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateAssignedReferenceData(_context, newProphet);
            return Page();
        }
    }
}
