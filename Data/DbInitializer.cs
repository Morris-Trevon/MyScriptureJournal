using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MyScriptureJournal.Data
{
    public class DbInitializer
    {
        public static void Initialize(JournalContext context)
        {
            //context.Database.EnsureCreated();

            // Look for any Journals.
            if (context.Journals.Any())
            {
                return;   // DB has been seeded
            }

            var journals = new Journal[]
            {
                new Journal{JournalName="Obedience, Commandments, Purpose of Man", CreationDate=DateTime.Parse("2019-09-01")},
                new Journal{JournalName="Visions, Spiritual Places", CreationDate=DateTime.Parse("2017-09-01")},
                new Journal{JournalName="Diligence", CreationDate=DateTime.Parse("2018-09-01")},
                new Journal{JournalName="Temple Worshippers follow Commandments", CreationDate=DateTime.Parse("2017-09-01")},
                new Journal{JournalName="All things were created for a purpose", CreationDate=DateTime.Parse("2017-09-01")},
                new Journal{JournalName="Angels", CreationDate=DateTime.Parse("2016-09-01")},
                new Journal{JournalName="Jesus", CreationDate=DateTime.Parse("2018-09-01")},
                new Journal{JournalName="Eternal Father", CreationDate=DateTime.Parse("2019-09-01")}
            };
            foreach (Journal j in journals)
            {
                context.Journals.Add(j);
            }
            context.SaveChanges();

            var prophet = new Prophets[]
            {
                new Prophets { FirstMidName = "Moses",     LastName = "Drew From The Water",
                    DispDate = DateTime.Parse("1391-01-01") },
                new Prophets { FirstMidName = "Jesus",    LastName = "The Christ",
                    DispDate = DateTime.Parse("0027-01-01") },
                new Prophets { FirstMidName = "John",   LastName = "The Baptist",
                    DispDate = DateTime.Parse("0034-01-01") },
                new Prophets { FirstMidName = "John", LastName = "The Beloved",
                    DispDate = DateTime.Parse("1835-01-01") },
                new Prophets { FirstMidName = "Abraham",   LastName = "Father Of Nations",
                    DispDate = DateTime.Parse("1830-01-01") },
                new Prophets { FirstMidName = "Joseph",   LastName = "Smith",
                    DispDate = DateTime.Parse("1824-01-01") }
            };

            foreach (Prophets i in prophet)
            {
                context.Prophet.Add(i);
            }
            context.SaveChanges();

            var city = new City[]
            {
                new City { Name = "Jerusalem", AssignedDate = DateTime.Parse("0027-01-01"),
                    ProphetID  = prophet.Single( i => i.LastName == "The Christ").ProphetID },
                new City { Name = "Jerusalem", AssignedDate = DateTime.Parse("0075-01-01"),
                    ProphetID  = prophet.Single( i => i.LastName == "The Baptist").ProphetID },
                new City { Name = "Egypt", AssignedDate = DateTime.Parse("0001-01-01"),
                    ProphetID  = prophet.Single( i => i.LastName == "Drew From The Water").ProphetID },
                new City { Name = "Palmyra", AssignedDate = DateTime.Parse("1835-01-01"),
                    ProphetID  = prophet.Single( i => i.LastName == "The Beloved").ProphetID }
            };

            foreach (City d in city)
            {
                context.City.Add(d);
            }
            context.SaveChanges();


            var priesthoodOffice = new PriesthoodOffice[]
             {
                new PriesthoodOffice {
                    ProphetID = prophet.Single( i => i.LastName == "The Christ").ProphetID,
                    priesthoodOffice = "Melchizedek" },
                new PriesthoodOffice {
                    ProphetID = prophet.Single( i => i.LastName == "The Baptist").ProphetID,
                    priesthoodOffice = "Aaronic" },
                new PriesthoodOffice {
                    ProphetID = prophet.Single( i => i.LastName == "The Beloved").ProphetID,
                    priesthoodOffice = "Melchizedek" }
             };

            foreach (PriesthoodOffice o in priesthoodOffice)
            {
                context.PriesthoodOffice.Add(o);
            }
            context.SaveChanges();

            var dispensationLinks = new DispensationLinks[]
            {
                new DispensationLinks {
                    ReferenceID = context.References.Single(c => c.Title == "Commandments").ReferenceID,
                    ProphetID = context.Prophet.Single(i => i.LastName == "Drew From The Water").ProphetID
                    },
                new DispensationLinks {
                    ReferenceID = context.References.Single(c => c.Title == "Creation" ).ReferenceID,
                    ProphetID = context.Prophet.Single(i => i.LastName == "Drew From The Water").ProphetID
                    },
                new DispensationLinks {
                    ReferenceID = context.References.Single(c => c.Title == "Visions" ).ReferenceID,
                    ProphetID = context.Prophet.Single(i => i.LastName == "The Beloved").ProphetID
                    },
                new DispensationLinks {
                    ReferenceID = context.References.Single(c => c.Title == "Jesus is ihe Word" ).ReferenceID,
                    ProphetID = context.Prophet.Single(i => i.LastName == "The Christ").ProphetID
                    },
                new DispensationLinks {
                    ReferenceID = context.References.Single(c => c.Title == "Creation" ).ReferenceID,
                    ProphetID = context.Prophet.Single(i => i.LastName == "The Christ").ProphetID
                    },
                new DispensationLinks {
                    ReferenceID = context.References.Single(c => c.Title == "Priesthood Restored" ).ReferenceID,
                    ProphetID = context.Prophet.Single(i => i.LastName == "Smith").ProphetID
                    },
                new DispensationLinks {
                    ReferenceID = context.References.Single(c => c.Title == "Spirit Prison" ).ReferenceID,
                    ProphetID = context.Prophet.Single(i => i.LastName == "The Christ").ProphetID
                    },
                new DispensationLinks {
                    ReferenceID = context.References.Single(c => c.Title == "Visions" ).ReferenceID,
                    ProphetID = context.Prophet.Single(i => i.LastName == "Smith").ProphetID
                    }
            };

            foreach (DispensationLinks ci in dispensationLinks)
            {
                context.DispensationLinks.Add(ci);
            }
            context.SaveChanges();


            var references = new Reference[]
            {
                new Reference{ReferenceID=1050,Title="Obedience", ChapterAndVerse="1Nephi 1:1", SpiritualNotes="Young people who are obedient become highly favored of the Lord"},
                new Reference{ReferenceID=4022,Title="Commandments", ChapterAndVerse="Exodus 20", SpiritualNotes="The Ten Commandments are half about worship and half about proper living"},
                new Reference{ReferenceID=4041,Title="Creation", ChapterAndVerse="Genesis 1:1-3", SpiritualNotes="The Earth was created with water, all living things have and need water to survive"},
                new Reference{ReferenceID=1045,Title="Visions", ChapterAndVerse="1Nephi 3:1", SpiritualNotes="The Lord reveals his mysteries through visions and prophets"},
                new Reference{ReferenceID=3141,Title="Jesus is the Word", ChapterAndVerse="John 1:1-3", SpiritualNotes="Jesus Christ created the Earth under the direction of His Father"},
                new Reference{ReferenceID=2021,Title="Spirit Prison", ChapterAndVerse="1 Peter 3", SpiritualNotes="The Spirit Prison is reserved for humans who were disobedient"},
                new Reference{ReferenceID=2042,Title="Priesthood Restored", ChapterAndVerse="D&C 84", SpiritualNotes="The Aaronic and Melchizedek Priesthood restored on earth"}
            };
            foreach (Reference r in references)
            {
                context.References.Add(r);
            }
            context.SaveChanges();

            
            var Notes = new Note[]
            {
                new Note {
                    JournalID = context.Journals.Single(s => s.JournalName == "Obedience, Commandments, Purpose of Man").ID,
                    ReferenceID = context.References.Single(c => c.Title == "Commandments" ).ReferenceID
                },
                new Note {
                    JournalID = context.Journals.Single(s => s.JournalName == "Diligence").ID,
                    ReferenceID = context.References.Single(c => c.Title == "Commandments" ).ReferenceID
                    },
                    new Note {
                    JournalID = context.Journals.Single(s => s.JournalName == "Obedience, Commandments, Purpose of Man").ID,
                    ReferenceID = context.References.Single(c => c.Title == "Obedience" ).ReferenceID
                    },
                    new Note {
                    JournalID = context.Journals.Single(s => s.JournalName == "Eternal Father").ID,
                    ReferenceID = context.References.Single(c => c.Title == "Obedience" ).ReferenceID
                    },
                    new Note {
                    JournalID = context.Journals.Single(s => s.JournalName == "Temple Worshippers follow Commandments").ID,
                    ReferenceID = context.References.Single(c => c.Title == "Spirit Prison" ).ReferenceID
                    },
                    new Note {
                    JournalID = context.Journals.Single(s => s.JournalName == "Jesus").ID,
                    ReferenceID = context.References.Single(c => c.Title == "Creation" ).ReferenceID
                    },
                    new Note {
                    JournalID = context.Journals.Single(s => s.JournalName == "Angels").ID,
                    ReferenceID = context.References.Single(c => c.Title == "Jesus is the Word" ).ReferenceID
                    },
                    new Note {
                    JournalID = context.Journals.Single(s => s.JournalName == "Eternal Father").ID,
                    ReferenceID = context.References.Single(c => c.Title == "Creation").ReferenceID
                    },
                    new Note {
                    JournalID = context.Journals.Single(s => s.JournalName == "Visions, Spiritual Places").ID,
                    ReferenceID = context.References.Single(c => c.Title == "Priesthood Restored").ReferenceID
                    },
                    new Note {
                    JournalID = context.Journals.Single(s => s.JournalName == "Obedience, Commandments, Purpose of Man").ID,
                    ReferenceID = context.References.Single(c => c.Title == "Spirit Prison").ReferenceID
                    },
                    new Note {
                    JournalID = context.Journals.Single(s => s.JournalName == "Visions, Spiritual Places").ID,
                    ReferenceID = context.References.Single(c => c.Title == "Priesthood Restored").ReferenceID
                    }
            };

            foreach (Note e in Notes)
            {
                var JournalInDataBase = context.Notes.Where(
                    s =>
                            s.Journal.ID == e.JournalID &&
                            s.Reference.ReferenceID == e.ReferenceID).SingleOrDefault();
                if (JournalInDataBase == null)
                {
                    context.Notes.Add(e);
                }
            }
            context.SaveChanges();

        }
    }
}
