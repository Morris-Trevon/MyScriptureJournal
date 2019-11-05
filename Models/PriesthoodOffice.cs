using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyScriptureJournal.Models
{
    //Office Assignment
    public class PriesthoodOffice
    {
        [Key]
        public int ProphetID { get; set; }
        
        [StringLength(50)]
        [Display(Name = "Priesthood Office")]
        public string priesthoodOffice { get; set; }
        public Prophets Prophet { get; set; }
    }
}
