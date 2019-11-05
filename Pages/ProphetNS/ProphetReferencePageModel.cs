using MyScriptureJournal.Data;
using MyScriptureJournal.Models;
using MyScriptureJournal.Models.GospelViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace MyScriptureJournal.Pages.ProphetNS
{
    public class ProphetReferencePageModel : PageModel
    {

        public List<AssignedReferenceData> AssignedReferenceDataList;

        public void PopulateAssignedReferenceData(JournalContext context,
                                               Prophets prophet)
        {
            var allReference = context.References;
            var prophetReferences = new HashSet<int>(
                prophet.dispensationLinks.Select(c => c.ReferenceID));
            AssignedReferenceDataList = new List<AssignedReferenceData>();
            foreach (var reference in allReference)
            {
                AssignedReferenceDataList.Add(new AssignedReferenceData
                {
                    ReferenceID = reference.ReferenceID,
                    Title = reference.Title,
                    Assigned = prophetReferences.Contains(reference.ReferenceID)
                });
            }
        }

        public void UpdateProphetReferences(JournalContext context,
            string[] selectedResources, Prophets prophetToUpdate)
        {
            if (selectedResources == null)
            {
                prophetToUpdate.dispensationLinks = new List<DispensationLinks>();
                return;
            }

            var selectedReferenceHS = new HashSet<string>(selectedResources);
            var prophetReferences = new HashSet<int>
                (prophetToUpdate.dispensationLinks.Select(c => c.References.ReferenceID));
            foreach (var reference in context.References)
            {
                if (selectedReferenceHS.Contains(reference.ReferenceID.ToString()))
                {
                    if (!prophetReferences.Contains(reference.ReferenceID))
                    {
                        prophetToUpdate.dispensationLinks.Add(
                            new DispensationLinks
                            {
                                ProphetID = prophetToUpdate.ProphetID,
                                ReferenceID = reference.ReferenceID
                            });
                    }
                }
                else
                {
                    if (prophetReferences.Contains(reference.ReferenceID))
                    {
                        DispensationLinks referenceToRemove
                            = prophetToUpdate
                                .dispensationLinks
                                .SingleOrDefault(i => i.ReferenceID == reference.ReferenceID);
                        context.Remove(referenceToRemove);
                    }
                }
            }
        }
    }
}