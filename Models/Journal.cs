using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyScriptureJournal.Models
{
    public class Journal
    {
        //Student
        public int ID { get; set; }
        
        [Required]
        [StringLength(70, ErrorMessage = "Journal name cannot be longer than 70 characters.")]
        [Display(Name = "Journal Name")]
        public string JournalName { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }
        
        [Display(Name = "Journal Name")]
        public string FullName
        {
            get
            {
                return JournalName;
            }
        }

        public ICollection<Note> Notes { get; set; }
    }
}
