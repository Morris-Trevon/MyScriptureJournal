using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.ScriptureJournal
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournal.Data.JournalContext _context;

        public IndexModel(MyScriptureJournal.Data.JournalContext context)
        {
            _context = context;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Journal> Journals { get; set; }

        //public IList<Journal> Journals { get; set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Journal> journalSN = from s in _context.Journals
                                             select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                journalSN = journalSN.Where(s => s.JournalName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    journalSN = journalSN.OrderByDescending(s => s.JournalName);
                    break;
                case "Date":
                    journalSN = journalSN.OrderBy(s => s.CreationDate);
                    break;
                case "date_desc":
                    journalSN = journalSN.OrderByDescending(s => s.CreationDate);
                    break;
                default:
                    journalSN = journalSN.OrderBy(s => s.JournalName);
                    break;
            }

            int pageSize = 4;
            Journals = await PaginatedList<Journal>.CreateAsync(
                journalSN.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
