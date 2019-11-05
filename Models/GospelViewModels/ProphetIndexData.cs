using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyScriptureJournal.Models.GospelViewModels
{
    public class ProphetIndexData
    {
        public IEnumerable<Prophets> prophet { get; set; }
        public IEnumerable<Reference> references { get; set; }
        public IEnumerable<Note> notes { get; set; }
    }
}
