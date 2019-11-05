using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyScriptureJournal.Models
{
    //Enrollment
    public enum Canon
    {
        OldTestament, NewTestament, BookOfMormon, PearlofGreatPrice, DoctrineAndCovenants
    }
    public class Note
    {
        [DisplayFormat(NullDisplayText = "No Canon Assigned")]
        public Canon? Canon { get; set; }
        public int NoteID { get; set; }
        public int ReferenceID { get; set; }
        public int JournalID { get; set; }
        public Reference Reference { get; set; }
        public Journal Journal { get; set; }
    }
}
