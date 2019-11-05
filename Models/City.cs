using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyScriptureJournal.Models
{
    public class City
    {
        public int CityID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Assigned Date")]
        public DateTime AssignedDate { get; set; }

        public int? ProphetID { get; set; }

        public Prophets Revelator { get; set; }
        public ICollection<Reference> References { get; set; }
    }
}
