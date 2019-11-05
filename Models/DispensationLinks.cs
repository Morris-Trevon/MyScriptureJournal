using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyScriptureJournal.Models
{
    //Course Assignments
    public class DispensationLinks
    {
        public int ProphetID { get; set; }
        public int ReferenceID { get; set; }
        public Prophets Prophet { get; set; }
        public Reference References { get; set; }
    }
}
