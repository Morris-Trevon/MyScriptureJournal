using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyScriptureJournal.Models
{
    //Course
    public class Reference
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int ReferenceID { get; set; }
        
        public string Title { get; set; }

        [StringLength(50)]
        public string ChapterAndVerse { get; set; }
        public string SpiritualNotes { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
        
        public ICollection<Note> Notes { get; set; }
        public ICollection<DispensationLinks> dispensationLinks { get; set; }
    }
}
