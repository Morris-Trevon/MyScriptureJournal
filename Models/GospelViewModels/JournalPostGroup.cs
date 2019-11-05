using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyScriptureJournal.Models.GospelViewModels
{
    public class JournalPostGroup
    {
        [DataType(DataType.Date)]
        public DateTime? CreationDate { get; set; }

        public int JournalCount { get; set; }
    }
}
