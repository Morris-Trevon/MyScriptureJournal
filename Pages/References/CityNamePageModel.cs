using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;

namespace MyScriptureJournal.Pages.References
{
    public class CityNamePageModelModel : PageModel
    {
        public SelectList CityNameSL { get; set; }

        public void PopulateCityDropDownList(JournalContext _context,
            object selectedCity = null)
        {
            var cityQuery = from d in _context.City
                            orderby d.Name // Sort by name.
                            select d;

            CityNameSL = new SelectList(cityQuery.AsNoTracking(),
                        "CityID", "Name", selectedCity);
        }
    }
}