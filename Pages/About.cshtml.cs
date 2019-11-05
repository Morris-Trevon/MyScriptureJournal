using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models.GospelViewModels;
using MyScriptureJournal.Models;
using Microsoft.EntityFrameworkCore;

namespace MyScriptureJournal.Pages
{
    public class AboutModel : PageModel
    {
        private readonly JournalContext _context;

        public AboutModel(JournalContext context)
        {
            _context = context;
        }

        public IList<JournalPostGroup> Journals { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<JournalPostGroup> data =
                from journal in _context.Journals
                group journal by journal.CreationDate into dateGroup
                select new JournalPostGroup()
                {
                    CreationDate = dateGroup.Key,
                    JournalCount = dateGroup.Count()
                };

            Journals = await data.AsNoTracking().ToListAsync();
        }
    }
}