using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;
using MyScriptureJournal.Models.GospelViewModels;

namespace MyScriptureJournal.Pages.ProphetNS
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournal.Data.JournalContext _context;

        public IndexModel(MyScriptureJournal.Data.JournalContext context)
        {
            _context = context;
        }

        public ProphetIndexData ProphetData { get; set; }
        public int ProphetID { get; set; }
        public int ReferenceID { get; set; }

        public async Task OnGetAsync(int? id, int? referenceID)
        {
            ProphetData = new ProphetIndexData();
            ProphetData.prophet = await _context.Prophet
                .Include(i => i.priestHoodOffice)
                .Include(i => i.dispensationLinks)
                    .ThenInclude(i => i.References)
                        .ThenInclude(i => i.City)
                //.Include(i => i.dispensationLinks)
                //    .ThenInclude(i => i.References)
                //        .ThenInclude(i => i.Notes)
                //            .ThenInclude(i => i.Journal)
                //.AsNoTracking()
                .OrderBy(i => i.FirstMidName)
                .ToListAsync();

            if (id != null)
            {
                ProphetID = id.Value;
                Prophets prophet = ProphetData.prophet
                    .Where(i => i.ProphetID == id.Value).Single();
                ProphetData.references = prophet.dispensationLinks.Select(s => s.References);
            }

            if (referenceID != null)
            {
                ReferenceID = referenceID.Value;
                var selectedReference = ProphetData.references
                    .Where(x => x.ReferenceID == referenceID).Single();
                await _context.Entry(selectedReference).Collection(x => x.Notes).LoadAsync();
                foreach (Note note in selectedReference.Notes)
                {
                    await _context.Entry(note).Reference(x => x.Journal).LoadAsync();
                }
                ProphetData.notes = selectedReference.Notes;
            }
        }
    }
}
